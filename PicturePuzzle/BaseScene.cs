using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PicturePuzzle;

public abstract class BaseScene
{
    public abstract void Update(GameTime gameTime, GraphicsDeviceManager graphics);

    public abstract void Draw(SpriteBatch spriteBatch);
}