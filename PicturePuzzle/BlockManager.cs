using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PicturePuzzle.Content;

namespace PicturePuzzle;

public class BlockManager
{
    public List<Block> Blocks = new();
    public static Dictionary<string, Texture2D> BlockTextures = new();

    private int _controlledBlockIndex = 4;

    public static void LoadTextures(ContentManager content)
    {
        List<string> textureNames = new List<string>()
        {
            "block_background",
            "block_1",
            "block_2",
            "block_3",
            "block_4",
            "block_5",
            "block_6",
            "block_7",
            "block_8",
            "block_9",
        };

        foreach (var name in textureNames)
        {
            BlockTextures.Add(name, content.Load<Texture2D>($"sprites/{name}"));
        }
    }

    public void LoadBlocks()
    {
        int boardTopLeftX = 225;
        int boardTopLeftY = 75;
        
        Block[,] blockArray =
        {
            {
                new Block("block_1", new Vector2(boardTopLeftX + 0, boardTopLeftY + 0)),
                new Block("block_2", new Vector2(boardTopLeftX + 120, boardTopLeftY + 0)),
                new Block("block_3", new Vector2(boardTopLeftX + 240, boardTopLeftY + 0)),
            },
            {
                new Block("block_4", new Vector2(boardTopLeftX + 0, boardTopLeftY + 120)),
                new Block("null", new Vector2(boardTopLeftX + 120, boardTopLeftY + 120)),
                new Block("block_5", new Vector2(boardTopLeftX + 240, boardTopLeftY + 120)),
            },
            {
                new Block("block_6", new Vector2(boardTopLeftX + 0, boardTopLeftY + 240)),
                new Block("block_7", new Vector2(boardTopLeftX + 120, boardTopLeftY + 240)),
                new Block("block_8", new Vector2(boardTopLeftX + 240, boardTopLeftY + 240)),
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

                blockArray[i, j].up = (up == -1) ? null : blockArray[up, j];
                blockArray[i, j].right = (right == -1) ? null : blockArray[i, right];
                blockArray[i, j].down = (down == -1) ? null : blockArray[down, j];
                blockArray[i, j].left = (left == -1) ? null : blockArray[i, left];

                Blocks.Add(blockArray[i, j]);
            }
        }

        List<string> randomisedTextures = RandomiseTextures();
        for (int i = 0; i < 9; i++)
        {
            Blocks[i].SetTextureName(randomisedTextures[i]);
            if (randomisedTextures[i] == "null")
                _controlledBlockIndex = i;
        }
    }

    private List<string> RandomiseTextures()
    {
        List<string> source = new()
        {
            "null",
            "block_1",
            "block_2",
            "block_3",
            "block_4",
            "block_5",
            "block_6",
            "block_7",
            "block_8",
        };
        List<string> randomTextures = new();
        Random random = new Random();
        List<int> acquiredIndexes = new();

        for(int i = 0; i < 9; i++)
        {
            int r = (int)random.NextInt64(0, 9);
            while (acquiredIndexes.Contains(r))
            {
                r = (int)random.NextInt64(0, 9);
            }
            
            randomTextures.Add(source[r]);
            acquiredIndexes.Add(r);
                Console.WriteLine(r.ToString());
        }

        return randomTextures;
    }

    public void DrawAllBlocks(SpriteBatch spriteBatch)
    {
        Blocks.ForEach(block => block.Draw(spriteBatch));
    }

    public bool KeyPressed(Keys keyboardKey)
    {
        switch (keyboardKey)
        {
            case Keys.Up:
                HandleUpMovement();
                return true;
            case Keys.Right:
                HandleRightMovement();
                return true;
            case Keys.Down:
                HandleDownMovement();
                return true;
            case Keys.Left:
                HandleLeftMovement();
                return true;
            default:
                return false;
        }
    }

    public void HandleUpMovement()
    {
        Block controlledBlock = Blocks[_controlledBlockIndex];
        if (controlledBlock.up != null)
        {
            controlledBlock.SwapTextures(controlledBlock.up);
            _controlledBlockIndex = Blocks.IndexOf(controlledBlock.up);
        }
    }

    public void HandleRightMovement()
    {
        Block controlledBlock = Blocks[_controlledBlockIndex];
        if (controlledBlock.right != null)
        {
            controlledBlock.SwapTextures(controlledBlock.right);
            _controlledBlockIndex = Blocks.IndexOf(controlledBlock.right);
        }
    }

    public void HandleDownMovement()
    {
        Block controlledBlock = Blocks[_controlledBlockIndex];
        if (controlledBlock.down != null)
        {
            controlledBlock.SwapTextures(controlledBlock.down);
            _controlledBlockIndex = Blocks.IndexOf(controlledBlock.down);
        }
    }

    public void HandleLeftMovement()
    {
        Block controlledBlock = Blocks[_controlledBlockIndex];
        if (controlledBlock.left != null)
        {
            controlledBlock.SwapTextures(controlledBlock.left);
            _controlledBlockIndex = Blocks.IndexOf(controlledBlock.left);
        }
    }

    public bool IsArranged()
    {
        List<string> correctFormat = new List<string>()
        {
            "block_1",
            "block_2",
            "block_3",
            "block_4",
            "block_5",
            "block_6",
            "block_7",
            "block_8",
            "null",
        };

        for (var i = 0; i < Blocks.Count; i++)
        {
            if (Blocks[i].GetTextureName() != correctFormat[i])
            {
                return false;
            }
        }

        return true;
    }
}