//using Dane;
//using Logika;
//using Logika.API;

//namespace LogikaTest
//{
//    [TestClass]
//    public class APITest
//    {
//        public class TestDane : DaneAPI
//        {
//            public TestDane() { }
//        }

//        private LogikaAPI logika = LogikaAPI.CreateApi(new TestDane());

//        [TestMethod]
//        public void TestCreateBall()
//        {
//            LogikaAPI logika = LogikaAPI.CreateApi();
//            logika.CreateBall();
//            Assert.AreEqual(logika.balls.Count, 1);

//            BallAPI createdBall = logika.balls[0];


//            Assert.IsTrue(createdBall.X >= 0 && createdBall.X < 1000);
//            Assert.IsTrue(createdBall.Y >= 0 && createdBall.Y < 1500);
//            Assert.IsTrue(createdBall.Vx >= -2 && createdBall.Vx <= 2);
//            Assert.IsTrue(createdBall.Vy >= -2 && createdBall.Vy <= 2);
//            Assert.AreEqual(5, createdBall.Radius);
//        }


//        [TestMethod]
//        public void TestBallMoveAndBallOutOfBounds()
//        {
//            // Przygotowanie
//            int boardWidth = 1000;
//            int boardHeight = 1500;

//            BallAPI ball = BallAPI.CreateAPI(0, 0, 5, 5, 5);

//            logika.balls.Add(ball);

//            ball.Move();
//            //Sprawdzenie czy kulka siê ruszy³a
//            Assert.IsTrue(ball.X == 5 && ball.Y == 5);


//            //Zdzerzenie z górn¹ œcian¹
//            ball.X = 0;
//            ball.Y = 0;
//            ball.Vx = 5;
//            ball.Vy = 5;
//            ball.Move();
//            ball.Y = boardHeight + ball.Radius;


//            ball.CheckCollisionWithBoard(boardWidth, boardHeight);

//            Assert.IsTrue(ball.Vx == 5 && ball.Vy == -5);

//            //Zderzenie z doln¹ œcian¹
//            ball.X = 0;
//            ball.Y = 0;
//            ball.Vx = 5;
//            ball.Vy = -5;
//            ball.Move();
//            ball.Y = 4;
//            ball.CheckCollisionWithBoard(boardWidth, boardHeight);

//            Assert.IsTrue(ball.Vx == 5 && ball.Vy == 5);

//            //Zderzenie w praw¹ œcian¹
//            ball.X = 0;
//            ball.Y = 0;
//            ball.Vx = 5;
//            ball.Vy = 5;
//            ball.Move();
//            ball.X = boardWidth + ball.Radius;
//            ball.CheckCollisionWithBoard(boardWidth, boardHeight);

//            Assert.IsTrue(ball.Vx == -5 && ball.Vy == 5);

//            //Zderzenie z lew¹ œcian¹
//            ball.X = 0;
//            ball.Y = 0;
//            ball.Vx = -5;
//            ball.Vy = 5;
//            ball.Move();
//            ball.X = 4;
//            ball.CheckCollisionWithBoard(boardWidth, boardHeight);

//            Assert.IsTrue(ball.Vx == 5 && ball.Vy == 5);
//        }

//    }
//}