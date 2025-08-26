using System;

namespace Lunar
{
    public class GameObject
    {
        public object BaseGameObject { get; private set; }

        public GameObject(object baseGameObject)
        {
            BaseGameObject = baseGameObject ?? throw new ArgumentNullException(nameof(baseGameObject));
        }
    }
}