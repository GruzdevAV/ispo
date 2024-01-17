using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DbFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using(MDK_02_02_GRUZDEVEntities db = new MDK_02_02_GRUZDEVEntities())
            {
                foreach (var str in new[] { "Саша", "Женя", "Валя", "Вася" })
                {
                    Firstname fn = new Firstname() { Firstname1 = str };
                    db.Firstnames.Add(fn);
                }
                foreach (var str in new[] { "Петросенко", "Журавленко", "Баленко", "Подорожник" })
                {
                    Surname sn = new Surname() { Surname1 = str };
                    db.Surnames.Add(sn);
                }
                db.SaveChanges();
                for (int i = 0; i < 5; i++)
                {
                    var fn = db.Firstnames.OrderBy(x => Guid.NewGuid()).Select(x => x.Id).Take(1).First();
                    var sn = db.Surnames.OrderBy(x => Guid.NewGuid()).Select(x => x.id).Take(1).First();
                    Teacher teacher = new Teacher() { Surname = sn, Firstname = fn };
                    db.Teachers.Add(teacher);
                }
                db.SaveChanges();
                var joinResult = db.Teachers.Join(
                    db.Surnames,
                    t => t.Surname,
                    sn => sn.id,
                    (t, i) => new { jrSurnames = i, jrTeachers = t }
                ).Join(
                    db.Firstnames,
                    t => t.jrTeachers.Firstname,
                    sn => sn.Id,
                    (t, i) => new { jrFirstnames = i, jrSurnames = t.jrSurnames, jrTeachers = t.jrTeachers }
                ).ToList();
                foreach(var line in joinResult)
                {
                    Console.WriteLine("{0}: {1} {2}", line.jrTeachers.Id, line.jrSurnames.Surname1, line.jrFirstnames.Firstname1);
                }
                var rand = db.Teachers
                    .OrderBy(x => Guid.NewGuid())
                    .Take(1)
                    .First();
                db.Teachers.Remove(rand);

                db.SaveChanges();
                 joinResult = db.Teachers.Join(
                    db.Surnames,
                    t => t.Surname,
                    sn => sn.id,
                    (t, i) => new { jrSurnames = i, jrTeachers = t }
                ).Join(
                    db.Firstnames,
                    t => t.jrTeachers.Firstname,
                    sn => sn.Id,
                    (t, i) => new { jrFirstnames = i, jrSurnames = t.jrSurnames, jrTeachers = t.jrTeachers }
                ).ToList();
                foreach (var line in joinResult)
                {
                    Console.WriteLine("{0}: {1} {2}", line.jrTeachers.Id, line.jrSurnames.Surname1, line.jrFirstnames.Firstname1);
                }
                Console.Read();
            }
        }
    }
}
