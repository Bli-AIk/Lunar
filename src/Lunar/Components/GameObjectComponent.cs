namespace Lunar.Components
{
    public struct GameObjectComponent
    {
        public GameObjectComponent(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public GameObject GameObject;
    }
}