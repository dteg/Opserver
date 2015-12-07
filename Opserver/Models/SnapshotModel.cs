using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Opserver;
using StackExchange.Opserver.Data.SQL;
using Opserver.Entity;
using System.Web.Mvc;

namespace Opserver
{
    public class SnapshotNodeModel
    {
        /// <summary>
        /// Generate a SnapshotNodeModel from a SQL Instance
        /// </summary>
        /// <param name="node"></param>
        public SnapshotNodeModel(SQLInstance node)
        {
            NodeName = node.Name;
            Date = DateTime.Now;
            BatchRequestsSec = Convert.ToInt32(node.GetPerfCounter("SQL Statistics", "Batch Requests/sec", "").CurrentValue);
            SQLCompilationsSec = Convert.ToInt32(node.GetPerfCounter("SQL Statistics", "SQL Compilations/sec", "").CurrentValue);
            TransactionsSec = Convert.ToInt32(node.GetPerfCounter("Databases", "Transactions/sec", "_Total").CurrentValue);
            IndexSearchesSec = Convert.ToInt32(node.GetPerfCounter("Access Methods", "Index Searches/sec", "").CurrentValue);
            LockRequestsSec = Convert.ToInt32(node.GetPerfCounter("SQL Statistics", "Batch Requests/sec", "").CurrentValue);
            ErrorsSec = Convert.ToInt32(node.GetPerfCounter("SQL Statistics", "Batch Requests/sec", "").CurrentValue);

            CPU = Convert.ToInt32(node.CurrentCPUPercent);
            RAM = Convert.ToInt32(node.CurrentMemoryPercent);
            Connections = Convert.ToInt32(node.Connections.Data.Count);
            Sessions = Convert.ToInt32(node.ServerProperties.Data.SessionCount);
            MaxWorkers = Convert.ToInt32(node.ServerProperties.Data.MaxWorkersCount);

            TotalServerMemory =
                Convert.ToInt32(node.GetPerfCounter("Memory Manager", "Total Server Memory (KB)", "").CurrentValue);
            TargetServerMemory =
                Convert.ToInt32(node.GetPerfCounter("Memory Manager", "Target Server Memory (KB)", "").CurrentValue);
            DatabaseCacheMemory =
                Convert.ToInt32(node.GetPerfCounter("Memory Manager", "Database Cache Memory (KB)", "").CurrentValue);
            FreeMemory =
                Convert.ToInt32(node.GetPerfCounter("Memory Manager", "Free Memory (KB)", "").CurrentValue);

            DataFilesSize =
                Convert.ToInt32(node.GetPerfCounter("Databases", "Data File(s) Size (KB)", "_Total").CurrentValue);
            LogFileSize =
                Convert.ToInt32(node.GetPerfCounter("Databases", "Log File(s) Size (KB)", "_Total").CurrentValue);
            LogFileUsedSize =
                Convert.ToInt32(node.GetPerfCounter("Databases", "Log File(s) Used Size (KB)", "_Total").CurrentValue);
            FreeSpaceinTempDB =
                Convert.ToInt32(node.GetPerfCounter("Transactions", "Free Space in tempdb (KB)", "").CurrentValue);

            PageLifeExpectancy =
                Convert.ToInt32(node.GetPerfCounter("Buffer Manager", "Page life expectancy", "").CurrentValue);
            PageLookupsSec =
                Convert.ToInt32(node.GetPerfCounter("Buffer Manager", "Page lookups/sec", "").CurrentValue);
            DatabasePages =
                Convert.ToInt32(node.GetPerfCounter("Buffer Manager", "Database pages", "").CurrentValue);
            CacheHitRatio =
                Convert.ToInt32(node.GetPerfCounter("Plan Cache", "Cache Hit Ratio", "_Total").CurrentValue);


        }
        public SnapshotNodeModel() { }

        /// <summary>
        /// Takes the context and saves a database performance snapshot 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public int SaveSnapshot(Entities context)
        {
            //Check if node exists in db if so, pull it's information
            if (context.Nodes.Any(x => x.NodeName == NodeName))
            {
                NodeID = context.Nodes.SingleOrDefault(x => x.NodeName == NodeName).NodeID;
            }
            else
            {
                var node = context.Nodes.Add(new Node
                {
                    NodeName = NodeName
                });
                context.SaveChanges();
                NodeID = node.NodeID;
            }
            //Create the snapshot using current object -- potential use of Automapper to automate this
            var snapshot = context.Snapshots.Add(new Snapshot 
            {
                BatchRequestsSec = BatchRequestsSec,
                SQLCompilationsSec = SQLCompilationsSec,
                TransactionsSec = TransactionsSec,
                IndexSearchesSec = IndexSearchesSec,
                LockRequestsSec = LockRequestsSec,
                ErrorsSec = ErrorsSec,
                CPU = CPU,
                RAM = RAM,
                Connections = Connections,
                Sessions = Sessions,
                MaxWorkers = MaxWorkers,
                TotalServerMemory = TotalServerMemory,
                TargetServerMemory = TargetServerMemory,
                DatabaseCacheMemory = DatabaseCacheMemory,
                FreeMemory = FreeMemory,
                DataFilesSize = DataFilesSize,
                LogFileSize = LogFileSize,
                LogFileUsedSize = LogFileUsedSize,
                FreeSpaceinTempDB = FreeSpaceinTempDB,
                PageLifeExpectancy = PageLifeExpectancy,
                PageLookupsSec = PageLookupsSec,
                DatabasePages = DatabasePages,
                ObjectsInCache = ObjectsInCache,
                CacheHitRatio = CacheHitRatio
            });
            //Save it to make sure DB contraints dont complain.
            context.SaveChanges();
            //Add snapshot node
            context.SnapshotNodes.Add(new Entity.SnapshotNode
            {
                SnapshotID = snapshot.SnapshotID,
                NodeID = NodeID,
                Date = DateTime.Now
          
            });

            context.SaveChanges();

            return snapshot.SnapshotID;
        }

        /// <summary>
        /// Deletes a snapshot based on the ID given
        /// </summary>
        /// <param name="context"></param>
        /// <param name="snapshotID"></param>
        /// <returns></returns>
        public static string DeleteSnapshot(Entities context, int snapshotID)
        {
            
            var snapshotNode = context.SnapshotNodes.FirstOrDefault(x => x.SnapshotID == snapshotID);
            var snapshot = new Snapshot { SnapshotID = snapshotID };
            //Delete the relationship between snapshotnode and snapshot first
            context.Entry(snapshotNode).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
            //Delete the snapshot
            context.Entry(snapshot).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();

            return "Success";
        }

        /// <summary>
        /// Return the list of NodeIDs and Names for select list
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetNodes(Entities context)
        {
            var list = new List<SelectListItem>();
            foreach (var node in context.Nodes)
            {
                foreach (var snapshotID in node.SnapshotNodes)
                {
                    list.Add(new SelectListItem
                    {
                        Value = snapshotID.SnapshotID.ToString(),
                        Text = node.NodeName + " - " + snapshotID.Date
                    });
                }
                
            }
            list.OrderBy( x=> x.Text);

            return list;
        }
        /// <summary>
        /// Assign itself value based on a context that is passed.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
       public static SnapshotNodeModel GetByID(int id, Entities context)
       {
           var efSnapShot = context.Snapshots.FirstOrDefault(x => x.SnapshotID == id);

           var snapshot = new SnapshotNodeModel{
               BatchRequestsSec = efSnapShot.BatchRequestsSec,
               SQLCompilationsSec = efSnapShot.SQLCompilationsSec,
               TransactionsSec = efSnapShot.TransactionsSec,
               IndexSearchesSec = efSnapShot.IndexSearchesSec,
               LockRequestsSec = efSnapShot.LockRequestsSec,
               ErrorsSec = efSnapShot.ErrorsSec,
               CPU = efSnapShot.CPU,
               RAM = efSnapShot.RAM,
               Connections = efSnapShot.Connections,
               Sessions = efSnapShot.Sessions,
               MaxWorkers = efSnapShot.MaxWorkers,
               TotalServerMemory = efSnapShot.TotalServerMemory,
               TargetServerMemory = efSnapShot.TargetServerMemory,
               DatabaseCacheMemory = efSnapShot.DatabaseCacheMemory,
               FreeMemory = efSnapShot.FreeMemory,
               DataFilesSize = efSnapShot.DataFilesSize,
               LogFileSize = efSnapShot.LogFileSize,
               LogFileUsedSize = efSnapShot.LogFileUsedSize,
               FreeSpaceinTempDB = efSnapShot.FreeSpaceinTempDB,
               PageLifeExpectancy = efSnapShot.PageLifeExpectancy,
               PageLookupsSec = efSnapShot.PageLookupsSec,
               DatabasePages = efSnapShot.DatabasePages,
               ObjectsInCache = efSnapShot.ObjectsInCache,
               CacheHitRatio = efSnapShot.CacheHitRatio,
               Date = efSnapShot.SnapshotNodes.FirstOrDefault(x=> x.SnapshotID == id).Date,
               NodeName = efSnapShot.SnapshotNodes.FirstOrDefault(x=> x.SnapshotID == id).Node.NodeName,
           };

            return snapshot;
       } 

        public int NodeID { get; set; }
        public string NodeName { get; set; }

        public int SnapshotID { get; set; }

        public System.DateTime Date { get; set; }
        public Nullable<int> BatchRequestsSec { get; set; }
        public Nullable<int> SQLCompilationsSec { get; set; }
        public Nullable<int> TransactionsSec { get; set; }
        public Nullable<int> IndexSearchesSec { get; set; }
        public Nullable<int> LockRequestsSec { get; set; }
        public Nullable<int> ErrorsSec { get; set; }
        public Nullable<int> CPU { get; set; }
        public Nullable<int> RAM { get; set; }
        public Nullable<int> Connections { get; set; }
        public Nullable<int> Sessions { get; set; }
        public Nullable<int> MaxWorkers { get; set; }
        public Nullable<int> TotalServerMemory { get; set; }
        public Nullable<int> TargetServerMemory { get; set; }
        public Nullable<int> DatabaseCacheMemory { get; set; }
        public Nullable<int> FreeMemory { get; set; }
        public Nullable<int> DataFilesSize { get; set; }
        public Nullable<int> LogFileSize { get; set; }
        public Nullable<int> LogFileUsedSize { get; set; }
        public Nullable<int> FreeSpaceinTempDB { get; set; }
        public Nullable<int> PageLifeExpectancy { get; set; }
        public Nullable<int> PageLookupsSec { get; set; }
        public Nullable<int> DatabasePages { get; set; }
        public Nullable<int> ObjectsInCache { get; set; }
        public Nullable<double> CacheHitRatio { get; set; }

    }
}
