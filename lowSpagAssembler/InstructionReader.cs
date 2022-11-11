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

        public InstructionReader(string code)
        {
            Code = code;
        }

        public List<Instruction> ReadInstructions()
        {
            var output = new List<Instruction>();

            var lines = Code.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
            ushort offset = 0;

            // Read labels and constants first
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                offset += 4;

                if (line.EndsWith(":"))
                {
                    var label = line.Split(":")[0];
                    Labels.Add(label, (ushort)(offset-4));
                    continue;
                }
            }

            foreach(var line in lines)
            {
                if (line.EndsWith(":") || string.IsNullOrWhiteSpace(line))
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
    }
}
