
namespace Name
{
    class Game
    {
        private State state;
        private State starting_state;
        private ChessBoard board;
        private ChessSetup setup;
        private List<Observer> observers;
        public List<Field> last_move;

        public Game(State _state, ChessSetup _setup)
        {
            starting_state = _state;
            state = _state;
            setup = _setup;
            board = _setup.makeChessBoard();
            last_move = new List<Field>();
            observers = new List<Observer>();
        }

        public void Reset()
        {
            state = starting_state;
            board = setup.makeChessBoard();
            last_move = new List<Field>();
        }

        public void RegisterObserver(Observer observer)
        {
            observers.Add(observer);
        }

        public void NotifyObservers()
        {
            foreach (Observer observer in observers)
            {
                observer.getNotice(board, last_move, state);
            }
        }

        public void ChangeState(State _state)
        {
            state = _state;
        }

        public void run()
        {
            while (true)
            {
                board.printBoard();
                state.HandleMove(this, board);
                if (state is EndProgramState)
                {
                    break;
                }
                NotifyObservers();
            }
        }
    }
}