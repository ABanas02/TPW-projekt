using System.Collections.ObjectModel;
using Logika;
using Logika.API;

namespace Model
{
    public class ModelAPI
    {
        private LogikaAPI _logikaAPI;

        public ModelAPI(int boardWidth, int boardHeight)
        {
            _logikaAPI = LogikaAPI.CreateApi(boardWidth, boardHeight);
        }

        public void Start()
        {
            _logikaAPI.Start();
        }

        public void Stop()
        {
            _logikaAPI.Stop();
        }

        public void CreateBall()
        {
            _logikaAPI.CreateBall();
        }

        public ObservableCollection<Ball> GetBalls()
        {
            ObservableCollection<Ball> balls = new ObservableCollection<Ball>();
            foreach (Ball ball in _logikaAPI.balls)
                balls.Add(ball);
            return balls;
        }

        public void SetBoardSize(int width, int height)
        {
            _logikaAPI.boardWidth = width;
            _logikaAPI.boardHeight = height;
        }
    }
}
