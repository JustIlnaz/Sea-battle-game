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
        Console.WriteLine("Игрок 1, расставьте свои корабли.");
        PlaceShips(player1Board);
        Console.Clear();
        Console.WriteLine("Игрок 2, расставьте свои корабли.");
        PlaceShips(player2Board);
        Console.Clear();

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
            string input = GetValidInput("Введите координаты корабля (например, A1)", ConvertToNumbers);
            string[] coords = input.Split(' ');
            int row = int.Parse(coords[0]);
            int col = int.Parse(coords[1]);

            if (row >= 0 && row < 5 && col >= 0 && col < 5 && board[row, col] == '~')
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
        string input = GetValidInput("Введите координаты выстрела (например, A1)", ConvertToNumbers);
        string[] coords = input.Split(' ');
        int row = int.Parse(coords[0]);
        int col = int.Parse(coords[1]);

        if (row >= 0 && row < 5 && col >= 0 && col < 5)
        {
            if (enemyBoard[row, col] == 'S')
            {
                Console.WriteLine("Попадание!");
                hitsBoard[row, col] = 'X'; // Отметка о попадании
                enemyBoard[row, col] = 'X'; // Уничтожение корабля
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
        else
        {
            Console.WriteLine("Неверные координаты. Попробуйте снова.");
            return false;
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
    static string ConvertToNumbers(string input)
    {
        input = input.ToUpper();
        if (input.Length != 2 || !char.IsLetter(input[0]) || !char.IsDigit(input[1]))
        {
            throw new ArgumentException("Неверный формат координат. Пример: A1");
        }

        char letter = input[0];
        char numberChar = input[1];

        // Преобразование буквы в столбец (0-4)
        int col = letter - 'A';

        // Преобразование цифры в строку (0-4)
        int row = int.Parse(numberChar.ToString()) - 1;

        if (row < 0 || row >= 5 || col < 0 || col >= 5)
        {
            throw new ArgumentException("Координаты выходят за пределы доски.");
        }

        return $"{row} {col}";
    }

    // Функция для безопасного ввода данных
    static string GetValidInput(string prompt, Func<string, string> validator)
    {
        while (true)
        {
            try
            {
                Console.Write(prompt + ": ");
                string input = Console.ReadLine();
                return validator(input); // Попытка преобразовать ввод
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}