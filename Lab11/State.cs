using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Name
{
    interface State
    {
        public abstract bool HandleMove(Board board);
    }

    class BlackTurnState: State
    {
        public bool HandleMove(Board board)
        {
            List<Field> move;
            bool success = false;
            while(true)
            {
                move = MoveReader.read();
                if(board.grid[move[0].Vertical, move[0].Horizontal]!=null)
                {
                    if(board.grid[move[0].Vertical, move[0].Horizontal].color == PieceColor.Black)
                    {
                        success = board.makeUserMove();
                    }else
                    {
                        Console.WriteLine("Нельзя ходить фигурами противника");
                    }
                }else
                {
                    Console.WriteLine("Фигура на поле отсутствует");
                }
                if(success){
                    if(board.isCheck(PieceColor.White))
                    {
                        Console.WriteLine("Шах Королю белых!");
                    }
                    if(board.isCheckMate(PieceColor.White))
                    {
                        Console.WriteLine("Мат Королю белых!");
                        return false;
                    }
                    return true;
                }
            }
        }
    }

    // class WhiteTurnState: State
    // {

    // }