using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public class Game1 : Game
{
    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private BlockManager _blockManager;
    private TimeSpan? _pressedTime;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        BlockManager.LoadTextures(Content);
        _blockManager = new();
        _blockManager.LoadBlocks();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_blockManager.IsArranged())
        {
            base.Update(gameTime);
            return;
        }

        var currentTime = gameTime.TotalGameTime;
        if (_pressedTime == null || currentTime - (TimeSpan)_pressedTime >= TimeSpan.FromMilliseconds(250))
        {
            foreach (var key in Keyboard.GetState().GetPressedKeys())
            {
                if (_blockManager.KeyPressed(key))
                {
                    _pressedTime = currentTime;
                    base.Update(gameTime);
                    return;
                }
            }

            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp))
            {
                _blockManager.HandleUpMovement();
                _pressedTime = currentTime;
            }
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight))
            {
                _blockManager.HandleLeftMovement();
                _pressedTime = currentTime;
            }
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown))
            {
                _blockManager.HandleUpMovement();
                _pressedTime = currentTime;
            }
            else if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
            {
                _blockManager.HandleRightMovement();
                _pressedTime = currentTime;
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        _blockManager.DrawAllBlocks(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}