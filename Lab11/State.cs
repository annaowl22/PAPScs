using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Name
{
    interface State
    {
        public abstract int HandleMove(ChessBoard board);
    }

    public static class GameStates
    {
        public const int White = 0;
        public const int Black = 1;
        public const int End = 2;
        public const int EndProgram = 3;
        public const int Replay = 4;
    }

    class BlackTurnState : State
    {
        public int HandleMove(ChessBoard board)
        {
            List<Field> move;
            bool success = false;
            while (true)
            {
                move = MoveReader.read();
                if (move[0].isEqual(move[1]))
                {
                    return GameStates.End;
                }
                if (board.grid[move[0].Vertical, move[0].Horizontal] != null)
                    {
                        if (board.grid[move[0].Vertical, move[0].Horizontal].color == PieceColor.Black)
                        {
                            success = board.makeUserMove(move[0], move[1]);
                        }
                        else
                        {
                            Console.WriteLine("Нельзя ходить фигурами противника");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Фигура на поле отсутствует");
                    }
                if (success)
                {
                    if (board.isCheck(PieceColor.White))
                    {
                        Console.WriteLine("Шах Королю белых!");
                    }
                    if (board.isCheckMate(PieceColor.White))
                    {
                        Console.WriteLine("Мат Королю белых!");
                        return GameStates.End;
                    }
                    return GameStates.White;
                }
            }
        }
    }
    class WhiteTurnState : State
    {
        public int HandleMove(ChessBoard board)
        {
            List<Field> move;
            bool success = false;
            while (true)
            {
                move = MoveReader.read();
                if (board.grid[move[0].Vertical, move[0].Horizontal] != null)
                {
                    if (board.grid[move[0].Vertical, move[0].Horizontal].color == PieceColor.White)
                    {
                        success = board.makeUserMove(move[0], move[1]);
                    }
                    else
                    {
                        Console.WriteLine("Нельзя ходить фигурами противника");
                    }
                }
                else
                {
                    Console.WriteLine("Фигура на поле отсутствует");
                }
                if (success)
                {
                    if (board.isCheck(PieceColor.Black))
                    {
                        Console.WriteLine("Шах Королю чёрных!");
                    }
                    if (board.isCheckMate(PieceColor.Black))
                    {
                        Console.WriteLine("Мат Королю чёрных!");
                        return GameStates.End;
                    }
                    return GameStates.Black;
                }
            }
        }
    }

    class EndgameState : State
    {
        public int HandleMove(ChessBoard board)
        {
            Console.WriteLine("Игра завершилась, хотите выйти и перезапустить игру с другими условиями или взять реванш?");
            Console.WriteLine("Напишите 0 для выхода или любой другой ввод для реванша");
            string? input = Console.ReadLine();
            while (true)
            {
                if (input == null)
                {
                    Console.WriteLine("Ввод не должен быть пустым");
                }
                else
                {
                    if (input == "0")
                    {
                        return GameStates.EndProgram;
                    }
                    else
                    {
                        return GameStates.Replay;
                    }
                }
                input = Console.ReadLine();
            }
        }
    }
}