namespace HappyThoughts.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using HappyThoughts.Data;
    using HappyThoughts.Data.Common;
    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Data.Seeding;
    using HappyThoughts.Services;
    using HappyThoughts.Services.Data;
    using HappyThoughts.Services.Data.Categories;
    using HappyThoughts.Services.Data.Comments;
    using HappyThoughts.Services.Data.Forums;
    using HappyThoughts.Services.Data.Messages;
    using HappyThoughts.Services.Data.Replies;
    using HappyThoughts.Services.Data.TopicReports;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Services.Data.UserReports;
    using HappyThoughts.Services.Data.Users;
    using HappyThoughts.Services.Data.UsersFollowers;
    using HappyThoughts.Services.Data.Votes;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Services.Messaging;
    using HappyThoughts.Web.Hubs;
    using HappyThoughts.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            Account cloudinaryCredentials = new Account(
                this.configuration["Cloudinary:CloudName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddSignalR();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // CSRF
            });
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ITopicsService, TopicsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<ITopicReportsService, TopicReportsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRepliesService, RepliesService>();
            services.AddTransient<IVotesService, VotesService>();
            services.AddTransient<IUserReportsService, UserReportsService>();
            services.AddTransient<IMessagesService, MessagesService>();
            services.AddTransient<IUsersFollowersService, UsersFollowersService>();
            services.AddTransient<IForumsService, ForumsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/Home/HttpError?statusCode={0}"); // Middleware for 404 error page
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("/chat");
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
