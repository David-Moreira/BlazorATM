using ATM;
using ATM.Core.Facade;
using ATM.Core.Interfaces.Services;
using ATM.Core.Interfaces.Validation;
using ATM.Core.Interfaces;
using ATM.Core.Services;
using ATM.Core.Validation;
using ATM.Infrastructure.Data;
using ATM.Infrastructure.Repositories;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddDbContext<ATMDBContext>(options => options.UseInMemoryDatabase("ATMDb"));

RegisterAuthServices(builder.Services);
RegisterAppServices(builder.Services);

await builder.Build().RunAsync();

void RegisterAuthServices(IServiceCollection services)
{

    services.AddOidcAuthentication(options =>
    {
        // Configure your authentication provider options here.
        // For more information, see https://aka.ms/blazor-standalone-auth
        builder.Configuration.Bind("Local", options.ProviderOptions);
    });

}

void RegisterAppServices(IServiceCollection services)
{
    services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
    services.AddScoped<IBankAccountRepo, BankAccountRepo>();
    services.AddScoped<ITransactionRepo, TransactionRepo>();

    services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
    services.AddScoped<IBankAccountService, BankAccountManager>();
    services.AddScoped<ITransactionService, TransactionManager>();
    services.AddScoped<IOperationService, OperationManager>();
    services.AddScoped<IOperationValidator, OperationValidator>();
}
