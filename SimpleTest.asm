# This example checks if the calculation of lowSpag isn't broken.
# It does this by checking if 25+25-10 = 40
# If its not, it will print a message telling you that the calculation is invalid
# If it is, it will tell you that the calculation is successful

# This example is fully documented.

CONST.STR CALC_INV Calculation Invalid!
CONST.STR CALC_SUC Calculation Successful!

NOP
STRBYTE 25, 2            # Store "25" into register 2
STRBYTE 25, 3            # Store "25" into register 3
STRBYTE 10, 4            # Store "10" into register 4
ADD 2, 3                 # Add register 2 (25) and register 3 (25)
SUB [arr], 4             # Subtract register 4 (10) from register [arr] (50)
                         
STRBYTE 40, 5            # Store byte 40 in Register 5
SKPEQU [arr], 5          # Fail if Arithmetic Result is NOT the same as Register 5 (40)
    JMP $error           # Jump to error part (skips the STRBYTE 255, 0)
MPTR_SET $CALC_SUC       # Set the Memory Pointer to the address of the CALC_SUC
JMP $print               # Jump to the label which is responsible for printing until it encounters a \x00 byte

loop:
    JMP $loop            # Go back to loop begin

error:                 
    MPTR_SET $CALC_INV   # The calculation failed, set the Memory Pointer to the address of the constant CALC_IVN
    JMP $print           # Call the print label

print:
    STRBYTE 0, 1         # Store 0 in the Register 1 for NULL-TERMINATION Check
    LD 0                 # Load the next character to print int Register 0
    SKPEQU 0, 1
        JMP $printChar   # Register 0 is NOT set to 0x00, print the character
    JMP $newline         # Register 0 is set to 0x00, so go to newline label

    printChar:
        PRINTA 0         # Print the character
        MPTR_INC         # Increase the memory pointer so we can read the next character
        JMP $print       # Jump back to the $print label

newline:
    STRBYTE 13, 0        # Print Carriage Return + Line Feed, then loop
    PRINTA 0             # CR
    STRBYTE 10, 0        # Store LF
    PRINTA 0             # LF
    JMP $loop            # Start an infinite loop