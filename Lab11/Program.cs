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

        }
    }
}