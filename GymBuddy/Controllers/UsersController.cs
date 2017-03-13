﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GymBuddy.Models;

namespace GymBuddy.Controllers
{
    public class UsersController : ApiController
    {
        private GymBuddyDb db = new GymBuddyDb();

        // GET: api/Users


        public IQueryable<tblUser> getAllUserlist()
        {
            return db.tblUsers;
        }

        // GET: api/Users/5
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult GettblUser(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            return Ok(tblUser);
        }

        


        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblUser(int id, tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblUser.userID)
            {
                return BadRequest();
            }

            db.Entry(tblUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult PosttblUser(tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblUsers.Add(tblUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblUser.userID }, tblUser);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult DeletetblUser(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            db.tblUsers.Remove(tblUser);
            db.SaveChanges();

            return Ok(tblUser);
        }
        [HttpGet]
        [ResponseType(typeof(tblUser))]
        [ActionName("userByEmail")]
        public IHttpActionResult userByEmail(string email)
        {
            var user = db.tblUsers.Where(x => x.email == email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [ResponseType(typeof(tblUser))]
        [ActionName("userByUserName")]
        public IHttpActionResult getUserByUserName(string username)
        {
            var user = db.tblUsers.Where(x => x.userName == username);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        [HttpGet]
        [ActionName("isverifieduser")]
        public IHttpActionResult getUserByUserName(string email,string password)
        {
            var user = db.tblUsers.Where(x => x.email ==email && x.password==password );
            if (user==null)
            {
                return NotFound();
            }
         
                return Ok(user);
            

            
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserExists(int id)
        {
            return db.tblUsers.Count(e => e.userID == id) > 0;
        }
    }
}