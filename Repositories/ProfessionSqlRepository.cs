using FundingDashboardAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FundingDashboardAPI.Repositories
{
    public class ProfessionSqlRepository : IProfessionRepository
    {
        private readonly AppDbContext db = null;

        public ProfessionSqlRepository(AppDbContext db)
        {
            this.db = db;
        }

        public void Delete(int professionId)
        {
            int count = db.Database.ExecuteSqlRaw("DELETE FROM Professions WHERE ProfessionId={0}", professionId);
        }

        public void Insert(Professions profession)
        {
            int count = db.Database.ExecuteSqlRaw("INSERT INTO Professions(ProfessionId, Name) VALUES({0})", profession.Name);

        }

        public List<Professions> SelectAll()
        {
            List<Professions> data = db.Professions.FromSqlRaw("SELECT ProfessionID, Name FROM Professions ORDER BY ProfessionID ASC").ToList();
            return data;
        }

        public Professions SelectByID(int professionId)
        {
            Professions profession = db.Professions.FromSqlRaw("SELECT ProfessionID, Name FROM Professions WHERE ProfessionID={0}", professionId).SingleOrDefault();
            return profession;
        }

        public void Update(Professions profession)
        {
            int count = db.Database.ExecuteSqlRaw("UPDATE Professions SET Name={0} WHERE ProfessionId={1}", profession.Name, profession.ProfessionId);
        }
    }
}
