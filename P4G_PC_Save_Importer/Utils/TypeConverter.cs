using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4G_PC_Save_Importer.Utils
{
    public enum EncodingConvert
    {
        None = 0,
        UTF8 = 1,
        ASCII = 2
    }

    public static class TypeConverter
    {
        public static string BytesToString(byte[] input, EncodingConvert encoding = EncodingConvert.UTF8)
        {
            return Encoding.UTF8.GetString(input);
        }

        public static byte[] StringToBytes(string input, EncodingConvert encoding = EncodingConvert.UTF8)
        {
            return Encoding.UTF8.GetBytes(input);
        }
    }
}
