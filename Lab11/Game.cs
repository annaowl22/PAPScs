
namespace Name
{
    class Game
    {
        public State state { get; private set; }
        private State starting_state;
        public ChessBoard board { get; private set; }
        private ChessSetup setup;
        private List<Observer> observers;
        public List<Field> last_move;
        public IMoveGetter readerWhite;
        public IMoveGetter readerBlack;

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
                state.HandleMove(this);
                if (state is EndProgramState)
                {
                    break;
                }
                NotifyObservers();
            }
        }
    }
}