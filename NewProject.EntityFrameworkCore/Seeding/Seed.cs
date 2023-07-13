using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewProject.Domain;

namespace NewProject.EntityFrameworkCore.Seeding
{
    public class DataInitialize
    {
        public static async Task Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                
                MainDbContext context = serviceScope.ServiceProvider.GetService<MainDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Person>>();
                ILogger<DataInitialize>? logger = applicationBuilder.ApplicationServices.GetRequiredService<ILogger<DataInitialize>>();
                
                await DataMigration(context, logger);
                
                if (!await context.Roles.AnyAsync())
                {
                    string[] roles = new string[] { "Admin", "User" };
                    if (!context.Roles.Any())
                    {
                        foreach (string role in roles)
                        {
                            if (!context.Roles.Any(r => r.Name == role))
                            {
                                try
                                {
                                    await roleManager.CreateAsync(new IdentityRole(role));
                                }
                                catch (Exception ex)
                                {
                                    logger.LogError(ex, ex.Message);
                                }
                            }
                        }
                    }
                }
                if (!await  context.Addresses.AnyAsync())
                {
                    var addresses = new List<Address>()
                    {
                        new Address()
                        {
                            address = "15 Mustafa Al-Nahas",
                            City = "Nasr City"
                        },
                        new Address()
                        {
                            address = "8 Makram Ebeid",
                            City = "Nasr City"
                        },
                        new Address()
                        {
                            address = "11 Al Tayaran Street",
                            City = "Nasr City"
                        },
                        new Address()
                        {
                            address = "10 Talaat Harb Street",
                            City = "Cairo Downtown"
                        },
                        new Address()
                        {
                            address = "Masr W-el Sudan",
                            City = "Cairo"
                        },
                        new Address()
                        {
                            address = "Qasr El Eyni Street",
                            City = "Cairo Downtown"
                        }
                    };
                    
                    context.Addresses.AddRange(addresses);
                }
                if (!await context.Users.AnyAsync())
                {
                    if (await userManager.FindByEmailAsync("Mohamed@asp.net") is null)
                    {
                        Person person = new Person()
                        {
                            UserName = "Mohamed",
                            Email = "Mohamed@asp.net",
                            PhoneNumber = "01234567890",
                            FullName = "mohamed_person",
                            Address = new Address()
                            {
                                address = "15 Ahmed Orabi",
                                City = "Maadi"
                            }
                        };
                        try
                        {
                            var userRes = await userManager.CreateAsync(person, "P@ssw0rd");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"An error occured During Add Person {person.UserName}");
                        }
                        
                    }
                    
                    if (await userManager.FindByEmailAsync("Ahmed@asp.net") is null)
                    {
                        Person person = new Person()
                        {
                            UserName = "Ahmed",
                            Email = "Ahmed@asp.net",
                            PhoneNumber = "01234567891",
                            FullName = "ahmed_person",
                            Address = new Address()
                            {
                                address = "10 Abbas El-Akkad",
                                City = "Nasr City"
                            }
                        };
                        try
                        {
                            var userRes = await userManager.CreateAsync(person, "P@ssw0rd");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"An error occured During Add Person {person.UserName}");
                        }
                    }
                }
            }
        }
        public static async Task<bool> DataMigration(MainDbContext context, ILogger<DataInitialize> logger)
        {
            try
            {
                await context.Database.MigrateAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured During Migration");
                return false;
            }
        }
        
    }

}
