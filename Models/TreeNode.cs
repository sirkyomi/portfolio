namespace Portfolio.Web.Models;

public sealed class TreeNode
{
    public required string Name { get; init; }
    public bool IstOrdner { get; init; }
    public string? Pfad { get; init; }
    public bool StandardOffen { get; init; } = true;
    public List<TreeNode> Kinder { get; init; } = [];
}
