using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LostTimeDBTest
{
    [TestFixture]
    public sealed class DatabaseConnectionTest
    {
        [Test]
        public void TestCakeBuild()
        {
            LostTimeDB.GoogleAccount cakeBuild = new LostTimeDB.GoogleAccount();
            Assert.That(cakeBuild.BuildCake("Cake Build OK").Equals("Cake Build OK"));
        }
    }


}
