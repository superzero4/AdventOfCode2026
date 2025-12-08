using Core;

namespace Laboratories;

public class Solver : ISolver
{

    public string Solve(IEnumerable<string> lines)
    {
        long[] timelines = null;
        List<int> cache = new();
        foreach (var line in lines)
        {
            if (timelines == null)
            {
                timelines = new long[line.Length];
                timelines[line.IndexOf('S')] = 1;
                continue;
            }
            for (int j = 0; j < line.Length; j++)
            {
                var ch = line[j];
                if (ch == '^')
                    if (timelines[j]>0)
                        cache.Add(j);
            }
            foreach (var c in cache)
            {
                var curr = timelines[c];
                timelines[c] = 0;
                timelines[c - 1]+= curr;
                timelines[c + 1]+= curr;
            }
            cache.Clear();
        }
        long sum = 0;
        foreach (var c in timelines)
            sum += c;
        return sum.ToString();
    }

    public string FileName => "Laboratories/Input.txt";
}