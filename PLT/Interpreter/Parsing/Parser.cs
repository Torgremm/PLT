using System.Text.RegularExpressions;

namespace PLT.Interpreter;

static internal class Parser
{
    static internal (List<Instruction> Instructions, Dictionary<string, int> Labels) Parse(string stlCode)
    {
        var instructions = new List<Instruction>();
        stlCode = FilterOperations(stlCode);
        var labels = new Dictionary<string, int>();

        var lines = stlCode.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        int index = 0;


        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();

            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                continue;

            if (line.EndsWith(':'))
            {
                var label = line.TrimEnd(':');
                if (!labels.ContainsKey(label))
                {
                    labels[label] = index;
                }
                continue;
            }

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
            index++;
        }

        return (instructions, labels);
    }

    private static string FilterOperations(string code)
    {
        var comparisonPattern = new Regex(@"([<=|>=|<>|=|<|>])([A-Z])?", RegexOptions.Compiled);
        var typedMathPattern = new Regex(@"([\+\-\*/])([A-Z])", RegexOptions.Compiled);

        code = comparisonPattern.Replace(code, match =>
        {
            var op = match.Groups[1].Value switch
            {
                "<=" => "LEQ",
                ">=" => "GEQ",
                "<>" => "NEQ",
                "==" => "EQ",
                "<" => "LT",
                ">" => "GT",
                "=" => "STORE",
                _ => throw new InvalidOperationException()
            };

            var type = match.Groups[2].Value;
            return $"{op} {type}";
        });

        code = typedMathPattern.Replace(code, match =>
        {
            var op = match.Groups[1].Value switch
            {
                "+" => "ADD",
                "-" => "SUB",
                "*" => "MUL",
                "/" => "DIV",
                _ => throw new InvalidOperationException()
            };

            var type = match.Groups[2].Value;
            return $"{op} {type}";
        });

        return code;
    }
}

