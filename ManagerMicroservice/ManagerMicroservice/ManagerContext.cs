using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagerMicroservice
{
    public class ManagerContext : DbContext
    {


        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {

        }
        public DbSet<Manager> Managers { get; set; }

        public DbSet<Customer> Customers { get; set; }


    }
}



