using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TicTacToe;

public class Board
{
    public char[,] board = new char[3, 3];
    public char currentPlayer;
    public Board()
    {
        currentPlayer = 'X';
        CreateBoard();
    }

    public void CreateBoard()
    {
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                //Console.WriteLine("I chek it");
                board[row, col] = ' ';
            }
        }
    }

    public bool IsFilled()
    {
        foreach (char item in board)
        {
            if (item == ' ') return false;
        }
        return true;
    }

    public bool MakeMove(int row, int col)
    {
        if (board[row, col] == ' ')
        {
            board[row, col] = currentPlayer;
            return true;
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j]);
            }
            Console.WriteLine();
        }
        return false;
    }

    public bool IsWinner(char player)
    {
        // Перевірка рядків
        for (int row = 0; row < 3; row++)
        {
            if (board[row, 0] == board[row, 1] && board[row, 0] == board[row, 2] && player == board[row, 0])
                return true;
        }

        // Перевірка колонок
        for (int col = 0; col < 3; col++)
        {
            if (board[0, col] == board[1, col] && board[0, col] == board[2, col] && player == board[0, col])
                return true;
        }

        // Перевірка діагоналей
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
            return true;
        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            return true;

        return false;
    }


    public char Current_player
    {
        get { return currentPlayer; }
        set { currentPlayer = value; }
    }

    public char[,] GameBoard
    {
        get { return board; }
        set { board = value; }
    }
}