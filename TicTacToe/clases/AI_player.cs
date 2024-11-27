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
    private Board board;
    public AiPlayer(Board board)
    {
        this.board = board;
    }
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
    public (int, int) BestMove()
    {
        int bestScore = int.MinValue;
        (int, int) move = (-1, -1);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board.GameBoard[i, j] == ' ')
                {
                    board.GameBoard[i, j] = 'O';
                    int score = board.MinMax(false);
                    board.GameBoard[i, j] = ' ';
                    if (score > bestScore)
                    {
                        bestScore = score;
                        move = (i, j);
                    }
                }
            }
        }
        return move;
    }
}