using Dane;
using Logika;
using Logika.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Numerics;

namespace LogikaTest
{
    [TestClass]
    public class APITest
    {
        public class TestDane : DaneAPI
        {
            public TestDane(int width, int height) : base()
            {
                Boundary = new Boundary(width, height);
            }
        }

        private LogikaAPI logika;

        [TestInitialize]
        public void Setup()
        {
            logika = LogikaAPI.CreateApi(new TestDane(390, 190), null);
        }

        [TestMethod]
        public void TestCreateBall()
        {
            logika.CreateBall();
            Assert.AreEqual(1, logika.balls.Count);

            BallAPI createdBall = logika.balls[0];

            Assert.IsTrue(createdBall.Position.X >= 0 && createdBall.Position.X <= 390);
            Assert.IsTrue(createdBall.Position.Y >= 0 && createdBall.Position.Y <= 190);
            Assert.IsTrue(createdBall.Vx >= 1 && createdBall.Vx <= 3);
            Assert.IsTrue(createdBall.Vy >= 1 && createdBall.Vy <= 3);
            Assert.AreEqual(25, createdBall.Radius);
        }
    }
}
