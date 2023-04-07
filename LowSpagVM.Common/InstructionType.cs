namespace LowSpagVM.Common
{
    // v2 implementation notice: do not use registers, use stack (32-bit) instead
    public enum InstructionType : byte
    {
        NOP = 0x00,

        #region Arithmetics 0x1?
        /// <summary>
        /// Flow:
        ///   Add topmost value of stack to the next value of the stack. Pop those two values from the stack.
        ///   Push the result to the stack.
        /// 
        /// The flow is the same across the next 5 instructions.
        /// </summary>
        ADD = 0x10,
        SUB = 0x11,
        DIV = 0x13,
        MUL = 0x14,
        MOD = 0x15,

        #endregion

        #region Flow 0x2?
        /// <summary>
        /// Operand: Memory Address (32 bit)
        /// Flow:
        ///   Branches unconditionally.
        /// </summary>
        BR = 0x20,

        /// <summary>
        /// Operand: Memory Address (32 bit)
        /// Flow:
        ///   Branches if topmost stack value is 0. Pops the value from the stack.
        /// </summary>
        BRIZ = 0x21,

        /// <summary>
        /// Operand: Memory Address (32 bit)
        /// Flow:
        ///   Branches if topmost stack value is not 0. Pops the value from the stack.
        /// </summary>
        BRINZ = 0x22,
        #endregion

        #region Data 0x3?
        /// <summary>
        /// Operand: Memory Address (32 bit)
        /// Flow:
        ///   Loads a 32-bit value from the specified memory address and pushes it to the stack.
        /// </summary>
        LOAD32 = 0x30,

        /// <summary>
        /// Operand: Memory Address (32 bit)
        /// Flow:
        ///   Stores a 32-bit value from the top of the stack to the specified memory address.
        /// </summary>
        STORE32 = 0x31,

        /// <summary>
        /// Operand: 32-bit value
        /// Flow:
        ///   Push the specified 32-bit value to the stack.
        /// </summary>
        PUSH32 = 0x32,

        /// <summary>
        /// Flow:
        ///   Pop a 32-bit value from the stack.
        /// </summary>
        POP32 = 0x33,

        /// <summary>
        /// Flow:
        ///   Duplicates the topmost value of the stack. Pushes the duplicated value to the stack.
        /// </summary>
        DUP32 = 0x34,
        #endregion

        #region Special Instructions 0x7? (required), 0x8? (implementation optional)
        /// <summary>
        /// Flow:
        ///   Prints the topmost value of the stack as ASCII character. Pops the value from the stack.
        /// </summary>
        PRINT8 = 0x70,
        #endregion
    }
}