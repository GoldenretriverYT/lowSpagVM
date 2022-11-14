using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            // arithmetic
            { InstructionType.ADD, new(InstAdd, InstructionType.ADD) },
            { InstructionType.SUB, new(InstSub, InstructionType.SUB) },
            { InstructionType.MUL, new(InstMul, InstructionType.MUL) },
            { InstructionType.DIV, new(InstDiv, InstructionType.DIV) },

            // flow
            { InstructionType.SKPEQU, new(InstSkipEqu, InstructionType.SKPEQU) },
            { InstructionType.JMP, new(InstJmp, InstructionType.JMP) },


            // data
            { InstructionType.STRBYTE, new(InstStrByte, InstructionType.STRBYTE) },

            // special
            {InstructionType.PRINTN, new(InstPrintNumber, InstructionType.PRINTN) },
            {InstructionType.PRINTA, new(InstPrintAscii, InstructionType.PRINTA) },


        };
        
        public static void InstNop(CPU cpu, byte[] instruction)
        {
            cpu.IncreasePC(4);
        }

        #region Arithmetic Instructions
        public static void InstAdd(CPU cpu, byte[] instruction)
        {
            if(!ValidRegister(instruction[1]) || !ValidRegister(instruction[2]))
            {
                throw new Exception("CPU Exception: INVALID REGISTER");
            }

            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] + cpu.Registers[instruction[2]]);
            Debug.WriteLine($"Add Result: {cpu.Registers[0xF]}");
            cpu.IncreasePC(4);
        }
        public static void InstSub(CPU cpu, byte[] instruction)
        {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] - cpu.Registers[instruction[2]]);
            Debug.WriteLine($"Sub Result: {cpu.Registers[0xF]}");
            cpu.IncreasePC(4);
        }
        public static void InstMul(CPU cpu, byte[] instruction)
        {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] * cpu.Registers[instruction[2]]);
            Debug.WriteLine($"Mul Result: {cpu.Registers[0xF]}");
            cpu.IncreasePC(4);
        }
        public static void InstDiv(CPU cpu, byte[] instruction)
        {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] / cpu.Registers[instruction[2]]);
            Debug.WriteLine($"Div Result: {cpu.Registers[0xF]}");
            cpu.IncreasePC(4);
        }
        #endregion

        #region Flow Instructions
        public static void InstSkipEqu(CPU cpu, byte[] instruction)
        {
            Debug.WriteLine($"Equality check: {cpu.Registers[instruction[1]]} == {cpu.Registers[instruction[2]]}");
            if (cpu.Registers[instruction[1]] == cpu.Registers[instruction[2]]) cpu.IncreasePC(4);

            cpu.IncreasePC(4);
        }

        public static void InstJmp(CPU cpu, byte[] instruction)
        {
            //Console.WriteLine(string.Join(", ", instruction[0..2]));
            cpu.Jump(BitConverter.ToUInt16(instruction[1..3]));
        }

        #endregion

        #region Data Instructions

        public static void InstStrByte(CPU cpu, byte[] instruction)
        {
            cpu.Registers[instruction[2]] = instruction[1];

            cpu.IncreasePC(4);
        }
        #endregion

        #region Special Instructions

        public static void InstPrintNumber(CPU cpu, byte[] instruction)
        {
            Console.Write(cpu.Registers[instruction[1]]);

            cpu.IncreasePC(4);
        }

        public static void InstPrintAscii(CPU cpu, byte[] instruction)
        {
            Console.Write((char)cpu.Registers[instruction[1]]);

            cpu.IncreasePC(4);
        }

        #endregion

        private static bool ValidRegister(byte b) => b < CPU.REGISTER_COUNT;
    }
}
