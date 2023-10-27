using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PicturePuzzle.Content;

namespace PicturePuzzle;

public class BlockManager
{
    public List<Block> blocks;

    public void LoadBlocks(GraphicsDeviceManager graphics)
    {
        blocks = new List<Block>()
        {
            new Block("block_1", new Vector2(0, 0), graphics),
            new Block("block_2", new Vector2(120, 0), graphics),
            new Block("block_3", new Vector2(240, 0), graphics),
            new Block("block_4", new Vector2(0, 120), graphics),
            new Block("block_5", new Vector2(120, 120), graphics),
            new Block("block_6", new Vector2(240, 120), graphics),
            new Block("block_7", new Vector2(0, 240), graphics),
            new Block("block_8", new Vector2(120, 240), graphics),
            new Block("block_9", new Vector2(240, 240), graphics),
            new Block("block_empty", new Vector2(120, 360), graphics),
        };
    }

    public void DrawAllBlocks(SpriteBatch spriteBatch)
    {
        blocks.ForEach(block => block.Draw(spriteBatch));
    }
}