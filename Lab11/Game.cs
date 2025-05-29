
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

        public void run()
        {
            bool flag = true;
            int state_id;
            while(flag)
            {
                state_id = state.HandleMove(board);
                if(state_id == GameStates.White)
                {
                    state = new WhiteTurnState();
                }
                else if(state_id == GameStates.Black)
                {
                    state = new BlackTurnState();
                }
                else if(state_id == GameStates.End)
                {
                    state = new EndgameState();
                }
                else if(state_id == GameStates.Replay)
                {
                    state = starting_state;
                    board = setup.makeChessBoard();
                }else
                {
                    flag = false;
                }
            }
        }
    }
}