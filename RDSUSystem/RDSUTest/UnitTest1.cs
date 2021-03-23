using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDSUServer.Controllers.Sportsman;

namespace RDSUTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            for(int i = 0; i < 100; i++)
            {
                byte res = 255;
                if (i >= 60) res = 11;
                else if (i >= 50) res = 10;
                else if (i >= 40) res = 9;
                else if (i >= 30) res = 8;
                else if (i >= 21) res = 7;
                else if (i >= 19) res = 6;
                else if (i >= 16) res = 5;
                else if (i >= 14) res = 4;
                else if (i >= 12) res = 3;
                else if (i >= 10) res = 2;
                else res = 1;
                Assert.AreEqual(res,RegisterToTournamentController.OldReader(i));
            }
        }
    }
}
