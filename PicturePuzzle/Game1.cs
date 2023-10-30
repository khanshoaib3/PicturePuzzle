using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PicturePuzzle;

public class Game1 : Game
{
    // ReSharper disable once NotAccessedField.Local
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public BaseScene CurrentScene;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        CurrentScene = new TitleScene(this);
    }

    protected override void Update(GameTime gameTime)
    {
        CurrentScene?.Update(gameTime, _graphics);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        CurrentScene?.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}