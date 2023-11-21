using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PicturePuzzle;

public class SimpleBoard
{
    private static Texture2D _boardBackgroundTexture;

    private readonly List<Block> _blocks = new();
    private readonly Dictionary<string, Texture2D> _blockTextures = new();
    private readonly Dictionary<Texture2D, string> _blockTexturesReversed = new();
    private readonly List<string> _textureNamesInOrder;


    private int _controlledBlockIndex = 4;
    private readonly Game1 _game1;
    private const int BoardTopLeftX = 225;
    private const int BoardTopLeftY = 75;


    public SimpleBoard(Game1 game1)
    {
        _game1 = game1;
        _textureNamesInOrder = new List<string>()
        {
            "sprites/block_1",
            "sprites/block_2",
            "sprites/block_3",
            "sprites/block_4",
            "sprites/block_5",
            "sprites/block_6",
            "sprites/block_7",
            "sprites/block_8",
            "null",
        };
        LoadTextures(_game1.Content);
        LoadBlocks();
    }

    private void LoadTextures(ContentManager content)
    {
        foreach (var name in _textureNamesInOrder)
        {
            if (name == "null") continue;
            var texture2D = content.Load<Texture2D>(name);
            _blockTextures.Add(name, texture2D);
            _blockTexturesReversed.Add(texture2D, name);
        }

        _boardBackgroundTexture = content.Load<Texture2D>("sprites/BoardBackground");
    }

    private void LoadBlocks()
    {
        Block[,] blockArray =
        {
            {
                new Block(BoardTopLeftX + 0, BoardTopLeftY + 0),
                new Block(BoardTopLeftX + 120, BoardTopLeftY + 0),
                new Block(BoardTopLeftX + 240, BoardTopLeftY + 0),
            },
            {
                new Block(BoardTopLeftX + 0, BoardTopLeftY + 120),
                new Block(BoardTopLeftX + 120, BoardTopLeftY + 120),
                new Block(BoardTopLeftX + 240, BoardTopLeftY + 120),
            },
            {
                new Block(BoardTopLeftX + 0, BoardTopLeftY + 240),
                new Block(BoardTopLeftX + 120, BoardTopLeftY + 240),
                new Block(BoardTopLeftX + 240, BoardTopLeftY + 240),
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
        List<string> source = new();
        source.Add("null");
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

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_boardBackgroundTexture, new Vector2(BoardTopLeftX - 10, BoardTopLeftY - 10), Color.White);
        _blocks.ForEach(block => block.Draw(spriteBatch));
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
        }
    }

    public void HandleRightMovement()
    {
        Block controlledBlock = _blocks[_controlledBlockIndex];
        if (controlledBlock.Right != null)
        {
            controlledBlock.SwapTextures(controlledBlock.Right);
            _controlledBlockIndex = _blocks.IndexOf(controlledBlock.Right);
        }
    }

    public void HandleDownMovement()
    {
        Block controlledBlock = _blocks[_controlledBlockIndex];
        if (controlledBlock.Down != null)
        {
            controlledBlock.SwapTextures(controlledBlock.Down);
            _controlledBlockIndex = _blocks.IndexOf(controlledBlock.Down);
        }
    }

    public void HandleLeftMovement()
    {
        Block controlledBlock = _blocks[_controlledBlockIndex];
        if (controlledBlock.Left != null)
        {
            controlledBlock.SwapTextures(controlledBlock.Left);
            _controlledBlockIndex = _blocks.IndexOf(controlledBlock.Left);
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
}