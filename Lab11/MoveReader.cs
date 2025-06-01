using ChessEngine.Engine;
using System.Text.RegularExpressions;

namespace Name
{
    interface IMoveGetter
    {
        List<Field> read();
        Piece pawnTurn(PieceColor color, Field position);
    }
    class MoveConsoleReader : IMoveGetter
    {
        private static readonly Regex MovePattern = new Regex(@"^([a-h])([1-8])-([a-h])([1-8])$", 
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex GoalPattern = new Regex(@"^Г[О]+Л$",RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex LosePattern = new Regex(@"^Сдаюсь$",RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex CheckPattern = new Regex(@"^Анализ$",RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public List<Field> read()
        {
            List<Field> move = new List<Field>();
            Console.WriteLine("Введите ход в формате e2-e4 или признайте поражение словом СДАЮСЬ");
            string? input;
            while (true)
            {
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ход не введён. Пожалуйста, повторите ввод корректно");
                    continue;
                }
                var match = MovePattern.Match(input.Trim());
                var goal = GoalPattern.Match(input.Trim());
                var check = CheckPattern.Match(input.Trim());
                var lose = LosePattern.Match(input.Trim());
                if (goal.Success)
                {
                    Console.WriteLine("Ты дурак? Мы в шахматы играем");
                }
                else if (check.Success)
                {
                    Console.WriteLine("Запрошен анализ ситуации");
                }
                else if (lose.Success)
                {
                    Console.WriteLine("Игрок признал поражение");
                    move.Add(new Field(0,0));
                    move.Add(new Field(0,0));
                    return move;
                }
                else if (!match.Success)
                {
                    Console.WriteLine("Неверный формат ввода. Правильный формат: e2-e4");
                }
                else
                {
                    Field fieldFrom = new Field(char.ToLower(input[0]) - 'a', input[1] - '1');
                    Field fieldTo = new Field(char.ToLower(input[3]) - 'a', input[4] - '1');
                    move.Add(fieldFrom);
                    move.Add(fieldTo);
                    return move;
                }
            }
        }
        public Piece pawnTurn(PieceColor color, Field position)
        {
            Console.WriteLine("Введите символ фигуры, в которую хотите превратить пешку");
            string? input;
            while(true)
            {
                input = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Ввод не должен быть пустым, выберите фигуру");
                }else if(input == "K")
                {
                    Console.WriteLine("Пешка не может превратиться в Короля");
                }else if(input == "p")
                {
                    Console.WriteLine("Пешка не может остаться Пешкой");
                }else if(input == "Q")
                {
                    Console.WriteLine("Пешка превращается в Ферзя");
                    return new Queen(color,position);
                }else if(input == "R")
                {
                    Console.WriteLine("Пешка превращается в Ладью");
                    return new Rook(color,position);
                }else if(input == "N")
                {
                    Console.WriteLine("Пешка превращается в Коня");
                    return new Knight(color,position);
                }else if(input == "B")
                {
                    Console.WriteLine("Пешка превращается в Слона");
                    return new Bishop(color,position);
                }else 
                {
                    Console.WriteLine("Некорректный ввод");
                }
            }
        }
    }

    class CoreChessEngineAdapter : IMoveGetter
    {
        private readonly Game game;
        private string input = "";
        private Engine.Difficulty difficulty;

        public CoreChessEngineAdapter(Game game)
        {
            this.game = game;

            //🥴

            Console.WriteLine("""
            Сложность ИИ (насколько долго зависать?):
            Можно числом от 0 или строкой, регистр не важен
            """);

            Console.WriteLine(string.Join(", ", Enum.GetNames(typeof(Engine.Difficulty))));

            while (
                !Enum.TryParse(Console.ReadLine(), true, out difficulty)
                ||
                !Enum.IsDefined(typeof(Engine.Difficulty), difficulty)
            )
            {
                Console.WriteLine("Неверный ввод");
            }

            Console.WriteLine($"Сложность {difficulty}");
        }

        public List<Field> read()
        {
            string fen = "";

            for (int h = 8; h-- > 0; )
            {
                int spaces = 0;
                for (int v = 0; v < 8; v++) {
                    Piece? piece = game.board.grid[v, h];
                    if (piece == null)
                        spaces++;
                    else {
                        if (spaces > 0)
                            fen += spaces;
                        spaces = 0;

                        string c = piece.symbol;
                        c = (piece.color == PieceColor.White) ? c.ToUpper() : c.ToLower();
                        
                        fen += c;
                    }
                }

                if (spaces > 0)
                    fen += spaces;

                if (h > 0) fen += '/';
            }

            fen += ' ';

            fen += (game.state is WhiteTurnState) ? 'w' : 'b';
            fen += ' ';

            string castling = "";

            bool whiteRookMoved = true;
            bool blackRookMoved = true;

            foreach (var item in game.board.grid) {
                if (item == null) continue;

                if (item.name != "rook") continue;

                if (item.color == PieceColor.White)
                    whiteRookMoved = item.hasMoved;
                else
                    blackRookMoved = item.hasMoved;
            }

            if (!game.board.getKing(PieceColor.White).hasMoved && !whiteRookMoved)
                castling += "KQ";


            if (!game.board.getKing(PieceColor.Black).hasMoved && !blackRookMoved)
                castling += "kq";


            fen += (castling == "") ? "-" : castling; //рокировка //castling
            fen += ' ';

            fen += "- "; //en passant //взятое на переходе

            fen += "0 "; //Halfmove clock //Счётчик полуходов

            fen += "1"; //Fullmove number //Номер хода

            Console.WriteLine(fen);
            //Console.ReadLine();

            var engine = new Engine(fen);
            engine.GameDifficulty = difficulty;
            engine.AiPonderMove();
            MoveContent lastMove = engine.GetMoveHistory().ToArray()[0];
            string move = lastMove.GetPureCoordinateNotation();

            if (move.Length > 4)
                input = move.Substring(5);
            else
                input = "";

            Console.WriteLine(move);
            Console.WriteLine(input);
            //Console.ReadLine();

            return new List<Field>
            {
                new(move[0]-'a', move[1]-'1'),
                new(move[2]-'a', move[3]-'1')
            };
        }

        public Piece pawnTurn(PieceColor color, Field position)
        {
            switch (input) {
                case "Q":
                    Console.WriteLine("Пешка превращается в Ферзя");
                    return new Queen(color, position);
                case "R":
                    Console.WriteLine("Пешка превращается в Ладью");
                    return new Rook(color, position);
                case "N":
                    Console.WriteLine("Пешка превращается в Коня");
                    return new Knight(color, position);
                case "B":
                    Console.WriteLine("Пешка превращается в Слона");
                    return new Bishop(color, position);
                default:
                    throw new Exception($"Неверный ход ИИ: пешка не может стать {input}");
            }
        }
    }
}