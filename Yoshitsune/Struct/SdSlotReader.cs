using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Yoshitsune.Utils;

namespace Yoshitsune.Struct
{
    /// <summary>
    /// Reads the sdslot.bin file from Vita and can convert it into *.binslot for Steam version
    /// </summary>
    public class SdSlotReader
    {
        private byte[] contents;
        
        /// <summary>
        /// Flags for used slots (first system.bin, then for 16 times data00XX.bin)
        /// </summary>
        public byte[] usedSlots;

        /// <summary>
        /// System binslot data
        /// </summary>
        public byte[] systemBinSlot;
        /// <summary>
        /// Data binslot entries
        /// </summary>
        public byte[][] dataBinSlots = new byte[16][];

        /// <summary>
        /// Load it with a string path for sdslot.bin
        /// </summary>
        /// <param name="sdSlotBinPath">Path of the file</param>
        public SdSlotReader(string sdSlotBinPath)
        {
            this.usedSlots = new byte[17];
            this.systemBinSlot = new byte[0x200];

            this.contents = File.ReadAllBytes(sdSlotBinPath);

            Array.Copy(this.contents, 0x200, this.usedSlots, 0, 17);

            Array.Copy(this.contents, 0x400, this.systemBinSlot, 0, 0x200);

            for(int i = 1; i <= 16; i++)
            {
                if (this.usedSlots[i] == 0x00)
                    continue;

                this.dataBinSlots[i - 1] = new byte[0x400];
                Array.Copy(this.contents, 0x400 + (i * 0x400), this.dataBinSlots[i - 1], 0, 0x400);
            }
        }

        /// <summary>
        /// Convert the sdslot.bin (Vita) into *.binslot (Steam). Save files must follow the data00XX.bin pattern.
        /// </summary>
        /// <param name="outputPath">Output result</param>
        /// <param name="inputSavePath">Input path of the save files</param>
        public void convertToBinslot(string outputPath, string inputSavePath)
        {
            for (int i = 1; i <= 16; i++)
            {
                if (this.usedSlots[i] == 0x00)
                    continue;

                if (!File.Exists($@"{inputSavePath}\data00{i}.bin") && !File.Exists($@"{inputSavePath}\data000{i}.bin"))
                    throw new FileNotFoundException($"File data00XX.bin not found (index {i})");

                Binslot binslot = new Binslot(this.dataBinSlots[i-1], false);

                binslot.mysteryHash = binslot.calculateMysteryHash();
                binslot.saveFileHash = HashMD5.getMd5BytesHash(File.ReadAllBytes($@"{inputSavePath}\data00{(i < 10 ? $"0{i}" : i.ToString())}.bin"));

                binslot.originPath = outputPath + $@"\data00{(i < 10 ? $"0{i}": i.ToString())}.binslot";

                binslot.saveBinslot();
            }
        }
    }
}
