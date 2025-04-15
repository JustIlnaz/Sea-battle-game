using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
     

class SeaBattle
    {
        static char[,] player1Board = new char[5, 5]; // Доска игрока 1
        static char[,] player2Board = new char[5, 5]; // Доска игрока 2

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
                PrintBoards();

                // Ход игрока
                if (player1Turn)
                {
                    Console.WriteLine("\nХод игрока 1:");
                    MakeMove(player2Board);
                    if (CheckWin(player2Board))
                    {
                        Console.WriteLine("Игрок 1 победил!");
                        gameOver = true;
                    }
                }
                else
                {
                    Console.WriteLine("\nХод игрока 2:");
                    MakeMove(player1Board);
                    if (CheckWin(player1Board))
                    {
                        Console.WriteLine("Игрок 2 победил!");
                        gameOver = true;
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
                    player1Board[i, j] = '~';
                    player2Board[i, j] = '~';
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
                string[] coords = input.Split(' ');
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

        static void MakeMove(char[,] board)
        {
            Console.Write("Введите координаты выстрела (строка столбец): ");
            string input = Console.ReadLine();
            string[] coords = input.Split(' ');
            int row = int.Parse(coords[0]);
            int col = int.Parse(coords[1]);

            if (board[row, col] == 'S')
            {
                Console.WriteLine("Попадание!");
                board[row, col] = 'X';
            }
            else if (board[row, col] == '~')
            {
                Console.WriteLine("Промах!");
                board[row, col] = 'O';
            }
            else
            {
                Console.WriteLine("Вы уже стреляли сюда. Попробуйте снова.");
            }
        }

        static void PrintBoards()
        {
            Console.WriteLine("\nДоска игрока 1:");
            PrintBoard(player1Board);

            Console.WriteLine("\nДоска игрока 2:");
            PrintBoard(player2Board);
        }

        static void PrintBoard(char[,] board)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static bool CheckWin(char[,] board)
        {
            foreach (char cell in board)
            {
                if (cell == 'S')
                    return false;
            }
            return true;
        }
    }
}
}
