﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dane
{
    public class Boundary
    {
        public int Width { get; set; }
        public int Height { get; set; }
        private Random random = new Random();

        public Boundary(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public BallAPI CreateBall()
        {
            int x = random.Next(40, Width - 40);
            int y = random.Next(40, Height - 40);

            int Vx = random.Next(1, 3);
            int Vy = Vx != 0 ? random.Next(1, 3) : random.Next(1, 3);

            int mass = 3;
            int radius = 25;

            return BallAPI.CreateAPI(x, y, Vx, Vy, radius, mass, this);
        }
    }
}
