using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opserver.Entity;
using System.Linq;

namespace opserver.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ContextConnection()
        {
            bool isValid = false;
            try
            {
                var context = new Entities();
                context.Nodes.FirstOrDefault();
                isValid = true;
            }
            catch
            {
                isValid = false;
            }

            Assert.IsTrue(isValid);
        }

        public void SaveSnapshot()
        {

        }

        public void PullSnapshot()
        {

        }

        //Save snapshot, another one, and pull previous snapshot
        public void SaveSavePull()
        {

        }

    }
}
