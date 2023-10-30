using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public class TitleScene : BaseScene
{
    private TimeSpan? _pressedTime;
    private Game1 _game1;
    
    public TitleScene(Game1 game1)
    {
        _game1 = game1;
        LoadTextures(game1.Content);
    }

    private void LoadTextures(ContentManager content)
    {
    }

    public override void Update(GameTime gameTime, GraphicsDeviceManager graphics)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            _game1.Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            _game1.CurrentScene = new GameplayScene(_game1);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
    }
}