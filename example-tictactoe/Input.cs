using examples.Phases;

namespace examples;

public class Input(Board board)
{
    public Tuple<int,int>GetInput()
    {
        while (true)
        {
            var row = _getNumberBetween13("Select a row (1-3):");
            var col = _getNumberBetween13("Select a col (1-3):");
            var value = board.GetCell(row, col);
            if (value == Constants.ValueEmpty)
            {
                return new  Tuple<int, int>(row, col);
            }
            else
            {
                Console.WriteLine("ERROR! choose an empty cell");
            }
        }
    }

    private int _getNumberBetween13(string message)
    {
        while (true)
        {
            Console.WriteLine(message);
            Console.Out.Flush();
            var line = Console.ReadLine();
            if (int.TryParse(line, out var number))
            {
                if (number > 0 && number <= 3)
                {
                    return number;
                }
            }
            Console.WriteLine("ERROR! valor invalido");
        }
    }
}