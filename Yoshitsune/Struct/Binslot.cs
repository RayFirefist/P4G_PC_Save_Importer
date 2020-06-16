using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string originPath { set; get; }
        /// <summary>
        /// 0x00 - 0x08 bytes; fixed to be SAVE0001
        /// </summary>
        public byte[] head { private set; get; }
        /// <summary>
        /// 0x08 - 0x18 bytes; mystery hash
        /// </summary>
        public byte[] mysteryHash { private set; get; }
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
        /// Quick method to overwrite the file with the contents of the class right now.
        /// </summary>
        public void saveBinslot()
        {
            byte[] output = this.getCompiledBinslot();
            File.WriteAllBytes(this.originPath, output);
        }
    }
}
