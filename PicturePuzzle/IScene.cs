using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PicturePuzzle;

public interface IScene
{
    void Update(GameTime gameTime, GraphicsDeviceManager graphics);
    void Draw(SpriteBatch spriteBatch);
}