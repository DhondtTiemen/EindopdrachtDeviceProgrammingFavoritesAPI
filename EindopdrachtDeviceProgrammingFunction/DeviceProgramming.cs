using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using EindopdrachtDeviceProgrammingFunction.Models;

namespace EindopdrachtDeviceProgrammingFunction
{
    public class DeviceProgramming
    {
        [FunctionName("addFavoriteCircuit")]
        public async Task<IActionResult> addFavorite(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "favorites/circuits/{circuitid}")] HttpRequest req,
            string circuitid,
            ILogger log)
        {
            //Connectie maken met Table Storage
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

            //JSON uitlezen
            var json = await new StreamReader(req.Body).ReadToEndAsync();
            //JSON omzetten naar object in C#
            var data = JsonConvert.DeserializeObject<CircuitInfo>(json);

            //Unieke GUID aanmaken
            string guid = Guid.NewGuid().ToString();

            CircuitInfo circuitInfo = new CircuitInfo(circuitid, guid)
            {
                Id = guid,
                CircuitId = circuitid,
            };

            //Connecteren met Table Storage
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable table = cloudTableClient.GetTableReference("tblCircuits");
            await table.CreateIfNotExistsAsync();

            //INSERT INTO operation maken waar we object meegeven
            TableOperation insertOperation = TableOperation.Insert(circuitInfo);
            await table.ExecuteAsync(insertOperation);

            return new OkObjectResult(circuitInfo);
        }

        [FunctionName("addFavoriteDriver")]
        public async Task<IActionResult> addFavoriteDriver(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "favorites/drivers/{driverid}")] HttpRequest req,
            string driverid,
            ILogger log)
        {
            //Connectie maken met Table Storage
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

            //JSON uitlezen
            var json = await new StreamReader(req.Body).ReadToEndAsync();
            //JSON omzetten naar object in C#
            var data = JsonConvert.DeserializeObject<DriverInfo>(json);

            //Unieke GUID aanmaken
            string guid = Guid.NewGuid().ToString();

            DriverInfo driverInfo = new DriverInfo(driverid, guid)
            {
                Id = guid,
                DriverId = driverid,
            };

            //Connecteren met Table Storage
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            CloudTable table = cloudTableClient.GetTableReference("tblDrivers");
            await table.CreateIfNotExistsAsync();

            //INSERT INTO operation maken waar we object meegeven
            TableOperation insertOperation = TableOperation.Insert(driverInfo);
            await table.ExecuteAsync(insertOperation);

            return new OkObjectResult(driverInfo);
        }
    }
}
