using System.Collections.Generic;
using Lunar.Core.Base;

namespace Lunar.Core.ECS.Interfaces;

/// <summary>
///     Basic input interface.
/// </summary>
public interface IInput
{
    bool GetKeyDown(KeyCode keycode);

    bool GetKey(KeyCode keycode);

    bool GetKeyUp(KeyCode keycode);
}

/// <summary>
///     Behavioral interface based on Basic input.
/// </summary>
public interface IInputActions
{
    Dictionary<string, KeyCode[]> Bindings { get; }
    IInput Input { get; }

    /// <summary>
    ///     Set the key bindings for a given action.
    /// </summary>
    /// <param name="action">Logical action name (e.g. "Jump").</param>
    /// <param name="keys">One or more physical keys to bind.</param>
    /// <returns>True if the binding is set successfully, false otherwise.</returns>
    bool SetBinding(string action, params KeyCode[] keys);

    /// <summary>
    ///     Remove the binding for a given action.
    /// </summary>
    /// <param name="action">Logical action name.</param>
    /// <returns>True if the binding was removed, false if it did not exist.</returns>
    bool RemoveBinding(string action);

    /// <summary>
    ///     Remove all bindings.
    /// </summary>
    void ClearBindings();

    /// <summary>
    ///     Check if the given action was pressed down this frame.
    /// </summary>
    /// <param name="action">Logical action name.</param>
    /// <returns>True if the action was pressed down this frame.</returns>
    bool GetActionDown(string action);

    /// <summary>
    ///     Check if the given action is currently held.
    /// </summary>
    /// <param name="action">Logical action name.</param>
    /// <returns>True if the action is being held.</returns>
    bool GetAction(string action);

    /// <summary>
    ///     Check if the given action was released this frame.
    /// </summary>
    /// <param name="action">Logical action name.</param>
    /// <returns>True if the action was released this frame.</returns>
    bool GetActionUp(string action);
}