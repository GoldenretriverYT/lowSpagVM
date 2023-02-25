using LowSpagVM.Common;
using System.Diagnostics;

namespace libLowSpagVM
{
    public class CPU
    {
        public const int REGISTER_COUNT = 16;
        public const ushort MEMORY_SIZE = 32767;

        public Memory Memory { get; set; }
        public ushort MemoryPtr { get; set; } = 0;
        public byte[] Registers { get; init; }

        // Events that can be listened to by a debugger
        public Action OnBreakpoint { get; set; }
        public Action AfterCycle { get; set; }

        public InstructionType CurrentInstructionType => (InstructionType)Memory.Read(pc);
        public byte[] CurrentInstructionBytes => Memory.Read(pc, 4);


        internal bool shouldBreak = false;
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

        public void Run()
        {
            while(true)
            {
                if (pc+4 >= MEMORY_SIZE) break;
                Cycle();
                AfterCycle?.Invoke();

                if (shouldBreak) {
                    OnBreakpoint?.Invoke();
                    shouldBreak = false;
                    return; // Stop execution until Run() is called again
                }
            }
        }

        public void Cycle()
        {
            if (!Instructions.CPUInstructions.ContainsKey(CurrentInstructionType))
            {
                throw new Exception("VM Fatal: Instruction " + CurrentInstructionType + " is not implemented.");
            }

            if(CurrentInstructionType != InstructionType.NOP) LSDbg.WriteLine($"Executing {CurrentInstructionType} : [{string.Join(", ", CurrentInstructionBytes)}]");

            Instructions.CPUInstructions[CurrentInstructionType].Execute(this, CurrentInstructionBytes);
        }

        public void IncreasePC(ushort v)
        {
            pc += v;
        }

        public void Jump(ushort v)
        {
            LSDbg.WriteLine($"Jumping to {v}");
            pc = v;
        }
    }
}