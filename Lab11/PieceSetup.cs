using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Name
{
    interface ChessSetup
    {
        ChessBoard makeChessBoard();
    }

    class ChessStartingPosition
    {

        public ChessBoard makeChessBoard()
        {
            ChessBoard board = new ChessBoard();
            board.grid[0, 0] = new Rook(PieceColor.White, new Field(0, 0));
            board.grid[7, 0] = new Rook(PieceColor.White, new Field(7, 0));
            board.grid[0, 7] = new Rook(PieceColor.Black, new Field(0, 7));
            board.grid[7, 7] = new Rook(PieceColor.Black, new Field(7, 7));
            
            board.grid[1, 0] = new Knight(PieceColor.White, new Field(1, 0));
            board.grid[6, 0] = new Knight(PieceColor.White, new Field(6, 0));
            board.grid[1, 7] = new Knight(PieceColor.Black, new Field(1, 7));
            board.grid[6, 7] = new Knight(PieceColor.Black, new Field(6, 7));
            
            board.grid[2, 0] = new Bishop(PieceColor.White, new Field(2, 0));
            board.grid[5, 0] = new Bishop(PieceColor.White, new Field(5, 0));
            board.grid[2, 7] = new Bishop(PieceColor.Black, new Field(2, 7));
            board.grid[5, 7] = new Bishop(PieceColor.Black, new Field(5, 7));
            
            board.grid[3, 0] = new Queen(PieceColor.White, new Field(3, 0));
            board.grid[4, 0] = new King(PieceColor.White, new Field(4, 0));
            board.grid[3, 7] = new Queen(PieceColor.Black, new Field(3, 7));
            board.grid[4, 7] = new King(PieceColor.Black, new Field(4, 7));

            for (int i = 0; i < 8; i++)
            {    
                board.grid[i, 1] = new Pawn(PieceColor.White, new Field(i, 1));
                board.grid[i, 6] = new Pawn(PieceColor.Black, new Field(i, 6));
            }
            return board;
        }
    }
}