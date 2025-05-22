using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;
using Piece;
using Chess;


class Program()
{
    static void Main(string[] args)
    {
        Pawn pawn1 = new Pawn(White, new Field(2,2));
        Pawn pawn2 = new Pawn(Black, new Field(2,3));
        Pawn pawn3 = new Pawn(White, new Field(3,3));
        Pawn pawn4 = new Pawn(Black, new Field(1,3));
        ChessBoard board = new ChessBoard();
        board.grid[2,2] = pawn1;
        board.grid[2,3] = pawn2;
        board.grid[3,3] = pawn3;
        board.grid[1,3] = pawn4;
        List<Field> moves = pawn1.getMoves(board);
        for(Field move in moves){
            Console.WriteLine(move.Vertical.toString()+' '+move.Horizontal.toString()+'\n');
        }
    }
}
