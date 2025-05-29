namespace Name
{
    interface Observer
    {
        public void getNotice(ChessBoard board, List<Field> move, State state);
    }

    class Notator : Observer
    {
        List<string> notation;

        public Notator()
        {
            notation = new List<string>();
        }
        public void Reset()
        {
            notation = new List<string>();
        }

        public void getNotice(ChessBoard board, List<Field> move, State state)
        {
            if (state is EndgameState)
            {
                Console.WriteLine("Запись шахматной партии");
                for (int i = 0; i < notation.Count(); i++)
                {
                    Console.WriteLine((i + 1).ToString() + ". " + notation[i]);
                }
                return;
            }
            if (move.Count == 0)
            {
                Reset();
                return;
            }
            string this_move = "";
            this_move += board.grid[move[1].Vertical, move[1].Horizontal].symbol;
            this_move += (move[0].Vertical + 'a');
            this_move += (move[0].Horizontal + 1).ToString();
            this_move += "-";
            this_move += (move[1].Vertical + 'a');
            this_move += (move[1].Horizontal + 1).ToString();
            if (board.grid[move[1].Vertical, move[1].Horizontal].color == PieceColor.White && board.isCheck(PieceColor.Black) |
            board.grid[move[1].Vertical, move[1].Horizontal].color == PieceColor.Black && board.isCheck(PieceColor.White))
            {
                this_move += "+";
            }
            if (board.grid[move[1].Vertical, move[1].Horizontal].color == PieceColor.White)
            {
                notation.Add(this_move);
            }
            else
            {
                notation[notation.Count-1] += " " + this_move;
            }
        }
    }
}