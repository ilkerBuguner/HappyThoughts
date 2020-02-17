using HappyThoughts.Data;
using System;

namespace TestDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var db = new HappyThoughtsDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
