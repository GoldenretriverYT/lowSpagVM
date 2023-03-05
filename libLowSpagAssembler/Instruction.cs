using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLowSpagAssembler
{
    public struct Instruction
    {
        public InstructionType Type { get; set; }
        public byte[] Data { get; set; }

        public bool IsRawData { get; set; }

        public Instruction(InstructionType type, byte[] data, bool isRawData = false)
        {
            Console.WriteLine("Creating " + type + " " + data[0] + " " + data[1] + " " + data[2] + "; rawData = " + isRawData);
            this.Type = type;
            this.Data = data;
            this.IsRawData = isRawData;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="addr"></param>
        /// <returns>Offset to skip</returns>
        public int Emit(byte[] dest, int addr)
        {
            if (!IsRawData) {
                //Console.WriteLine($"Emitting {Type} {Data[0]} {Data[1]} {Data[2]} at {addr}");
                dest[addr] = (byte)Type;
                dest[addr + 1] = Data[0];
                dest[addr + 2] = Data[1];
                dest[addr + 3] = Data[2];
                return 4;
            }else {
                Buffer.BlockCopy(Data, 0, dest, addr, Data.Length);
                return Data.Length;
            }
        }

        public int GetEmittedSize() {
            if(IsRawData) Console.WriteLine("RawData Inst data size " + Data.Length);
            return IsRawData ? Data.Length : 4;
        }
    }
}
