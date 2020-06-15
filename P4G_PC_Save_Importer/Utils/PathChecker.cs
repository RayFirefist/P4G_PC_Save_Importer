using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace P4G_PC_Save_Importer.Utils
{
    public class PathChecker
    {
        /// <summary>
        /// Checks if the provided directory is a Persona 4 Golden Steam save path
        /// </summary>
        /// <param name="path">Path to check</param>
        /// <returns>True if it's really a legit path, false if not</returns>
        static public string[] checkPath(string path)
        {
            string[] output = new string[16];
            int i = 0;

            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                // Gonna ignore the binslot. Checking about them later
                if (file.Contains(".binslot"))
                    continue;

                Debug.WriteLine(file);
                
                Match check = Regex.Match(file, "data00(?<firstDigit>[0-9])(?<secondDigit>[0-9]).bin");
                
                if (check.Success)
                {
                    Debug.WriteLine(file + "slot");
                    if (File.Exists(file + "slot"))
                    {
                        int index = int.Parse(check.Groups["firstDigit"].Value + check.Groups["secondDigit"].Value);
                        string value = check.Groups["firstDigit"].Value + check.Groups["secondDigit"].Value;
                        output[i++] = value;
                        Debug.WriteLine(index);
                        Debug.WriteLine("----------------------------------------------------------------------");
                    }
                }
            }

            return output;
        }
    }
}
