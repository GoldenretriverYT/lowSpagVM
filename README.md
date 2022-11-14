# lowSpagVM
lowSpag is an assembly-like language except that it doesn't compile into machine code

### Goals
The goal is to represent assembly languages and having a working assembler and VM for it. I guess you could kinda compare it with MSIL except that MSIL has actual good languages you can use to target it.

### Performance
Since it runs in a "VM" written in C# which in itself runs in a "VM" (with JIT Compilation), its definitely slower than machine code.

### Docs
There are no docs as of now. Some basics are:<br>
`myLabel:` defines a label, to get the memory address of it use `$myLabel`<br>
There are 16 registers. 15 is the **Ar**itmetic **R**esult Register but can obviously be manually written anyways. For registers with aliases, you can use `[ALIAS]` to reference it. Example: `[ARR]`
