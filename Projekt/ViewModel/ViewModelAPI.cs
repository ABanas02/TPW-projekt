using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public abstract class ViewModelAPI : INotifyPropertyChanged
    {
        private ObservableCollection<object> _objects;

        public static ViewModelAPI CreateViewModelAPI()
        {
            return new ViewModelAPIBase();
        }

        public ObservableCollection<object> Objects
        {
            get => _objects;
            set
            {
                _objects = value;
                OnPropertyChanged();
            }
        }
        public abstract void CreateBall();
        public abstract ObservableCollection<object> GetObjects();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public abstract void Start();
        public abstract void Stop();

        internal class ViewModelAPIBase : ViewModelAPI
        {
            public ICommand CreateBallCommand { get; }
            public ICommand StartCommand { get; }
            public ICommand StopCommand { get; }
            private ModelAPI _model;
            public ViewModelAPIBase()
            {
                _model = ModelAPI.CreateApi(null);
                CreateBallCommand = new RelayCommand(CreateBall);
                StartCommand = new RelayCommand(Start);
                StopCommand = new RelayCommand(Stop);
                Objects = GetObjects();
            }

            public override void CreateBall()
            {
                _model.CreateBall();
                var ball = _model.GetBalls().Last();
                ball.BallPositionChanged += BallPositionChangedHandler;
                Objects = GetObjects();
            }

            public override ObservableCollection<object> GetObjects()
            {
                return _model.GetObjects();
            }
            private void BallPositionChangedHandler(object sender, BallEvents e)
            {
                Objects = GetObjects();
            }
            public override void Start()
            {
                _model.Start();
            }

            public override void Stop()
            {
                _model.Stop();
            }
        }
    }
}
