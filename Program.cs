using StudentManagementSystem.Clients;
using StudentManagementSystem.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var studentInfoStoreApiUrl = builder.Configuration["studentInfoStoreApiUrl"]; // Reads URL from JSON file

builder.Services.AddSingleton<LoginClient>();

builder.Services.AddHttpClient<LoginClient>(
    client => client.BaseAddress = new Uri(studentInfoStoreApiUrl!)); // Adds URI for backend DB

builder.Services.AddHttpClient<CourseManagement>(
    client => client.BaseAddress = new Uri(studentInfoStoreApiUrl!));

builder.Services.AddHttpClient<StudentManagement>(
    client => client.BaseAddress = new Uri(studentInfoStoreApiUrl!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

System.IO.File.WriteAllText("loggedInDetails.csv", string.Empty); // Clears contents of file used to sstore logged in user

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
