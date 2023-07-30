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
        public Action ExecutionDone { get; set; }

        public InstructionType CurrentInstructionType => (InstructionType)Memory.Read(pc);
        public byte[] CurrentInstructionBytes => Memory.Read(pc, 4);
        public uint ProgramCounter => pc;


        internal bool shouldBreak = false;
        private uint pc = 0;

        public static CPU Load(byte[] file)
        {
            if(file.Length % 4 != 0)
            {
                throw new Exception("Corrupted executable. Not padded to 4 bytes.");
            }

            if (file.Length > MEMORY_SIZE) {
                throw new Exception("Executable exceeds allocated memory. Max size is " + MEMORY_SIZE + " bytes.");
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

            ExecutionDone?.Invoke();
        }

        public void Cycle()
        {
            if (!Instructions.CPUInstructions.ContainsKey(GetInstructionName(CurrentInstructionType)))
            {
                throw new Exception("VM Fatal: Instruction " + GetInstructionName(CurrentInstructionType) + " is not implemented.");
            }

            Instructions.CPUInstructions[GetInstructionName(CurrentInstructionType)].Execute(this, CurrentInstructionBytes);
        }

        #region Cosmos Compatibility
        public string GetInstructionName(InstructionType inst) {
            switch (inst) {
                case InstructionType.NOP: return "NOP";

                // Arithmetic 0x1?
                case InstructionType.ADD: return "ADD";
                case InstructionType.SUB: return "SUB";
                case InstructionType.MUL: return "MUL";
                case InstructionType.DIV: return "DIV";
                case InstructionType.MOD: return "MOD";

                // Flow 0x2?
                case InstructionType.JMPIZ: return "JMPIZ";
                case InstructionType.SKPEQU: return "SKPEQU";
                case InstructionType.JMP: return "JMP";
                case InstructionType.BREAK:  return "BREAK";

                // Data 0x3?
                case InstructionType.STR: return "STR";
                case InstructionType.LD: return "LD";
                case InstructionType.MEMSTR: return "MEMSTR";
                case InstructionType.STRBYTE: return "STRBYTE";
                    
                case InstructionType.MPTR_INC: return "MPTR_INC";
                case InstructionType.MPTR_DEC: return "MPTR_DEC";
                case InstructionType.MPTR_SET: return "MPTR_SET";
                case InstructionType.MPTR_SETREG: return "MPTR_SETREG";

                // Special Instructions, 0x7? (required), 0x8? (impl. optional)
                case InstructionType.PRINTN: return "PRINTN";
                case InstructionType.PRINTA: return "PRINTA";

                case InstructionType.SYSCALL: return "SYSCALL";

                default: throw new Exception($"Parsing failure: Unknown instruction type");
            }
        }
        #endregion

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