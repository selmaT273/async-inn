﻿using System;
using async_inn.Models;
using Microsoft.EntityFrameworkCore;

namespace async_inn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        // Allow context to be configured by magic
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
    }
}
