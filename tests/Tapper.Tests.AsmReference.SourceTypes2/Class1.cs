namespace Tapper.Tests.AsmReference.SourceTypes2;

[TranspilationSource]
public class Class2
{
    public int Value { get; init; }
    public string Name { get; init; } = default!;
    public string? Name2 { get; init; }
    public Guid Id { get; set; }
}
