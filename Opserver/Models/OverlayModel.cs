using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackExchange.Opserver.Data.SQL;
using Opserver.Entity;

namespace StackExchange.Opserver.Models
{
    public class OverlayModel
    {
                //list arrays with snapshot stats to load in
                public List<int> batchRequestSec = new List<int>();
                public List<int> SQLCompilationsSec = new List<int>();
                public List<int> transactionsSec = new List<int>();
                public List<int> indexSearchesSec = new List<int>();
                public List<int> lockRequestsSec = new List<int>();
                public List<int> errorsSec = new List<int>();
                public List<int> CPU = new List<int>();
                public List<int> RAM = new List<int>();
                public List<int> connections = new List<int>();
                public List<int> sessions = new List<int>();
                public List<int> maxWorkers = new List<int>();
                public List<int> dataFileSize = new List<int>();
                public List<int> logFileSize = new List<int>();
                public List<int> logFileUsedSize = new List<int>();
                public List<int> freeSpaceinTempDB = new List<int>();
                public List<int> pageLifeExpectancy = new List<int>();
                public List<int> pageLookupSec = new List<int>();
                public List<int> databasePages = new List<int>();
                public List<int> cacheHitRatio = new List<int>();

                public List<DateTime> snapshotDate = new List<DateTime>();
                public List<int> nodeList = new List<int>();



                public List<int> batchRequestSec2 = new List<int>();
                public List<int> SQLCompilationsSec2 = new List<int>();
                public List<int> transactionsSec2 = new List<int>();
                public List<int> indexSearchesSec2 = new List<int>();
                public List<int> lockRequestsSec2 = new List<int>();
                public List<int> errorsSec2 = new List<int>();
                public List<int> CPU2 = new List<int>();
                public List<int> RAM2 = new List<int>();
                public List<int> connections2 = new List<int>();
                public List<int> sessions2 = new List<int>();
                public List<int> maxWorkers2 = new List<int>();
                public List<int> dataFileSize2 = new List<int>();
                public List<int> logFileSize2 = new List<int>();
                public List<int> logFileUsedSize2 = new List<int>();
                public List<int> freeSpaceinTempDB2 = new List<int>();
                public List<int> pageLifeExpectancy2 = new List<int>();
                public List<int> pageLookupSec2 = new List<int>();
                public List<int> databasePages2 = new List<int>();
                public List<int> cacheHitRatio2 = new List<int>();

                public List<DateTime> snapshotDate2 = new List<DateTime>();
                public List<int> nodeList2 = new List<int>();

                //public String nodeNumber = "";
                public String nodeName, nodeName2 = "";
                public String brs_json, SQLCompilationsSec_json, trs_json, idxs_json, lrs_json, ers_json, cpu_json, ram_json, connections_json, sessions_json, maxWorkers_json, dataFileSize_json, logFileSize_json, logFileUsedSize_json, freeSpaceinTempDB_json, pageLifeExpectancy_json, pageLookupSec_json, databasePages_json, cacheHitRatio_json = "";
                public String brs_json2, SQLCompilationsSec_json2, trs_json2, idxs_json2, lrs_json2, ers_json2, cpu_json2, ram_json2, connections_json2, sessions_json2, maxWorkers_json2, dataFileSize_json2, logFileSize_json2, logFileUsedSize_json2, freeSpaceinTempDB_json2, pageLifeExpectancy_json2, pageLookupSec_json2, databasePages_json2, cacheHitRatio_json2 = "";

        public OverlayModel(Entities context, int nodeID, int nodeID2)
        {
            //get node with ID
            //var node = context.Nodes.FirstOrDefault(x => x.NodeID == nodeID);
            var node = context.Nodes.FirstOrDefault(x => x.NodeID == nodeID);
            var node2 = context.Nodes.FirstOrDefault(x => x.NodeID == nodeID2);
            //nodeNumber = nodeID.ToString();
            nodeName = node.NodeName; //get name for page
            nodeName2 = node2.NodeName;

            //add in data into list arrays
            //DateTime fourteenAgo = DateTime.Today.AddDays(-14);
            // var snaps = node.SnapshotNodes.Where(x => x.Date > fourteenAgo);
            var snaps = node.SnapshotNodes;
            var snaps2 = node2.SnapshotNodes;

            foreach (var snapshotnode in snaps)
            {
                var snapshot = snapshotnode.Snapshot;
                snapshotDate.Add(snapshotnode.Date);

                batchRequestSec.Add((int)snapshot.BatchRequestsSec);
                SQLCompilationsSec.Add((int)snapshot.SQLCompilationsSec);
                transactionsSec.Add((int)snapshot.TransactionsSec);
                indexSearchesSec.Add((int)snapshot.IndexSearchesSec);
                lockRequestsSec.Add((int)snapshot.LockRequestsSec);
                errorsSec.Add((int)snapshot.ErrorsSec);
                CPU.Add((int)snapshot.CPU);
                RAM.Add((int)snapshot.RAM);
                connections.Add((int)snapshot.Connections);
                sessions.Add((int)snapshot.Sessions);
                maxWorkers.Add((int)snapshot.MaxWorkers);
                dataFileSize.Add((int)snapshot.DataFilesSize);
                logFileSize.Add((int)snapshot.LogFileSize);
                logFileUsedSize.Add((int)snapshot.LogFileUsedSize);
                freeSpaceinTempDB.Add((int)snapshot.FreeSpaceinTempDB);
                pageLifeExpectancy.Add((int)snapshot.PageLifeExpectancy);
                pageLookupSec.Add((int)snapshot.PageLookupsSec);
                databasePages.Add((int)snapshot.DatabasePages);
                cacheHitRatio.Add((int)snapshot.CacheHitRatio);
            }

            foreach (var snapshotnode in snaps2)
            {
                var snapshot = snapshotnode.Snapshot;
                snapshotDate2.Add(snapshotnode.Date);

                batchRequestSec2.Add((int)snapshot.BatchRequestsSec);
                SQLCompilationsSec2.Add((int)snapshot.SQLCompilationsSec);
                transactionsSec2.Add((int)snapshot.TransactionsSec);
                indexSearchesSec2.Add((int)snapshot.IndexSearchesSec);
                lockRequestsSec2.Add((int)snapshot.LockRequestsSec);
                errorsSec2.Add((int)snapshot.ErrorsSec);
                CPU2.Add((int)snapshot.CPU);
                RAM2.Add((int)snapshot.RAM);
                connections2.Add((int)snapshot.Connections);
                sessions2.Add((int)snapshot.Sessions);
                maxWorkers2.Add((int)snapshot.MaxWorkers);
                dataFileSize2.Add((int)snapshot.DataFilesSize);
                logFileSize2.Add((int)snapshot.LogFileSize);
                logFileUsedSize2.Add((int)snapshot.LogFileUsedSize);
                freeSpaceinTempDB2.Add((int)snapshot.FreeSpaceinTempDB);
                pageLifeExpectancy2.Add((int)snapshot.PageLifeExpectancy);
                pageLookupSec2.Add((int)snapshot.PageLookupsSec);
                databasePages2.Add((int)snapshot.DatabasePages);
                cacheHitRatio2.Add((int)snapshot.CacheHitRatio);
            }

            brs_json = convertToJson(this.batchRequestSec, snapshotDate);
            SQLCompilationsSec_json = convertToJson(this.SQLCompilationsSec, snapshotDate);
            trs_json = convertToJson(this.transactionsSec, snapshotDate);
            idxs_json = convertToJson(this.indexSearchesSec, snapshotDate);
            lrs_json = convertToJson(this.lockRequestsSec, snapshotDate);
            ers_json = convertToJson(this.errorsSec, snapshotDate);
            cpu_json = convertToJson(this.CPU, snapshotDate);
            ram_json = convertToJson(this.RAM, snapshotDate);
            connections_json = convertToJson(this.connections, snapshotDate);
            sessions_json = convertToJson(this.sessions, snapshotDate);
            maxWorkers_json = convertToJson(this.maxWorkers, snapshotDate);
            dataFileSize_json = convertToJson(this.dataFileSize, snapshotDate);
            logFileSize_json = convertToJson(this.logFileSize, snapshotDate);
            logFileUsedSize_json = convertToJson(this.logFileUsedSize, snapshotDate);
            freeSpaceinTempDB_json = convertToJson(this.freeSpaceinTempDB, snapshotDate);
            pageLifeExpectancy_json = convertToJson(this.pageLifeExpectancy, snapshotDate);
            pageLookupSec_json = convertToJson(this.pageLookupSec, snapshotDate);
            databasePages_json = convertToJson(this.databasePages, snapshotDate);
            cacheHitRatio_json = convertToJson(this.cacheHitRatio, snapshotDate);

            brs_json2 = convertToJson(this.batchRequestSec2, snapshotDate2);
            SQLCompilationsSec_json2 = convertToJson(this.SQLCompilationsSec2, snapshotDate2);
            trs_json2 = convertToJson(this.transactionsSec2, snapshotDate2);
            idxs_json2 = convertToJson(this.indexSearchesSec2, snapshotDate2);
            lrs_json2 = convertToJson(this.lockRequestsSec2, snapshotDate2);
            ers_json2 = convertToJson(this.errorsSec2, snapshotDate2);
            cpu_json2 = convertToJson(this.CPU2, snapshotDate2);
            ram_json2 = convertToJson(this.RAM2, snapshotDate2);
            connections_json2 = convertToJson(this.connections2, snapshotDate2);
            sessions_json2 = convertToJson(this.sessions2, snapshotDate2);
            maxWorkers_json2 = convertToJson(this.maxWorkers2, snapshotDate2);
            dataFileSize_json2 = convertToJson(this.dataFileSize2, snapshotDate2);
            logFileSize_json2 = convertToJson(this.logFileSize2, snapshotDate2);
            logFileUsedSize_json2 = convertToJson(this.logFileUsedSize2, snapshotDate2);
            freeSpaceinTempDB_json2 = convertToJson(this.freeSpaceinTempDB2, snapshotDate2);
            pageLifeExpectancy_json2 = convertToJson(this.pageLifeExpectancy2, snapshotDate2);
            pageLookupSec_json2 = convertToJson(this.pageLookupSec2, snapshotDate2);
            databasePages_json2 = convertToJson(this.databasePages2, snapshotDate2);
            cacheHitRatio_json2 = convertToJson(this.cacheHitRatio2, snapshotDate2);

        }

        //converts the model data to JSON format as well as dates to milliseconds
        public String convertToJson(List<int> stat, List<DateTime> snapDate)
        {
            List<double> dates = new List<double>();

            foreach (var date in snapDate)
            {
                TimeSpan span = date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                //dates.Add((span.TotalSeconds) * 1000);
                dates.Add(span.TotalMilliseconds);
            }

            //var i = 0;
            //convert the data into a json string
            var json_string = "[";
            var skipNum = 0;
            if (stat.Count > 25)
            {
                skipNum = stat.Count - 25;
            }
            stat.RemoveRange(0, skipNum);
            //foreach (var item in stat)
            for (var i = 0; i < 25; i++)
            {
                if (i < stat.Count)
                {
                    json_string += "[" + dates.ElementAt(i) + ", " + stat[i] + "],";
                }
            }
            json_string = json_string.TrimEnd(",");
            json_string += "]";

            return json_string;
        }
        
    }

}