
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ProductsAndGroupsApi.Db;
using ProductsAndGroupsApi.Dto.Map;
using ProductsAndGroupsApi.Repo;

namespace ProductsAndGroupsApi
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

            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(cb =>
            {
                cb.RegisterType<ProductsAndGroupsRepo>().As<IProductsAndGroupsRepo>();
                cb.Register(c => new AppDbContext(builder.Configuration.GetConnectionString("Db"))).InstancePerDependency();
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
