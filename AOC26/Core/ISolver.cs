namespace Core;

public interface ISolver
{
    public string Solve(IEnumerable<string> lines);
    public string FileName { get; }
}