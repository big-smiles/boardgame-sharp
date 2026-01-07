namespace boardgames_sharp.Player;

public readonly record struct PlayerId(ulong Id)
{
  public ulong Id { get; } = Id;
}