using GymBuddy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace GymBuddy.Controllers
{
    public class NutritionController : ApiController
    {
        private GymBuddyDb db = new GymBuddyDb();
        [HttpGet]
        [ResponseType(typeof(tblNutritionPlan))]
        [ActionName("getNutritionByID")]
        public IHttpActionResult GetNutritionPlan(int id)
        {
            tblNutritionPlan nutrition = db.tblNutritionPlans.Find(id);
            if (nutrition == null)
            {
                return NotFound();
            }

            return Ok(nutrition);
        }
        [ActionName("addNutrition")]
        [ResponseType(typeof(tblNutritionPlan))]
        public IHttpActionResult PosttblUser(tblNutritionPlan nutrition )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblNutritionPlans.Add(nutrition);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nutrition.nutritionID }, nutrition);
        }
        [ActionName("deleteNutritionByID")]
        [ResponseType(typeof(tblNutritionPlan))]
        public IHttpActionResult DeleteNutrition(int id)
        {
            tblNutritionPlan nutrition = db.tblNutritionPlans.Find(id);
            if (nutrition == null)
            {
                return NotFound();
            }

            db.tblNutritionPlans.Remove(nutrition);
            db.SaveChanges();

            return Ok(nutrition);
        }
        [ActionName("updateNutritionByID")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblUser(int id, tblNutritionPlan tblNutritionPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblNutritionPlan.userID)
            {
                return BadRequest();
            }

            db.Entry(tblNutritionPlan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblNutritionPlanExists(id))
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

        private bool tblNutritionPlanExists(int id)
        {
            return db.tblNutritionPlans.Count(e => e.nutritionID == id) > 0;
        }
    }
   
}
