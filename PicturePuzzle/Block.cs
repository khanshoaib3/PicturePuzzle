using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PicturePuzzle.Content;

public class Block
{
    private Texture2D _spriteAtlas;
    private string _currentBlockTexture;
    private Vector2 _position;

    public Block up;
    public Block right;
    public Block down;
    public Block left;
    
    public Block(string currentBlockTexture, Vector2 position)
    {
        _currentBlockTexture = currentBlockTexture;
        _position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Texture2D backgroundTexture;
        if (_currentBlockTexture != "null")
        {
            Texture2D currentBlockTexture;
            SimpleBoard.BlockTextures.TryGetValue(_currentBlockTexture, out currentBlockTexture);
            spriteBatch.Draw(currentBlockTexture, new Vector2(_position.X + 5, _position.Y + 5), Color.White);
        }
    }

    public string GetTextureName()
    {
        return _currentBlockTexture;
    }

    public void SetTextureName(string name)
    {
        _currentBlockTexture = name;
    }

    public void SwapTextures(Block fromBlock)
    {
        string temp = _currentBlockTexture;
        _currentBlockTexture = fromBlock._currentBlockTexture;
        fromBlock._currentBlockTexture = temp;
    }
}
