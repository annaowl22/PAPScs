
namespace Name
{
    interface State
    {
        public abstract void HandleMove(Game game);
    }

    class EndProgramState : State
    {
        public void HandleMove(Game game)
        {
            return;
        }
    }

    class BlackTurnState : State
    {
        public void HandleMove(Game game)
        {
            var board = game.board;
            var reader = game.readerBlack;

            List<Field> move;
            bool success = false;
            while (true)
            {
                move = reader.read();
                if (move[0].isEqual(move[1]))
                {
                    game.ChangeState(new EndgameState());
                    break;
                }
                if (board.grid[move[0].Vertical, move[0].Horizontal] != null)
                {
                    if (board.grid[move[0].Vertical, move[0].Horizontal].color == PieceColor.Black)
                    {
                        success = board.makeUserMove(move[0], move[1], reader);
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
                    game.last_move = move;
                    if (board.isCheck(PieceColor.White))
                    {
                        Console.WriteLine("Шах Королю белых!");
                    }
                    if (board.isCheckMate(PieceColor.White))
                    {
                        Console.WriteLine("Мат Королю белых!");
                        game.ChangeState(new EndgameState());
                        break;
                    }
                    game.ChangeState(new WhiteTurnState());
                    break;
                }
            }
        }
    }
    class WhiteTurnState : State
    {
        public void HandleMove(Game game)
        {
            var board = game.board;
            var reader = game.readerWhite;

            List<Field> move;
            bool success = false;
            while (true)
            {
                move = reader.read();
                if (move[0].isEqual(move[1]))
                {
                    game.ChangeState(new EndgameState());
                    break;
                }
                if (board.grid[move[0].Vertical, move[0].Horizontal] != null)
                {
                    if (board.grid[move[0].Vertical, move[0].Horizontal].color == PieceColor.White)
                    {
                        success = board.makeUserMove(move[0], move[1], reader);
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
                    game.last_move = move;
                    if (board.isCheck(PieceColor.Black))
                    {
                        Console.WriteLine("Шах Королю чёрных!");
                    }
                    if (board.isCheckMate(PieceColor.Black))
                    {
                        Console.WriteLine("Мат Королю чёрных!");
                        game.ChangeState(new EndgameState());
                        break;
                    }
                    game.ChangeState(new BlackTurnState());
                    break;
                }
            }
        }
    }

    class EndgameState : State
    {
        public void HandleMove(Game game)
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
                        game.ChangeState(new EndProgramState());
                        break;
                    }
                    else
                    {
                        game.Reset();
                        break;
                    }
                }
                input = Console.ReadLine();
            }
        }
    }
}