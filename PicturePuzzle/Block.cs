using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PicturePuzzle;

public class Block
{
    private Texture2D _texture;
    private readonly int _x;
    private readonly int _y;

    public Block Up;
    public Block Right;
    public Block Down;
    public Block Left;
    
    public Block(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_texture != null)
        {
            spriteBatch.Draw(_texture, new Vector2(_x + 5, _y + 5), Color.White);
        }
    }

    public Texture2D GetTexture()
    {
        return _texture;
    }

    public void SetTexture(Texture2D texture)
    {
        _texture = texture;
    }

    public void SwapTextures(Block fromBlock)
    {
        Texture2D temp = _texture;
        _texture = fromBlock._texture;
        fromBlock._texture = temp;
    }
}
