namespace HappyThoughts.Services.Data.Tests.Common
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data;
    using HappyThoughts.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class ServiceFactory
    {
        public ServiceFactory()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>
            (
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString())

            );

            IdentityBuilder builder = services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(ApplicationRole), builder.Services);

            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.AddRoleValidator<RoleValidator<ApplicationRole>>();
            builder.AddRoleManager<RoleManager<ApplicationRole>>();
            builder.AddSignInManager<SignInManager<ApplicationUser>>();
            // Taken from https://github.com/aspnet/MusicStore/blob/dev/test/MusicStore.Test/ManageControllerTest.cs (and modified)
            // IHttpContextAccessor is required for SignInManager, and UserManager
            var context = new DefaultHttpContext();
            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor { HttpContext = context });
            var serviceProvider = services.BuildServiceProvider();
            this.Context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            this.UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            this.RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        }

        public ApplicationDbContext Context { get; }

        public UserManager<ApplicationUser> UserManager { get; }

        public RoleManager<ApplicationRole> RoleManager { get; }

        public async Task SeedRoleAsync(string roleName)
        {
            var role = await this.RoleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await this.RoleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

    }
}