using boardgames_sharp.Entity;

namespace examples;

public static class Constants
{
    public static readonly PropertyId<int> XIntPropertyId = new PropertyId<int>(1);
    public static readonly PropertyId<int> YIntPropertyId = new PropertyId<int>(2);
    public static readonly PropertyId<int> ValueIntPropertyId = new PropertyId<int>(3);
    public const uint Player1 = 1;
    public const uint Player2 = 2;
    public const int ValueEmpty = 0;
    public const int ValuePlayer1 = 1;
    public const int ValuePlayer2 = 2;
}