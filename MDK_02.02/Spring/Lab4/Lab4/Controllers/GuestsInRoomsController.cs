using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lab4.Models;

namespace Lab4.Views
{
    public class GuestsInRoomsController : Controller
    {
        private Lab4DbVers1Entities db = new Lab4DbVers1Entities();

        // GET: GuestsInRooms
        public ActionResult Index()
        {
            var guestsInRooms = db.GuestsInRooms.Include(g => g.Guest).Include(g => g.Room);
            return View(guestsInRooms.ToList());
        }

        // GET: GuestsInRooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestsInRoom guestsInRoom = db.GuestsInRooms.Find(id);
            if (guestsInRoom == null)
            {
                return HttpNotFound();
            }
            return View(guestsInRoom);
        }

        // GET: GuestsInRooms/Create
        public ActionResult Create()
        {
            ViewBag.GUEST_ID = new SelectList(db.Guests, "GUEST_ID", "GUEST_NAME");
            ViewBag.ROOM_ID = new SelectList(db.Rooms, "ROOM_ID", "ROOM_NAME");
            return View();
        }

        // POST: GuestsInRooms/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ROOM_ID,GUEST_ID")] GuestsInRoom guestsInRoom)
        {
            if (ModelState.IsValid)
            {
                db.GuestsInRooms.Add(guestsInRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GUEST_ID = new SelectList(db.Guests, "GUEST_ID", "GUEST_NAME", guestsInRoom.GUEST_ID);
            ViewBag.ROOM_ID = new SelectList(db.Rooms, "ROOM_ID", "ROOM_NAME", guestsInRoom.ROOM_ID);
            return View(guestsInRoom);
        }

        // GET: GuestsInRooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestsInRoom guestsInRoom = db.GuestsInRooms.Find(id);
            if (guestsInRoom == null)
            {
                return HttpNotFound();
            }
            ViewBag.GUEST_ID = new SelectList(db.Guests, "GUEST_ID", "GUEST_NAME", guestsInRoom.GUEST_ID);
            ViewBag.ROOM_ID = new SelectList(db.Rooms, "ROOM_ID", "ROOM_NAME", guestsInRoom.ROOM_ID);
            return View(guestsInRoom);
        }

        // POST: GuestsInRooms/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ROOM_ID,GUEST_ID")] GuestsInRoom guestsInRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guestsInRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GUEST_ID = new SelectList(db.Guests, "GUEST_ID", "GUEST_NAME", guestsInRoom.GUEST_ID);
            ViewBag.ROOM_ID = new SelectList(db.Rooms, "ROOM_ID", "ROOM_NAME", guestsInRoom.ROOM_ID);
            return View(guestsInRoom);
        }

        // GET: GuestsInRooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestsInRoom guestsInRoom = db.GuestsInRooms.Find(id);
            if (guestsInRoom == null)
            {
                return HttpNotFound();
            }
            return View(guestsInRoom);
        }

        // POST: GuestsInRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GuestsInRoom guestsInRoom = db.GuestsInRooms.Find(id);
            db.GuestsInRooms.Remove(guestsInRoom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
