﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{


    public class TreeApiDbContext : DbContext
    {


        public TreeApiDbContext(DbContextOptions<TreeApiDbContext> options) : base(options)
        {
        }

        public DbSet<Tree.Model.Node> Nodes { get; set; }
        public DbSet<Diagnostics.Model.MJournal> Journals { get; set; }

    }


    
}
