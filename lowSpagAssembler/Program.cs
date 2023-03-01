using libLowSpagAssembler;

namespace lowSpagAssembler
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Syntax: lowSpagAssembler.exe <file> <output>");
                return;
            }

            if(!File.Exists(args[0]))
            {
                Console.WriteLine("File not found!");
                Console.WriteLine("Syntax: lowSpagAssembler.exe <file> <output>");
                return;
            }

            Assembler assembler = new(File.ReadAllText(args[0]));

            File.WriteAllBytes(args[1], assembler.Assemble());
        }
    }
}