using System.Diagnostics;
using Core;

namespace Cafeteria;

public class Solver : ISolver
{
    private class Range
    {
        public Range(long start, long end)
        {
            this.min = start;
            this.max = end;
        }

        public long min { get; set; }
        public long max { get; set; }
        public bool InRange(long value) => value >= min && value <= max;
    }

    private void Add(HashSet<Range> ranges, Range range)
    {
        bool flag = false;
        ranges.Remove(range);
        foreach (var already in ranges)
        {
            bool minIn = already.InRange(range.min);
            bool maxIn = already.InRange(range.max);
            if (minIn && maxIn)
                return;
            if (minIn)
            {
                already.max = range.max;
                Add(ranges, already);
                flag = true;
                break;
            }

            if (maxIn)
            {
                already.min = range.min;
                Add(ranges, already);
                flag = true;
                break;
            }

            if (range.InRange(already.min) && range.InRange(already.max))
            {
                already.min = range.min;
                already.max = range.max;
                Add(ranges, already);
                flag = true;
                break;
            }
        }

        if (!flag)
            ranges.Add(range);
    }

    public string Solve(IEnumerable<string> lines)
    {
        HashSet<Range> ranges = new();
        var en = lines.GetEnumerator();
        string line = null;
        do
        {
            en.MoveNext();
            line = en.Current;
            var split = line.Split('-');
            if (split.Length < 2)
                break;
            var range = new Range(long.Parse(split[0]), long.Parse(split[1]));
            Add(ranges, range);
        } while (!string.IsNullOrWhiteSpace(line.Trim())); //Safe on top of the break

        /* Part 1
         int count = 0;
        while (en.MoveNext())
        {
            line = en.Current;
            var val = long.Parse(line);
            foreach (var range in ranges)
            {
                if (range.InRange(val))
                {
                    count++;
                    break;
                }
            }
        }
        */
        return ranges.Sum(r => r.max - r.min + 1).ToString();
    }

    public string FileName => "Cafeteria/input.txt";
}