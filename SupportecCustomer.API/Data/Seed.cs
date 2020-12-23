using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Data
{
    public class Seed
    {

        public static void SeedUsers(DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }

        public static void SeedItems(DataContext context)
        {
            if (!context.Items.Any())
            {
                var itemsData = System.IO.File.ReadAllText("Data/ItemSeedData.json");
                var items = JsonConvert.DeserializeObject<List<Item>>(itemsData);

                foreach (var item in items)
                {
                    context.Items.Add(item);
                }
                context.SaveChanges();
            }
        }

        public static void SeedProducts(DataContext context)
        {
            if (!context.Products.Any())
            {
                List<Item> items = new List<Item>();

                items.Add(context.Items.Find(1));
                items.Add(context.Items.Find(3));
                items.Add(context.Items.Find(7));

                Product product = new Product()
                {
                    Name = "Tessy",
                    PhotoUrl = "http://supportec.nl/wp-content/uploads/2017/02/stoel009.jpg",
                    UserId = 1
                };

                context.Products.Add(product);
                context.SaveChanges();
                var productItems = new List<ProductItem>();

                foreach (var Item in items)
                {
                    productItems.Add(new ProductItem()
                    {
                        ProductId = 1,
                        ItemId = Item.Id
                    });
                }
                context.ProductItem.AddRange(productItems);
                context.SaveChanges();
            }

        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}