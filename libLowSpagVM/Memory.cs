using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLowSpagVM
{
    public class Memory
    {
        private byte[] data;
        private uint size;

        public Memory(uint size)
        {
            data = new byte[size];
            this.size = size;
        }

        public byte Read(uint addr)
        {
            if (addr > size) throw new Exception("Out of bounds read!");

            return data[addr];
        }

        public byte[] Read(uint addr, int readSize)
        {
            if (addr + readSize > size) throw new Exception("Out of bounds read!");

            byte[] resData = new byte[readSize];

            for(var offset = 0; offset < readSize; offset++)
            {
                resData[offset] = data[addr + offset];
            }

            return resData;
        }

        public void Set(uint addr, byte val)
        {
            if(addr > size)
            {
                Set(addr-size, val);
                return;
            }

            data[addr] = val;
        }

        public void Copy(byte[] source, uint addr)
        {
            for(var cAddr = 0; cAddr < source.Length; cAddr++)
            {
                var offset = cAddr + addr;
                data[offset] = source[cAddr];
            }
        }
    }
}
