using ClassLibrary1;
namespace TestProjekt1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            ClassLibrary1.Kalkulator k = new Kalkulator();

            Assert.AreEqual(k.add(2, 2), 4);
        }

    }
}