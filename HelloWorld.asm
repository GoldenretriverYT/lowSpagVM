 # This is a hello world example
CONST.STR HW Hello World!

MPTR_SET $HW                # Set the memory pointer to our Hello World constant

print:
  STRBYTE 0, 1              # Store a NULL byte into register 1 to check if the current char is \0
  LD 0                      # Load the next character into register 0
  SKEQU 0,1                 # If register 0 and 1 are the same, skip the next line
    JMP $printChar          # They are not the same, which means that the next character isn't a NULL byte
  JMP $newline              # We didn't jump to $printChar, therefore the next byte is a NULL byte
  
  printChar:
    PRINTA 0                # Print the character in register 0
    MPTR_INC                # Increase the memory pointer
    JMP $print              # Jump back to the start of $print

newline:
  STRBYTE 13, 0             # Store 13 (CR) into the register 0
  PRINTA 0                  # Print it
  STRBYTE 10, 0             # Store 10 (LF) into the register 0
  PRINTA 0                  # Print it
  JMP $loop                 # Optional: Start an infinite loop so the console doesnt close automatically

loop:
JMP $loop
