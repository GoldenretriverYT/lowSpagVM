using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libLowSpagAssembler
{
    public class InstructionReader
    {
        public string Code { get; }
        public Dictionary<string, ushort> Labels { get; } = new();
        public Dictionary<string, ushort> Constants { get; } = new();
        public ushort TotalConstantSize = 0;

        public InstructionReader(string code)
        {
            Code = code;
        }

        public List<Instruction> ReadInstructions()
        {
            var output = new List<Instruction>();

            var lines = Code.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            ushort offset = 4; // 4 as the code always starts with a JMP that skips constants

            // Read labels and constants first
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    continue;
                }

                if (line.StartsWith("CONST")) {
                    string[] args = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    string[] constTypeArgs = args[0].Split(".");

                    if (args.Length < 3) throw new Exception("Invalid constant definition");
                    if (constTypeArgs.Length < 2) throw new Exception("Invalid constant definition");

                    string constType = constTypeArgs[1];

                    switch(constType) {
                        case "STR": {
                            var stringArgs = args.Skip(2);
                            string dataStr = Join(stringArgs, ' '); // cosmos-compat branch can not use string.Join

                            byte[] data = Encoding.ASCII.GetBytes(dataStr + '\0');
                            ushort size = (ushort)RoundUp(data.Length, 4);

                            byte[] finalData = new byte[size];
                            Buffer.BlockCopy(data, 0, finalData, 0, data.Length);

                            Console.WriteLine("Adding new constant " + dataStr + " of size " + size + " at offset " + offset);
                            var inst = new Instruction(InstructionType.NOP, finalData, true); 

                            Constants[args[1]] = (ushort)(offset);

                            offset += size;
                            TotalConstantSize += size;
                            output.Add(inst);
                            break;
                        }

                        case "BIN": {

                            break;
                        }

                        default: {
                            throw new Exception($"Unknown constant definition type '{constType}'");
                        }
                    }

                    continue;
                }

                if (line.EndsWith(":"))
                {
                    var label = line.Split(":")[0];
                    Labels.Add(label, (ushort)(offset));
                    continue;
                }

                offset += 4;
            }

            foreach(var line in lines)
            {
                if (line.EndsWith(":") || line.StartsWith("CONST") || line.StartsWith("#") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var inst = ParseLine(line, this);
                output.Add(inst);
            }

            return output;
        }

        public Instruction ParseLine(string line, InstructionReader reader)
        {
            line = line.Split('#')[0];
            var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 1) throw new Exception("Parsing failure: Invalid instruction");

            var instruction = parts[0];
            var args = line.Substring(instruction.Length).Split(',', StringSplitOptions.TrimEntries);

            var instType = TryParseInstructionType(instruction.ToUpperInvariant());

            return InstructionTypeParser.ParseArguments(instType, (parts.Length > 1 ? args : new string[0]), reader);
        }

        #region Cosmos Compatibility
        public InstructionType TryParseInstructionType(string inst) {
            switch(inst) {
                case "NOP": return InstructionType.NOP;

                // Arithmetic 0x1?
                case "ADD": return InstructionType.ADD;
                case "SUB": return InstructionType.SUB;
                case "MUL": return InstructionType.MUL;
                case "DIV": return InstructionType.DIV;
                case "MOD": return InstructionType.MOD;

                // Flow 0x2?
                case "JMPIZ": return InstructionType.JMPIZ;
                case "SKPEQU": return InstructionType.SKPEQU;
                case "JMP": return InstructionType.JMP;
                case "BREAK": return InstructionType.BREAK;

                // Data 0x3?
                case "STR": return InstructionType.STR;
                case "LD": return InstructionType.LD;
                case "MEMSTR": return InstructionType.MEMSTR;
                case "STRBYTE": return InstructionType.STRBYTE;

                case "MPTR_INC": return InstructionType.MPTR_INC;
                case "MPTR_DEC": return InstructionType.MPTR_DEC;
                case "MPTR_SET": return InstructionType.MPTR_SET;
                case "MPTR_SETREG": return InstructionType.MPTR_SETREG;

                // Special Instructions, 0x7? (required), 0x8? (impl. optional)
                case "PRINTN": return InstructionType.PRINTN;
                case "PRINTA": return InstructionType.PRINTA;
                    
                case "SYSCALL": return InstructionType.SYSCALL;

                default: throw new Exception($"Parsing failure: Unknown instruction type '{inst}'");
            }
        }
        #endregion

        private int RoundUp(int n, int m) {
            return n >= 0 ? ((n + m - 1) / m) * m : (n / m) * m;
        }


        private string Join(IEnumerable<string> strings, char separator) {
            var sb = new StringBuilder();
            foreach (var str in strings) {
                sb.Append(str);
                sb.Append(separator);
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}
