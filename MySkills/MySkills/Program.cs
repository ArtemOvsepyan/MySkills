var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<MySkills.DAL.IDbSessionDAL, MySkills.DAL.DbSessionDAL>();
builder.Services.AddSingleton<MySkills.DAL.IAuthDAL, MySkills.DAL.AuthDAL>();
builder.Services.AddSingleton<MySkills.DAL.IUserTokenDAL, MySkills.DAL.UserTokenDAL>();
builder.Services.AddSingleton<MySkills.DAL.IProfileDAL, MySkills.DAL.ProfileDAL>();
builder.Services.AddSingleton<MySkills.DAL.ISkillDAL, MySkills.DAL.SkillDAL>();

builder.Services.AddScoped<MySkills.BL.Auth.IAuth, MySkills.BL.Auth.Auth>();
builder.Services.AddSingleton<MySkills.BL.Auth.IEncrypt, MySkills.BL.Auth.Encrypt>();
builder.Services.AddScoped<MySkills.BL.Auth.ICurrentUser, MySkills.BL.Auth.CurrentUser>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<MySkills.BL.Auth.IDbSession, MySkills.BL.Auth.DbSession>();
builder.Services.AddScoped<MySkills.BL.General.IWebCookie, MySkills.BL.General.WebCookie>();
builder.Services.AddSingleton<MySkills.BL.Profile.IProfile, MySkills.BL.Profile.Profile>();
builder.Services.AddSingleton<MySkills.BL.Resume.IResume, MySkills.BL.Resume.Resume>();
builder.Services.AddSingleton<MySkills.BL.Profile.ISkill, MySkills.BL.Profile.Skill>();

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.UseBlazorFrameworkFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

MySkills.DAL.DbHelper.ConnString = app.Configuration.GetConnectionString("DefualtConnection") ?? "";

app.Run();
