﻿using System;
using System.Threading;

namespace MazeGame
{
    class GameLoop
    {
        static void Main(string[] args)
        {
            
            Program program = new Program();
            program.Run();

            GameLoop game = new GameLoop();
            game.Run();
            
        }

        bool running = true;

        protected virtual void Start()
        {

        }

        public virtual void Update()
        {
            
        }

        protected virtual void Render()
        {

            Thread.Sleep(100);
            //Eye pain warning!!
            
        }

        void Stop()
        {
            running = false;
        }

        public void Run()
        {
            Start();
            while (running)
            {
                Update();
                Render();
            }
        }

        
    }
}