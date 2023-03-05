# lowSpagVM
lowSpag is an assembly-like language except that it doesn't compile into machine code

### Goals
The goal is to represent assembly languages and having a working assembler and VM for it. I guess you could kinda compare it with MSIL except that MSIL has actual good languages you can use to target it. (and MSIL is faster, lol)

### Performance
Since it runs in a "VM" written in C# which in itself runs in a "VM" (with JIT Compilation), its definitely slower than machine code.

### Docs
There are no docs as of now. Some basics are:<br>
`myLabel:` defines a label, to get the memory address of it use `$myLabel`

`CONST.TYPE NAME VALUE` defines a constant, to get the memory address you also have to use `$myConstant`, example:<br>
`CONST.STR MyString Hello World!`

If two constants and labels share the same name, `$name` will prioritise labels over constants.

Memory addressing works by internally storing a MemoryPointer (uint16, unlike the registers which are uint8). It can be incremented/decremented/set with following instructions: `MPTR_INC`, `MPTR_DEC`, `MPTR_SET (memory address)`, `MPTR_SETREG (reg1 LSB) (reg2 MSB)`

Memory addresses can be manually written too. Example:
`JMP #h03FF` (hex) or `JMP #1234` (dec)

Since the MemoryPointer is a uint16, the maximum memory address is 0xFFFF (65535). This means that you can only access 64KB of memory. The VM included in here allocates 32KB of memory by default.

There are 16 registers. 15 is the **Ar**itmetic **R**esult Register but can obviously be manually written anyways. For registers with aliases, you can use `[ALIAS]` to reference it. Example: `[ARR]`
Technically, up to 255 registers could be supported, but the assembler will not allow you to use more than 16 as most implementations of lowSpag only allocate 16 registers.<br>
<br>
Aliases:
  - `[ARR]` - Arithmetic Result Register; references register 15
  - `[RES]` - Result Register (used by syscalls); references register 14
  
### Syscalls
There is a syscall instruction. Whilst this is a 0x8_ instruction and therefore not required to be implemented.<br>
Syscall ids are also not "standardized", but following are recommend (if not implemented, ignore these calls but set [RES] to 0):<br>

 | Id | Description | Arguments | Return value | Support within this VM impl | Supporting within DebuggingVM
 | --- | --- | --- | --- | --- | --- |
 | 0x00 | Exit the program | None | nothing | Yes | Yes |
 | 0x01 | Print a string from memory at MEMPTR | None | Amount of printed chars into [RES] | Yes | Yes |
 | 0x02 | Print a character from memory at MEMPTR | None | nothing | Yes | Yes |
 | 0x03 | Print a character from args | b1: char | nothing | Yes | Yes |
 | 0x04 | Reads a key from keyboard | None | Pressed key ASCII into [RES] | Yes | Planned |
 | 0x05 | Reads a string from keyboard to MEMPTR (warning: overrides memory!) | None | Amount of read chars into [RES] | Yes | Planned |