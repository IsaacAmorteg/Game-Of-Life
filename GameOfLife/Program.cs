using System;

class GameOfLife
{
    static char[,] field;
    static int fieldWidth;
    static int fieldHeight;

    static void Main(string[] args)
    {
        // Set up the initial state of the field
        fieldWidth = 9;
        fieldHeight = 5;
        field = new char[fieldWidth, fieldHeight];

        GenerateRandomInitState();

        PrintField();

        while (true)
        {
            if (!UpdateField())
            {
                Console.WriteLine("Game over");
                Console.ReadKey();
                break;
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to continue to the next generation.");
            Console.ReadKey();
            Console.Clear();
            PrintField();
        }
    }

    static bool UpdateField()
    {
        bool hasChanged = false;
        char[,] nextField = new char[fieldWidth, fieldHeight];

        for (int x = 0; x < fieldWidth; x++)
        {
            for (int y = 0; y < fieldHeight; y++)
            {
                int neighbors = CountNeighbors(x, y);

                if (field[x, y] == '+' && (neighbors < 2 || neighbors > 3))
                {
                    nextField[x, y] = '-';
                    hasChanged = true;
                }
                else if (field[x, y] == '-' && neighbors == 3)
                {
                    nextField[x, y] = '+';
                    hasChanged = true;
                }
                else
                {
                    nextField[x, y] = field[x, y];
                }
            }
        }

        field = nextField;
        return hasChanged;
    }

    static int CountNeighbors(int x, int y)
    {
        int count = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;

                int neighborX = x + i;
                int neighborY = y + j;

                if (neighborX < 0) neighborX = fieldWidth - 1;
                if (neighborX >= fieldWidth) neighborX = 0;

                if (neighborY < 0) neighborY = fieldHeight - 1;
                if (neighborY >= fieldHeight) neighborY = 0;

                if (field[neighborX, neighborY] == '+')
                {
                    count++;
                }
            }
        }

        return count;
    }

    static void PrintField()
    {
        for (int y = fieldHeight; y > 0; y--)
        {
            Console.Write(y + " ");
            for (int x = 1; x <= fieldWidth; x++)
            {
                Console.Write(field[x - 1, y - 1] + " ");
            }
            Console.WriteLine();
        }

        Console.Write("0 ");
        for (int x = 1; x <= fieldWidth; x++)
        {
            Console.Write(x % 10 + " ");
        }
        Console.WriteLine();
    }
         
    static void GenerateRandomInitState()
    {
        Random randomState = new Random();
        for (int y = 0; y < fieldHeight; y++)
        {
            for (int x = 0; x < fieldWidth; x++)
            {
                field[x, y] = (randomState.Next(2) == 0) ? '-' : '+';
            }
        }
    }

}
