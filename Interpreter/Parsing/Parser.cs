namespace PLT.Interpreter;

internal class Parser
{
    internal List<Instruction> Parse(string stlCode)
    {
        var instructions = new List<Instruction>();
        var lines = stlCode.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();

            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                continue;

            var parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                continue;

            var op = parts[0];
            string? operandsPart = parts.Length > 1 ? parts[1] : null;

            string[] operands = Array.Empty<string>();
            if (!string.IsNullOrEmpty(operandsPart))
            {
                operands = operandsPart.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                        .Select(o => o.Trim())
                                        .ToArray();
            }

            instructions.Add(new Instruction(op, operands));
        }

        return instructions;
    }
}

