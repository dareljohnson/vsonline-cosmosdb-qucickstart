using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace workspace
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateItem().Wait();
        }

        private static async Task CreateItem()
        {
            var cosmosUrl ="https://vso-cosmosdb.documents.azure.com:443/";
            var cosmoskey = "7YmmJN39GvohOX1RkxNtX4LhbBNMTkWv3HhBkjEJmugBhCyKzhQhXUHpaXlD6M5knubuKzKFgqMdvdxA93lJLw==";
            var databaseName = "TestDB";
            var containerName ="MyContainerName";
            var partionKeyPath = "/partionKeyPath";
            var RuPerSec = 400;

            var client= new CosmosClient(cosmosUrl, cosmoskey);
            Database database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            Container container = await database.CreateContainerIfNotExistsAsync(containerName,partionKeyPath,RuPerSec);

            dynamic testItem = new {
                id = Guid.NewGuid().ToString(),
                partionKeyPath ="MyTestPkValue",
                details ="it's working!"
            };
            var response = await container.CreateItemAsync(testItem);
        }
    }
}
