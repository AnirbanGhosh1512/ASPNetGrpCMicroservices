using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {

            //Fetch Database connection and database information from API Settings file. 
            //Author: Anirban Ghosh
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //This method is to see the collection after the database operation is done and this is singletone approach
            //as it is call only when constructor of this class called. 
            //Author: Anirban Ghosh
            CatalogContextSeed.SeedData(Products);


        }
        public IMongoCollection<Product> Products { get; }
    }
}
