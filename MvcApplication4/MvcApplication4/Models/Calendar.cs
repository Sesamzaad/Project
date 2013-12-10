using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace MvcApplication4.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public string titel { get; set; }
        public DateTime datum { get; set; }
         public TimeSpan start { get; set; }
         public TimeSpan einde { get; set; }
         public string opdrachten { get; set; }
         public string modus { get; set; }
         public string note { get; set; }

        public class CalendarDBContext : DbContext
        {
            public DbSet<Calendar> CalendarEvents { get; set; }

        }



    }
}