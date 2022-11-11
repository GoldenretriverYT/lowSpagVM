using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lowSpagAssembler
{
    public struct Instruction
    {
        public InstructionType Type { get; set; }
        public byte[] Data { get; set; }

        public Instruction(InstructionType type, byte[] data)
        {
            this.Type = type;
            this.Data = data;
        }

        public void Emit(byte[] dest, int addr)
        {
            dest[addr] = (byte)Type;
            dest[addr + 1] = Data[0];
            dest[addr + 2] = Data[1];
            dest[addr + 3] = Data[2];

        }
    }
}
