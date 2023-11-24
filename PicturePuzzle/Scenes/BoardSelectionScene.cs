using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PicturePuzzle;

public class BoardSelectionScene : IScene
{
    private Game1 _game1;

    private Button _simpleBoard;
    private Button _emojiBoard;
    private Button _batmanBoard;

    public int Selected { get; private set; }

    public BoardSelectionScene(Game1 game1)
    {
        _game1 = game1;
        _simpleBoard = new Button("sprites/board_selection_scene/simple_button_unhovered",
            "sprites/board_selection_scene/simple_button_hovered", () => Selected = 1, game1);
        _simpleBoard.X = _game1.Window.ClientBounds.Width / 2 - _simpleBoard.Width / 2;
        _simpleBoard.Y = _game1.Window.ClientBounds.Height / 4 - _simpleBoard.Height / 2;

        _emojiBoard = new Button("sprites/board_selection_scene/emoji_button_unhovered",
            "sprites/board_selection_scene/emoji_button_hovered", () => Selected = 2, game1);
        _emojiBoard.X = _game1.Window.ClientBounds.Width / 2 - _emojiBoard.Width / 2;
        _emojiBoard.Y = _game1.Window.ClientBounds.Height / 2 - _emojiBoard.Height / 2;

        _batmanBoard = new Button("sprites/board_selection_scene/batman_button_unhovered",
            "sprites/board_selection_scene/batman_button_hovered", () => Selected = 3, game1);
        _batmanBoard.X = _game1.Window.ClientBounds.Width / 2 - _batmanBoard.Width / 2;
        _batmanBoard.Y = (3 * _game1.Window.ClientBounds.Height) / 4 - _batmanBoard.Height / 2;
    }

    public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
    {
        _simpleBoard.Update();
        _emojiBoard.Update();
        _batmanBoard.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _simpleBoard.Draw(spriteBatch);
        _emojiBoard.Draw(spriteBatch);
        _batmanBoard.Draw(spriteBatch);
    }
}