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
        public static Instruction ParseArguments(InstructionType type, string[] args)
        {
            switch(type)
            {
                case InstructionType.NOP:
                    return new(InstructionType.NOP, new byte[3]);
                case InstructionType.ADD:
                case InstructionType.SUB:
                case InstructionType.MUL:
                case InstructionType.DIV:
                case InstructionType.MOD:
                    if (!byte.TryParse(args[0], out var reg1)) reg1 = 0;
                    if (!byte.TryParse(args[1], out var reg2)) reg2 = 0;

                    return new Instruction(type, new byte[] { reg1, reg2, 0 });
                default:
                    throw new Exception("InstructionType parser not implemented yet!");
            }
        }
    }
}
