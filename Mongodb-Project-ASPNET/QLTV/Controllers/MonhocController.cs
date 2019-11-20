using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using QLTV.Models;

namespace QLTV.Controllers
{
    public class MonhocController : Controller
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

            var result = mongoDatabase.GetCollection<Monhoc>("Monhoc").Find(FilterDefinition<Monhoc>.Empty).Sort("{MonhocId: 1}").ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Monhoc monhoc)
        {
            try
            {
                mongoDatabase = GetMongoDatabase();

                mongoDatabase.GetCollection<Monhoc>("Monhoc").InsertOne(monhoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            mongoDatabase = GetMongoDatabase();

            Monhoc monhoc = mongoDatabase.GetCollection<Monhoc>("Monhoc").Find<Monhoc>(k => k.MonhocId == id).FirstOrDefault();
            if (monhoc == null)
            {
                return NotFound();
            }
            return View(monhoc);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            mongoDatabase = GetMongoDatabase();

            var result = mongoDatabase.GetCollection<Monhoc>("Monhoc").DeleteOne<Monhoc>(k => k.MonhocId == id);

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            mongoDatabase = GetMongoDatabase();

            var monhoc = mongoDatabase.GetCollection<Monhoc>("Monhoc").Find<Monhoc>(k => k.MonhocId == id).FirstOrDefault();
            if (monhoc == null)
            {
                return NotFound();
            }
            return View(monhoc);
        }
        [HttpPost]
        public IActionResult Edit(Monhoc monhoc)
        {
            try
            {

                mongoDatabase = GetMongoDatabase();
                //Build the where condition  
                var filter = Builders<Monhoc>.Filter.Eq("MonhocId", monhoc.MonhocId);
                //Build the update statement   
                var updatestatement = Builders<Monhoc>.Update.Set("MonhocId", monhoc.MonhocId);
                updatestatement = updatestatement.Set("MaMH", monhoc.MaMH);
                updatestatement = updatestatement.Set("TenMH", monhoc.TenMH);
                updatestatement = updatestatement.Set("KhoaQL", monhoc.KhoaQL);
                //fetch the details from CustomerDB based on id and pass into view  
                UpdateResult result = mongoDatabase.GetCollection<Monhoc>("Monhoc").UpdateOne(filter, updatestatement);
                if (result.IsAcknowledged == false)
                {
                    return BadRequest("Unable to update Fullname  " + monhoc.TenMH);
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