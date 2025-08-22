namespace Lunar.Interfaces
{
    public interface IInput
    {
        bool GetKeyDown(KeyCode keycode);

        bool GetKey(KeyCode keycode);

        bool GetKeyUp(KeyCode keycode);
    }
}