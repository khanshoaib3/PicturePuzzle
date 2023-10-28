using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PicturePuzzle.Content;

namespace PicturePuzzle;

public class BlockManager
{
    public List<Block> Blocks = new();

    public static Dictionary<string, Texture2D> BlockTextures = new();

    public static void LoadTextures(GraphicsDevice graphicsDevice)
    {
         string filePath;
         if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
             filePath = "/home/towk/Projects/PicturePuzzle/PicturePuzzle/Content/sprites/";
         else
             filePath = "\\Users\\yourUserName\\OneDrive\\Documents\\GitHub\\PicturePuzzle\\PicturePuzzle\\Content\\sprites\\";
         
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
             FileStream fileStream = new FileStream(filePath + name + ".png", FileMode.Open);
             BlockTextures.Add(name, Texture2D.FromStream(graphicsDevice, fileStream));
             fileStream.Dispose();
         }
    }

    public void LoadBlocks()
    {
        Block[,] blockArray =
        {
            {
                new Block("block_1", new Vector2(0, 0)),
                new Block("block_2", new Vector2(120, 0)),
                new Block("block_3", new Vector2(240, 0)),
            },
            {
                new Block("block_4", new Vector2(0, 120)),
                new Block("block_5", new Vector2(120, 120)),
                new Block("block_6", new Vector2(240, 120)),
            },
            {
                new Block("block_7", new Vector2(0, 240)),
                new Block("block_8", new Vector2(120, 240)),
                new Block("block_9", new Vector2(240, 240)),
            },
            {
                null,
                new Block("null", new Vector2(120, 360)),
                null,
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

                if (i == 2 && j == 1)
                    blockArray[i, j].down = blockArray[3, 1];
                
                Blocks.Add(blockArray[i, j]);
            }
        }

        blockArray[3, 1].up = blockArray[2, 1];
        blockArray[3, 1].right = null;
        blockArray[3, 1].down = null;
        blockArray[3, 1].left = null;

        Blocks.Add(blockArray[3, 1]);
    }

    public void DrawAllBlocks(SpriteBatch spriteBatch)
    {
        Blocks.ForEach(block => block.Draw(spriteBatch));
    }
}