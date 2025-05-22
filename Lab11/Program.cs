using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;
namespace Name
{
    class Program()
    {
        static void Main(string[] args)
        {
            Pawn pawn1 = new Pawn(PieceColor.White, new Field(2, 2));
            Pawn pawn2 = new Pawn(PieceColor.Black, new Field(2, 3));
            Pawn pawn3 = new Pawn(PieceColor.White, new Field(3, 3));
            Pawn pawn4 = new Pawn(PieceColor.Black, new Field(1, 3));
            Rook rook1 = new Rook(PieceColor.White, new Field(1, 2));
            ChessBoard board = new ChessBoard();
            board.grid[2, 2] = pawn1;
            board.grid[2, 3] = pawn2;
            board.grid[3, 3] = pawn3;
            board.grid[1, 3] = pawn4;
            board.grid[1, 2] = rook1;
            List<Field> moves = rook1.getMoves(board);
            board.printBoard();
            foreach(Field move in moves) {
                Console.WriteLine(move.Vertical.ToString() + ' ' + move.Horizontal.ToString() + '\n');
            }
        }
    }
}