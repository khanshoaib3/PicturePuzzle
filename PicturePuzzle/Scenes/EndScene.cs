using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PicturePuzzle;

public class EndScene : IScene
{
    private Game1 _game1;
    private readonly SpriteFont _04BFont;
    private readonly bool _hasWon;
    private readonly int _moves;
    private readonly int _timeLeft;
    
    private Button _playAgainButton;
    private Button _exitButton;

    private Texture2D _gameWonTexture;
    private Texture2D _gameLostTexture;

    public EndScene(Game1 game1, bool hasWon, int moves, int timeLeft)
    {
        _game1 = game1;
        _hasWon = hasWon;
        _moves = moves;
        _timeLeft = timeLeft;
        _04BFont = _game1.Content.Load<SpriteFont>("fonts/04B_30");
        _gameWonTexture = _game1.Content.Load<Texture2D>("sprites/end_scene/game_won");
        _gameLostTexture = _game1.Content.Load<Texture2D>("sprites/end_scene/game_lost");
        int centerX = _game1.Window.ClientBounds.Width / 2;
        _playAgainButton = new Button("sprites/common/play_again_button_unhovered", "sprites/common/play_again_button_hovered",
            () => game1.CurrentScene = new GameplayScene(game1), game1);
        _playAgainButton.X = centerX - _playAgainButton.Width + 55;
        _playAgainButton.Y = _game1.Window.ClientBounds.Height - _playAgainButton.Height - 60;
        _exitButton = new Button("sprites/common/exit_button_unhovered", "sprites/common/exit_button_hovered",
            () => game1.Exit(), game1);
        _exitButton.X = centerX + 100;
        _exitButton.Y = _game1.Window.ClientBounds.Height - _exitButton.Height - 60;
    }

    public virtual void Update(GameTime gameTime, GraphicsDeviceManager graphics)
    {
        _playAgainButton.Update();
        _exitButton.Update();
    }

    // ReSharper disable PossibleLossOfFraction
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Texture2D logoTexture = _hasWon ? _gameWonTexture : _gameLostTexture;
        int logoX = _game1.Window.ClientBounds.Width / 2 - logoTexture.Width / 2;
        int logoY = _game1.Window.ClientBounds.Height / 2 - logoTexture.Height / 2 - (_hasWon ? 100 : 80);
        spriteBatch.Draw(logoTexture, new Vector2(logoX, logoY), Color.White);

        if (_hasWon)
        {
            string movesText = $"Moves taken: {_moves}";
            Vector2 movesTextMiddlePoint = _04BFont.MeasureString(movesText) / 2;
            Vector2 movesTextPos = new Vector2(_game1.Window.ClientBounds.Width / 2, _game1.Window.ClientBounds.Height / 2);
            spriteBatch.DrawString(_04BFont, movesText, movesTextPos,
                new Color(129, 23, 27), 0, movesTextMiddlePoint, 1.2f, SpriteEffects.None, 0.5f);

            string timeText = $"Time Left: {_timeLeft}";
            Vector2 timeTextMiddlePoint = _04BFont.MeasureString(timeText) / 2;
            Vector2 timeTextPos = new Vector2(_game1.Window.ClientBounds.Width / 2, _game1.Window.ClientBounds.Height / 2 + 50);
            spriteBatch.DrawString(_04BFont, timeText, timeTextPos,
                new Color(129, 23, 27), 0, timeTextMiddlePoint, 1.2f, SpriteEffects.None, 0.5f);
        }
        
        _playAgainButton.Draw(spriteBatch);
        _exitButton.Draw(spriteBatch);
    }
}