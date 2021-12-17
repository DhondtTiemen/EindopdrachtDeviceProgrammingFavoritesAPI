using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace EindopdrachtDeviceProgrammingFunction.Models
{
    public class DriverInfo : TableEntity
    {
        public string Id { get; set; }
        public string DriverId { get; set; }

        public DriverInfo()
        {

        }

        public DriverInfo(string DriverId, string Id)
        {
            this.PartitionKey = DriverId;
            this.RowKey = Id;
        }
    }
}
