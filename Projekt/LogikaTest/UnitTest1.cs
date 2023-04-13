using Dane;
using Logika;
using Logika.API;

namespace LogikaTest
{
    [TestClass]
    public class APITest
    {
        [TestMethod]
        public void TestCreateBall()
        {
            DaneAPI dane = DaneAPI.CreateApi();
            LogikaAPI logika = LogikaAPI.CreateApi(dane, 1000, 1500);
            logika.CreateBall();
            Assert.AreEqual(logika.balls.Count, 1);

            Ball createdBall = logika.balls[0];


            Assert.IsTrue(createdBall.X >= 0 && createdBall.X < 1000);
            Assert.IsTrue(createdBall.Y >= 0 && createdBall.Y < 1500);
            Assert.IsTrue(createdBall.Vx >= -2 && createdBall.Vx <= 2);
            Assert.IsTrue(createdBall.Vy >= -2 && createdBall.Vy <= 2);
            Assert.AreEqual(5, createdBall.Radius);
        }
       
    }
}