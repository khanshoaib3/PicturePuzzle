using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public abstract class AbstractBoard
{
    private const int BoardTopLeftX = 10;
    private const int BoardTopLeftY = 10;

    private readonly List<Block> _blocks = new();
    private readonly Dictionary<string, Texture2D> _blockTextures = new();
    private readonly Dictionary<Texture2D, string> _blockTexturesReversed = new();
    private readonly List<string> _textureNamesInOrder;
    private Texture2D _boardBackgroundTexture;
    private readonly SpriteFont _04BFont;
    private readonly Game1 _game1;

    private int _controlledBlockIndex;
    private TimeSpan? _startingTime;
    private int _totalTimeInSeconds;
    
    public string BoardName { get; }
    public bool HasEnded { get; private set; }
    public bool HasWon { get; private set; }
    public int Moves { get; private set; }
    public int TimeLeft { get; private set; }


    public AbstractBoard(Game1 game1, List<string> textureNamesInOrder, string boardName, int totalTimeInSeconds)
    {
        _game1 = game1;
        _textureNamesInOrder = textureNamesInOrder;
        BoardName = boardName;
        TimeLeft = totalTimeInSeconds;
        _totalTimeInSeconds = totalTimeInSeconds;
        HasEnded = false;
        _04BFont = _game1.Content.Load<SpriteFont>("fonts/04B_30");
        LoadTextures();
        LoadBlocks();
    }

    private void LoadTextures()
    {
        foreach (var name in _textureNamesInOrder)
        {
            if (name == "null") continue;
            var texture2D = _game1.Content.Load<Texture2D>(name);
            _blockTextures.Add(name, texture2D);
            _blockTexturesReversed.Add(texture2D, name);
        }

        _boardBackgroundTexture = _game1.Content.Load<Texture2D>("sprites/common/board_background");
    }

    private void LoadBlocks()
    {
        int blockStartX = BoardTopLeftX + 5;
        int blockStartY = BoardTopLeftY + 85;
        int blockWidth = _blockTextures.First().Value.Width;
        int blockHeight = _blockTextures.First().Value.Height;
        // ReSharper disable UselessBinaryOperation
        Block[,] blockArray =
        {
            {
                new Block(blockStartX + blockWidth * 0, blockStartY + blockHeight * 0),
                new Block(blockStartX + blockWidth * 1, blockStartY + blockHeight * 0),
                new Block(blockStartX + blockWidth * 2, blockStartY + blockHeight * 0),
            },
            {
                new Block(blockStartX + blockWidth * 0, blockStartY + blockHeight * 1),
                new Block(blockStartX + blockWidth * 1, blockStartY + blockHeight * 1),
                new Block(blockStartX + blockWidth * 2, blockStartY + blockHeight * 1),
            },
            {
                new Block(blockStartX + blockWidth * 0, blockStartY + blockHeight * 2),
                new Block(blockStartX + blockWidth * 1, blockStartY + blockHeight * 2),
                new Block(blockStartX + blockWidth * 2, blockStartY + blockHeight * 2),
            }
        };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int right = (j + 1 > 2) ? -1 : (j + 1);
                int left = (j - 1 < 0) ? -1 : (j - 1);
                int down = (i + 1 > 2) ? -1 : (i + 1);
                int up = (i - 1 < 0) ? -1 : (i - 1);

                blockArray[i, j].Up = (up == -1) ? null : blockArray[up, j];
                blockArray[i, j].Right = (right == -1) ? null : blockArray[i, right];
                blockArray[i, j].Down = (down == -1) ? null : blockArray[down, j];
                blockArray[i, j].Left = (left == -1) ? null : blockArray[i, left];

                _blocks.Add(blockArray[i, j]);
            }
        }

        List<string> randomisedTextures = RandomiseTextures();
        for (int i = 0; i < 9; i++)
        {
            if (randomisedTextures[i] == "null")
            {
                _controlledBlockIndex = i;
                continue;
            }

            _blocks[i].SetTexture(_blockTextures[randomisedTextures[i]]);
        }
    }

    private List<string> RandomiseTextures()
    {
        List<string> randomTextures = new();
        Random random = new Random();
        List<int> acquiredIndexes = new();

        // Move "null" to first
        List<string> source = new() { "null" };
        foreach (var name in _textureNamesInOrder)
        {
            if (name != "null") source.Add(name);
        }

        for (int i = 0; i < 9; i++)
        {
            int r = (int)random.NextInt64(0, 9);
            while (acquiredIndexes.Contains(r))
            {
                r = (int)random.NextInt64(0, 9);
            }

            randomTextures.Add(source[r]);
            acquiredIndexes.Add(r);
        }

        if (IsSolvable(acquiredIndexes))
            return randomTextures;
        else
            return RandomiseTextures();
    }

    private bool IsSolvable(List<int> indexes)
    {
        int inversions = 0;
        for (int i = 0; i < 9 - 1; i++)
        {
            for (int j = i + 1; j < 9; j++)
            {
                if (indexes[j] > 0 && indexes[i] > 0 && indexes[i] > indexes[j])
                    inversions++;
            }
        }

        return (inversions % 2 == 0);
    }

    public bool KeyPressed(Keys keyboardKey)
    {
        switch (keyboardKey)
        {
            case Keys.Up:
                HandleDownMovement();
                return true;
            case Keys.Right:
                HandleLeftMovement();
                return true;
            case Keys.Down:
                HandleUpMovement();
                return true;
            case Keys.Left:
                HandleRightMovement();
                return true;
            default:
                return false;
        }
    }

    public void HandleUpMovement()
    {
        Block controlledBlock = _blocks[_controlledBlockIndex];
        if (controlledBlock.Up != null)
        {
            controlledBlock.SwapTextures(controlledBlock.Up);
            _controlledBlockIndex = _blocks.IndexOf(controlledBlock.Up);
            Moves++;
        }
    }

    public void HandleRightMovement()
    {
        Block controlledBlock = _blocks[_controlledBlockIndex];
        if (controlledBlock.Right != null)
        {
            controlledBlock.SwapTextures(controlledBlock.Right);
            _controlledBlockIndex = _blocks.IndexOf(controlledBlock.Right);
            Moves++;
        }
    }

    public void HandleDownMovement()
    {
        Block controlledBlock = _blocks[_controlledBlockIndex];
        if (controlledBlock.Down != null)
        {
            controlledBlock.SwapTextures(controlledBlock.Down);
            _controlledBlockIndex = _blocks.IndexOf(controlledBlock.Down);
            Moves++;
        }
    }

    public void HandleLeftMovement()
    {
        Block controlledBlock = _blocks[_controlledBlockIndex];
        if (controlledBlock.Left != null)
        {
            controlledBlock.SwapTextures(controlledBlock.Left);
            _controlledBlockIndex = _blocks.IndexOf(controlledBlock.Left);
            Moves++;
        }
    }

    public bool IsArranged()
    {
        for (var i = 0; i < 9; i++)
        {
            if (_blocks[i].GetTexture() == null)
            {
                if (_textureNamesInOrder[i] != "null")
                {
                    return false;
                }

                continue;
            }

            if (_blockTexturesReversed[_blocks[i].GetTexture()] != _textureNamesInOrder[i])
            {
                return false;
            }
        }

        return true;
    }

    public void Update(GameTime gameTime)
    {
        if (HasEnded) return;
        
        _startingTime ??= gameTime.TotalGameTime;
        TimeLeft = _totalTimeInSeconds - (int)(gameTime.TotalGameTime - (TimeSpan)_startingTime).TotalSeconds;
        
        if (IsArranged())
        {
            HasWon = true;
            HasEnded = true;
            return;
        }

        if (TimeLeft <= 0)
        {
            HasWon = false;
            HasEnded = true;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_boardBackgroundTexture, new Vector2(BoardTopLeftX, BoardTopLeftY), Color.White);
        _blocks.ForEach(block => block.Draw(spriteBatch));

        spriteBatch.DrawString(_04BFont, $"Moves: {Moves}", new Vector2(BoardTopLeftX + 15, BoardTopLeftY + 57),
            new Color(129, 23, 27), 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0.5f);

        Vector2 textMiddlePoint = _04BFont.MeasureString(BoardName) / 2;
        // ReSharper disable once PossibleLossOfFraction
        spriteBatch.DrawString(_04BFont, BoardName,
            new Vector2(BoardTopLeftX + _boardBackgroundTexture.Width / 2,
                BoardTopLeftY + 40), new Color(84, 8, 4), 0, textMiddlePoint, 1.6f, SpriteEffects.None, 0.5f);

        var x = BoardTopLeftX + _boardBackgroundTexture.Width - _04BFont.MeasureString($"Time: {TimeLeft}").X + 12;
        spriteBatch.DrawString(_04BFont, $"Time: {TimeLeft}",
            new Vector2(x, BoardTopLeftY + 57), new Color(129, 23, 27), 0, Vector2.Zero, 0.8f, SpriteEffects.None,
            0.5f);
    }
}