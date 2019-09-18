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
using DataLayer;

namespace WebApplication2.Controllers
{
    public class AuthorsApiController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/AuthorsApi
        public IQueryable<Authors> GetAuthors()
        {
            return db.Authors;
        }

        // GET: api/AuthorsApi/5
        [ResponseType(typeof(Authors))]
        public IHttpActionResult GetAuthors(int id)
        {
            Authors authors = db.Authors.Find(id);
            if (authors == null)
            {
                return NotFound();
            }

            return Ok(authors);
        }

        // PUT: api/AuthorsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthors(int id, Authors authors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != authors.Id)
            {
                return BadRequest();
            }

            db.Entry(authors).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorsExists(id))
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

        // POST: api/AuthorsApi
        [ResponseType(typeof(Authors))]
        public IHttpActionResult PostAuthors(Authors authors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Authors.Add(authors);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = authors.Id }, authors);
        }

        // DELETE: api/AuthorsApi/5
        [ResponseType(typeof(Authors))]
        public IHttpActionResult DeleteAuthors(int id)
        {
            Authors authors = db.Authors.Find(id);
            if (authors == null)
            {
                return NotFound();
            }

            db.Authors.Remove(authors);
            db.SaveChanges();

            return Ok(authors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorsExists(int id)
        {
            return db.Authors.Count(e => e.Id == id) > 0;
        }
    }
}