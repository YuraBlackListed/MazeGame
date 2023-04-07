using System;

namespace MazeGame
{

    class Program : GameLoop
    {

        private static int width = 30;
        private static int height = 15;

        private static int obsticleFreq = 10;

        private int destinationX = 0;
        private int destinationY = 0;

        private int playerX = width / 2;
        private int playerY = height /2;

        private char[,] characters = new char[width, height];

        private Random random = new Random();

        public char playerChar = 'O';
        public char obsticleChar = '=';
        public char backgroundChar = ' ';
        public char destinationChar = 'B';

        public bool alive = true;
        public bool passed = false;

        protected override void Start()
        {
            Console.WindowWidth = width;
            Console.WindowHeight = height;

            GenerateMaze();

            GenerateLocation(out destinationX, out destinationY);

            GenerateLocation(out playerX, out playerY);

        }
        public override void Update()
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

                    char character;

                    if (chance <= obsticleFreq)
                    {
                        character = obsticleChar;
                        characters[i, j] = character;
                    }
                    else
                    {
                        character = backgroundChar;
                    }

                    characters[i, j] = character;
                }
            }
        }
        public void DrawMaze()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Console.SetCursorPosition(i, j);

                    Console.Write(characters[i, j]);
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

                if (characters[randomX, randomY] == backgroundChar)
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
        public void DrawDestination(char symbol)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.SetCursorPosition(destinationX, destinationY);

            Console.Write(symbol);
        }
        public void DrawPlayer(char symbol)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(playerX, playerY);

            Console.Write(symbol);
        }

        public void CheckPlayerStatus()
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
            if(characters[playerX, playerY] == obsticleChar)
            {
                return true;
            }
            return false;
        }
        private bool PlayerPassed()
        {
            if (playerX == destinationX && playerY == destinationY)
            {
                return true;
            }
            return false;
        }

        public void CheckInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if(playerX != 0)
                    {
                        playerX--;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    
                    if (playerX != width - 1)
                    {
                        playerX++;
                    }
                    break;

                case ConsoleKey.UpArrow:
                    
                    if (playerY != 0)
                    {
                        playerY--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    
                    if (playerY != height - 1)
                    {
                        playerY++;
                    }
                    break;
            }
        }
    }
}
