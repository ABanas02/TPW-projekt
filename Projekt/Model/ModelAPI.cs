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

        public abstract ObservableCollection<object> GetObjects();
        
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

            public override ObservableCollection<object> GetObjects()
            {
                ObservableCollection<object> objects = new ObservableCollection<object>();
                foreach (BallAPI ball in _logikaAPI.balls)
                    objects.Add(ball);
                return objects;
            }
        }
    }
}
