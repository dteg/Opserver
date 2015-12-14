using System;
using Opserver.Entity;
using System.Linq;
using StackExchange.Opserver.Data.SQL;
using Opserver;
using StackExchange.Opserver;

namespace StackExchange.Opserver.Tests
{
    public class SQLEntityTests
    {
        private Entities context;

        public SQLEntityTests(Entities Context)
        {
            context = Context;
        }

        /// <summary>
        /// If context connection failes, please check your connection strings in the app/web.config to point to the right DB.
        /// </summary>
        public string ContextConnection()
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

            return isValid ? "Success" :"Failed";
        }

        public string DoNodeExist(string nodeName)
        {
            try
            { 
                var node = context.Nodes.FirstOrDefault(x => x.NodeName == nodeName);

                if (node == null)
                    throw new Exception();
            }
            catch
            {
                return "Failed";
            }
            return "Success";
        }

        public string DoSnapshotExist(string nodeName)
        {
            try
            {
                var snapshot = context.Nodes.FirstOrDefault(x => x.NodeName == nodeName).SnapshotNodes.FirstOrDefault().Snapshot;
                if (snapshot == null)
                    throw new Exception();
            }
            catch
            {

                return "Failed";
            }

            return "Success";
        }
        /// <summary>
        /// Saves a snapshot and tries to pull the same snapshot from the DB. Edit the nodeName to your appropriate nodename
        /// </summary>
        public string SaveAndPull(string nodeName)
        {
            try { 
                var snapshotID = SaveSnapshot(nodeName);
                var pullID = PullSnapshot(snapshotID);

                if (snapshotID != pullID.SnapshotID)
                    throw new Exception();
            }
            catch
            {
                return "Failed";
            }
            return "Success";
        }

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