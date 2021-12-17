using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace EindopdrachtDeviceProgrammingFunction.Models
{
    public class CircuitInfo : TableEntity
    {
        public string Id { get; set; }
        public string CircuitId { get; set; }

        public CircuitInfo()
        {

        }

        public CircuitInfo(string CircuitId, string Id)
        {
            this.PartitionKey = CircuitId;
            this.RowKey = Id;
        }
    }
}
