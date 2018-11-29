

namespace IsometricCommunity.GameObjects.Interfaces
{
    public interface ICloneable<T> where T : MapObject
    {
        T Clone();
    }
}
