using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;

namespace TicTacToe;

public class AiPlayer
{
    private Random random = new Random();
    public (int, int) GetMove(Board board)
    {
        var possible_moves = new List<(int, int)>();
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (board.GameBoard[row, col] == ' ')
                {
                    possible_moves.Add((row, col));
                }
            }
        }
        if (possible_moves.Count > 0)
        {
            return possible_moves[random.Next(possible_moves.Count)];
        }

        return (-1, -1);
    }
}