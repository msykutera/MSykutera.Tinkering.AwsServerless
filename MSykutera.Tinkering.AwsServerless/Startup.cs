using Amazon;
using Amazon.DynamoDBv2;
using MSykutera.Tinkering.AwsServerless.Dtos;
using MSykutera.Tinkering.AwsServerless.Repositories;
using MSykutera.Tinkering.AwsServerless.Settings;

namespace MSykutera.Tinkering.AwsServerless;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<DatabaseSettings>(Configuration.GetSection(DatabaseSettings.KeyName));
        services.AddControllers();
        services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(RegionEndpoint.USEast1));
        services.AddSingleton<IRepository<PostDto>, PostRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}