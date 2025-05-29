
namespace Name
{
    class Game
    {
        private State state;
        private State starting_state;
        private ChessBoard board;
        private ChessSetup setup;

        public Game(State _state, ChessSetup _setup)
        {
            starting_state = _state;
            state = _state;
            setup = _setup;
            board = _setup.makeChessBoard();
        }

        public void Reset()
        {
            state = starting_state;
            board = setup.makeChessBoard();
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
            }
        }
    }
}