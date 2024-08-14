using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projeto_API_Power_BI.Models;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace Projeto_API_Power_BI.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

public async Task<IActionResult> Index()
{
    try
    {
        string workspaceId = _configuration["PowerBI:WorkspaceId"];
        string reportId = _configuration["PowerBI:ReportId"];
        string embedToken = await GetEmbedToken(workspaceId, reportId);
        ViewBag.EmbedToken = embedToken;
        ViewBag.ReportId = reportId;
        ViewBag.EmbedUrl = $"https://app.powerbi.com/reportEmbed?reportId={reportId}&groupId={workspaceId}";
        return View();
    }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<string> GetEmbedToken(string workspaceId, string reportId)
    {
        string clientId = _configuration["PowerBI:ApplicationId"];
        string clientSecret = _configuration["PowerBI:ApplicationSecret"];
        string tenantId = _configuration["PowerBI:TenantId"];
        string authorityUri = $"https://login.microsoftonline.com/{tenantId}";

        var clientApp = ConfidentialClientApplicationBuilder.Create(clientId)
            .WithClientSecret(clientSecret)
            .WithAuthority(authorityUri)
            .Build();

        var authResult = await clientApp.AcquireTokenForClient(new[] {"https://analysis.windows.net/powerbi/api/.default"}).ExecuteAsync();

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

        var generateEmbedTokenUrl = $"https://api.powerbi.com/v1.0/myorg/groups/{workspaceId}/reports/{reportId}/generateToken";
        var response = await client.PostAsync(generateEmbedTokenUrl, new StringContent("{\"accessLevel\":\"View\"}", System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
            return data.token;
        }

        throw new Exception("Falha ao gerar o Embed Token");
    }
}
