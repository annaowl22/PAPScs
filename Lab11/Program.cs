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
            ChessStartingPosition setup = new ChessStartingPosition();
            ChessBoard board = setup.makeChessBoard();
            board.printBoard();
            MoveReader reader = new MoveReader();
            List<Field> move = MoveReader.read();
            board.makeUserMove(move[0], move[1]);
            board.printBoard();
            move = MoveReader.read();
            board.makeUserMove(move[0], move[1]);
            board.printBoard();
            move = MoveReader.read();
            board.makeUserMove(move[0], move[1]);
            board.printBoard();
            move = MoveReader.read();
            board.makeUserMove(move[0],move[1]);
            board.printBoard();
        }
    }
}