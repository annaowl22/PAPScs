namespace Name
{
    class Program()
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в игру!");
            string? input;
            ChessSetup setup;
            State state;
            Game game;
            while(true)
            {
                Console.WriteLine("""
                0: выход
                1: настройка игры
                Любой другой ввод: сыграть классическую партию
                """);

                input = Console.ReadLine();
                if(input == "0")
                {
                    break;
                }
                else if(input == "1")
                {
                    Console.WriteLine("""
                    Выберите шахматную ситуацию:
                    1: Пешка белых проходит и превращается.
                    2: Мат в один ход - белые ходят.
                    3: Мат в два хода - белые ходят.
                    4: Мат в три хода - белые ходят.
                    5: Мат в два хода - черные ходят.
                    Любой другой ввод: Вернуться назад
                    """);

                    switch (Console.ReadLine())
                    {
                        case "1":
                            setup = new ChessPawnTurnPosition();
                            state = new WhiteTurnState();
                            break;
                        case "2":
                            setup = new MateInOneTurnWhiteStart();
                            state = new WhiteTurnState();
                            break;
                        case "3":
                            setup = new MateInTwoTurnsWhiteStart();
                            state = new WhiteTurnState();
                            break;
                        case "4":
                            setup = new MateInThreeTurnsWhiteStart();
                            state = new WhiteTurnState();
                            break;
                        case "5":
                            setup = new MateInTwoTurnsWhiteStartSmall();
                            state = new BlackTurnState();
                            break;
                        default:
                            continue;
                    }
                }
                else
                {
                    setup = new ChessStartingPosition();
                    state = new WhiteTurnState();
                }
                game = new Game(state, setup);
                game.RegisterObserver(new Notator());
                Console.WriteLine("Подключить оценивание позиции? Введите что-нибудь, если да, иначе пропустите ввод");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    game.RegisterObserver(new Onexbet());
                }

                Console.WriteLine("""
                Хотите ли вы, чтобы ИИ управлял шахматами?
                1: ИИ ходит чёрными
                2: ИИ ходит белыми
                Любой другой ввод: ИИ нет
                """);

                switch (Console.ReadLine())
                {
                    case "1":
                        game.readerBlack = new CoreChessEngineAdapter(game);
                        game.readerWhite = new MoveConsoleReader();
                        break;
                    case "2":
                        game.readerBlack = new MoveConsoleReader();
                        game.readerWhite = new CoreChessEngineAdapter(game);
                        break;
                    default:
                        game.readerBlack = //new MoveConsoleReader(); //апофиг
                        game.readerWhite = new MoveConsoleReader();
                        break;
                }

                game.run();
            }
            Console.WriteLine("До следующей сессии!");
        }
    }
}