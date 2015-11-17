using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StackExchange.Opserver.Data.SQL;
using Opserver.Entity;

namespace StackExchange.Opserver.Models
{
    public class ChartsModel
    {

        //list arrays with snapshot stats to load in
        public  List<int> batchRequestSec = new List<int>();
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


        public List<DateTime> snapshotDate = new List<DateTime>();
        public List<int> nodeList = new List<int>();
        
        //public String nodeNumber = "";
        public String nodeName = "";
        public String brs_json, SQLCompilationsSec_json, trs_json, idxs_json, lrs_json, ers_json, cpu_json, ram_json, connections_json = "";


        public ChartsModel(Entities context, int nodeID)
        {
            //get node with ID
            var node = context.Nodes.FirstOrDefault(x => x.NodeID == nodeID);
            //nodeNumber = nodeID.ToString();
            nodeName = node.NodeName; //get name for page

            //add in data into list arrays
            foreach (var snapshotnode in node.SnapshotNodes)
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

            }

            brs_json = convertToJson(this.batchRequestSec);
            SQLCompilationsSec_json = convertToJson(this.SQLCompilationsSec);
            trs_json = convertToJson(this.transactionsSec);
            idxs_json = convertToJson(this.indexSearchesSec);
            lrs_json = convertToJson(this.lockRequestsSec);
            ers_json = convertToJson(this.errorsSec);
            cpu_json = convertToJson(this.CPU);
            ram_json = convertToJson(this.RAM);
            connections_json = convertToJson(this.connections);

        }

        //converts the model data to JSON format as well as dates to milliseconds
        public String convertToJson(List<int> stat)
        {
            List<double> dates = new List<double>();

            foreach (var date in this.snapshotDate)
            {
                TimeSpan span = date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                //dates.Add((span.TotalSeconds) * 1000);
                dates.Add(span.TotalMilliseconds);
            }

            var i = 0;
            //convert the data into a json string
            var json_string = "[";
            foreach (var item in stat)
            {
                json_string += "[" + dates.ElementAt(i) + ", " + item + "],";
                i++;
            }
            json_string = json_string.TrimEnd(",");
            json_string += "]";

            return json_string;
        }
    }
}