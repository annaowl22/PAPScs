using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;

namespace Chess{

    class ChessBoard: Unit
    {
        public Piece[,] grid;

        public ChessBoard()
        {
            grid = new Piece[8, 8];
        }

        public List<Piece> getWhitePieces()
        {
            List<Piece> white_pieces = new List<Piece>();
            for(int v = 0; v < 8; v++){
                for(int h = 0; h < 8; h++){
                    if(grid[v,h] != null){
                        if(grid[v,h].color == White){
                            white_pieces.add(grid[v,h]);
                        }
                    }
                }
            }
            return white_pieces;
        }

        public List<Piece> getBlackPieces()
        {
            List<Piece> black_pieces = new List<Piece>();
            for(int v = 0; v < 8; v++){
                for(int h = 0; h < 8; h++){
                    if(grid[v,h] != null){
                        if(grid[v,h].color == Black){
                            black_pieces.add(grid[v,h]);
                        }
                    }
                }
            }
            return black_pieces;
        }
    }
}