using Quartz;
using Quartz.Cronjob.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Quartz services
builder.Services.AddQuartz(opts =>
{
    var cronjob1Key = new JobKey("Cronjob1");
    opts.UseMicrosoftDependencyInjectionJobFactory();
    opts.AddJob<Cronjob1>(job => // Define the job
    {
        job.WithIdentity(cronjob1Key);
        //job.StoreDurably(); // Make job persistent (optional)
    });

    opts.AddTrigger(trigger => // Define the cron trigger
    {
        trigger.ForJob(cronjob1Key)
            .WithIdentity("Cronjob1-trigger")
            .WithCronSchedule("0 0/1 * 1/1 * ? *"); // every minute
    });

    // Add Cronjob2 definition
    var cronjob2Key = new JobKey("Cronjob2");
    opts.AddJob<Cronjob2>(job => // Replace with your actual job class name
    {
        job.WithIdentity(cronjob2Key);
        //job.StoreDurably(); // Make job persistent (optional)
    });

    // Add cron trigger for Cronjob2
    opts.AddTrigger(trigger =>
    {
        trigger.ForJob(cronjob2Key) // Identify by job name
            .WithIdentity("Cronjob2-trigger")
            .WithCronSchedule("0 0/2 * 1/1 * ? *"); // Set your desired cron expression
    });
});

// Add Quartz startup service to automatically start jobs on application startup
builder.Services.AddQuartzHostedService();

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
