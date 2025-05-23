using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;

namespace Name{

    class ChessBoard : Unit
    {
        public Piece?[,] grid;

        public ChessBoard()
        {
            grid = new Piece?[8, 8];
        }

        public List<Piece> getWhitePieces()
        {
            List<Piece> white_pieces = new List<Piece>();
            for (int v = 0; v < 8; v++)
            {
                for (int h = 0; h < 8; h++)
                {
                    if (grid[v, h] != null)
                    {
                        if (grid[v, h].color == PieceColor.White)
                        {
                            white_pieces.Add(grid[v, h]);
                        }
                    }
                }
            }
            return white_pieces;
        }

        public List<Piece> getBlackPieces()
        {
            List<Piece> black_pieces = new List<Piece>();
            for (int v = 0; v < 8; v++)
            {
                for (int h = 0; h < 8; h++)
                {
                    if (grid[v, h] != null)
                    {
                        if (grid[v, h].color == PieceColor.Black)
                        {
                            black_pieces.Add(grid[v, h]);
                        }
                    }
                }
            }
            return black_pieces;
        }

        public int getValue()
        {
            int value = 0;
            List<Piece> white_pieces = getWhitePieces();
            List<Piece> black_pieces = getBlackPieces();
            foreach (Piece piece in white_pieces)
            {
                value += piece.getValue();
            }
            foreach (Piece piece in black_pieces)
            {
                value -= piece.getValue();
            }
            return value;
        }
        public void printBoard()
        {
            Console.WriteLine("---------------------------------");
            for(int h = 7; h > -1; h--){
                Console.Write("|");
                for(int v = 0; v < 8; v++){
                    if (grid[v, h] != null)
                    {
                        if(grid[v, h].color == PieceColor.White)
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
                Console.WriteLine("---------------------------------");
            }
        }
        private void removePiece(Field field)
        {
            grid[field.Vertical, field.Horizontal] = null;
        }

        public bool makeMove(Field a, Field b)
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
            grid[b.Vertical, b.Horizontal] = piece;
            removePiece(a);
            return true;
        }
    }
}