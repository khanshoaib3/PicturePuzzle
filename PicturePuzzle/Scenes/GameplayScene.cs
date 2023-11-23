using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public class GameplayScene : IScene
{
    private Game1 _game1;
    private AbstractBoard _currentBoard;
    private TimeSpan? _pressedTime;

    public GameplayScene(Game1 game1)
    {
        _game1 = game1;
        _currentBoard = new EmojiBoard(_game1, 120);
    }

    public virtual void Update(GameTime gameTime, GraphicsDeviceManager graphics)
    {
        _currentBoard.Update(gameTime);

        if (_currentBoard.HasEnded)
        {
            _game1.CurrentScene = new EndScene(_game1, _currentBoard.HasWon, _currentBoard.Moves, _currentBoard.TimeLeft);
            return;
        }

        HandleInputs(gameTime);
    }

    private void HandleInputs(GameTime gameTime)
    {
        var currentTime = gameTime.TotalGameTime;
        if (_pressedTime != null && currentTime - (TimeSpan)_pressedTime < TimeSpan.FromMilliseconds(250)) return;

        foreach (var key in Keyboard.GetState().GetPressedKeys())
        {
            if (!_currentBoard.HandleKeyPressed(key)) continue;
            _pressedTime = currentTime;
            return;
        }

        if (_currentBoard.HandleGamepadButton(GamePad.GetState(PlayerIndex.One)))
        {
            _pressedTime = currentTime;
            return;
        }

        if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
            _currentBoard.HandleMouseLeftButton(Mouse.GetState().X, Mouse.GetState().Y))
        {
            _pressedTime = currentTime;
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        _currentBoard.Draw(spriteBatch);
    }
}