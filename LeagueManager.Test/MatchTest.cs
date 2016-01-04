using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MTGLeagueManager.Model;

namespace LeagueManager.Test
{
    [TestClass]
    public class MatchTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void FirstMatchEveryOneIsEqual()
        {
            var stephane = new Player("Stephane");
            var thomas = new Player("Thomas");
            
            var m1 = new Match(stephane, thomas, new DateTime(2015, 12, 21));

            var winP1 = m1.GetWinPercentFor(stephane, thomas);
            var winP2 = m1.GetWinPercentFor(thomas, stephane);
            
            Assert.AreEqual(0.5, winP1);
            Assert.AreEqual(0.5, winP2);
        }

        [TestMethod]
        public void FirstMatchPointsComputeIfP1Win()
        {
            var stephane = new Player("Stephane");
            var thomas = new Player("Thomas");

            var m1 = new Match(stephane, thomas, new DateTime(2015, 12, 21));

            m1.UpdateScore(2, 0);

            Assert.AreEqual(8, m1.Points1);
            Assert.AreEqual(-8, m1.Points2);

            Assert.AreEqual(1600 + 8, stephane.Points);
            Assert.AreEqual(1600 + -8, thomas.Points);
        }

        [TestMethod]
        public void FirstMatchPointsComputeIfP2Win()
        {
            var stephane = new Player("Stephane");
            var thomas = new Player("Thomas");

            var m1 = new Match(stephane, thomas, new DateTime(2015, 12, 21));

            m1.UpdateScore(0, 2);

            Assert.AreEqual(-8, m1.Points1);
            Assert.AreEqual(8, m1.Points2);

            Assert.AreEqual(1600 + -8, stephane.Points);
            Assert.AreEqual(1600 + 8, thomas.Points);
        }

        [TestMethod]
        public void SecondMatchPointsComputeIfP1Win()
        {
            var arnault = new Player("Arnault");
            var sylvain = new Player("Sylvain");
            var stephane = new Player("Stephane");
            var mickael = new Player("Mickael");
            var thomas = new Player("Thomas");
            var olivier = new Player("Olivier");

            //round1
            var r1m1 = new Match(stephane, thomas, new DateTime(2015, 12, 21));
            r1m1.UpdateScore(2, 0);
            var r1m2 = new Match(arnault, sylvain, new DateTime(2015, 12, 21));
            r1m2.UpdateScore(2, 1);
            var r1m3 = new Match(mickael, olivier, new DateTime(2015, 12, 21));
            r1m3.UpdateScore(2, 1);
            
            Assert.AreEqual(1600 + 8, stephane.Points);
            Assert.AreEqual(1600 + -8, thomas.Points);
            Assert.AreEqual(1600 + 8, arnault.Points);
            Assert.AreEqual(1600 + -8, sylvain.Points);
            Assert.AreEqual(1600 + 8, mickael.Points);
            Assert.AreEqual(1600 + -8, olivier.Points);

            //round2
            var r2m1 = new Match(stephane, sylvain, new DateTime(2015, 12, 22));
            r2m1.UpdateScore(2, 0);
            var r2m2 = new Match(arnault, mickael, new DateTime(2015, 12, 22));
            r2m2.UpdateScore(2, 1);
            var r2m3 = new Match(olivier, thomas, new DateTime(2015, 12, 23));
            r2m3.UpdateScore(2, 1);

            Assert.AreEqual(1600 + 8 + 8, stephane.Points);
            Assert.AreEqual(1600 + -8 + -8, thomas.Points);
            Assert.AreEqual(1600 + 8 + 8, arnault.Points);
            Assert.AreEqual(1600 + -8 + -8, sylvain.Points);
            Assert.AreEqual(1600 + 8 + -8, mickael.Points);
            Assert.AreEqual(1600 + -8 + 8, olivier.Points);

            //round3
            var r3m1 = new Match(arnault, stephane, new DateTime(2015, 12, 24));
            r3m1.UpdateScore(2, 1);
            var r3m2 = new Match(mickael, thomas, new DateTime(2015, 12, 28));
            r3m2.UpdateScore(0, 2);
            var r3m3 = new Match(olivier, sylvain, new DateTime(2015, 12, 29));
            r3m3.UpdateScore(2, 0);

            Assert.AreEqual(1600 + 8 + 8 + -8, stephane.Points);
            Assert.AreEqual(1600 + -8 + -8 + 8, thomas.Points);
            Assert.AreEqual(1600 + 8 + 8 + 8, arnault.Points);
            Assert.AreEqual(1600 + -8 + -8 + -8, sylvain.Points);
            Assert.AreEqual(1600 + 8 + -8 + -8, mickael.Points);
            Assert.AreEqual(1600 + -8 + 8 + 8, olivier.Points);
        }
    }
}
