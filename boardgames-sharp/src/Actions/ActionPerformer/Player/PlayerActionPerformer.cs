using boardgames_sharp.Player;

namespace boardgames_sharp.Actions.ActionPerformer.Player;

public interface IPlayerActionPerformer
{
    public void Win(PlayerId winner);
}
internal class PlayerActionPerformer: IPlayerActionPerformer, IInitializeWithEngineRoot
{
    public void Win(PlayerId winner)
    {
        if (_playerManager == null)
        {
            throw new BadRootInitializationException("Player manager not initialized");
        }
        _playerManager.Win(winner);
    }

    public void initialize(EngineRoot engineRoot)
    {
        _playerManager = engineRoot.PlayerManager;
    }
    private IPlayerManager? _playerManager;
}