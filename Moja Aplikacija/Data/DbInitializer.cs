using Moja_Aplikacija.Entity;

namespace Moja_Aplikacija.Data
{
    public class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            var created =context.Database.EnsureCreated();
            if (!created)
            {
                return;
            }
            
            var genres = new Genre[]
            {
                new Genre{Name = "Roman",},
                new Genre{ Name ="Fikcija"},
                new Genre{ Name ="Drama"}
            };
            context.Genre.AddRange(genres);
            context.SaveChanges();

            var writers = new Writer[]
            {
                new Writer{FirstName = "Branko",LastName = "Copic",ImageName="Branko.jfif"},
                new Writer{FirstName ="Fjord",LastName = "Dostojevski",ImageName="Fjord.jfif"},
                new Writer{FirstName = "Ivo",LastName = "Andric", ImageName = "Andric.jfif"},
                new Writer{FirstName = "Pet",LastName = "Barker", ImageName = "Pet.jfif"}
            };
            context.Writer.AddRange(writers);
            context.SaveChanges();

            var books = new Book[]
            {
                new Book{
                    Name = "Orlovi rano lete",
                    Writer = context.Writer.FirstOrDefault(p => p.WriterId == 1),
                    YearIssued = 1957,
                    Genre = context.Genre.FirstOrDefault(p => p.GenreId == 1),
                    Active = true,
                    ImageName="orlovi_rano_lete.jfif"

                },
                new Book
                {
                    Name = "Price o Niku",
                    Writer = context.Writer.FirstOrDefault(p => p.WriterId == 1),
                    YearIssued = 1956,
                    Genre= context.Genre.FirstOrDefault(p => p.GenreId == 2),
                    Active = true,
                    ImageName="price_o_niku.jfif"

                },
                new Book
                {
                    Name = "Ponizeni i uvredjeni",
                    Writer = context.Writer.FirstOrDefault(p => p.WriterId == 2),
                    YearIssued = 1956,
                    Genre= context.Genre.FirstOrDefault(p => p.GenreId == 2),
                    Active = true,
                    ImageName="ponizeni_i_uvredjeni.jfif"

                },
                new Book
                {
                    Name = "Na Drini cuprija",
                    Writer = context.Writer.FirstOrDefault(p => p.WriterId == 3),
                    YearIssued = 1956,
                    Genre= context.Genre.FirstOrDefault(p => p.GenreId == 1),
                    Active = true,
                    ImageName="na_drini_cuprija.jfif"

                },
                new Book
                {
                    Name = "Cutanje devojaka",
                    Writer = context.Writer.FirstOrDefault(p => p.WriterId == 4),
                    YearIssued = 1956,
                    Genre= context.Genre.FirstOrDefault(p => p.GenreId == 3),
                    Active = true,
                    ImageName="cutanje_devojke.jfif"

                },
            };
            context.Book.AddRange(books);
            context.SaveChanges();

            var user = new Role {  Name = "User", Active = true };
            var admin = new Role {  Name = "Admin", Active = true };
            var roles = new Role[]
            {
               admin,
               user
            };
            context.Role.AddRange(roles);
            context.SaveChanges();

            var users = new User[]
            {
                 new User{
                        FirstName = "Stefan",
                        LastName = "Reljic",
                        UserName = "relja24",
                        Password = "stefan",
                        Email = "stefan@gmail.com",
                        Active = true,
                        UserRole = new List<UserRole>{ new UserRole { Role = admin }  }
                    },
                     new User{
                        FirstName = "Nikola",
                        LastName = "Nikolic",
                        UserName = "nikola12",
                        Password = "nikola12",
                        Email = "korisnik@nesto.com",
                        Active = true,
                        UserRole = new List<UserRole>{ new UserRole { Role = user }  }
                    }
            };
            context.User.AddRange(users);
            context.SaveChanges();
        }
    }
}
