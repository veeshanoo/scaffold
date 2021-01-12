using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace scaffold.Models
{
    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            Database.SetInitializer<DbCtx>(new Initp());
            //Database.SetInitializer<DbCtx>(new CreateDatabaseIfNotExists<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseIfModelChanges<DbCtx>());
            //Database.SetInitializer<DbCtx>(new DropCreateDatabaseAlways<DbCtx>());
        }
        public DbSet<Film> Film { get; set; }
        public DbSet<Recenzie> Recenzie { get; set; }
    }

    public class Initp : DropCreateDatabaseAlways<DbCtx>
    {
        protected override void Seed(DbCtx ctx)
        {
            ctx.Film.Add(new Film { IDFilm = 1, Denumire = "Jurrasic Park" });
            ctx.Film.Add(new Film { IDFilm = 2, Denumire = "Godfather" });
            ctx.Film.Add(new Film { IDFilm = 3, Denumire = "Good fellas" });

            ctx.Recenzie.Add(new Recenzie
            {
                IDRecenzie = 1,
                Titlu = "titlu 1",
                Nota = 4,
                IDFilm = 1,
            });

            ctx.Recenzie.Add(new Recenzie
            {
                IDRecenzie = 2,
                Titlu = "titlu 2",
                Nota = 3,
                IDFilm = 1,
            });

            ctx.Recenzie.Add(new Recenzie
            {
                IDRecenzie = 3,
                Titlu = "titlu 3",
                Nota = 3,
                IDFilm = 2,
            });

            ctx.Recenzie.Add(new Recenzie
            {
                IDRecenzie = 4,
                Titlu = "titlu 4",
                Nota = 5,
                IDFilm = 3,
            });

            ctx.SaveChanges();
            base.Seed(ctx);
        }
    }
}