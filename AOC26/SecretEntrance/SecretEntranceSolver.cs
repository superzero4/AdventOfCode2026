using System.Diagnostics;
using Core;

namespace SecretEntrance;

public class SecretEntranceSolver : ISolver
{
    public string Solve(IEnumerable<string> lines)
    {
        int current = 50;
        int total = 0;
        bool skip = false;
        foreach (var line in lines)
        {
            bool pos = line[0] == 'R';
            int dist = int.Parse(line[1..]);
            total += current / 100;
            if (pos)
            {
                if (dist >= 100 - current)
                {
                    total += 1 + (dist - (100 - current)) / 100;
                }
            }
            else
            {
                if (dist >= current)
                {
                    total += 1 + (dist - current) / 100;
                    if (current == 0)
                        total--;
                }
            }
            current += pos ? dist : -dist;
            current = (current % 100 + 100) % 100;

            Console.WriteLine($"line {line}, current {current}, total {total}");
            skip = false;
        }

        return total.ToString();
    }

    public string FileName => "SecretEntrance/input.txt";
}