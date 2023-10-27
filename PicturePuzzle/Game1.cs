using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public class Game1 : Game
{
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
        _blockManager = new();
        _blockManager.LoadBlocks(_graphics);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        TimeSpan currentTime = gameTime.TotalGameTime;
        
        if (Keyboard.GetState().IsKeyDown(Keys.Right)
            && (_pressedTime == null || currentTime - (TimeSpan)_pressedTime >= TimeSpan.FromMilliseconds(250)))
        {
            _pressedTime = currentTime;
            _blockManager.blocks[9].UpdatePosition(_blockManager.blocks[9].GetX() + 120, _blockManager.blocks[9].GetY());
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.Left)
                 && (_pressedTime == null || currentTime - (TimeSpan)_pressedTime >= TimeSpan.FromMilliseconds(250)))
        {
            _pressedTime = currentTime;
            _blockManager.blocks[9].UpdatePosition(_blockManager.blocks[9].GetX() - 120, _blockManager.blocks[9].GetY());
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