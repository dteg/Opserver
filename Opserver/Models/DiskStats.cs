using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StackExchange.Opserver.Models
{
	public class DiskStats
	{

		/*
			

		*/
		public string DriveLetter { get; set; }
		public DateTime StartDate { get; set; }
		public int NumOfWrites { get; set; }
		public long NumOfBytesWritten { get; set; }
		public int NumOfReads { get; set; }
		public int TotalNumOfBytesRead { get; set; }
		public long TotalSizeOnDiskBytes { get; set; }
	}
}