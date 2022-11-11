namespace LowSpagVM.Common
{
    public enum InstructionType : byte
    {
        NOP = 0x00,
        // arithmetic -> result always stored in register 15 and syntax always <INSTRUCTION> <REG1> <REG2>
        ADD = 0x10,
        SUB = 0x11,
        DIV = 0x13,
        MUL = 0x14,
        MOD = 0x15,
        // flow
        JMPIZ = 0x20, // jump if zero <register>
        JMPEQU = 0x21, // jump if equ <register 1> <register 2>
        // data
        STR = 0x30, // write register STR <register>, <memory address (2b)>
        LD = 0x31, // read register LD <register>, <memory address (2b)>
    }
}