using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Data
{
    public class webContext : DbContext
    {
        public webContext (DbContextOptions<webContext> options)
            : base(options)
        {
        }

        public DbSet<web.Models.Maluco> Maluco { get; set; }
    }
}
