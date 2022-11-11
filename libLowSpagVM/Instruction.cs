using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLowSpagVM
{
    public class Instruction
    {
        public InstructionType instructionType;
        public Action<CPU, byte[]> Execute { get; init; }

        public Instruction(Action<CPU, byte[]> exec, InstructionType instructionType)
        {
            Execute = exec;
            this.instructionType = instructionType;
        }
    }
}
