using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Front.Models
{
    public class SchoolDBContext: DbContext
    {
        public DbSet<Student> Students { get; set; }

    }

}