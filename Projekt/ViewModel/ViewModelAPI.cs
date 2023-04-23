using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public abstract class ViewModelAPI : INotifyPropertyChanged
    {
        //protected ModelAPI _model;
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

        public abstract void Start();
        public abstract void Stop();
        public abstract void CreateBall();
        public abstract ObservableCollection<object> GetObjects();

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
                StartCommand = new RelayCommand(Start);
                StopCommand = new RelayCommand(Stop);
                CreateBallCommand = new RelayCommand(CreateBall);
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

            public override void CreateBall()
            {
                _model.CreateBall();
                Objects = GetObjects();
            }

            public override ObservableCollection<object> GetObjects()
            {
                return _model.GetObjects();
            }
        }
    }
}
