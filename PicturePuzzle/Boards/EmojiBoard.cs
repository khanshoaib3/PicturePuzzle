using System.Collections.Generic;

namespace PicturePuzzle;

public class EmojiBoard : AbstractBoard
{
    private static readonly List<string> TextureNamesInOrder = new()
    {
        "sprites/emoji_board/block_1",
        "sprites/emoji_board/block_2",
        "sprites/emoji_board/block_3",
        "sprites/emoji_board/block_4",
        "sprites/emoji_board/block_5",
        "sprites/emoji_board/block_6",
        "sprites/emoji_board/block_7",
        "sprites/emoji_board/block_8",
        "null",
    };

    public EmojiBoard(Game1 game1, int totalTimeInSeconds)
        : base(game1, TextureNamesInOrder, "Emoji Board", totalTimeInSeconds)
    { }
}