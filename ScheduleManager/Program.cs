using Microsoft.EntityFrameworkCore;
using ScheduleManager.Logics.CheckValidRecord;
using ScheduleManager.Logics.File;
using ScheduleManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ReadFile>();
builder.Services.AddDbContext<ScheduleManagerContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
