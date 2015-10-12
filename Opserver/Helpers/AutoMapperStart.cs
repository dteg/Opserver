using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Opserver;
using Opserver.Entity;

namespace StackExchange.Opserver.Helpers
{
    public class AutoMapperStart
    {
        public static void StartMapping()
        {
            Mapper.CreateMap<SnapshotNodeModel, Snapshot>();
            Mapper.CreateMap<Snapshot, SnapshotNodeModel>();
            Mapper.CreateMap<SnapshotNodeModel, Node>();
        }
    }
}