using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opserver.Entity;
using System.Linq;
using StackExchange.Opserver.Data.SQL;
using Opserver;

namespace opserver.Tests
{
    [TestClass]
    public class UnitTest1
    {
        //Edit for your own configuration
        private string nodeName = "Apache";
        private Entities context;

        public UnitTest1()
        {
            context = new Entities();
        }

        /// <summary>
        /// If context connection failes, please check your connection strings in the app/web.config to point to the right DB.
        /// </summary>
        [TestMethod]
        public void ContextConnection()
        {
            bool isValid = false;
            try
            {
                isValid = context.Nodes.Any();
                
            }
            catch
            {
                isValid = false;
            }

            Assert.IsTrue(isValid);
        }

        //Helper functions if somehow you are able to get settings to be recognized (the testing instance doesn't have Opserver.Core running when it tests
        private int SaveSnapshot()
        {
            var snapshotModel = new SnapshotNodeModel(SQLInstance.Get(nodeName));
            return snapshotModel.SaveSnapshot(context);
        }

        private Snapshot PullSnapshot(int snapshotID)
        {
            return context.Snapshots.FirstOrDefault(x => x.SnapshotID == snapshotID);  

        }

        private string DeleteSnapshot(int snapshotID)
        {
            return SnapshotNodeModel.DeleteSnapshot(context, snapshotID);
        }

    }
}
