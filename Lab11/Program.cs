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
            ChessSetup setup = new ChessPawnTurnPosition();
            ChessBoard board = setup.makeChessBoard();
            board.printBoard();
            Console.WriteLine(board.getValue().ToString());
            MoveReader reader = new MoveReader();
            List<Field> move = MoveReader.read();
            board.makeUserMove(move[0], move[1]);
            if (board.isCheckMate(PieceColor.White))
            {
                Console.WriteLine("Mate");
            }
            else
            {
                Console.WriteLine("The show must go on");
            }
            board.printBoard();
            Console.WriteLine(board.getValue().ToString());
            move = MoveReader.read();
            board.makeUserMove(move[0], move[1]);
            board.printBoard();
            Console.WriteLine(board.getValue().ToString());
            move = MoveReader.read();
            board.makeUserMove(move[0], move[1]);
            board.printBoard();
            Console.WriteLine(board.getValue().ToString());
            move = MoveReader.read();
            board.makeUserMove(move[0], move[1]);
            if (board.isCheckMate(PieceColor.Black))
            {
                Console.WriteLine("Mate");
            }
            else
            {
                Console.WriteLine("The show must go on");
            }
            board.printBoard();
            Console.WriteLine(board.getValue().ToString());
        }
    }
}