using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        _currentBoard = new SimpleBoard(_game1, 120);
    }

    private void LoadTextures(ContentManager content)
    {
    }

    public virtual void Update(GameTime gameTime, GraphicsDeviceManager graphics)
    {
        if (_currentBoard.HasEnded)
        {
            _game1.CurrentScene = new EndScene(_game1, _currentBoard.HasWon, _currentBoard.Moves, _currentBoard.TimeLeft);
            return;
        }

        _currentBoard.Update(gameTime);

        var currentTime = gameTime.TotalGameTime;
        if (_pressedTime == null || currentTime - (TimeSpan)_pressedTime >= TimeSpan.FromMilliseconds(250))
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F11))
            {
                _pressedTime = currentTime;
                graphics.ToggleFullScreen();
                return;
            }

            foreach (var key in Keyboard.GetState().GetPressedKeys())
            {
                if (_currentBoard.KeyPressed(key))
                {
                    _pressedTime = currentTime;
                    return;
                }
            }

            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
            {
                _currentBoard.HandleDownMovement();
                _pressedTime = currentTime;
            }
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight))
            {
                _currentBoard.HandleLeftMovement();
                _pressedTime = currentTime;
            }
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
            {
                _currentBoard.HandleUpMovement();
                _pressedTime = currentTime;
            }
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
            {
                _currentBoard.HandleRightMovement();
                _pressedTime = currentTime;
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        _currentBoard.Draw(spriteBatch);
    }
}