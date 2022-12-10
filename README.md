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

There are 16 registers. 15 is the **Ar**itmetic **R**esult Register but can obviously be manually written anyways. For registers with aliases, you can use `[ALIAS]` to reference it. Example: `[ARR]`
