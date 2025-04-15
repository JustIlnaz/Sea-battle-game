using System;

class SeaBattle
{
    static char[,] player1Board = new char[5, 5]; // Доска с кораблями игрока 1
    static char[,] player1Hits = new char[5, 5]; // Доска с результатами выстрелов игрока 1
    static char[,] player2Board = new char[5, 5]; // Доска с кораблями игрока 2
    static char[,] player2Hits = new char[5, 5]; // Доска с результатами выстрелов игрока 2

    static void Main()
    {
        Console.WriteLine("Добро пожаловать в игру 'Морской бой'!");

        // Инициализация досок
        InitializeBoards();

        // Расстановка кораблей
        PlaceShips(player1Board);
        PlaceShips(player2Board);

        bool player1Turn = true;
        bool gameOver = false;

        while (!gameOver)
        {
            // Отрисовка досок
            PrintBoards(player1Hits, player2Hits);

            // Ход игрока
            if (player1Turn)
            {
                Console.WriteLine("\nХод игрока 1:");
                if (MakeMove(player2Board, player2Hits))
                {
                    // Проверка на победу
                    if (CheckAllShipsDestroyed(player2Board))
                    {
                        Console.WriteLine("Игрок 1 победил!");
                        gameOver = true;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nХод игрока 2:");
                if (MakeMove(player1Board, player1Hits))
                {
                    // Проверка на победу
                    if (CheckAllShipsDestroyed(player1Board))
                    {
                        Console.WriteLine("Игрок 2 победил!");
                        gameOver = true;
                    }
                }
            }

            player1Turn = !player1Turn; // Смена хода
        }
    }

    static void InitializeBoards()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                player1Board[i, j] = '~'; // Незанятые клетки
                player1Hits[i, j] = '~';  // Результаты выстрелов игрока 1
                player2Board[i, j] = '~'; // Незанятые клетки
                player2Hits[i, j] = '~';  // Результаты выстрелов игрока 2
            }
        }
    }

    static void PlaceShips(char[,] board)
    {
        Console.WriteLine("\nРасставьте свои корабли (3 штуки).");
        for (int i = 0; i < 3; i++)
        {
            Console.Write($"Введите координаты корабля {i + 1} (строка столбец): ");
            string input = Console.ReadLine();
            string[] coords = ConvertToNumbers(input); // Преобразование в числа
            int row = int.Parse(coords[0]);
            int col = int.Parse(coords[1]);

            if (board[row, col] == '~')
            {
                board[row, col] = 'S';
            }
            else
            {
                Console.WriteLine("Неверная координата или место занято. Попробуйте снова.");
                i--;
            }
        }
    }

    static bool MakeMove(char[,] enemyBoard, char[,] hitsBoard)
    {
        Console.Write("Введите координаты выстрела (строка столбец): ");
        string input = Console.ReadLine();
        string[] coords = ConvertToNumbers(input); // Преобразование в числа
        int row = int.Parse(coords[0]);
        int col = int.Parse(coords[1]);

        if (enemyBoard[row, col] == 'S')
        {
            Console.WriteLine("Попадание!");
            hitsBoard[row, col] = 'X'; // Отметка о попадании
            return true; // Вернуть true, если попали
        }
        else if (enemyBoard[row, col] == '~')
        {
            Console.WriteLine("Промах!");
            hitsBoard[row, col] = 'O'; // Отметка о промахе
            return false; // Вернуть false, если промазали
        }
        else
        {
            Console.WriteLine("Вы уже стреляли сюда. Попробуйте снова.");
            return false; // Вернуть false, если повторный выстрел
        }
    }

    static void PrintBoards(char[,] player1Hits, char[,] player2Hits)
    {
        Console.WriteLine("\nДоска игрока 1:");
        PrintBoard(player1Hits);

        Console.WriteLine("\nДоска игрока 2:");
        PrintBoard(player2Hits);
    }

    static void PrintBoard(char[,] board)
    {
        // Вывод заголовков столбцов
        Console.Write("  ");
        for (int j = 0; j < 5; j++)
        {
            Console.Write((char)('A' + j) + " "); // Буквенная метка
        }
        Console.WriteLine();

        for (int i = 0; i < 5; i++)
        {
            Console.Write(i + 1 + " "); // Номер строки
            for (int j = 0; j < 5; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static bool CheckAllShipsDestroyed(char[,] board)
    {
        foreach (char cell in board)
        {
            if (cell == 'S')
                return false;
        }
        return true;
    }

    // Функция преобразования букв в цифры
    static string[] ConvertToNumbers(string input)
    {
        string[] coords = input.ToUpper().Split(' ');
        if (coords.Length != 2)
        {
            throw new ArgumentException("Неверный формат координат. Например: A1");
        }

        // Преобразование буквы в число
        int row = int.Parse(coords[0]); // Строка остается числом
        int col = coords[1][0] - 'A'; // Преобразование буквы в число

        return new string[] { row.ToString(), col.ToString() };
    }
}