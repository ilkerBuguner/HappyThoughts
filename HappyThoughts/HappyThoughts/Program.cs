using HappyThoughts.Data;
using System;

namespace HappyThoughts
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HappyThoughtsDbContext db = new HappyThoughtsDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
