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
        int centerX = _game1.Window.ClientBounds.Width / 2;
        _startButton = new Button("sprites/common/start_button_unhovered", "sprites/common/start_button_hovered",
            () => game1.CurrentScene = new GameplayScene(game1), game1);
        _startButton.X = centerX - _startButton.Width - 80;
        _startButton.Y = _game1.Window.ClientBounds.Height - _startButton.Height - 140;
        _exitButton = new Button("sprites/common/exit_button_unhovered", "sprites/common/exit_button_hovered",
            () => game1.Exit(), game1);
        _exitButton.X = centerX + 80;
        _exitButton.Y = _game1.Window.ClientBounds.Height - _exitButton.Height - 140;
        LoadTextures(game1.Content);
    }

    private void LoadTextures(ContentManager content)
    {
        _logoTexture = content.Load<Texture2D>("sprites/title_screen/logo");
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
        int xPos = _game1.Window.ClientBounds.Width / 2 - _logoTexture.Width / 2;
        int yPos = _game1.Window.ClientBounds.Height / 2 - _logoTexture.Height / 2 - 80;
        spriteBatch.Draw(_logoTexture, new Vector2(xPos, yPos), Color.White);
        _startButton.Draw(spriteBatch);
        _exitButton.Draw(spriteBatch);
    }
}