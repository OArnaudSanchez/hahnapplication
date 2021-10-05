using Hahn.ApplicatonProcess.July2021.Data.Options;
using Hahn.ApplicatonProcess.July2021.Domain.Exceptions;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Data.DataAccess
{
    public class HttpDataAccess : IDataAcess
    {
        private readonly HttpClient _client;
        private readonly AssetsOptions _assetsOptions;
        private readonly ResourcesOptions _resourcesOptions;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _resourcePath;
        private readonly ILogger _logger;
        public HttpDataAccess(IOptions<AssetsOptions> assetsOptions, IHostingEnvironment hostingEnvironment, IOptions<ResourcesOptions> resourceOptions, ILogger<HttpDataAccess> logger)
        {
            _assetsOptions = assetsOptions.Value;
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri(_assetsOptions.BaseUrl);
            _client.DefaultRequestHeaders.TryAddWithoutValidation(_assetsOptions.ApiAuth, string.Concat(_assetsOptions.AuthKey, _assetsOptions.ApiKey));
            _hostingEnvironment = hostingEnvironment;
            _resourcesOptions = resourceOptions.Value;
            _resourcePath = $"{_hostingEnvironment.ContentRootPath}\\{_resourcesOptions.Folder}\\{_resourcesOptions.ResourceName}{_resourcesOptions.ResourceExtension}";
            _logger = logger;
        }
        public async Task FetchAssets()
        {
            _logger.LogInformation("The Method FetchAssets in the HttpDataAccess class has been accessed");
            if (await GetDataFromJsonFile() == null)
            {
                var response = await _client.GetAsync(_assetsOptions.EndPoint);
                if (!response.IsSuccessStatusCode) throw new BussinessException("Error from https://api.coincap.io/v2/assets 'You exceeded you 200 request(s) rate limit of your free plane' PLEASE TRY AGAIN", 400);
                var jsonContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("The Request to the https://api.coincap.io/v2/assets has been made");
                await CreateJsonFile(jsonContent);
            }
        }

        public async Task<bool> CreateJsonFile(string jsonContent)
        {
            try
            {
                _logger.LogInformation("The Method CreateJsonFile in the HttpDataAccess class has been accessed");

                var directoryPath = $"{_hostingEnvironment.ContentRootPath}\\{_resourcesOptions.Folder}";

                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

                using (var fileStream = File.Open(_resourcePath, FileMode.Create, FileAccess.Write))
                {
                    if (!File.Exists(_resourcePath)) File.Create(_resourcePath);
                    using (var writer = new StreamWriter(fileStream))
                    {
                        await writer.WriteAsync(jsonContent);
                        _logger.LogInformation("The File With json Data has been created");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new BussinessException(ex.Message, 400);
            }
            return true;
        }

        public async Task<List<Asset>> GetDataFromJsonFile()
        {
            _logger.LogInformation("The Method GetDataFromJsonFile in the HttpDataAccess class has been accessed");
            if (!File.Exists(_resourcePath)) return null;
            var assets = JObject.Parse(await File.ReadAllTextAsync(_resourcePath))?.SelectToken("data")?.ToObject<List<Asset>>();
            return assets;
        }
    }
}
