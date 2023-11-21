using System.Collections.Generic;

namespace PicturePuzzle;

public class SimpleBoard : AbstractBoard
{
    private static readonly List<string> TextureNamesInOrder = new()
    {
        "sprites/simple_board/block_1",
        "sprites/simple_board/block_2",
        "sprites/simple_board/block_3",
        "sprites/simple_board/block_4",
        "sprites/simple_board/block_5",
        "sprites/simple_board/block_6",
        "sprites/simple_board/block_7",
        "sprites/simple_board/block_8",
        "null",
    };

    public SimpleBoard(Game1 game1, int totalTimeInSeconds)
        : base(game1, TextureNamesInOrder, "Simple Board", totalTimeInSeconds)
    { }
}