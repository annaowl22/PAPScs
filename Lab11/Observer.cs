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
            if(board.grid[move[1].Vertical, move[1].Horizontal].name == "king" && move[0].Vertical==move[1].Vertical - 2)
            {
                this_move = "0-0";
            }
            else if(board.grid[move[1].Vertical, move[1].Horizontal].name == "king" && move[0].Vertical==move[1].Vertical + 2)
            {
                this_move = "0-0-0";
            }
            else
            {
                this_move += board.grid[move[1].Vertical, move[1].Horizontal].symbol;
                this_move += (char)('a' + move[0].Vertical);
                this_move += move[0].Horizontal + 1;
                this_move += "-";
                this_move += (char)('a' + move[1].Vertical);
                this_move += move[1].Horizontal + 1;
            }
            if (board.isCheck(PieceColor.Black)|board.isCheck(PieceColor.White))
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