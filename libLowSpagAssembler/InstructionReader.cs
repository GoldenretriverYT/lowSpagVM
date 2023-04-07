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
        public Dictionary<string, uint> Labels { get; } = new();
        public Dictionary<string, uint> Constants { get; } = new();
        public uint TotalConstantSize = 0;

        public InstructionReader(string code)
        {
            Code = code;
        }

        public List<Instruction> ReadInstructions()
        {
            var output = new List<Instruction>();

            var lines = Code.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            uint offset = 5; // 5 as the code always starts with a JMP that skips constants

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
                            string dataStr = string.Join(' ', args.Skip(2));
                            byte[] data = Encoding.ASCII.GetBytes(dataStr + '\0');
                            uint size = (uint)RoundUp(data.Length, 5);

                            byte[] finalData = new byte[size];
                            Buffer.BlockCopy(data, 0, finalData, 0, data.Length);

                            var inst = new Instruction(InstructionType.NOP, finalData, true); 

                            Constants[args[1]] = (uint)(offset);

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
                    Labels.Add(label, (uint)(offset));
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

            if(!Enum.TryParse<InstructionType>(instruction.ToUpperInvariant(), out var instType)) throw new Exception("Parsing failure: Unknown instruction type");

            return InstructionTypeParser.ParseArguments(instType, (parts.Length > 1 ? args : new string[0]), reader);
        }

        private int RoundUp(int n, int m) {
            return n >= 0 ? ((n + m - 1) / m) * m : (n / m) * m;
        }
    }
}
