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
            DatabaseConnection.CakeBuild cakeBuild = new DatabaseConnection.CakeBuild();
            Assert.That(DatabaseConnection.BuildCake("Cake Build OK").Equals("Cake Build OK"));

        }
    }


}
