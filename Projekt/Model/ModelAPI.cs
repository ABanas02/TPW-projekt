using System.Collections.ObjectModel;
using Logika;
using Logika.API;

namespace Model
{
    public abstract class ModelAPI
    {
        
        public static ModelAPI CreateApi()
        {
            return new ModelAPIBase(390, 190);
        }

        public abstract void Start();

        public abstract void Stop();


        public abstract void CreateBall();

        public abstract ObservableCollection<Ball> GetBalls();
        
        internal class ModelAPIBase: ModelAPI
        {
            private LogikaAPI _logikaAPI;
            public ModelAPIBase(int boardWidth, int boardHeight)
            {
                _logikaAPI = LogikaAPI.CreateApi();
            }

            public override void Start()
            {
                _logikaAPI.Start();
            }

            public override void Stop()
            {
                _logikaAPI.Stop();
            }

            public override void CreateBall()
            {
                _logikaAPI.CreateBall();
            }

            public override ObservableCollection<Ball> GetBalls()
            {
                ObservableCollection<Ball> balls = new ObservableCollection<Ball>();
                foreach (Ball ball in _logikaAPI.balls)
                    balls.Add(ball);
                return balls;
            }
        }
    }
}
