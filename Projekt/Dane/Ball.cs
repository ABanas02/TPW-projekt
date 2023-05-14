using System.Numerics;
using System.Threading.Tasks;

namespace Dane
{
    public abstract class BallAPI : INotifyBallPositionChanged
    {
        public static BallAPI CreateAPI(Vector2 position, int Vx, int Vy, int radius, int mass, Boundary boundary)
        {
            return new BallBase(position, Vx, Vy, radius, mass, boundary);
        }
        public abstract Vector2 Position { get; }
        public abstract int Mass { get; set; }
        public abstract int X { get; }
        public abstract int Y { get; }
        public abstract int Vx { get; set; }
        public abstract int Vy { get; set; }
        public abstract int Radius { get; set; }
        public abstract int Diameter { get; }
        public Boundary Boundary { get; private set; }
        public event BallPositionChangedEventHandler BallPositionChanged;

        internal class BallBase : BallAPI
        {
            private Vector2 _position;
            private int _Vx;
            private int _Vy;
            private int _radius;
            private int _mass;

            public BallBase(Vector2 position, int _Vx, int _Vy, int _radius, int _mass, Boundary boundary)
            {
                this._position = position;
                this._Vx = _Vx;
                this._Vy = _Vy;
                this._radius = _radius;
                this._mass = _mass;
                this.Boundary = boundary;
                Task.Run(() => Move());
            }

            private async Task Move()
            {
                while (true)
                {
                    int newX = (int)_position.X + _Vx;
                    int newY = (int)_position.Y + _Vy;
                    Vector2 newPosition = new Vector2(newX, newY);
                    setPosition(newPosition);

                    double velocity = Math.Sqrt(_Vx * _Vx + _Vy * _Vy);
                    await Task.Delay(TimeSpan.FromMilliseconds(2 * velocity));
                }
            }

            public override Vector2 Position
            {
                get { return _position; }
            }

            private void setPosition(Vector2 newPosition)
            {
                _position.X = newPosition.X;
                _position.Y = newPosition.Y;
                BallPositionChanged?.Invoke(this, new BallEvents(newPosition));
            }

            public override int X { get { return (int)_position.X; } }
            public override int Y { get { return (int)_position.Y; } }
            public override int Vx
            {
                get { return _Vx; }
                set { _Vx = value; }
            }

            public override int Vy
            {
                get { return _Vy; }
                set { _Vy = value; }
            }

            public override int Radius
            {
                get { return _radius; }
                set { _radius = value; }
            }

            public override int Diameter
            {
                get { return Radius * 2; }
            }

            public override int Mass
            {
                get { return _mass; }
                set { _mass = value; }
            }
        }
    }
}
