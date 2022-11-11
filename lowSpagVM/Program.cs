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
        }
    }
}