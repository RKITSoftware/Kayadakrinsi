using Middleware.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adds service for custom middleware
builder.Services.AddTransient<CustomMiddleware>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Branches middleware pipeline
app.Map("/win", winHandler);

app.Map("/lost", lostHandler);

// Uses custom middleware
app.UseMiddleware<CustomMiddleware>();

app.UseMiddleware<MyMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Handler for win branch
void winHandler(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("You won!");
    });
}

// Handler for lost branch
void lostHandler(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("You lost!");
    });
}























//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}