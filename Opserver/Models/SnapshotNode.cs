using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Opserver;
using StackExchange.Opserver.Data.SQL;
using Opserver.Entity;

namespace Opserver
{
    public class SnapshotNode
    {

        public SnapshotNode(SQLInstance node)
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

        public string SaveSnapshot()
        {
            var context = new Entities();

            //Check if node exists in db
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

            context.SaveChanges();

            context.SnapshotNodes.Add(new Entity.SnapshotNode
            {
                SnapshotID = snapshot.SnapshotID,
                NodeID = NodeID,
                Date = DateTime.Now
          
            });

            context.SaveChanges();

            return "Success";
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
