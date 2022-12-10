namespace LowSpagVM.Common
{
    public enum InstructionType : byte
    {
        NOP = 0x00,

        #region Arithmetics 0x1?
        ADD = 0x10,
        SUB = 0x11,
        DIV = 0x13,
        MUL = 0x14,
        MOD = 0x15,

        #endregion

        #region Flow 0x2?

        /// <summary>
        /// Jumps if the register is 0. Bytes: JMPIZ REG1 MEM_ADDR(2b)
        /// </summary>
        JMPIZ = 0x20,

        /// <summary>
        /// Skips next instruction if two register are equal. Bytes: SKPEQU REG1 REG2
        /// <para>Usage would be: </para>
        /// <para>SKPEQU 1 2 -- If they are equal, continue, else go to error handler</para>
        /// <para>JMP 1000 -- They are not equal, go to error handler</para>
        /// </summary>
        SKPEQU = 0x21,

        /// <summary>
        /// Jumps to address. Bytes: JMP MEM_ADDR(2b)
        /// </summary>
        JMP = 0x22,


        #endregion

        #region Data 0x3?

        // data
        /// <summary>
        /// Writes from register to memory. Bytes: STR REG1
        /// </summary>
        STR = 0x30,
        /// <summary>
        /// Writes from memory to register. Bytes: LD REG1
        /// </summary>
        LD = 0x31,
        /// <summary>
        /// Stores value to memory. Bytes: MEMSTR VAL
        /// </summary>
        MEMSTR = 0x32,
        /// <summary>
        /// Stores value to register. Bytes: STRBYTE VAL REG1
        /// </summary>
        STRBYTE = 0x33,

        /// <summary>
        /// Increases the memory pointer by 1
        /// </summary>
        MPTR_INC = 0x34,
        /// <summary>
        /// Decreases the memory pointer by 1
        /// </summary>
        MPTR_DEC = 0x35,
        /// <summary>
        /// Sets the memory pointer. Bytes: MPTR_SET (memory addr (2b))
        /// </summary>
        MPTR_SET = 0x36,
        /// <summary>
        /// Sets the memory pointer from registers. Bytes: MPTR_SETREG REG1(LSB) REG2(MSB) 
        /// </summary>
        MPTR_SETREG = 0x37,

        #endregion

        #region Special Instructions 0x7?, 0x8?
        /// <summary>
        /// Prints the numeric value of a register. Bytes: PRNTN REG1
        /// </summary>
        PRINTN = 0x70,
        /// <summary>
        /// Prints the value converted to an ASCII char of a register. Bytes: PRNTA REG1
        /// </summary>
        PRINTA = 0x71,
        #endregion

        #region Compile-Time Instructions 0xF?
        #endregion
    }
}