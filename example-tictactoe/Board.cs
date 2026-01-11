namespace examples.Phases;

public class Board
{
    private int[,] _board = new int[3, 3];

    public Board()
    {
        foreach (var row in Enumerable.Range(0, 3))
        {
            foreach (var col in Enumerable.Range(0, 3))
            {
                _board[row, col] = 0;
            }
        }
    }

    public int GetCell(int row, int col)
    {
        row = row - 1;
        col = col - 1;
        if (row < 0 || row >= 3 || col < 0 || col >= 3)
        {
            throw new ArgumentOutOfRangeException();
        }
        return _board[row, col];
    }
    public void SetCell(int row, int col, int value)
    {
        row = row - 1;
        col = col - 1;
        if (row < 0 || row >= 3 || col < 0 || col >= 3)
        {
            throw new ArgumentOutOfRangeException();
        }

        if (value != Constants.ValueEmpty && value != Constants.ValuePlayer1 && value != Constants.ValuePlayer2)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        _board[row, col] = value;
    }

    public void Draw()
    {
        foreach (var row in Enumerable.Range(0, 3))
        {
            var line = "";
            foreach (var col in Enumerable.Range(0, 3))
            {
                switch (_board[row, col])
                {
                    case Constants.ValuePlayer1:
                        line += " O ";
                        break;
                    case Constants.ValuePlayer2:
                        line += " X ";
                        break;
                    case Constants.ValueEmpty:
                        line += "   ";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (col < 2)
                {
                    line += "||";
                }
            }
            Console.WriteLine(line);
            if (row < 2)
            {
                Console.WriteLine("=============");    
            }
            
        }
    }
    
}