using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PicturePuzzle.Content;

namespace PicturePuzzle;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Block> _blocks;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _blocks = new List<Block>()
        {
            new Block("block_1", new Vector2(0, 0), _graphics),
            new Block("block_2", new Vector2(120, 0), _graphics),
            new Block("block_3", new Vector2(240, 0), _graphics),
            new Block("block_4", new Vector2(0, 120), _graphics),
            new Block("block_5", new Vector2(120, 120), _graphics),
            new Block("block_6", new Vector2(240, 120), _graphics),
            new Block("block_7", new Vector2(0, 240), _graphics),
            new Block("block_8", new Vector2(120, 240), _graphics),
            new Block("block_9", new Vector2(240, 240), _graphics),
            new Block("block_empty", new Vector2(120, 360), _graphics),
        };
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        
        _blocks.ForEach(block => block.Draw(_spriteBatch));
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}