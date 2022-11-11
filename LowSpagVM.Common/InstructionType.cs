namespace LowSpagVM.Common
{
    public enum InstructionType : byte
    {
        NOP = 0x00,

        #region Arithmetics
        ADD = 0x10,
        SUB = 0x11,
        DIV = 0x13,
        MUL = 0x14,
        MOD = 0x15,

        #endregion

        #region Flow

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

        #region Data

        // data
        /// <summary>
        /// Writes from memory to register. Bytes: STR REG1 MEM_ADDR(2b)
        /// </summary>
        STR = 0x30,
        /// <summary>
        /// Reads from register to memory. Bytes: LD REG1 MEM_ADDR(2b)
        /// </summary>
        LD = 0x31,
        /// <summary>
        /// Stores value to memory. Bytes: MEMSTR VAL MEM_ADDR(2b)
        /// </summary>
        MEMSTR = 0x32,
        /// <summary>
        /// Stores value to register. Bytes: STRBYTE VAL REG1
        /// </summary>
        STRBYTE = 0x33,

        #endregion
    }
}