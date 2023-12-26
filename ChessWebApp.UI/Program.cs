using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ChessWebApp.UI;
using ChessWebApp.UI.Services;
using Blazored.LocalStorage;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("Api:UriHttps"))
});
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBoardService, BoardService>();

await builder.Build().RunAsync();