using System;
using System.Collections.Generic;
using System.Timers;
using Dane;

namespace Logika.API
{
    public abstract class LogikaAPI
    {
        public DaneAPI daneAPI;
        public List<Ball> balls;
        public System.Timers.Timer timer;
        public int boardWidth;
        public int boardHeight;
        public Random random;

        public LogikaAPI(DaneAPI daneAPI, int boardWidth, int boardHeight)
        {
            this.daneAPI = daneAPI;
            this.boardWidth = boardWidth;
            this.boardHeight = boardHeight;
            balls = new List<Ball>();
            random = new Random();

            timer = new System.Timers.Timer(1000 / 60); // 60 FPS
            timer.Elapsed += OnTimerTick;
        }

        public static LogikaAPI CreateApi(DaneAPI daneAPI, int boardWidth, int boardHeight)
        {
            return new BallAPIBase(daneAPI, boardWidth, boardHeight);
        }

        public abstract void CreateBall();


        public void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            foreach (var ball in balls)
            {
                ball.Move();
                ball.CheckCollisionWithBoard(boardWidth, boardHeight);
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }

    internal class BallAPIBase : LogikaAPI
    {
        public BallAPIBase(DaneAPI daneAPI, int boardWidth, int boardHeight) : base(daneAPI, boardWidth, boardHeight) { }

        public override void CreateBall()
        {
            int x = random.Next(0, boardWidth);
            int y = random.Next(0, boardHeight);
            int Vx = random.Next(-2, 3);
            int Vy = random.Next(-2, 3);
            int radius = 5;
            balls.Add(new Ball(x, y, Vx, Vy, radius));
           
        }
    }
}
