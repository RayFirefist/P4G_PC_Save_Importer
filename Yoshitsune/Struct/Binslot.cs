using System;
using System.IO;
using Yoshitsune.Utils;

namespace Yoshitsune.Struct
{
    /// <summary>
    /// Save type for conversion/etc.
    /// </summary>
    public enum P4G_Save_Type
    {
        None = 0,
        Vita_JP = 1,
        Vita_US = 2,
        Vita_EU = 3,
        Vita_KR = 4,
        Steam = 5
    }

    /// <summary>
    /// Class which deals with .binslot files
    /// </summary>
    public class Binslot
    {
        /// <summary>
        /// File contents
        /// </summary>
        private byte[] contents;

        public readonly byte[] wtfMagic = { 0x50, 0x34, 0x47, 0x4F, 0x4C, 0x44, 0x45, 0x4E }; // P4GOLDEN

        public string originPath { set; get; }
        /// <summary>
        /// 0x00 - 0x08 bytes; fixed to be SAVE0001
        /// </summary>
        public byte[] head { private set; get; }
        /// <summary>
        /// 0x08 - 0x18 bytes; mystery hash
        /// </summary>
        public byte[] mysteryHash { set; get; }
        /// <summary>
        /// 0x18 - 0x28 bytes; data00XX.bin save MD5
        /// </summary>
        public byte[] saveFileHash { set; get; }
        /// <summary>
        /// 0x28 - EOF bytes; sdslot.bin data
        /// </summary>
        public byte[] sdSlotData { private set; get; }

        /// <summary>
        /// Constructor by providing binslot file path
        /// </summary>
        /// <param name="originPath">.binslot file path</param>
        public Binslot(string originPath)
        {
            this.originPath = originPath;
            this.contents = File.ReadAllBytes(originPath);

            this.head = new byte[8];
            this.mysteryHash = new byte[16];
            this.saveFileHash = new byte[16];
            this.sdSlotData = new byte[844];

            // Setting parameters
            Array.Copy(this.contents, 0, this.head, 0, 8);
            Array.Copy(this.contents, 8, this.mysteryHash, 0, 16);
            Array.Copy(this.contents, 8 + 16, this.saveFileHash, 0, 16);
            Array.Copy(this.contents, 8 + 16 + 16, this.sdSlotData, 0, 844);
        }

        /// <summary>
        /// Load binslot reader/writer with a byte array input
        /// </summary>
        /// <param name="originalBuffer">byte array of sdslot/binslot</param>
        /// <param name="steamHeaders">Parse the byte array as a Steam version (true, default) or Vita version (false)</param>
        public Binslot(byte[] originalBuffer, bool steamHeaders = true)
        {
            this.contents = originalBuffer;

            this.head = new byte[8];
            this.mysteryHash = new byte[16];
            this.saveFileHash = new byte[16];
            this.sdSlotData = new byte[844];

            if (steamHeaders)
            {
                // If the provided file has Steam version header
                Array.Copy(this.contents, 0, this.head, 0, 8);
                Array.Copy(this.contents, 8, this.mysteryHash, 0, 16);
                Array.Copy(this.contents, 8 + 16, this.saveFileHash, 0, 16);
                Array.Copy(this.contents, 8 + 16 + 16, this.sdSlotData, 0, 844);
            }
            else 
            {
                // If the provided buffer is from sdslot.bin (Vita) or something else without the Steam version header
                Array.Copy(new byte[] { 0x53, 0x41, 0x56, 0x45, 0x30, 0x30, 0x30, 0x31 }, 0, this.head, 0, 8); // SAVE0001
                Array.Copy(new byte[] { }, 0, this.mysteryHash, 0, 0); // Empty hash
                Array.Copy(new byte[] { }, 0, this.saveFileHash, 0, 0); // Empty hash
                Array.Copy(this.contents, 0, this.sdSlotData, 0, 844);
            }
        }

        /// <summary>
        /// Clone construct
        /// </summary>
        /// <param name="toClone">Object to clone</param>
        public Binslot(Binslot toClone)
        {
            this.originPath = toClone.originPath;
            this.head = toClone.head;
            this.mysteryHash = toClone.mysteryHash;
            this.saveFileHash = toClone.saveFileHash;
            this.sdSlotData = toClone.sdSlotData;
        }

        /// <summary>
        /// Get bytes array of compiled PC version of binslot
        /// </summary>
        /// <returns>bytes array of the final file (PC)</returns>
        public byte[] getCompiledBinslot()
        {
            byte[] output = new byte[884];

            // 0x00 - 0x08
            Array.Copy(this.head, 0, output, 0, 8);
            // 0x08 - 0x18
            Array.Copy(this.mysteryHash, 0, output, 8, 16);
            // 0x18 - 0x28
            Array.Copy(this.saveFileHash, 0, output, 8 + 16, 16);
            // 0x28 - EOF
            Array.Copy(this.sdSlotData, 0, output, 8 + 16 + 16, 844);

            return output;
        }

        /// <summary>
        /// Calculate mystery hash
        /// </summary>
        /// <returns>byte array of mystery hash</returns>
        public byte[] calculateMysteryHash()
        {
            byte[] calcluation;

            MemoryStream ms = new MemoryStream();
            ms.Write(this.sdSlotData, 0, this.sdSlotData.Length);
            ms.Write(this.wtfMagic, 0, this.wtfMagic.Length);

            calcluation = ms.ToArray();

            ms.Dispose();

            return HashMD5.getMd5BytesHash(calcluation);
        }

        /// <summary>
        /// Quick method to overwrite the file with the contents of the class right now.
        /// </summary>
        public void saveBinslot()
        {
            byte[] output = this.getCompiledBinslot();
            File.WriteAllBytes(this.originPath, output);
        }
    }
}
