using System;
using P4G_PC_Save_Importer.Utils;

namespace P4G_PC_Save_Importer_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PathChecker.checkPath(@"C:\Program Files (x86)\Steam\userdata\882484028\1113000\remote");
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
