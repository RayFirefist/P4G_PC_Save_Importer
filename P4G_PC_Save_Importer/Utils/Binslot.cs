using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4G_PC_Save_Importer.Utils
{
    public class Binslot
    {
        private byte[] contents;

        public string originPath { set; get; }
        public byte[] head { private set; get; } // 0x00 - 0x08 bytes; mostly SAVE0001
        public byte[] unknownHash { private set; get; } // 0x08 - 0x18 bytes
        public byte[] saveFileHash { set; get; } // 0x18 - 0x28 bytes
        public byte[] sdSlotData { private set; get; } // 0x28 - EOF bytes

        public Binslot(string originPath)
        {
            this.originPath = originPath;
            this.contents = File.ReadAllBytes(originPath);

            this.head = new byte[8];
            this.unknownHash = new byte[16];
            this.saveFileHash = new byte[16];
            this.sdSlotData = new byte[844];

            // Setting parameters
            Array.Copy(this.contents, 0, this.head, 0, 8);
            Array.Copy(this.contents, 8, this.unknownHash, 0, 16);
            Array.Copy(this.contents, 8 + 16, this.saveFileHash, 0, 16);
            Array.Copy(this.contents, 8 + 16 + 16, this.sdSlotData, 0, 844);
        }

        // Clone construct
        public Binslot(Binslot toClone)
        {
            this.originPath = toClone.originPath;
            this.head = toClone.head;
            this.unknownHash = toClone.unknownHash;
            this.saveFileHash = toClone.saveFileHash;
            this.sdSlotData = toClone.sdSlotData;
        }

        public byte[] getCompiledBinslot()
        {
            byte[] output = new byte[884];

            // 0x00 - 0x08
            Array.Copy(this.head, 0, output, 0, 8);
            // 0x08 - 0x18
            Array.Copy(this.unknownHash, 0, output, 8, 16);
            // 0x18 - 0x28
            Array.Copy(this.saveFileHash, 0, output, 8 + 16, 16);
            // 0x28 - EOF
            Array.Copy(this.sdSlotData, 0, output, 8 + 16 + 16, 844);

            return output;
        }

        public void saveBinslot()
        {
            byte[] output = this.getCompiledBinslot();
            File.WriteAllBytes(this.originPath, output);
        }
    }
}
