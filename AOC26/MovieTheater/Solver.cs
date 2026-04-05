using System.Numerics;
using Core;

namespace MovieTheater;

public class Solver : ISolver
{
    private record struct Point(int id, Vector2 val);

    public string Solve(IEnumerable<string> lines)
    {
        List<Point> minX = new();
        List<Point> maxX = new();
        List<Point> minY = new();
        List<Point> maxY = new();

        void Replace(List<Point> target, Point source, Func<Point, float> selector1)
        {
            if (target.Count == 0)
            {
                target.Add(source);
                return;
            }

            var delta = selector1(target[0]) - selector1(source);
            if (delta > 0)
            {
                target.Clear();
                target.Add(source);
            }
            else if (delta == 0)
            {
                target.Add(source);
            }
        }

        ulong Area(Point p1, Point p2)
        {
            return (ulong)(Math.Abs(p1.val.X - p2.val.X) + 1L) * (ulong)(Math.Abs(p1.val.Y - p2.val.Y) + 1L);
        }

        int i = 0;
        foreach (var line in lines)
        {
            var coords = line.Split(",").Select(int.Parse).ToArray();
            Point curr = new(i, new Vector2(coords[0], coords[1]));
            Replace(minX, curr, s => s.val.X);
            Replace(maxX, curr, s => -s.val.X);
            Replace(minY, curr, s => s.val.Y);
            Replace(maxY, curr, s => -s.val.Y);
            i++;
        }

        ulong max = 0;
        foreach (var p1 in maxX.Concat(minX).Concat(maxY).Concat(minY))
        {
            foreach (var p2 in maxX.Concat(minX).Concat(maxY).Concat(minY))
            {
                var area = Area(p1, p2);
                if (area > max)
                {
                    max = area;
                }
            }
        }

        return max.ToString();
    }

    public string FileName => "MovieTheater/input.txt";
}