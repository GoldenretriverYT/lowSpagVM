namespace libLowSpagAssembler
{
    public class Assembler
    {
        string content;

        public Assembler(string content)
        {
            this.content = content;
        }

        public byte[] Assemble()
        {
            var reader = new InstructionReader(content);

            var insts = reader.ReadInstructions();

            byte[] constsOffset = BitConverter.GetBytes(reader.TotalConstantSize + 4); // Add SKIP CONSTANTS jmp instructions
            var constsOffsetInstruction = new Instruction(LowSpagVM.Common.InstructionType.JMP, new byte[] { constsOffset[0], constsOffset[1], 0 });
            insts.Insert(0, constsOffsetInstruction);

            var size = insts.Sum((el) => el.GetEmittedSize());

            Console.WriteLine("Creating output buffer of size " + size);
            var output = new byte[size];
            var off = 0;

            foreach (var inst in insts)
            {
                off += inst.Emit(output, off);
            }

            return output;
        }
    }
}