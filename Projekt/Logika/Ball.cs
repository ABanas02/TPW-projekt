using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logika
{
    public class Ball : INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private int _Vx;
        private int _Vy;
        private int _radius;

        public Ball(int _x, int _y, int _Vx, int _Vy, int _radius)
        {
            this._x = _x;
            this._y = _y;
            this._Vx = _Vx;
            this._Vy = _Vy;
            this._radius = _radius;
        }
        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }
        public int Vx
        {
            get { return _Vx; }
            set
            {
                _Vx = value;
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public int Vy
        {
            get { return _Vy; }
            set
            {
                _Vy = value;
            }
        }

        public int Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
            }
        }
        public int Diameter
        {
            get { return Radius * 2; }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Move()
        {
            X += Vx;
            Y += Vy;
        }

        public void CheckCollisionWithBoard(int boardWidth, int boardHeight)
        {
            if (X - Radius < 0 || X + Radius > boardWidth)
            {
                Vx = -Vx;
            }

            if (Y - Radius < 0 || Y + Radius > boardHeight)
            {
                Vy = -Vy;
            }
        }
    }
}
