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

namespace GymBuddy.Controllers
{
    public class WorkOutPlanController : ApiController
    {
        private GymBuddyDb db = new GymBuddyDb();



        [ActionName("getWorkOutByID")]
        [ResponseType(typeof(tblWorkOutPlan))]
        public IHttpActionResult GettblWorkOutPlan(int id)
        {
            tblWorkOutPlan tblWorkOutPlan = db.tblWorkOutPlans.Find(id);
            if (tblWorkOutPlan == null)
            {
                return NotFound();
            }

            return Ok(tblWorkOutPlan);
        }

        [ActionName("updateWorkOutByID")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblWorkOutPlan(int id, tblWorkOutPlan tblWorkOutPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblWorkOutPlan.workOutID)
            {
                return BadRequest();
            }

            db.Entry(tblWorkOutPlan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblWorkOutPlanExists(id))
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

        [ActionName("addWorkoutPlan")]
        [ResponseType(typeof(tblWorkOutPlan))]
        public IHttpActionResult PosttblWorkOutPlan(tblWorkOutPlan tblWorkOutPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblWorkOutPlans.Add(tblWorkOutPlan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblWorkOutPlan.workOutID }, tblWorkOutPlan);
        }

        [ActionName("deleteWorkOutByID")]
        [ResponseType(typeof(tblWorkOutPlan))]
        public IHttpActionResult DeletetblWorkOutPlan(int id)
        {
            tblWorkOutPlan tblWorkOutPlan = db.tblWorkOutPlans.Find(id);
            if (tblWorkOutPlan == null)
            {
                return NotFound();
            }

            db.tblWorkOutPlans.Remove(tblWorkOutPlan);
            db.SaveChanges();

            return Ok(tblWorkOutPlan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblWorkOutPlanExists(int id)
        {
            return db.tblWorkOutPlans.Count(e => e.workOutID == id) > 0;
        }
    }
}