using System.Diagnostics;
using Core;

namespace Lobby;

public class Solver : ISolver
{
    public string Solve(IEnumerable<string> lines)
    {
        long sum = 0;
        foreach (var bank in lines)
        {
            int[] values = bank.Select(c => int.Parse(c.ToString())).ToArray();
            int index = -1;
            for (int b = 11; b >= 0; b--)
            {
                int max = -1;
                for (int i = index + 1; i < values.Length - b; i++)
                {
                    if (values[i] > max)
                    {
                        max = values[i];
                        index = i;
                    }
                }

                //Console.WriteLine($"Bank: {bank}, Bit: {b}, Max: {max}, Pow : {(long)Math.Pow(10, b)}, Index: {index}");

                sum += max * (long)Math.Pow(10, b);
            }
        }

        return sum.ToString();
    }

    public string FileName => "Lobby/input.txt";
}