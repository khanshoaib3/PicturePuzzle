using System.Collections.Generic;

namespace PicturePuzzle;

public class BatmanBoard : AbstractBoard
{
    private static readonly List<string> TextureNamesInOrder = new()
    {
        "sprites/batman_board/batman_0_0",
        "sprites/batman_board/batman_0_1",
        "sprites/batman_board/batman_0_2",
        "sprites/batman_board/batman_0_3",
        "sprites/batman_board/batman_1_0",
        "sprites/batman_board/batman_1_1",
        "sprites/batman_board/batman_1_2",
        "sprites/batman_board/batman_1_3",
        "sprites/batman_board/batman_2_0",
        "sprites/batman_board/batman_2_1",
        "sprites/batman_board/batman_2_2",
        "sprites/batman_board/batman_2_3",
        "sprites/batman_board/batman_3_0",
        "sprites/batman_board/batman_3_1",
        "sprites/batman_board/batman_3_2",
        // "sprites/batman_board/batman_3_3",
        "null",
    };

    public BatmanBoard(Game1 game1, int totalTimeInSeconds)
        : base(game1, TextureNamesInOrder, "Batman Board", totalTimeInSeconds, gridSize: 4)
    { }
}