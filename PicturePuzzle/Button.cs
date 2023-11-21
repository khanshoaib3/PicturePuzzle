using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public class Button
{
    private Texture2D _unHoveredTexture;
    private Texture2D _hoveredTexture;

    private Action _onClick;
    private Game1 _game1;

    private bool _hovered;

    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool Hovered => _hovered;
    
    public Button(string unHoveredTextureName, string hoveredTextureName, Action onClick, Game1 game1)
    {
        _game1 = game1;
        _unHoveredTexture = _game1.Content.Load<Texture2D>(unHoveredTextureName);
        _hoveredTexture = _game1.Content.Load<Texture2D>(hoveredTextureName);
        _onClick = onClick;
        X = 0;
        Y = 0;
        Width = _unHoveredTexture.Width;
        Height = _unHoveredTexture.Height;
    }

    public void Update()
    {
        int mouseX = Mouse.GetState().X;
        int mouseY = Mouse.GetState().Y;

        if (mouseX >= X && mouseX <= X + _hoveredTexture.Width && mouseY >= Y && mouseY <= Y + _hoveredTexture.Height)
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
            spriteBatch.Draw(_hoveredTexture, new Vector2(X, Y), Color.White);
        else
            spriteBatch.Draw(_unHoveredTexture, new Vector2(X, Y), Color.White);
    }
}