using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Logika;
using Logika.API;
using Model;

namespace ViewModel
{
    public abstract class ViewModelAPI : INotifyPropertyChanged
    {
        //protected ModelAPI _model;
        private ObservableCollection<Ball> _balls;

        public static ViewModelAPI CreateViewModelAPI()
        {
            return new ViewModelAPIBase();
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

        public abstract void Start();
        public abstract void Stop();
        public abstract void CreateBall();
        public abstract ObservableCollection<Ball> GetBalls();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal class ViewModelAPIBase : ViewModelAPI
        {
            public ICommand StartCommand { get; }
            public ICommand StopCommand { get; }
            public ICommand CreateBallCommand { get; }
            private ModelAPI _model;
            public ViewModelAPIBase()
            {
                _model = ModelAPI.CreateApi();
                //_model = model;
                StartCommand = new RelayCommand(Start);
                StopCommand = new RelayCommand(Stop);
                CreateBallCommand = new RelayCommand(CreateBall);
                Balls = GetBalls();
            }

            public override void Start()
            {
                _model.Start();
            }

            public override void Stop()
            {
                _model.Stop();
            }

            public override void CreateBall()
            {
                _model.CreateBall();
                Balls = GetBalls();
            }

            public override ObservableCollection<Ball> GetBalls()
            {
                return _model.GetBalls();
            }
        }
    }
}
