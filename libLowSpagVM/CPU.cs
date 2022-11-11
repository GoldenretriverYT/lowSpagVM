using LowSpagVM.Common;

namespace libLowSpagVM
{
    public class CPU
    {
        public const int REGISTER_COUNT = 16;
        public const int MEMORY_SIZE = 32767;

        public Memory Memory { get; set; }
        public byte[] Registers { get; init; }
        private uint pc = 0;

        public static CPU Load(byte[] file)
        {
            if(file.Length % 4 != 0)
            {
                throw new Exception("Corrupted executable. Not padded to 4 bytes.");
            }

            CPU cpu = new CPU();
            cpu.Memory.Copy(file, 0x00);

            return cpu;
        }

        private CPU()
        {
            Memory = new Memory(MEMORY_SIZE);
            Registers = new byte[REGISTER_COUNT];
        }

        public void Cycle()
        {
            var instType = (InstructionType)Memory.Read(pc);

            if (!Instructions.CPUInstructions.ContainsKey(instType))
            {
                throw new Exception("VM Fatal: Instruction " + instType + " is not implemented.");
            }

            Instructions.CPUInstructions[instType].Execute(this, Memory.Read(pc, 4));
        }

        public void IncreasePC(uint v)
        {
            pc += v;
        }
    }
}