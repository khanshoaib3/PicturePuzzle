using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public class GameplayScene : BaseScene
{
    private Game1 _game1;
    private SimpleBoard _simpleBoard;
    private TimeSpan? _pressedTime;

    public GameplayScene(Game1 game1)
    {
        _game1 = game1;
        _simpleBoard = new SimpleBoard(_game1);
    }

    private void LoadTextures(ContentManager content)
    {
    }

    public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
    {
        if (_simpleBoard.HasEnded)
        {
            _game1.CurrentScene = new EndScene(_game1, _simpleBoard.HasWon, _simpleBoard.Moves, _simpleBoard.TimeLeft);
            return;
        }

        _simpleBoard.Update(gameTime);

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
                if (_simpleBoard.KeyPressed(key))
                {
                    _pressedTime = currentTime;
                    return;
                }
            }

            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
            {
                _simpleBoard.HandleDownMovement();
                _pressedTime = currentTime;
            }
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight))
            {
                _simpleBoard.HandleLeftMovement();
                _pressedTime = currentTime;
            }
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
            {
                _simpleBoard.HandleUpMovement();
                _pressedTime = currentTime;
            }
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
            {
                _simpleBoard.HandleRightMovement();
                _pressedTime = currentTime;
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _simpleBoard.Draw(spriteBatch);
    }
}