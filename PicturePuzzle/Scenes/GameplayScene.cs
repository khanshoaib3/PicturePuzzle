using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public class GameplayScene : IScene
{
    private Game1 _game1;
    private BoardSelectionScene? _boardSelectionPage;
    private AbstractBoard? _currentBoard;
    private TimeSpan? _pressedTime;

    public GameplayScene(Game1 game1)
    {
        _game1 = game1;
        _boardSelectionPage = new BoardSelectionScene(_game1);
    }

    public virtual void Update(GameTime gameTime, GraphicsDeviceManager graphics)
    {
        if (_boardSelectionPage != null)
        {
            switch (_boardSelectionPage.Selected)
            {
                case 0:
                    break;
                case 1:
                    _currentBoard = new SimpleBoard(_game1, 80);
                    _boardSelectionPage = null;
                    break;
                case 2:
                    _currentBoard = new EmojiBoard(_game1, 150);
                    _boardSelectionPage = null;
                    break;
                case 3:
                    _currentBoard = new BatmanBoard(_game1, 240);
                    _boardSelectionPage = null;
                    break;
            }

            _boardSelectionPage?.Update(gameTime, graphics);
            return;
        }

        if (_currentBoard == null) return;

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
        if (_currentBoard == null) return;

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
        _boardSelectionPage?.Draw(spriteBatch);
        _currentBoard?.Draw(spriteBatch);
    }
}