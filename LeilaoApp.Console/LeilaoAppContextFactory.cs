using LeilaoApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeilaoAppConsole
{
    class LeilaoAppContextFactory : IDesignTimeDbContextFactory<LeilaoAppDbContext>
    {
        public LeilaoAppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LeilaoAppDbContext>();
            builder.UseSqlServer(Program.SqlConnectionString);
            return new LeilaoAppDbContext(builder.Options);
        }
    }

}
