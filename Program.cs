using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmloyeeManagementSystem.Data;
using EmloyeeManagementSystem.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EMSContextConnection") ?? throw new InvalidOperationException("Connection string 'EMSContextConnection' not found.");

builder.Services.AddDbContext<EMSContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<EMSUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EMSContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.MapRazorPages();

using(var scope = app.Services.CreateScope()){
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Employee" };

    foreach(var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<EMSUser>>();

    string FirstName = "adminFirst";
    string LastName = "adminLast";
    string email = "admin@admin.com";
    string password = "Admin@123";
   
    if(await userManager.FindByEmailAsync(email)== null)
    {
        var user = new EMSUser();
        user.UserName = email;
        user.Email = email;
        user.FirstName = FirstName;
        user.LastName = LastName;

        await userManager.CreateAsync(user,password);

        await userManager.AddToRoleAsync(user, "Admin");
    }

    string employeeFirstName = "employeeFirst";
    string employeeLastName = "employeeLast";
    string employeeEmail = "employee@employee.com";
    string employeePassword = "Employee@123";

    if (await userManager.FindByEmailAsync(employeeEmail) == null)
    {
        var user2 = new EMSUser();
        user2.UserName = employeeEmail;
        user2.Email = employeeEmail;
        user2.FirstName = employeeFirstName;
        user2.LastName = employeeLastName;

        await userManager.CreateAsync(user2, employeePassword);

        await userManager.AddToRoleAsync(user2, "Employee");
    }

}



app.Run();
