using BasketballShopClient;
using BasketballShopClient.Authentication;
using BasketballShopClient.Services;
using BasketballShopSharedLibrary.Contracts;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProduct, ClientServices>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IUserAccountService, ClientServices>();
builder.Services.AddSingleton<LoadingService>();

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();


await builder.Build().RunAsync();
