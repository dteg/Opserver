using Opserver.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Opserver
{
    public class NodeSnapshotList
    {
        public string NodeName { get; set; }
        public List<SelectListItem> SnapshotList { get; set; }

        public static List<NodeSnapshotList> NodesSnapshotList(Entities context)
        {
            var list = new List<NodeSnapshotList>();

            foreach (var node in context.Nodes)
            {
                var nodeSnapshot = new NodeSnapshotList();
                nodeSnapshot.NodeName = node.NodeName;
                nodeSnapshot.SnapshotList = new List<SelectListItem>();
                foreach (var nodesnap in node.SnapshotNodes)
                {
                    nodeSnapshot.SnapshotList.Add(new SelectListItem
                    {
                        Value = nodesnap.SnapshotID.ToString(),
                        Text = nodesnap.Date.ToString()
                    });
                }

                list.Add(nodeSnapshot);
            }
            return list;
        }

    }
}