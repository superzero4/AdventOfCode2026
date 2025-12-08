using System.Numerics;
using Core;

namespace Playground;

public class Solver : ISolver
{
    public string Solve(IEnumerable<string> lines)
    {
        List<List<int>> circuits = new();
        List<Vector3> boxes = new();
        foreach (var line in lines)
        {
            var spl = line.Split(',').Select(int.Parse).ToArray();
            int ind = circuits.Count;
            boxes.Add(new Vector3(spl[0], spl[1], spl[2]));
            var hash = new List<int>();
            hash.Add(ind);
            circuits.Add(hash);
        }

        HashSet<(int, int)> visited = new();
        //Will get sorted only when we enumerate so we find the mins one by one
        (int i1, int i2, float dist) min = default;
        while (circuits[0].Count != boxes.Count) //When all are linked, any cell (so first one) would point to the same unique list containing all indexes
        {
            min = (-1, -1, float.MaxValue);
            for (var i = 0; i < boxes.Count; i++)
            {
                var b1 = boxes[i];
                for (var j = i + 1; j < boxes.Count; j++)
                {
                    if (visited.Contains((i, j)))
                        continue;
                    if (circuits[i] == circuits[j])//Already in same circuit, won't give anything wiring them
                    {
                        visited.Add((i, j));
                        continue;
                    }
                    var b2 = boxes[j];
                    var dist = (b1 - b2).Length();
                    if (dist < min.dist)
                        min = (i, j, dist);
                }
            }

            visited.Add((min.i1, min.i2));

            var c2 = circuits[min.i2];
            var c1 = circuits[min.i1];
            foreach (var other in c2)
            {
                c1.Add(other);
                circuits[other] = c1;
            }

            c2.Clear();
        }

        return (boxes[min.i1].X * boxes[min.i2].X).ToString();
        /*

                for (int k = 0; k < mins.Count; k++)
                {
                    if (dist < mins[k].dist)
                    {
                        mins.Insert(k, (i, j, dist));
                        if (mins.Count > 1000)
                            mins.RemoveAt(mins.Count-1);
                        break;
                    }
                }
            }
        }
*/


        return circuits.Distinct().OrderByDescending(c => c.Count).Take(3).Select(c => c.Count)
            .Aggregate(1L, (current, c) => current * c)
            .ToString();
    }

    public string FileName => "Playground/input.txt";
}