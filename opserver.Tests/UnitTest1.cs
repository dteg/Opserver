using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Opserver.Entity;
using System.Linq;
using StackExchange.Opserver.Data.SQL;
using Opserver;
using StackExchange.Opserver;

namespace opserver.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Entities context;

        public UnitTest1()
        {
            context = new Entities();
            OpserverCore.Init();
            
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

        [TestMethod]
        public void DoNodeExist()
        {
            var node = context.Nodes.First();
            Assert.IsNotNull(node);
        }

        [TestMethod]
        public void DoSnapshotExist()
        {
            var snapshot = context.Snapshots.First();

            Assert.IsNotNull(snapshot);
        }
        /// <summary>
        /// Saves a snapshot and tries to pull the same snapshot from the DB. Edit the nodeName to your appropriate nodename
        /// </summary>
        [TestMethod]
        public void SaveAndPull()
        {
            string nodeName = "Apache";
            var snapshotID = SaveSnapshot(nodeName);
            var pullID = PullSnapshot(snapshotID);
            Assert.AreSame(snapshotID, pullID.SnapshotID);
        }
        //Helper functions if somehow you are able to get settings to be recognized (the testing instance doesn't have Opserver.Core running when it tests
        private int SaveSnapshot(string nodeName)
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
