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

            var reader = new InstructionReader(File.ReadAllText(args[0]));

            var insts = reader.ReadInstructions();

            byte[] constsOffset = BitConverter.GetBytes(reader.TotalConstantSize + 4); // Add SKIP CONSTANTS jmp instructions
            var constsOffsetInstruction = new Instruction(LowSpagVM.Common.InstructionType.JMP, new byte[] { constsOffset[0], constsOffset[1], 0 });
            insts.Insert(0, constsOffsetInstruction);

            var output = new byte[insts.Sum((el) => el.GetEmittedSize())];
            var off = 0;

            foreach (var inst in insts)
            {
                off += inst.Emit(output, off);
            }

            File.WriteAllBytes(args[1], output);
        }
    }
}