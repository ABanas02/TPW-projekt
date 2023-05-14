using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using Dane;

namespace DaneTest
{
    [TestClass]
    public class BallAPITest
    {
        [TestMethod]
        public void CreateAPITest()
        {
            Vector2 position = new Vector2(100, 100);
            int Vx = 2;
            int Vy = 2;
            int radius = 25;
            int mass = 3;
            Boundary boundary = new Boundary(200, 200);

            BallAPI ball = BallAPI.CreateAPI(position, Vx, Vy, radius, mass, boundary);

            Assert.AreEqual(position, ball.Position);
            Assert.AreEqual(Vx, ball.Vx);
            Assert.AreEqual(Vy, ball.Vy);
            Assert.AreEqual(radius, ball.Radius);
            Assert.AreEqual(mass, ball.Mass);
        }
    }
}