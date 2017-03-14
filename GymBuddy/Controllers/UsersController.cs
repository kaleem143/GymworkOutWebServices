using System;
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
using System.Data.Entity.Core.Objects;

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
        /*
         * api/users/addRemoveClient?id=2&requestcode=2
        return 0 for no updation
        return 1 for successful update */
        [HttpPut]
        [ActionName("addRemoveClient")]
        public int apporveUser(int  id,int requestcode)
        {
            var username = new ObjectParameter("userName", typeof(string));
            return db.pro_approveClient1(id, requestcode,username);
            
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
      



        [ActionName("updateUser")]
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
        [ResponseType(typeof(tblUser))]
        [ActionName("getListClients")]
        public IHttpActionResult getlistClient(int  coachId)
        {
            var coach = from c in db.tblUsers
                        where c.coachID==coachId && c.userType=="client"
                        select c.firstName + " " +c.lastName;
            if (coach == null)
            {
                return NotFound();
            }

            return Ok(coach);
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