using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using QLTV.Models;

namespace QLTV.Controllers
{
    public class KhoaController : Controller
    {
        private IMongoDatabase mongoDatabase;
        public IMongoDatabase GetMongoDatabase()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            return mongoClient.GetDatabase("QLTV");
        }
        [HttpGet]
        public IActionResult Index()
        {
            mongoDatabase = GetMongoDatabase();

            var result = mongoDatabase.GetCollection<Khoa>("Khoa").Find(FilterDefinition<Khoa>.Empty).Sort("{KhoaId: 1}").ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Get the database connection  
            mongoDatabase = GetMongoDatabase();
            //fetch the details from CustomerDB and pass into view  
            Khoa khoa = mongoDatabase.GetCollection<Khoa>("Khoa").Find<Khoa>(k => k.KhoaId == id).FirstOrDefault();
            if (khoa == null)
            {
                return NotFound();
            }
            return View(khoa);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Khoa khoa)
        {
            try
            {
                //Get the database connection  
                mongoDatabase = GetMongoDatabase();
                mongoDatabase.GetCollection<Khoa>("Khoa").InsertOne(khoa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Get the database connection  
            mongoDatabase = GetMongoDatabase();
            //fetch the details from CustomerDB and pass into view  
            var result = mongoDatabase.GetCollection<Khoa>("Khoa").DeleteOne<Khoa>(k => k.KhoaId == id);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Get the database connection  
            mongoDatabase = GetMongoDatabase();
            //fetch the details from CustomerDB based on id and pass into view  
            var khoa = mongoDatabase.GetCollection<Khoa>("Khoa").Find<Khoa>(k => k.KhoaId == id).FirstOrDefault();
            if (khoa == null)
            {
                return NotFound();
            }
            return View(khoa);
        }
        [HttpPost]
        public IActionResult Edit(Khoa khoa)
        {
            try
            {
                //Get the database connection  
                mongoDatabase = GetMongoDatabase();
                //Build the where condition  
                var filter = Builders<Khoa>.Filter.Eq("KhoaId", khoa.KhoaId);
                //Build the update statement   
                var updatestatement = Builders<Khoa>.Update.Set("KhoaId", khoa.KhoaId);
                updatestatement = updatestatement.Set("KhoaName", khoa.KhoaName);
                updatestatement = updatestatement.Set("Address", khoa.Address);
                //fetch the details from CustomerDB based on id and pass into view  
                var result = mongoDatabase.GetCollection<Khoa>("Khoa").UpdateOne(filter, updatestatement);
                if (result.IsAcknowledged == false)
                {
                    return BadRequest("Unable to update Customer  " + khoa.KhoaName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }


    }
}