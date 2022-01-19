using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebApplicationDapperMVC.Models;

namespace WebApplicationDapperMVC.Controllers
{
    public class FriendController : Controller
    {

        private IConnectionService _connectionService;
        public FriendController(IConnectionService service)
        {
            _connectionService = service;
        }

        // GET: FriendController
        public ActionResult Index()
        {
            List<Friend> FriendList = new List<Friend>();
            using (IDbConnection db = new SqlConnection(_connectionService.GetConnectionString()))
            {

                FriendList = db.Query<Friend>("Select * From Friends").ToList();
            }
            return View(FriendList);
        }

        // GET: FriendController/Details/5
        public ActionResult Details(int id)
        {
            Friend friend = new Friend();
            using (IDbConnection db = new SqlConnection(_connectionService.GetConnectionString()))
            {
                friend = db.Query<Friend>("Select * From Friends " +
                                       "WHERE FriendId = @Id", new { Id = id }).SingleOrDefault();
            }
            return View(friend);
        }

        // GET: FriendController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FriendController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create(Friend friend)
        {
            using (IDbConnection db = new SqlConnection(_connectionService.GetConnectionString()))

            {
                string sqlQuery = "Insert Into Friends " +
                    "(FriendName,City,PhoneNumber,Age) Values(@FriendName,@City,@PhoneNumber,@Age)";

                int rowsAffected = db.Execute(sqlQuery, new { FriendName = friend.FriendName,
                                                              City = friend.City, PhoneNumber= friend.PhoneNumber,
                                                              Age = friend.Age});
            }
            return RedirectToAction("Index");
        }

        // GET: FriendController/Edit/5
        public ActionResult Edit(int id)
        {
            Friend friend = new Friend();
            using (IDbConnection db = new SqlConnection(_connectionService.GetConnectionString()))
            {
                friend = db.Query<Friend>("Select * From Friends " +
                                       "WHERE FriendId =@FriendId", new { FriendId=id }).SingleOrDefault();
            }
            return View(friend);
        }

        // POST: FriendController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Friend friend)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionService.GetConnectionString()))
                {
                    string sqlQuery = "update Friends set FriendName=@FriendName," +
                        "City=@City,PhoneNumber=@PhoneNumber, Age=@Age where friendId=@FriendId";
                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        FriendId = friend.FriendId,
                        FriendName = friend.FriendName,
                        City = friend.City,
                        PhoneNumber = friend.PhoneNumber,
                        Age = friend.Age
                    });
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FriendController/Delete/5
        public ActionResult Delete(int id)
        {
            Friend friend = new Friend();
            using (IDbConnection db = new SqlConnection(_connectionService.GetConnectionString()))
            {
                friend = db.Query<Friend>("Select * From Friends " +
                                       "WHERE FriendID =@FriendId", new { FriendId=id }).SingleOrDefault();
            }
            return View(friend);
        }

        // POST: FriendController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int FriendId, IFormCollection collection)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionService.GetConnectionString()))
                {
                    string sqlQuery = "Delete From Friends WHERE FriendID = @FriendId";
                    int rowsAffected = db.Execute(sqlQuery, new { FriendId = FriendId } );
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
