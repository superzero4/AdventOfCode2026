using Core;

namespace PrintingDepartment;

public class Solver : ISolver
{
    public string Solve(IEnumerable<string> lines)
    {
        Dictionary<(int x, int y), int> points = new();
        int count = 0;

        (int x, int y) max = (0, 0);
        (int x, int y)[] neighbors =
        [
            (-1, -1), (0, -1), (1, -1),
            (-1, 0), (1, 0),
            (-1, 1), (0, 1), (1, 1)
        ];
        int j = 0;
        foreach (var l in lines)
        {
            int i = 0;
            foreach (var c in l)
            {
                if (c == '@')
                    points[(i, j)] = 0;
                i++;
            }

            j++;
        }

        max.y = j;
        max.x = lines.First().Length;
        var remove = new Stack<(int x, int y)>();
        var tbRemoved = new HashSet<(int x, int y)>();
        for (var y = 0; y < max.y; y++)
        {
            for (var x = 0; x < max.x; x++)
            {
                if (points.ContainsKey((x, y)))
                {
                    foreach (var n in neighbors)
                    {
                        var np = (x: x + n.x, y: y + n.y);
                        if (points.ContainsKey(np))
                            points[np]++;
                    }
                }
            }
        }

        foreach (var v in points)
        {
            if (v.Value < 4)
            {
                remove.Push(v.Key);
                tbRemoved.Add(v.Key);
            }
        }
        count = 0;
        while (remove.Count > 0)
        {
            var head = remove.Pop();
            points.Remove(head);
            count++;
            foreach (var n in neighbors)
            {
                var np = (x: head.x + n.x, y: head.y + n.y);
                if (points.ContainsKey(np))
                {
                    points[np]--;
                    if (points[np] <= 3 && !tbRemoved.Contains(np))
                    {
                        remove.Push(np);
                        tbRemoved.Add(np);
                    }
                }
            }
        }

        return count.ToString();
    }

    public string FileName => "PrintingDepartment/input.txt";
}