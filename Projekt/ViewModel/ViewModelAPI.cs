using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Logika;
using Model;

namespace ViewModel
{
    public class ViewModelAPI : INotifyPropertyChanged
    {
        private readonly ModelAPI _model;
        private ObservableCollection<Ball> _balls;

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand CreateBallCommand { get; }

        public ViewModelAPI(ModelAPI model)
        {
            _model = model;
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
            CreateBallCommand = new RelayCommand(CreateBall);
            Balls = GetBalls();
        }

        public ObservableCollection<Ball> Balls
        {
            get => _balls;
            set
            {
                _balls = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Ball> GetBalls()
        {
            return _model.GetBalls();
        }

        public void Start()
        {
            _model.Start();
        }

        public void Stop()
        {
            _model.Stop();
        }

        public void CreateBall()
        {
            _model.CreateBall();
            Balls = GetBalls();
        }

        public void SetBoardSize(int width, int height)
        {
            _model.SetBoardSize(width, height);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
