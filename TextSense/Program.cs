using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using TextSense.Components;
using TextSense.Configuration;
using TextSense.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<OpenAiOptions>(
    builder.Configuration.GetSection(OpenAiOptions.SectionName));
builder.Services.Configure<AzureLanguageOptions>(
    builder.Configuration.GetSection(AzureLanguageOptions.SectionName));

builder.Services.AddHttpClient(OpenAiTextGenerationService.HttpClientName, (sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<OpenAiOptions>>().Value;
    if (!string.IsNullOrWhiteSpace(options.Endpoint))
        client.BaseAddress = new Uri(options.Endpoint);
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", options.ApiKey);
});

builder.Services.AddHttpClient(AzureTextAnalysisService.HttpClientName, (sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<AzureLanguageOptions>>().Value;
    if (!string.IsNullOrWhiteSpace(options.Endpoint))
        client.BaseAddress = new Uri(options.Endpoint);
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", options.ApiKey);
});

builder.Services.AddScoped<ITextGenerationService, OpenAiTextGenerationService>();
builder.Services.AddScoped<ITextAnalysisService, AzureTextAnalysisService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();