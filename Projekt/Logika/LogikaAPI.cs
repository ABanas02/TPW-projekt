using Dane;
using System.Timers;

namespace Logika.API
{
    public abstract class LogikaAPI
    {
        public static LogikaAPI CreateApi(DaneAPI daneAPI)
        {
            if (daneAPI == null)
            {
                return new BallAPIBase(DaneAPI.CreateApi(390, 190));
            }
            else
            {
                return new BallAPIBase(daneAPI);
            }
        }
        public List<BallAPI> balls;
        public Random random;
        public abstract void CheckCollisionsBetweenBalls();
        public abstract void CheckCollisionWithBoard();
        public abstract void CreateBall();
        public abstract void StartMovingBalls();
        public abstract void MoveBalls();
    }

    internal class BallAPIBase : LogikaAPI
    {
        private DaneAPI _daneAPI;
        public BallAPIBase(DaneAPI daneAPI)
        {
            _daneAPI = daneAPI;
            balls = new List<BallAPI>();
            random = new Random();
        }
        public override void CreateBall()
        {
            balls.Add(_daneAPI.Boundary.CreateBall());
            var ball = balls[balls.Count - 1];
            if (balls.Count == 1)
            {
                StartMovingBalls();
            }
        }
        public override void StartMovingBalls()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    MoveBalls();
                    CheckCollisionWithBoard();
                    CheckCollisionsBetweenBalls();

                    Thread.Sleep(1000 / 60);
                }
            });
        }
        public override void MoveBalls()
        {
            foreach (var ball in balls)
            {
                ball.X += ball.Vx;
                ball.Y += ball.Vy;
            }
        }
        public override void CheckCollisionWithBoard()
        {
            foreach (var ball in balls)
            {
                if (ball.X - ball.Radius < 0)
                {
                    ball.Vx = Math.Abs(ball.Vx); // Make sure the ball moves to the right
                    ball.X = ball.Radius; // Make sure the ball is within the board
                }
                else if (ball.X + ball.Radius > _daneAPI.Boundary.Width)
                {
                    ball.Vx = -Math.Abs(ball.Vx); // Make sure the ball moves to the left
                    ball.X = _daneAPI.Boundary.Width - ball.Radius; // Make sure the ball is within the board
                }

                if (ball.Y - ball.Radius < 0)
                {
                    ball.Vy = Math.Abs(ball.Vy); // Make sure the ball moves downwards
                    ball.Y = ball.Radius; // Make sure the ball is within the board
                }
                else if (ball.Y + ball.Radius > _daneAPI.Boundary.Height)
                {
                    ball.Vy = -Math.Abs(ball.Vy); // Make sure the ball moves upwards
                    ball.Y = _daneAPI.Boundary.Height - ball.Radius; // Make sure the ball is within the board
                }
            }
        }

        public override void CheckCollisionsBetweenBalls()
        {
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    BallAPI ball1 = balls[i];
                    BallAPI ball2 = balls[j];

                    int distance = (int)Math.Sqrt(Math.Pow(ball1.X - ball2.X, 2) + Math.Pow(ball1.Y - ball2.Y, 2));

                    if (distance <= ball1.Radius / 2 + ball2.Radius / 2)
                    {
                        // collision detected
                        int v1x = ball1.Vx;
                        int v1y = ball1.Vy;
                        int v2x = ball2.Vx;
                        int v2y = ball2.Vy;

                        ball1.Vx = (v1x * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2x) / (ball1.Mass + ball2.Mass);
                        ball1.Vy = (v1y * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2y) / (ball1.Mass + ball2.Mass);
                        ball2.Vx = (v2x * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1x) / (ball1.Mass + ball2.Mass);
                        ball2.Vy = (v2y * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1y) / (ball1.Mass + ball2.Mass);
                    }
                }
            }
        }
    }
}
