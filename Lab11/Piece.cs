
using System.Collections.Generic;
namespace Name
{
    public enum PieceColor { White, Black }
    interface Unit
    {
        int getValue();
    }

    class Field
    {
        public int Vertical { get; }
        public int Horizontal { get; }

        public Field(int X, int Y)
        {
            Vertical = X;
            Horizontal = Y;
        }
        public bool IsValid()
        {
            return (Vertical < 8 && Vertical >= 0 && Horizontal < 8 && Horizontal >= 0);
        }
    }

    class Piece : Unit
    {
        public PieceColor color;
        public int value;
        public Field position;
        public string name;
        public string symbol;
        public bool hasMoved;

        public Piece(PieceColor _color, int _value, Field _position, string _name, string _symbol)
        {
            color = _color;
            value = _value;
            position = _position;
            name = _name;
            symbol = _symbol;
            hasMoved = false;
        }

        public virtual List<Field> getPossibleMoves(List<Piece> pieces)
        {
            return new List<Field>();
        }
        public virtual List<Field> getMoves(List<Piece> pieces)
        {
            return new List<Field>();
        }

        public int getValue()
        {
            return value;
        }
    }

    class Pawn : Piece
    {
        public Pawn(PieceColor _color, Field _position) : base(_color, 1, _position, "pawn", "p") { }

        public List<Field> getPossibleMoves(ChessBoard board)
        {
            List<Field> moves = new List<Field>();
            if (color == PieceColor.White)
            {
                if (board.grid[position.Vertical, position.Horizontal + 1] == null)
                {
                    moves.Add(new Field(position.Vertical, position.Horizontal + 1));
                    if (board.grid[position.Vertical, 3] == null && position.Horizontal == 1)
                    {
                        moves.Add(new Field(position.Vertical, 3));
                    }
                }
                Field eatLeft = new Field(position.Vertical - 1, position.Horizontal + 1);
                if (eatLeft.IsValid())
                {
                    if (board.grid[eatLeft.Vertical, eatLeft.Horizontal] != null)
                    {
                        if (board.grid[eatLeft.Vertical, eatLeft.Horizontal].color == PieceColor.Black)
                        {
                            moves.Add(eatLeft);
                        }
                    }
                }
                Field eatRight = new Field(position.Vertical + 1, position.Horizontal + 1);
                if (eatRight.IsValid())
                {
                    if (board.grid[eatRight.Vertical, eatRight.Horizontal] != null)
                    {
                        if (board.grid[eatRight.Vertical, eatRight.Horizontal].color == PieceColor.Black)
                        {
                            moves.Add(eatRight);
                        }
                    }
                }
            }
            else
            {
                if (board.grid[position.Vertical, position.Horizontal - 1] == null)
                {
                    moves.Add(new Field(position.Vertical, position.Horizontal - 1));
                    if (board.grid[position.Vertical, 4] == null && position.Horizontal == 6)
                    {
                        moves.Add(new Field(position.Vertical, 4));
                    }
                }
                Field eatLeft = new Field(position.Vertical - 1, position.Horizontal - 1);
                if (eatLeft.IsValid())
                {
                    if (board.grid[eatLeft.Vertical, eatLeft.Horizontal] != null)
                    {
                        if (board.grid[eatLeft.Vertical, eatLeft.Horizontal].color == PieceColor.White)
                        {
                            moves.Add(eatLeft);
                        }
                    }
                }
                Field eatRight = new Field(position.Vertical + 1, position.Horizontal - 1);
                if (eatRight.IsValid())
                {
                    if (board.grid[eatRight.Vertical, eatRight.Horizontal] != null)
                    {
                        if (board.grid[eatRight.Vertical, eatRight.Horizontal].color == PieceColor.White)
                        {
                            moves.Add(eatRight);
                        }
                    }
                }
            }
            return moves;
        }

        public List<Field> getMoves(ChessBoard board)
        {
            return getPossibleMoves(board);
        }
    }

    class Rook: Piece
    {
        public Rook(PieceColor _color, Field _position) : base(_color, 5, _position, "rook", "R") { }

        public List<Field> getPossibleMoves(ChessBoard board)
        {
            List<Field> moves = new List<Field>();
            for(int i = 1; i < 7; i++){
                Field field = new Field(position.Vertical, position.Horizontal + i)
                if(!field.IsValid){
                    break;
                }
                if(board.grid[field.Vertical, field.Horizontal] != null){
                    if(board.grid[field.Vertical, field.Horizontal].color != color){
                        moves.Add(field);
                    }
                    break;
                }
                moves.Add(field);
            }
            for(int i = 1; i < 7; i++){
                Field field = new Field(position.Vertical, position.Horizontal - i)
                if(!field.IsValid){
                    break;
                }
                if(board.grid[field.Vertical, field.Horizontal] != null){
                    if(board.grid[field.Vertical, field.Horizontal].color != color){
                        moves.Add(field);
                    }
                    break;
                }
                moves.Add(field);
            }
            for(int i = 1; i < 7; i++){
                Field field = new Field(position.Vertical + i, position.Horizontal)
                if(!field.IsValid){
                    break;
                }
                if(board.grid[field.Vertical, field.Horizontal] != null){
                    if(board.grid[field.Vertical, field.Horizontal].color != color){
                        moves.Add(field);
                    }
                    break;
                }
                moves.Add(field);
            }
            for(int i = 1; i < 7; i++){
                Field field = new Field(position.Vertical - i, position.Horizontal)
                if(!field.IsValid){
                    break;
                }
                if(board.grid[field.Vertical, field.Horizontal] != null){
                    if(board.grid[field.Vertical, field.Horizontal].color != color){
                        moves.Add(field);
                    }
                    break;
                }
                moves.Add(field);
            }
        }

        public List<Field> getMoves(ChessBoard board)
        {
            return getPossibleMoves(board);
        }
    }
}