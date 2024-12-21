using ShmupTest.Systems;
using Microsoft.Xna.Framework;

namespace ShmupTest.Messages;

public readonly record struct ExampleMessage();
public readonly record struct InputAction(float Value, Actions Action, ActionState State);
public readonly record struct MousePosition(Vector2 Position);