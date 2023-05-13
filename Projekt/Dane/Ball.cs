using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Dane
{
    public abstract class BallAPI
    {
        public static BallAPI CreateAPI(int x, int y, int Vx, int Vy, int radius, int mass, Boundary boundary)
        {
            return new BallBase(x, y, Vx, Vy, radius, mass, boundary);
        }
        
        public abstract int Mass { get; set; }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int Vx { get; set; }
        public abstract int Vy { get; set; }
        public abstract int Radius { get; set; }
        public abstract int Diameter { get; }
        public Boundary Boundary { get; private set; }

        internal class BallBase : BallAPI, INotifyPropertyChanged
        {
            private int _x;
            private int _y;
            private int _Vx;
            private int _Vy;
            private int _radius;
            private int _mass;

            public BallBase(int _x, int _y, int _Vx, int _Vy, int _radius, int _mass, Boundary boundary)
            {
                this._x = _x;
                this._y = _y;
                this._Vx = _Vx;
                this._Vy = _Vy;
                this._radius = _radius;
                this._mass = _mass;
                this.Boundary = boundary;
            }

            public override int X
            {
                get { return _x; }
                set
                {
                    _x = value;
                    OnPropertyChanged();
                }
            }
            public override int Vx
            {
                get { return _Vx; }
                set
                {
                    _Vx = value;
                }
            }

            public override int Y
            {
                get { return _y; }
                set
                {
                    _y = value;
                    OnPropertyChanged();
                }
            }

            public override int Vy
            {
                get { return _Vy; }
                set
                {
                    _Vy = value;
                }
            }

            public override int Radius
            {
                get { return _radius; }
                set
                {
                    _radius = value;
                }
            }
            public override int Diameter
            {
                get { return Radius * 2; }
            }

            public override int Mass
            {
                get { return _mass; }
                set
                {
                    _mass = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
