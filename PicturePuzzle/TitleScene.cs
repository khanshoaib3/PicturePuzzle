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
    private Texture2D _logoTexture;
    private Button _startButton;
    private Button _exitButton;

    public TitleScene(Game1 game1)
    {
        _game1 = game1;
        _startButton = new Button(100, 340, "sprites/title_screen/start_button_unhovered",
            "sprites/title_screen/start_button_hovered", () => game1.CurrentScene = new GameplayScene(game1), game1);
        _exitButton = new Button(520, 340, "sprites/title_screen/exit_button_unhovered",
            "sprites/title_screen/exit_button_hovered", () => game1.Exit(), game1);
        LoadTextures(game1.Content);
    }

    private void LoadTextures(ContentManager content)
    {
        _logoTexture = content.Load<Texture2D>("sprites/title_screen/logo");
        _startButton.LoadTextures(content);
        _exitButton.LoadTextures(content);
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
        
        _startButton.Update();
        _exitButton.Update();
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_logoTexture, new Vector2(150, 80), Color.White);
        _startButton.Draw(spriteBatch);
        _exitButton.Draw(spriteBatch);
    }
}