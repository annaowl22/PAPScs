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

        public Piece getKing(PieceColor color)
        {
            List<Piece> my_pieces;
            if(color==PieceColor.Black){
                my_pieces = getBlackPieces();
            }else{
                my_pieces = getWhitePieces();
            }
            foreach(Piece piece in my_pieces){
                if(piece.name == 'king'){
                    return piece;
                }
            }
            return Pawn(PieceColor.White, new Field(0,0));
        }

        public bool isCheck(PieceColor color)
        {
            Piece king = getKing(color);
            List<Piece> enemies;
            if(color==PieceColor.White){
                enemies = getBlackPieces();
            }else{
                enemies = getWhitePieces();
            }
            foreach(Piece enemy in enemies){
                if(king.position.isIn(enemy.getPossibleMoves(this))){
                    return true;
                }
            }
            return false;
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