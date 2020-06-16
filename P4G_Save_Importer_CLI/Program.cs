using System;
using System.IO;
using Yoshitsune.Struct;
using Yoshitsune.Utils;

namespace P4G_Save_Importer_CLI
{
    class Program
    {
        static void printHelp()
        {
            Console.WriteLine("Commands list:");
            Console.WriteLine("     -h or --help");
            Console.WriteLine("         Displays this screen.");
            Console.WriteLine("     -sp or --save_path");
            Console.WriteLine("         Target your Steam 'remote' save file path. Check on GitHub repository for more info about it.");
            Console.WriteLine("     -i or --input_file");
            Console.WriteLine("         The save file which you want to port into your save files");
            Console.WriteLine("     -s or --slot");
            Console.WriteLine("         Save slot which you want to use. Remember that it can't create a new slot from scratch, so make sure that you created the slot which you want to use.");
        }

        static void Main(string[] args)
        {

            string savePath = null, inputFile = null;
            int slot = -1;

            bool savePathFlag = false, inputFileFlag = false, slotFlag = false;

            // Head
            Console.WriteLine("Persona 4 Golden Save converter");
            Console.WriteLine("Made by RayFirefist");
            Console.WriteLine("Github: https://github.com/RayFirefist/P4G_PC_Save_Importer");
            Console.WriteLine("");

            // If arguments are none, useless to continue
            if (args.Length == 0)
            {
                printHelp();
                return;
            }

            // Arguments parsing
            foreach (string argument in args)
            {
                if (savePathFlag)
                {
                    savePath = argument;
                    savePathFlag = false;
                }

                if (inputFileFlag)
                {
                    inputFile = argument;
                    inputFileFlag = false;
                }

                if (slotFlag)
                {
                    slot = int.Parse(argument);
                    if (slot < 1 || slot > 16)
                    {
                        // Slot must be between 1 and 16
                        Console.WriteLine("ERROR: Slot must be a number between 1 and 16");
                        return;
                    }
                    slotFlag = false;
                }

                switch (argument)
                {
                    case "-h":
                    case "--help":
                        printHelp();
                        return;
                    case "-sp":
                    case "--save_path":
                        savePathFlag = true;
                        break;
                    case "-i":
                    case "--input_file":
                        inputFileFlag = true;
                        break;
                    case "-s":
                    case "--slot":
                        slotFlag = true;
                        break;
                }
            }
        
            // Parameters check
            if (savePath == null)
            {
                Console.WriteLine("ERROR: Save path parameter is not declared");
                return;
            }

            if (inputFile == null)
            {
                Console.WriteLine("ERROR: Save file input parameter is not declared");
                return;
            }

            if (slot == -1)
            {
                Console.WriteLine("ERROR: Save slot parameter is not declared");
                return;
            }

            // Path checks

            var slots = PathChecker.checkPath(savePath);
            bool isValid = false;

            foreach (string tempSlot in slots)
                if (tempSlot != null)
                    isValid = true;

            if (!isValid)
            {
                Console.WriteLine($"ERROR: Not a valid game save file path.");
                return;
            }

            string binSlotPath = $@"{savePath}\data00{(slot < 10 ? $"0{slot}" : $"{slot}")}.binslot";

            if (!File.Exists(binSlotPath))
            {
                Console.WriteLine($"ERROR: Save slot not initialized. Please initialize it by saving at slot {slot}");
                return;
            }

            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"ERROR: Input file doesn't exist. Check the parameters.");
                return;
            }

            // Finally time to do the thing.
            Binslot binslot = new Binslot(binSlotPath);

            binslot.saveFileHash = HashMD5.getMd5BytesHash(File.ReadAllBytes(inputFile));

            binslot.saveBinslot();

            Console.WriteLine("Done!");
        }
    }
}
