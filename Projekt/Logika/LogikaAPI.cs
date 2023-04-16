using System;
using System.Collections.Generic;
using System.Timers;
using Dane;

namespace Logika.API
{
    public abstract class LogikaAPI
    {
        public List<Ball> balls;
        public System.Timers.Timer timer;
        public int boardWidth;
        public int boardHeight;
        public Random random;

        public static LogikaAPI CreateApi(int boardWidth, int boardHeight)
        {
            return new BallAPIBase(boardWidth, boardHeight);
        }

        public abstract void CreateBall();
        public abstract void OnTimerTick(object sender, ElapsedEventArgs e);
        public abstract void Start();
        public abstract void Stop();
       
    }

    internal class BallAPIBase : LogikaAPI
    {
        private DaneAPI daneAPI;
        public BallAPIBase(int boardWidth, int boardHeight) 
        {
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
            balls = new List<Ball>();
            random = new Random();
            daneAPI = DaneAPI.CreateApi();
            timer = new System.Timers.Timer(1000 / 60); // 60 FPS
            timer.Elapsed += OnTimerTick;
        }

        public override void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            foreach (var ball in balls)
            {
                ball.Move();
                ball.CheckCollisionWithBoard(boardWidth, boardHeight);
            }
        }

        public override void CreateBall()
        {
            int x = random.Next(11, boardWidth - 11);
            int y = random.Next(11, boardHeight - 11);

            int Vx = 0;
            int Vy = 0;

            while (Vx == 0 || Vy == 0)
            {
                Vx = random.Next(-2, 2);
                Vy = random.Next(-2, 2);
            }
          
            int radius = 5;
            balls.Add(new Ball(x, y, Vx, Vy, radius));
           
        }

        public override void Start()
        {
            timer.Start();
        }

        public override void Stop()
        {
            timer.Stop();
        }
    }
}
