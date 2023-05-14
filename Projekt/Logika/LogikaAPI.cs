using Dane;
using System.ComponentModel;
using System.Numerics;
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
        public abstract void CreateBall();
    }

    internal class BallAPIBase : LogikaAPI
    {
        private DaneAPI _daneAPI;
        private static readonly ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
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
            ball.subscribeToPropertyChanged(CheckCollisions);
        }

        private bool CheckCollisionsBetweenBalls(BallAPI ball1, BallAPI ball2)
        {
            Vector2 position1 = ball1.Position;
            Vector2 position2 = ball2.Position;
            int distance = (int)Math.Sqrt(Math.Pow((position1.X + ball1.Vx) - (position2.X + ball2.Vx), 2) + Math.Pow((position1.Y + ball1.Vx) - (position2.Y + ball2.Vy), 2));
            if (distance <= ball1.Radius / 2 + ball2.Radius / 2)
            {
                readerWriterLockSlim.EnterWriteLock();
                try
                {
                    int v1x = ball1.Vx;
                    int v1y = ball1.Vy;
                    int v2x = ball2.Vx;
                    int v2y = ball2.Vy;

                    int newV1X = (v1x * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2x) / (ball1.Mass + ball2.Mass);
                    int newV1Y = (v1y * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2y) / (ball1.Mass + ball2.Mass);
                    int newV2X = (v2x * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1x) / (ball1.Mass + ball2.Mass);
                    int newV2Y = (v2y * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1y) / (ball1.Mass + ball2.Mass);
                    ball1.Vx = (v1x * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2x) / (ball1.Mass + ball2.Mass);
                    ball1.Vy = (v1y * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2y) / (ball1.Mass + ball2.Mass);
                    ball2.Vx = (v2x * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1x) / (ball1.Mass + ball2.Mass);
                    ball2.Vy = (v2y * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1y) / (ball1.Mass + ball2.Mass);
                }
                finally
                {
                    readerWriterLockSlim.ExitWriteLock();
                }
                return false;
            }
            return true;
        }

        private void CheckCollisionWithBoard(BallAPI ball)
        {
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                Vector2 position = ball.Position;
                if (position.X - ball.Radius < 0)
                {
                    ball.Vx = Math.Abs(ball.Vx);
                    position.X = ball.Radius;
                }
                else if (position.X + ball.Radius > _daneAPI.Boundary.Width)
                {
                    ball.Vx = -Math.Abs(ball.Vx);
                    position.X = _daneAPI.Boundary.Width - ball.Radius;
                }

                if (position.Y - ball.Radius < 0)
                {
                    ball.Vy = Math.Abs(ball.Vy);
                    position.Y = ball.Radius;
                }
                else if (position.Y + ball.Radius > _daneAPI.Boundary.Height)
                {
                    ball.Vy = -Math.Abs(ball.Vy);
                    position.Y = _daneAPI.Boundary.Height - ball.Radius;
                }
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }
        private void CheckCollisions(object sender, PropertyChangedEventArgs e)
        {
            BallAPI ball = (BallAPI)sender;
            if (ball != null)
            {
                CheckCollisionWithBoard(ball);

                foreach (var ball2 in balls)
                {
                    if (!ball2.Equals(ball))
                    {
                        CheckCollisionsBetweenBalls(ball, ball2);
                    }
                }
            }
        }
    }
}
