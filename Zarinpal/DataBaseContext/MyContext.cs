using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Zarinpal
{
    public class MyContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}