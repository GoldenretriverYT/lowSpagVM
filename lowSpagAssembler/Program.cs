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
            var output = new byte[insts.Count * 4];
            var off = 4;

            byte[] constsOffset = BitConverter.GetBytes(reader.TotalConstantSize + 4);
            var constsOffsetInstruction = new Instruction(LowSpagVM.Common.InstructionType.JMP, new byte[] { constsOffset[0], constsOffset[1], 0 });
            insts.Add(constsOffsetInstruction);

            foreach(var inst in insts)
            {
                inst.Emit(output, off);
                off += 4;
            }

            File.WriteAllBytes(args[1], output);
        }
    }
}