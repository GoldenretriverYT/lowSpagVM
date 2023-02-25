using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLowSpagVM
{
    public class Instructions
    {
        // These Actions provide an easy way to make libLowSpagVM work with any kind of output
        public static Action<string> Write { get; set; } = Console.Write;


        public static Dictionary<InstructionType, Instruction> CPUInstructions = new Dictionary<InstructionType, Instruction>()
        {
            { InstructionType.NOP, new(InstNop, InstructionType.NOP) },

            // arithmetic
            { InstructionType.ADD, new(InstAdd, InstructionType.ADD) },
            { InstructionType.SUB, new(InstSub, InstructionType.SUB) },
            { InstructionType.MUL, new(InstMul, InstructionType.MUL) },
            { InstructionType.DIV, new(InstDiv, InstructionType.DIV) },
            { InstructionType.MOD, new(InstMod, InstructionType.MOD) },

            // flow
            { InstructionType.SKPEQU, new(InstSkipEqu, InstructionType.SKPEQU) },
            { InstructionType.JMP, new(InstJmp, InstructionType.JMP) },
            { InstructionType.BREAK, new(InstBreak, InstructionType.BREAK) },

            // data
            { InstructionType.STR, new(InstStr, InstructionType.STR) },
            { InstructionType.LD, new(InstLd, InstructionType.LD) },
            { InstructionType.STRBYTE, new(InstStrByte, InstructionType.STRBYTE) },
            { InstructionType.MPTR_INC, new(InstMptrInc, InstructionType.MPTR_INC) },
            { InstructionType.MPTR_DEC, new(InstMptrDec, InstructionType.MPTR_DEC) },
            { InstructionType.MPTR_SET, new(InstMptrSet, InstructionType.MPTR_SET) },
            { InstructionType.MPTR_SETREG, new(InstMptrSetReg, InstructionType.MPTR_SETREG) },

            // special
            {InstructionType.PRINTN, new(InstPrintNumber, InstructionType.PRINTN) },
            {InstructionType.PRINTA, new(InstPrintAscii, InstructionType.PRINTA) },

            {InstructionType.SYSCALL, new(InstSyscall, InstructionType.SYSCALL) }
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
            LSDbg.WriteLine($"Add Result: {cpu.Registers[0xF]}");
            cpu.IncreasePC(4);
        }
        public static void InstSub(CPU cpu, byte[] instruction)
        {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] - cpu.Registers[instruction[2]]);
            LSDbg.WriteLine($"Sub Result: {cpu.Registers[0xF]}");
            cpu.IncreasePC(4);
        }
        public static void InstMul(CPU cpu, byte[] instruction)
        {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] * cpu.Registers[instruction[2]]);
            LSDbg.WriteLine($"Mul Result: {cpu.Registers[0xF]}");
            cpu.IncreasePC(4);
        }
        public static void InstDiv(CPU cpu, byte[] instruction)
        {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] / cpu.Registers[instruction[2]]);
            LSDbg.WriteLine($"Div Result: {cpu.Registers[0xF]}");
            cpu.IncreasePC(4);
        }
        public static void InstMod(CPU cpu, byte[] instruction) {
            cpu.Registers[0xF] = (byte)(cpu.Registers[instruction[1]] % cpu.Registers[instruction[2]]);
            LSDbg.WriteLine($"Mod Result: {cpu.Registers[0xF]}");
            cpu.IncreasePC(4);
        }
        #endregion

        #region Flow Instructions
        public static void InstSkipEqu(CPU cpu, byte[] instruction)
        {
            LSDbg.WriteLine($"Equality check: {cpu.Registers[instruction[1]]} == {cpu.Registers[instruction[2]]}");
            if (cpu.Registers[instruction[1]] == cpu.Registers[instruction[2]]) cpu.IncreasePC(4);

            cpu.IncreasePC(4);
        }

        public static void InstJmp(CPU cpu, byte[] instruction)
        {
            //Console.WriteLine(string.Join(", ", instruction[0..2]));
            cpu.Jump(BitConverter.ToUInt16(instruction[1..3]));
        }

        public static void InstBreak(CPU cpu, byte[] instruction) {
            cpu.shouldBreak = true;
            cpu.IncreasePC(4);
        }

        #endregion

        #region Data Instructions

        public static void InstStrByte(CPU cpu, byte[] instruction)
        {
            cpu.Registers[instruction[2]] = instruction[1];

            cpu.IncreasePC(4);
        }

        public static void InstStr(CPU cpu, byte[] instruction) {
            cpu.Memory.Set(cpu.MemoryPtr, cpu.Registers[instruction[1]]);

            cpu.IncreasePC(4);
        }

        public static void InstLd(CPU cpu, byte[] instruction) {
            cpu.Registers[instruction[1]] = cpu.Memory.Read(cpu.MemoryPtr);

            cpu.IncreasePC(4);
        }

        public static void InstMptrInc(CPU cpu, byte[] instruction) {
            cpu.MemoryPtr++;
            cpu.IncreasePC(4);
        }

        public static void InstMptrDec(CPU cpu, byte[] instruction) {
            cpu.MemoryPtr--;
            cpu.IncreasePC(4);
        }

        public static void InstMptrSet(CPU cpu, byte[] instruction) {
            cpu.MemoryPtr = BitConverter.ToUInt16(instruction[1..3]);
            cpu.IncreasePC(4);
        }

        public static void InstMptrSetReg(CPU cpu, byte[] instruction) {
            cpu.MemoryPtr = BitConverter.ToUInt16(new byte[] { cpu.Registers[instruction[1]], cpu.Registers[instruction[2]] });
            cpu.IncreasePC(4);
        }

        #endregion

        #region Special Instructions

        public static void InstPrintNumber(CPU cpu, byte[] instruction)
        {
            Write?.Invoke(cpu.Registers[instruction[1]].ToString());

            cpu.IncreasePC(4);
        }

        public static void InstPrintAscii(CPU cpu, byte[] instruction)
        {
            Write?.Invoke(((char)cpu.Registers[instruction[1]]).ToString());

            cpu.IncreasePC(4);
        }

        public static void InstSyscall(CPU cpu, byte[] instruction)
        {
            byte syscallId = instruction[1];
            byte reg1 = instruction[2];
            byte reg2 = instruction[3];

            switch(syscallId)
            {
                case 0x00: // In this implementation, 0x00 is SetConsoleForegroundColor
                    byte colorIdx = cpu.Registers[reg1];

                    if(colorIdx >= 30 && colorIdx <= 37 || colorIdx >= 90 && colorIdx <= 97)
                    {
                        // write ansi escape code
                        Write?.Invoke("\x1b[" + colorIdx + "m");
                    }
                    break;

                case 0x01: // 0x01 is SetConsoleBackgroundColor
                    colorIdx = cpu.Registers[reg1];

                    if (colorIdx >= 40 && colorIdx <= 47 || colorIdx >= 100 && colorIdx <= 107)
                    {
                        // write ansi escape code
                        Write?.Invoke("\x1b[" + colorIdx + "m");
                    }
                    break;
                case 0x02: // Print string - extracts the whole string from memPtr (until 0x00)
                    uint memPtr = cpu.MemoryPtr;
                    string str = "";

                    while(cpu.Memory.Read(memPtr) != 0x00)
                    {
                        str += (char)cpu.Memory.Read(memPtr);
                        memPtr++;
                    }

                    Write?.Invoke(str);
                    break;
            }

            cpu.IncreasePC(4);
        }

        #endregion

        private static bool ValidRegister(byte b) => b < CPU.REGISTER_COUNT;
    }
}
