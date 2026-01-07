namespace boardgames_sharp.Player;

public interface IPlayerManager
{
    public void Win(PlayerId winnerPlayerId);
    public PlayerId? GetWinnerPlayerId();
}
internal sealed class PlayerManager(HashSet<PlayerId> players):IPlayerManager
{
    private HashSet<PlayerId> _players = players;
    private PlayerId? _winnerPlayerId = null;

    public void Win(PlayerId winnerPlayerId)
    {
        if (_winnerPlayerId != null)
        {
            throw new Exception("Cannot win a player that is already winning"); 
        }

        if (!_players.Contains(winnerPlayerId))
        {
            throw new Exception("Invalid winner playerId=" + winnerPlayerId.Id.ToString() );
        }
        _winnerPlayerId = winnerPlayerId;
    }

    public PlayerId? GetWinnerPlayerId()
    {
        return _winnerPlayerId;
    }
}