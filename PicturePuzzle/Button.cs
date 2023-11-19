using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public class Button
{
    private int x;
    private int y;
    private string _unHoveredTextureName;
    private string _hoveredTextureName;
    private Action _onClick;
    private Game1 _game1;

    private Texture2D _unHoveredTexture;
    private Texture2D _hoveredTexture;

    private bool _hovered;
    
    public Button(int x, int y, string unHoveredTextureName, string hoveredTextureName, Action onClick, Game1 game1)
    {
        this.x = x;
        this.y = y;
        _unHoveredTextureName = unHoveredTextureName;
        _hoveredTextureName = hoveredTextureName;
        _onClick = onClick;
        _game1 = game1;
    }

    public void LoadTextures(ContentManager content)
    {
        _unHoveredTexture = content.Load<Texture2D>(_unHoveredTextureName);
        _hoveredTexture = content.Load<Texture2D>(_hoveredTextureName);
    }

    public void Update()
    {
        int mouseX = Mouse.GetState().X;
        int mouseY = Mouse.GetState().Y;

        if (mouseX >= x && mouseX <= x + _hoveredTexture.Width && mouseY >= y && mouseY <= y + _hoveredTexture.Height)
        {
            _hovered = true;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                _onClick.Invoke();
        }
        else
        {
            _hovered = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_hovered)
            spriteBatch.Draw(_hoveredTexture, new Vector2(x, y), Color.White);
        else
            spriteBatch.Draw(_unHoveredTexture, new Vector2(x, y), Color.White);
    }
}