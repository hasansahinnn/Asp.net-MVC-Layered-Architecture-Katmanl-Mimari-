﻿using DAL.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBContextUsage : DbContext
    {
        public DBContextUsage() : base("DBContextUsage")
        {
        }
        public DbSet<Model1> Model1 { get; set; }
      

    }
}
