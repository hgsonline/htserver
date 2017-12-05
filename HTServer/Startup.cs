using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using HTServer.Filters;
using HTServer.Models; 
using Microsoft.EntityFrameworkCore;
using HTServer.Commonlibary;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace HTServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.Configure<MyConfigReader>(Configuration);
            // Not Requried as of now Install - Package MySql.Data.EntityFrameworkCore - Version 8.0.8 - 
            // for linux using docker ip addresss to be used
            // "myConnectionString": "server=192.168.43.164;port=3306; user=root; password=myroot; database=htdatabase;SslMode=none"
            // "myConnectionString": "server=localhost;port=3306; user=root; password=myroot; database=htdatabase;SslMode=none"
            // "myConnectionString": "server=healthtrustdb.c99mihenwkah.us-west-2.rds.amazonaws.com;port=3306; user=master; password=Hgso2017; database=htdatabase;SslMode=none"

            var sqlConnectionString = Configuration.GetConnectionString("myConnectionString");

            services.AddDbContext<HTDataContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("myConnectionString"));
            });

            // ********************
            // Setup CORS
            // ********************
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });


            // Register the Swagger generator, defining one or more Swagger documents  
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Health Trust Web API", Version = "v1" });
                c.AddSecurityDefinition("Token", new ApiKeyScheme() { In = "header", Description = "Please insert Token", Name = "Token", Type = "apiKey" });
        });

            services.AddMvc();

            // Read email settings
            services.Configure<EmailConfig>(Configuration.GetSection("Email"));
            // Register email service 
            services.AddTransient<IEmailService, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseDefaultFiles();
           // app.UseStaticFiles();

            //app.UseCors(builder =>
            //builder.AllowAnyOrigin()
            //.AllowAnyHeader()
            //.AllowAnyMethod());

            // ********************
            // USE CORS - might not be required.
            // ********************
            app.UseCors("SiteCorsPolicy");
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.  
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.  
            app.UseSwaggerUI(c =>
            {
              c.SwaggerEndpoint("/swagger/v1/swagger.json", "HT API V1");
              //c.SwaggerEndpoint("/htserver/swagger/v1/swagger.json", "HT API V1");
            });
            
            app.UseMvc();
        }
    }
}
