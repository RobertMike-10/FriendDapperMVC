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

        private ConnectionService _connectionService;
        public FriendController(ConnectionService service)
        {
            _connectionService = service;
        }

        // GET: FriendController
        public ActionResult Index()
        {
            List<Friend> FriendList = new List<Friend>();
            using (IDbConnection db = new SqlConnection(_connectionService.GetConnectionString()))
            {

                FriendList = db.Query<Friend>("Select * From tblFriends").ToList();
            }
            return View(FriendList);
        }

        // GET: FriendController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FriendController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FriendController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FriendController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FriendController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FriendController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FriendController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
