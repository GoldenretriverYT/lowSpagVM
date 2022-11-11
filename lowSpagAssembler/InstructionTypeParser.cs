using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lowSpagAssembler
{
    public static class InstructionTypeParser
    {
        private static InstructionReader reader;

        public static Instruction ParseArguments(InstructionType type, string[] args, InstructionReader reader)
        {
            InstructionTypeParser.reader = reader;
            byte reg1, reg2, memb1, memb2, val;

            switch(type)
            {
                case InstructionType.NOP:
                    return new(InstructionType.NOP, new byte[3]);

                #region Arithmetic
                case InstructionType.ADD:
                case InstructionType.SUB:
                case InstructionType.MUL:
                case InstructionType.DIV:
                case InstructionType.MOD:
                    reg1 = TryParseRegister(args[0]);
                    reg2 = TryParseRegister(args[1]);

                    return new Instruction(type, new byte[] { reg1, reg2, 0 });
                #endregion

                #region Flow
                case InstructionType.JMPIZ:
                    reg1 = TryParseRegister(args[0]);
                    (memb1, memb2) = TryReadMemoryAddress(args[1]);

                    return new Instruction(type, new byte[] { reg1, memb1, memb2 });
                case InstructionType.SKPEQU:
                    reg1 = TryParseRegister(args[0]);
                    reg2 = TryParseRegister(args[1]);

                    return new Instruction(type, new byte[] { reg1, reg2, 0 });
                case InstructionType.JMP:
                    (memb1, memb2) = TryReadMemoryAddress(args[0]);

                    return new Instruction(type, new byte[] { memb1, memb2, 0 });

                #endregion

                #region Data
                case InstructionType.STR:
                case InstructionType.LD:
                    reg1 = TryParseRegister(args[0]);
                    (memb1, memb2) = TryReadMemoryAddress(args[1]);

                    return new Instruction(type, new byte[] { reg1, memb1, memb2 });
                case InstructionType.MEMSTR:
                    val = TryParseByte(args[0]);
                    (memb1, memb2) = TryReadMemoryAddress(args[1]);

                    return new Instruction(type, new byte[] { val, memb1, memb2 });
                case InstructionType.STRBYTE:
                    val = TryParseByte(args[0]);
                    reg1 = TryParseRegister(args[1]);

                    return new Instruction(type, new byte[] { val, reg1, 0 });
                #endregion Data

                default:
                    throw new Exception("InstructionType parser not implemented yet!");
            }
        }

        public static byte TryParseRegister(string arg)
        {
            if (byte.TryParse(arg, out var reg)) return reg;

            switch(arg.ToLowerInvariant())
            {
                case "[arr]": return 15; // AR(ITHMETIC) R(ESULT) (REGISTER)
            }

            throw new Exception("Expected valid register");
        }

        public static (byte, byte) TryReadMemoryAddress(string arg1)
        {
            if(arg1.StartsWith("$"))
            {
                string lbl = arg1.Split('$')[1];

                if(reader.Labels.ContainsKey(lbl))
                {
                    var bytes = BitConverter.GetBytes(reader.Labels[lbl]);
                    return (bytes[0], bytes[1]);
                }else
                {
                    throw new Exception("Unknown label!");
                }
            }else
            {
                if (!short.TryParse(arg1, out var shrt)) throw new Exception("Invalid address!");
                var bytes = BitConverter.GetBytes(shrt);
                return (bytes[0], bytes[1]);
            }
        }

        public static byte TryParseByte(string arg)
        {
            if (!byte.TryParse(arg, out var b)) throw new Exception("Invalid byte!");

            return b;
        }
    }
}
