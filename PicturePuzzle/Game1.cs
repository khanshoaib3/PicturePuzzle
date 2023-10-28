using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PicturePuzzle.Content;

namespace PicturePuzzle;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private BlockManager _blockManager;
    private TimeSpan? _pressedTime;
    int controlledBlockIndex = 9;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        BlockManager.LoadTextures(_graphics.GraphicsDevice);
        _blockManager = new();
        _blockManager.LoadBlocks();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        TimeSpan currentTime = gameTime.TotalGameTime;
        Block controlledBlock = _blockManager.Blocks[controlledBlockIndex];
        if (Keyboard.GetState().IsKeyDown(Keys.Up)
            && (_pressedTime == null || currentTime - (TimeSpan)_pressedTime >= TimeSpan.FromMilliseconds(250)))
        {
            _pressedTime = currentTime;
            if (controlledBlock.up != null)
            {
                controlledBlock.SwapTextures(controlledBlock.up);
                controlledBlockIndex = _blockManager.Blocks.IndexOf(controlledBlock.up);
            }
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.Right)
                 && (_pressedTime == null || currentTime - (TimeSpan)_pressedTime >= TimeSpan.FromMilliseconds(250)))
        {
            _pressedTime = currentTime;
            if (controlledBlock.right != null)
            {
                controlledBlock.SwapTextures(controlledBlock.right);
                controlledBlockIndex = _blockManager.Blocks.IndexOf(controlledBlock.right);
            }
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.Down)
                 && (_pressedTime == null || currentTime - (TimeSpan)_pressedTime >= TimeSpan.FromMilliseconds(250)))
        {
            _pressedTime = currentTime;
            if (controlledBlock.down != null)
            {
                controlledBlock.SwapTextures(controlledBlock.down);
                controlledBlockIndex = _blockManager.Blocks.IndexOf(controlledBlock.down);
            }
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.Left)
                 && (_pressedTime == null || currentTime - (TimeSpan)_pressedTime >= TimeSpan.FromMilliseconds(250)))
        {
            _pressedTime = currentTime;
            if (controlledBlock.left != null)
            {
                controlledBlock.SwapTextures(controlledBlock.left);
                controlledBlockIndex = _blockManager.Blocks.IndexOf(controlledBlock.left);
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