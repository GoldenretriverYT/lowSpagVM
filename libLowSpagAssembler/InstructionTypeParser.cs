using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLowSpagAssembler
{
    public static class InstructionTypeParser
    {
        private static InstructionReader reader;
        private static byte[] EmptyData => new byte[4];
        
        public static Instruction ParseArguments(InstructionType type, string[] args, InstructionReader reader)
        {
            InstructionTypeParser.reader = reader;
            byte reg1 = 0, reg2 = 0, memb1, memb2, val;

            switch(type)
            {
                case InstructionType.NOP:
                    return new(InstructionType.NOP, EmptyData);

                #region Arithmetic

                #endregion

                #region Flow
                
                #endregion

                #region Data
                
                #endregion Data

                #region Special Instructions
                
                #endregion

                #region Compile-Time Instructions
                #endregion

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

        public static byte[] TryReadMemoryAddress(string arg1)
        {
            if(arg1.StartsWith("$"))
            {
                string lbl = arg1.Split('$')[1];

                if (reader.Labels.ContainsKey(lbl)) {
                    var bytes = BitConverter.GetBytes(reader.Labels[lbl]);
                    return new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                } else if (reader.Constants.ContainsKey(lbl)) {
                    var bytes = BitConverter.GetBytes(reader.Constants[lbl]);
                    return new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
                } else {
                    throw new Exception("Unknown label!");
                }
            }else if(arg1.StartsWith("#h")) {
                string memAddr = arg1.Split("#h")[1];

                if (memAddr.Length != 4) throw new Exception("Invalid address #h" + memAddr);

                return new byte[4] { Convert.ToByte(memAddr[0].ToString(), 16), Convert.ToByte(memAddr[1].ToString(), 16),
                    Convert.ToByte(memAddr[2].ToString(), 16), Convert.ToByte(memAddr[3].ToString(), 16) };
            } else
            {
                if (!uint.TryParse(arg1, out var shrt)) throw new Exception("Invalid address!");
                var bytes = BitConverter.GetBytes(shrt);
                return new byte[4] { bytes[0], bytes[1], bytes[2], bytes[3] };
            }
        }

        public static byte TryParseByte(string arg)
        {
            if (!byte.TryParse(arg, out var b)) throw new Exception("Invalid byte!");

            return b;
        }
    }
}
