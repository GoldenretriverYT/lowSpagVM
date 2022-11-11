using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLowSpagVM
{
    internal class Instructions
    {
        public static Dictionary<InstructionType, Instruction> CPUInstructions = new Dictionary<InstructionType, Instruction>()
        {
            { InstructionType.NOP, new(InstNop, InstructionType.NOP) },

            { InstructionType.ADD, new(InstAdd, InstructionType.ADD) },
            { InstructionType.SUB, new(InstSub, InstructionType.SUB) },
            { InstructionType.MUL, new(InstMul, InstructionType.MUL) },
            { InstructionType.DIV, new(InstDiv, InstructionType.DIV) },
        };
        
        public static void InstNop(CPU cpu, byte[] instruction)
        {
            cpu.IncreasePC(4);
        }

        public static void InstAdd(CPU cpu, byte[] instruction)
        {
            if(!ValidRegister(instruction[1]) || !ValidRegister(instruction[2]))
            {
                throw new Exception("CPU Exception: INVALID REGISTER");
            }

            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] + cpu.Registers[instruction[2]]);
            cpu.IncreasePC(4);
        }
        public static void InstSub(CPU cpu, byte[] instruction)
        {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] - cpu.Registers[instruction[2]]);
            cpu.IncreasePC(4);
        }
        public static void InstMul(CPU cpu, byte[] instruction)
        {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] * cpu.Registers[instruction[2]]);
            cpu.IncreasePC(4);
        }
        public static void InstDiv(CPU cpu, byte[] instruction)
        {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] / cpu.Registers[instruction[2]]);
            cpu.IncreasePC(4);
        }


        private static bool ValidRegister(byte b) => b < CPU.REGISTER_COUNT;
    }
}
