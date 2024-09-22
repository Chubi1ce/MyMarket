
using Autofac;
using Autofac.Extensions.DependencyInjection;
using StoragesApi.Db;
using StoragesApi.Dto;
using StoragesApi.Repositories;

namespace StoragesApi
{
    public class Program
    {
        public static WebApplication BuilderWebApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(cb => 
            {
                cb.RegisterType<StorageRepository>().As<IStorageRepository>();
                cb.Register(c => new AppDbContext(cfg.GetConnectionString("Db"))).InstancePerDependency();
            });

            return builder.Build();
        }

        public static void Main(string[] args)
        {
            var app = BuilderWebApp(args);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            AppContext.SetSwitch("Npgsql.EnableLegasyTimestampBehavior", true);

            app.Run();
        }
    }
}
