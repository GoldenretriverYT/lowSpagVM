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

        public InstructionReader(string code)
        {
            Code = code;
        }

        public List<Instruction> ReadInstructions()
        {
            var output = new List<Instruction>();

            var lines = Code.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);

            foreach (var line in lines)
            {
                var inst = ParseLine(line);
                output.Add(inst);
            }

            return output;
        }

        public Instruction ParseLine(string line)
        {
            var parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 1) throw new Exception("Parsing failure: Invalid instruction");

            if(!Enum.TryParse<InstructionType>(parts[0], out var instType)) throw new Exception("Parsing failure: Unknown instruction type");

            return InstructionTypeParser.ParseArguments(instType, (parts.Length > 1 ? parts[1].Split(',') : new string[0]));
        }
    }
}
