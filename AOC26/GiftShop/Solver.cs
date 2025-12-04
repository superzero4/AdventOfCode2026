using System.Diagnostics;
using Core;

namespace GiftShop;

public class Solver : ISolver
{
    public string Solve(IEnumerable<string> lines)
    {
        long sum = 0;
        foreach (var line in lines)
        {
            foreach (var range in line.Split(","))
            {
                if(range.Trim().Length == 0)
                    continue;
                var bounds = range.Split("-").Select(long.Parse).ToArray();
                long min = bounds[0];
                long max = bounds[1];
                for (long i = min; i <= max; i++)
                {
                    var s = i.ToString();
                    for (int d = s.Length; d >= 2; d--)
                    {
                        if (s.Length % d != 0)
                            continue;
                        var size = s.Length / d;
                        bool flag = true;
                        string lastSlice = s.Substring(0, size);
                        for (int slice = 1; slice < d; slice++)
                        {
                            var temp = s.Substring(slice * size, size);
                            if (temp != lastSlice)
                            {
                                flag = false;
                                break;
                            }
                            lastSlice = temp;
                        }

                        if (flag)
                        {
                            sum += i;
                            Console.WriteLine($"Found repeating number:{i}");
                            break;
                        }
                    }
                }
            }
        }

        return sum.ToString();
    }

    public string FileName => "GiftShop/input.txt";
}