
using Autofac;
using Autofac.Extensions.DependencyInjection;
using PlacementApi.Db;
using PlacementApi.Dto;
using PlacementApi.Repo;

namespace PlacementApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.RegisterType<PlacementRepo>().As<IPlacementRepo>();
                cb.Register(c=> new AppDbContext(cfg.GetConnectionString("Db"))).InstancePerDependency();
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
