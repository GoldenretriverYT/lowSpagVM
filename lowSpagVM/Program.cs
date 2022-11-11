using libLowSpagVM;
using System;

namespace lowSpagVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1)
            {
                Console.WriteLine("Syntax: lowSpagVM.exe <file>");
                return;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File not found!");
                Console.WriteLine("Syntax: lowSpagVM.exe <file>");
                return;
            }


            CPU cpu = CPU.Load(File.ReadAllBytes(args[0]));
            cpu.Run();
            Console.WriteLine(string.Join(", ", cpu.Registers));
        }
    }
}