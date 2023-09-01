
using API_key_authentication.Authentication;

namespace API_key_authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(/*x => x.Filters.Add<ApiKeyFilter>())*/);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ApiKeyFilter>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(setup =>
                { 

                });
            }

            app.UseHttpsRedirection();

            //app.UseMiddleware<ApiKeyMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}