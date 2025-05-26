using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Name
{
    interface State
    {
        public abstract void HandleMove(Game game);
    }

    class BlackTurnState: State
    {
        public void HandleMove(Game game)
        {
            List<Field> move;
            bool success = false;
            while(true)
            {
                move = MoveReader.read();
                if(game.board.grid[move[0].Vertical, move[0].Horizontal]!=null)
                {
                    if(game.board.grid[move[0].Vertical, move[0].Horizontal].color == PieceColor.Black)
                    {
                        success = game.board.makeUserMove();
                    }else
                    {
                        Console.WriteLine("Нельзя ходить фигурами противника");
                    }
                }else
                {
                    Console.WriteLine("Фигура на поле отсутствует");
                }
                if(success){
                    if(game.board.isCheck(PieceColor.White))
                    {
                        Console.WriteLine("Шах Королю белых!");
                    }
                    if(game.board.isCheckMate(PieceColor.White))
                    {
                        Console.WriteLine("Мат Королю белых!");
                        return false;
                    }
                    return true;
                }
            }
        }
    }

    class WhiteTurnState: State
    {

    }

    class WhiteUserTurnState: State
    {
        public void HandleMove(Game game)
        {

        }
    }

    class BlackUserTurnState: State
    {
        public void HandleMove(Game game)
        {

        }
    }

    class IIWhiteTurnState: State
    {
        public void HandleMove(Game game)
        {

        }
    }

    class IIBlackTurnState: State
    {
        public void HandleMove(Game game)
        {

        }
    }
}