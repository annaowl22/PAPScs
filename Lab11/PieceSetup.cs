
namespace Name
{
    interface ChessSetup
    {
        ChessBoard makeChessBoard();
    }

    class ChessStartingPosition : ChessSetup
    {
        public ChessBoard makeChessBoard()
        {
            ChessBoard board = new ChessBoard();

            board.AddPiece(new Rook(PieceColor.White, new Field(0, 0)));
            board.AddPiece(new Rook(PieceColor.White, new Field(7, 0)));
            board.AddPiece(new Knight(PieceColor.White, new Field(1, 0)));
            board.AddPiece(new Knight(PieceColor.White, new Field(6, 0)));
            board.AddPiece(new Bishop(PieceColor.White, new Field(2, 0)));
            board.AddPiece(new Bishop(PieceColor.White, new Field(5, 0)));
            board.AddPiece(new Queen(PieceColor.White, new Field(3, 0)));
            board.AddPiece(new King(PieceColor.White, new Field(4, 0)));

            board.AddPiece(new Rook(PieceColor.Black, new Field(0, 7)));
            board.AddPiece(new Rook(PieceColor.Black, new Field(7, 7)));
            board.AddPiece(new Knight(PieceColor.Black, new Field(1, 7)));
            board.AddPiece(new Knight(PieceColor.Black, new Field(6, 7)));
            board.AddPiece(new Bishop(PieceColor.Black, new Field(2, 7)));
            board.AddPiece(new Bishop(PieceColor.Black, new Field(5, 7)));
            board.AddPiece(new Queen(PieceColor.Black, new Field(3, 7)));
            board.AddPiece(new King(PieceColor.Black, new Field(4, 7)));

            for (int i = 0; i < 8; i++)
            {
                board.AddPiece(new Pawn(PieceColor.White, new Field(i, 1)));
                board.AddPiece(new Pawn(PieceColor.Black, new Field(i, 6)));
            }

            return board;
        }
    }

    class ChessPawnTurnPosition : ChessSetup
    {
        public ChessBoard makeChessBoard()
        {
            ChessBoard board = new ChessBoard();

            board.AddPiece(new Pawn(PieceColor.White, new Field(0, 5)));
            board.AddPiece(new King(PieceColor.White, new Field(4, 0)));
            board.AddPiece(new King(PieceColor.Black, new Field(4, 7)));

            return board;
        }
    }

    class MateInOneTurnWhiteStart : ChessSetup
    {
        public ChessBoard makeChessBoard()
        {
            ChessBoard board = new ChessBoard();

            board.AddPiece(new King(PieceColor.White, new Field(2, 0)));
            board.AddPiece(new Rook(PieceColor.White, new Field(3, 0)));
            board.AddPiece(new Rook(PieceColor.White, new Field(7, 0)));
            board.AddPiece(new Bishop(PieceColor.White, new Field(2, 3)));
            board.AddPiece(new Knight(PieceColor.White, new Field(4, 6)));

            board.AddPiece(new Pawn(PieceColor.White, new Field(0, 1)));
            board.AddPiece(new Pawn(PieceColor.White, new Field(1, 1)));
            board.AddPiece(new Pawn(PieceColor.White, new Field(4, 2)));
            board.AddPiece(new Pawn(PieceColor.White, new Field(5, 1)));
            board.AddPiece(new Pawn(PieceColor.White, new Field(6, 1)));
            board.AddPiece(new Pawn(PieceColor.White, new Field(6, 4)));

            board.AddPiece(new Queen(PieceColor.Black, new Field(2, 5)));
            board.AddPiece(new King(PieceColor.Black, new Field(5, 7)));
            board.AddPiece(new Rook(PieceColor.Black, new Field(0, 7)));
            board.AddPiece(new Rook(PieceColor.Black, new Field(4, 7)));
            board.AddPiece(new Bishop(PieceColor.Black, new Field(2, 7)));
            board.AddPiece(new Bishop(PieceColor.Black, new Field(6, 6)));
            board.AddPiece(new Knight(PieceColor.Black, new Field(3, 6)));

            board.AddPiece(new Pawn(PieceColor.Black, new Field(0, 4)));
            board.AddPiece(new Pawn(PieceColor.Black, new Field(3, 3)));
            board.AddPiece(new Pawn(PieceColor.Black, new Field(3, 5)));
            board.AddPiece(new Pawn(PieceColor.Black, new Field(6, 5)));

            return board;
        }
    }

    class MateInTwoTurnsWhiteStart : ChessSetup
    {
        public ChessBoard makeChessBoard()
        {
            ChessBoard board = new ChessBoard();

            board.AddPiece(new Rook(PieceColor.White, new Field(0, 0)));  
            board.AddPiece(new Knight(PieceColor.White, new Field(1, 0)));  
            board.AddPiece(new Bishop(PieceColor.White, new Field(2, 0)));  
            board.AddPiece(new Queen(PieceColor.White, new Field(3, 0)));  
            board.AddPiece(new Rook(PieceColor.White, new Field(4, 0)));  
            board.AddPiece(new Knight(PieceColor.White, new Field(5, 2)));  
            board.AddPiece(new King(PieceColor.White, new Field(6, 0)));  

            board.AddPiece(new Pawn(PieceColor.White, new Field(0, 1)));  
            board.AddPiece(new Pawn(PieceColor.White, new Field(1, 1)));  
            board.AddPiece(new Pawn(PieceColor.White, new Field(2, 1)));  
            board.AddPiece(new Pawn(PieceColor.White, new Field(4, 1)));  
            board.AddPiece(new Pawn(PieceColor.White, new Field(5, 1)));  
            board.AddPiece(new Pawn(PieceColor.White, new Field(6, 1)));  
            board.AddPiece(new Pawn(PieceColor.White, new Field(7, 1)));  

            board.AddPiece(new Rook(PieceColor.Black, new Field(0, 7)));  
            board.AddPiece(new Knight(PieceColor.Black, new Field(1, 7)));  
            board.AddPiece(new Queen(PieceColor.Black, new Field(1, 3)));  
            board.AddPiece(new King(PieceColor.Black, new Field(4, 7)));  
            board.AddPiece(new Bishop(PieceColor.Black, new Field(5, 7)));  
            board.AddPiece(new Knight(PieceColor.Black, new Field(6, 7)));  
            board.AddPiece(new Rook(PieceColor.Black, new Field(7, 7)));  
            board.AddPiece(new Bishop(PieceColor.Black, new Field(5, 4)));  

            board.AddPiece(new Pawn(PieceColor.Black, new Field(0, 6)));  
            board.AddPiece(new Pawn(PieceColor.Black, new Field(1, 6)));  
            board.AddPiece(new Pawn(PieceColor.Black, new Field(2, 6)));  
            board.AddPiece(new Pawn(PieceColor.Black, new Field(3, 4)));  
            board.AddPiece(new Pawn(PieceColor.Black, new Field(5, 5)));  
            board.AddPiece(new Pawn(PieceColor.Black, new Field(6, 6)));  
            board.AddPiece(new Pawn(PieceColor.Black, new Field(7, 6)));  

            return board;
        }
    }

    class MateInThreeTurnsWhiteStart : ChessSetup
    {
        public ChessBoard makeChessBoard()
        {
            ChessBoard board = new ChessBoard();

            board.AddPiece(new King(PieceColor.White, new Field(0, 0)));
            board.AddPiece(new Queen(PieceColor.White, new Field(3, 1)));
            board.AddPiece(new Bishop(PieceColor.White, new Field(3, 7)));
            board.AddPiece(new Rook(PieceColor.White, new Field(7, 6)));

            board.AddPiece(new King(PieceColor.Black, new Field(2, 5)));
            board.AddPiece(new Bishop(PieceColor.Black, new Field(4, 5)));
            board.AddPiece(new Knight(PieceColor.Black, new Field(5, 4)));
            board.AddPiece(new Queen(PieceColor.Black, new Field(6, 1)));
            board.AddPiece(new Rook(PieceColor.Black, new Field(7, 1)));

            board.AddPiece(new Pawn(PieceColor.Black, new Field(0, 5)));
            board.AddPiece(new Pawn(PieceColor.Black, new Field(1, 4)));

            return board;
        }
    }

    class MateInTwoTurnsWhiteStartSmall : ChessSetup
    {
        public ChessBoard makeChessBoard()
        {
            ChessBoard board = new ChessBoard();

            board.AddPiece(new King(PieceColor.White, new Field(5, 7)));
            board.AddPiece(new Queen(PieceColor.White, new Field(4, 7)));
            board.AddPiece(new Rook(PieceColor.White, new Field(4, 6)));

            board.AddPiece(new King(PieceColor.Black, new Field(7, 7)));
            board.AddPiece(new Knight(PieceColor.Black, new Field(5, 6)));

            return board;
        }
    }
}