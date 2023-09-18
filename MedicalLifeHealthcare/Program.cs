using MedicalLifeHealthcare.Core;
using Microsoft.EntityFrameworkCore;
using MedicalLifeHealthcare.Areas.Identity.Data;
using MedicalLifeHealthcare.Core.Repositories;
using MedicalLifeHealthcare.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HelpingHands.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
//emasil services
builder.Services.AddTransient<IEmailSender, EmailSender>();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();



//creating Default Roles
using (var scope = app.Services.CreateScope())
{
    var roleManager =

    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Nurse", "Doctor", "Counsellor", "Pathology","Patient" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))

            await roleManager.CreateAsync(new IdentityRole(role));
    }

}
//Default Admin
using (var scope = app.Services.CreateScope())
{
    var userManager =

    scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string firstName = "Danny";
    string lastName = "James";
    string email = "Daniel.enompilo.healthcare@gmail.com";
    string password = "Daniel@2023";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new ApplicationUser();
        user.FirstName = firstName; user.LastName = lastName;
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;

        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
    //hcek if the user has  role ad assign it to them if the dont have/
    else
    {
        //get the ID of the user to ch
        var admin = await userManager.FindByEmailAsync(email);
        //check if theadmin has role
        var hasRole = await userManager.IsInRoleAsync(admin, "Admin");
        if (!hasRole)
        {
            //if teh user doesnt has a role we assign the role of the admin
            await userManager.AddToRoleAsync(admin, "A");
        }
    }

}
app.Run();