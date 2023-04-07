using System;

namespace MazeGame
{
    class Program : GameLoop
    {
        private enum Direction
        {
            up,
            down,
            left,
            right,
        }

        private static int width = 30;
        private static int height = 15;

        private static int obsticleFreq = 10;

        private int destinationX = 0;
        private int destinationY = 0;

        private int playerX = width / 2;
        private int playerY = height /2;

        private bool[,] characters = new bool[width, height];

        private Random random = new Random();

        private char playerChar = 'O';
        private char obsticleChar = '=';
        private char backgroundChar = ' ';
        private char destinationChar = 'B';

        private bool alive = true;
        private bool passed = false;

        protected override void Start()
        {
            Console.WindowWidth = width;
            Console.WindowHeight = height;

            GenerateMaze();

            GenerateLocation(out destinationX, out destinationY);

            GenerateLocation(out playerX, out playerY);

        }
        protected override void Update()
        {
            base.Update();
            CheckInput();
            CheckPlayerStatus();
        }

        protected override void Render()
        {
            DrawMaze();
            DrawPlayer(playerChar);
            DrawDestination(destinationChar);

            if (!alive)
            {
                Console.Clear();
                Console.WindowWidth = 60;
                Console.WindowHeight = 20;

                Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2);

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Kikril has beaten your ass");

                Console.SetCursorPosition(Console.WindowWidth / 2 - 2, Console.WindowHeight / 2 + 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You died");
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (passed)
            {
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);

                Console.WindowWidth = 60;
                Console.WindowHeight = 20;

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("You escaped Kikril homeworks");
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
        private void GenerateMaze()
        {
            for(int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int chance = random.Next(0, 100);

                    if (chance <= obsticleFreq)
                    {
                        characters[i, j] = true;
                    }
                    else
                    {
                        characters[i, j] = false;
                    }

                    
                }
            }
        }
        private void DrawMaze()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Console.SetCursorPosition(i, j);
                    if(characters[i, j])
                    {
                        Console.Write(obsticleChar);
                    }
                    else
                    {
                        Console.Write(backgroundChar);
                    }
                }
            }
        }

        private void GenerateLocation(out int x, out int y)
        {
            while(true)
            {
                Random random = new Random();

                int randomX = random.Next(0, width);
                int randomY = random.Next(0, height);

                if (characters[randomX, randomY] == false)
                {
                    x = randomX;
                    y = randomY;
                    break;
                }
            }
        }

/*        public (int, int) GenerateDestination()
        {
            while (true)
            {
                int destinationX = random.Next(0, width - 1);
                int destinationY = random.Next(0, height - 1);

                if (characters[destinationX, destinationY] == ' ')
                {
                    return (destinationX, destinationY);
                }
            }
        }

        (int destinationX, int destinationY) = GenerateDestination();*/
        private void DrawDestination(char symbol)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(destinationX, destinationY);

            Console.Write(symbol);
        }
        private void DrawPlayer(char symbol)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(playerX, playerY);

            Console.Write(symbol);
        }

        private void CheckPlayerStatus()
        {
            if(PlayerPassed())
            {
                passed = true;
            }
            if(PlayerDead())
            {
                alive = false;
            }
        }
        private bool PlayerDead()
        {
             return characters[playerX, playerY] == true;
        }
        private bool PlayerPassed()
        {
            return playerX == destinationX && playerY == destinationY;
        }
        private void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.up:
                    if (playerY != 0)
                    {
                        playerY--;
                    }
                    break;

                case Direction.down:
                    if (playerY != height - 1)
                    {
                        playerY++;
                    }
                    break;

                case Direction.left:
                    if (playerX != 0)
                    {
                        playerX--;
                    }
                    break;

                case Direction.right:
                    if (playerX != width - 1)
                    {
                        playerX++;
                    }
                    break;

            }
        }
        private void CheckInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    Move(Direction.up);
                    break;

                case ConsoleKey.DownArrow:
                    Move(Direction.down);
                    break;

                case ConsoleKey.LeftArrow:
                    Move(Direction.left);
                    break;

                case ConsoleKey.RightArrow:
                    Move(Direction.right);
                    break;

            }
        }
        
    }
}
