var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<DAL.Repositories.IFilmModelRepository>(provider => new DAL.Repositories.FilmModelRepository(connectionString));
builder.Services.AddScoped<serviceLibary.Services.FilmModel>();
builder.Services.AddScoped<DAL.Repositories.ISerieRepository>(provider => new DAL.Repositories.SerieRepository(connectionString));
builder.Services.AddScoped<serviceLibary.Services.SerieService>();
builder.Services.AddScoped<DAL.Repositories.IGenreRepository>(provider => new DAL.Repositories.GenreRepository(connectionString));
builder.Services.AddScoped<serviceLibary.Services.GenreService>();
builder.Services.AddScoped<DAL.Repositories.IWatchingListRepository>(provider => new DAL.Repositories.WatchingListRepository(connectionString));
builder.Services.AddScoped<serviceLibary.Services.WatchingListService>();
builder.Services.AddScoped<DAL.Repositories.IWatchedListRepository>(provider => new DAL.Repositories.WatchedListRepository(connectionString));
builder.Services.AddScoped<serviceLibary.Services.WatchedListService>();
builder.Services.AddScoped<DAL.Repositories.IUserRepository>(provider => new DAL.Repositories.UserRepository(connectionString));
builder.Services.AddScoped<serviceLibary.Services.UserService>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FilmModel}/{action=Index}/{id?}");
app.Run();