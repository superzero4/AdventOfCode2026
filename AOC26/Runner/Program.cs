using SecretEntrance;
namespace Core;

class Program
{
    static void Main(string[] args)
    {
        ISolver solver;
        int day = int.Parse(args[0]);
        solver = day switch
        {
            0 => new SecretEntranceSolver(),
            1 => new GiftShop.Solver(),
            2 => new Lobby.Solver(),
            3 => new PrintingDepartment.Solver(),
            4 => new Cafeteria.Solver(),
            5 => new TrashCompactor.Solver(),
            6 => new Laboratories.Solver(),
            _ => throw new NotImplementedException(),
        };
        DirectoryInfo? dir = new DirectoryInfo(Environment.CurrentDirectory);
        while (dir != null && dir.Name != "Runner")
        {
            dir = dir.Parent;
        }

        if (dir == null)
            throw new Exception("Could not find Runner directory");

        dir = dir?.Parent;
        var input = System.IO.File.ReadLines(Path.Combine(dir.FullName,solver.FileName));
        string result = solver.Solve(input);
        Console.WriteLine(result);
    }
}