namespace boardgames_sharp.Player;

internal sealed class PlayerManager(HashSet<uint> players)
{
    public HashSet<uint> Players = players;
    
}