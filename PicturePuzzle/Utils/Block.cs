using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PicturePuzzle;

public class Block
{
    public Texture2D? Texture { get; set; }
    private readonly int _x;
    private readonly int _y;

    public Block? Up;
    public Block? Right;
    public Block? Down;
    public Block? Left;

    public Block(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Texture != null)
        {
            spriteBatch.Draw(Texture, new Vector2(_x + 5, _y + 5), Color.White);
        }
    }

    public bool Contains(int x, int y) => Texture != null && x >= _x && x <= _x + Texture.Width && y >= _y && y <= _y + Texture.Height;

    public void SwapTextures(Block fromBlock)
    {
        Texture2D temp = Texture;
        Texture = fromBlock.Texture;
        fromBlock.Texture = temp;
    }
}