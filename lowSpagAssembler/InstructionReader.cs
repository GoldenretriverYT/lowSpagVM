using LowSpagVM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lowSpagAssembler
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

            var lines = Code.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
            ushort offset = 4; // 4 as the code always starts with a JMP that skips constants

            // Read labels and constants first
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (line.StartsWith("CONST")) {
                    string[] args = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    string[] constTypeArgs = args[0].Split(".");

                    if (args.Length < 2) throw new Exception("Invalid constant definition");
                    if (constTypeArgs.Length < 2) throw new Exception("Invalid constant definition");

                    string constType = constTypeArgs[1];

                    switch(constType) {
                        case "STR": {

                            break;
                        }

                        case "STRNT": {

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
                if (line.EndsWith(":") || line.StartsWith("CONST") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var inst = ParseLine(line.Trim(), this);
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

            if(!Enum.TryParse<InstructionType>(instruction.ToUpperInvariant(), out var instType)) throw new Exception("Parsing failure: Unknown instruction type");

            return InstructionTypeParser.ParseArguments(instType, (parts.Length > 1 ? args : new string[0]), reader);
        }
    }
}
