using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PicturePuzzle.Content;

public class Block
{
    private Texture2D _spriteAtlas;
    private Vector2 _position;
    public Block(string file, Vector2 position, GraphicsDeviceManager graphics)
    {
       // For Linux Users
        
         FileStream fileStream = new FileStream($"/home/towk/Projects/PicturePuzzle/PicturePuzzle/Content/sprites/{file}.png", FileMode.Open);
        
       // FileStream fileStream = new FileStream($"\\Users\\yourUserName\\OneDrive\\Documents\\GitHub\\PicturePuzzle\\PicturePuzzle\\Content\\sprites\\{file}.png", FileMode.Open);
        
        _spriteAtlas = Texture2D.FromStream(graphics.GraphicsDevice, fileStream);
        // _spriteAtlas = Content.Load<Texture2D>("sprites/block_empty");
        fileStream.Dispose();

        _position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_spriteAtlas, _position, Color.White);
    }

    public void UpdatePosition(int x, int y)
    {
        _position.X = x;
        _position.Y = y;
    }

    public int GetX()
    {
        return (int)_position.X;
    }

    public int GetY()
    {
        return (int)_position.Y;
    }
}
