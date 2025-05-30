using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;

namespace Name{
    

    class ChessBoard : Unit
    {
        static int[,] centerControl = {
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 2, 3, 3, 2, 1, 0 },
            { 0, 2, 4, 6, 6, 4, 2, 0 },
            { 0, 3, 6, 8, 8, 6, 3, 0 },
            { 0, 3, 6, 8, 8, 6, 3, 0 },
            { 0, 2, 4, 6, 6, 4, 2, 0 },
            { 0, 1, 2, 3, 3, 2, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 }
        };
        public Piece?[,] grid;

        public ChessBoard()
        {
            grid = new Piece?[8, 8];
        }

        public Piece getKing(PieceColor color)
        {
            List<Piece> my_pieces = getColorPieces(color);
            foreach (Piece piece in my_pieces)
            {
                if (piece.name == "king")
                {
                    return piece;
                }
            }
            return new Pawn(PieceColor.White, new Field(0, 0));
        }

        public bool isCheck(PieceColor color)
        {
            Piece king = getKing(color);
            if(king is Pawn){
                return false;
            }
            List<Piece> enemies;
            if (color == PieceColor.White)
            {
                enemies = getColorPieces(PieceColor.Black);
            }
            else
            {
                enemies = getColorPieces(PieceColor.White);
            }
            foreach (Piece enemy in enemies)
            {
                if (king.position.isIn(enemy.getPossibleMoves(this)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isCheckMate(PieceColor color)
        {
            List <Piece> pieces = getColorPieces(color);
            if (isCheck(color))
            {
                foreach (Piece piece in pieces)
                {
                    if (piece.getMoves(this).Count != 0)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public List<Piece> getColorPieces(PieceColor color)
        {
            List<Piece> pieces = new List<Piece>();
            for (int v = 0; v < 8; v++)
            {
                for (int h = 0; h < 8; h++)
                {
                    if (grid[v, h] != null)
                    {
                        if (grid[v, h].color == color)
                        {
                            pieces.Add(grid[v, h]);
                        }
                    }
                }
            }
            return pieces;
        }


        public int getValue()
        {
            //Pieces
            int value = 0;
            List<Piece> white_pieces = getColorPieces(PieceColor.White);
            List<Piece> black_pieces = getColorPieces(PieceColor.Black);
            foreach (Piece piece in white_pieces)
            {
                value += piece.getValue();
            }
            foreach (Piece piece in black_pieces)
            {
                value -= piece.getValue();
            }
            value *= 100;

            //Centre
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (grid[i, j] != null)
                    {
                        if (grid[i, j].color == PieceColor.White)
                        {
                            value += centerControl[i, j];
                        }
                        else
                        {
                            value -= centerControl[i, j];
                        }
                    }
                }
            }
            //Moves
            foreach (Piece piece in white_pieces)
            {
                value += piece.getMoves(this).Count;
            }
            foreach (Piece piece in black_pieces)
            {
                value -= piece.getMoves(this).Count;
            }

            return value;
        }
        public void printBoard()
        {
            Console.WriteLine("  ---------------------------------");
            for (int h = 7; h > -1; h--)
            {
                Console.Write(h + 1);
                Console.Write(" |");
                for (int v = 0; v < 8; v++)
                {

                    if (grid[v, h] != null)
                    {
                        if (grid[v, h].color == PieceColor.White)
                        {
                            Console.Write(" " + grid[v, h].symbol + " |");
                        }
                        else
                        {
                            Console.Write(" ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(grid[v, h].symbol);
                            Console.ResetColor();
                            Console.Write(" |");
                        }
                    }
                    else
                    {
                        Console.Write("   |");
                    }
                }
                Console.Write("\n");
                Console.WriteLine("  ---------------------------------");
            }
            Console.WriteLine("    a   b   c   d   e   f   g   h");
        }
        private void removePiece(Field field)
        {
            grid[field.Vertical, field.Horizontal] = null;
        }

        public void AddPiece(Piece piece)
        {
            int x = piece.position.Vertical; 
            int y = piece.position.Horizontal; 

            grid[x, y] = piece;
        }

        public bool makeUserMove(Field a, Field b)
        {
            if (grid[a.Vertical, a.Horizontal] == null)
            {
                Console.WriteLine("Нет фигуры!");
                return false;
            }
            Piece piece = grid[a.Vertical, a.Horizontal];
            List<Field> moves = piece.getMoves(this);
            if (!b.isIn(moves))
            {
                Console.WriteLine("Невозможный ход!");
                return false;
            }
            piece.position = b;
            ChessBoard new_board = DeepCopy();
            new_board.AddPiece(piece);
            new_board.removePiece(a);
            if(new_board.isCheck(piece.color)){
                Console.WriteLine("Собственный король не должен оказаться под шахом");
                return false;
            }
            piece.hasMoved = true;
            AddPiece(piece);
            removePiece(a);
            if (piece.name == "king")
            {
                if (b.Vertical - a.Vertical == 2)
                {
                    AddPiece(new Rook(piece.color, new Field(5, a.Horizontal)));
                    removePiece(new Field(7, a.Horizontal));
                }
                if (b.Vertical - a.Vertical == -3)
                {
                    AddPiece(new Rook(piece.color, new Field(3, a.Horizontal)));
                    removePiece(new Field(0, a.Horizontal));
                }
            }else if(piece.name == "pawn")
            {
                if(piece.color==PieceColor.White && b.Horizontal==7 || piece.color==PieceColor.Black && b.Horizontal==0)
                {
                    grid[b.Vertical, b.Horizontal] = MoveReader.pawnTurn(piece.color, b);
                }
            }
            return true;
        }
        public bool makeMove(Field a, Field b)
        {
            if (grid[a.Vertical, a.Horizontal] == null)
            {
                return false;
            }
            Piece piece = grid[a.Vertical, a.Horizontal];
            List<Field> moves = piece.getPossibleMoves(this);
            if (!b.isIn(moves))
            {
                return false;
            }
            piece.position = b;
            ChessBoard new_board = DeepCopy();
            new_board.AddPiece(piece);
            new_board.removePiece(a);
            if (new_board.isCheck(piece.color))
            {
                return false;
            }
            piece.hasMoved = true;
            AddPiece(piece);
            removePiece(a);
            if (piece.name == "king")
            {
                if (b.Vertical - a.Vertical == 2)
                {
                    AddPiece(new Rook(piece.color, new Field(5, a.Horizontal)));
                    removePiece(new Field(7, a.Horizontal));
                }
                if (b.Vertical - a.Vertical == -3)
                {
                    AddPiece(new Rook(piece.color, new Field(3, a.Horizontal)));
                    removePiece(new Field(0, a.Horizontal));
                }
            }
            return true;
        }
        public ChessBoard DeepCopy()
        {
            ChessBoard copy = new ChessBoard();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (grid[x, y] != null)
                    {
                        copy.grid[x, y] = grid[x, y].Copy(); // Копируем фигуру
                    }
                }
            }
            return copy;
        }
    }
}