namespace Logika
{
    public class Ball
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
            set { _x = value; }
        }
        public int Vx
        {
            get { return _Vx; }
            set { _Vx = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Vy
        {
            get { return _Vy; }
            set { _Vy = value; }
        }

        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }


        public void Move()
        {
            _x += _Vx;
            _y += _Vy;
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