using Core;

namespace TrashCompactor;

public class Solver : ISolver
{
    public record struct Operation(List<long> vals, char op)
    {
        public Operation(char op) : this(new(), op)
        {
        }

        public Operation() : this(new(), '\0')
        {
        }

        public Operation(long val) : this()
        {
            vals.Add(val);
        }

        public long Resolve()
        {
            return op switch
            {
                '+' => vals.Sum(),
                '*' => vals.Aggregate(1L, (a, b) => a * b),
                _ => vals[0],
            };
        }
    }

    public string Solve(IEnumerable<string> lines)
    {
        var listed = lines.ToList();
        Operation current = default; 
        long sum = 0;
        for (int i = 0; i < listed[0].Length; i++)
        {
            var opch = listed[^1][i];
            if (opch == '+' || opch == '*')
            {
                if (current != default)
                {
                    //Last val is empty row, might be 0 in a * operation, removed
                    current.vals.RemoveAt(current.vals.Count - 1);
                    sum += current.Resolve();
                }
                current = new Operation(opch);
            }

            long val = 0;
            for (int pow = 1, j = listed.Count - 2; j >= 0; j--)
            {
                var ch = listed[j][i];
                if (ch == ' ')
                    continue;
                val += (ch - '0') * pow;
                pow *= 10;
            }

            current.vals.Add(val);
        }

        sum += current.Resolve();
        return sum.ToString();
    }

    public string FileName => "TrashCompactor/input.txt";
}