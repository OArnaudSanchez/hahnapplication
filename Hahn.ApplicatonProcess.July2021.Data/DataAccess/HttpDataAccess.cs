using Hahn.ApplicatonProcess.July2021.Data.Options;
using Hahn.ApplicatonProcess.July2021.Domain.Exceptions;
using Hahn.ApplicatonProcess.July2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.AspNetCore.Hosting;
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
        public HttpDataAccess(IOptions<AssetsOptions> assetsOptions, IHostingEnvironment hostingEnvironment, IOptions<ResourcesOptions> resourceOptions)
        {
            _assetsOptions = assetsOptions.Value;
            _client = new HttpClient();
            _client.BaseAddress = new System.Uri(_assetsOptions.BaseUrl);
            _client.DefaultRequestHeaders.TryAddWithoutValidation(_assetsOptions.ApiAuth, string.Concat(_assetsOptions.AuthKey, _assetsOptions.ApiKey));
            _hostingEnvironment = hostingEnvironment;
            _resourcesOptions = resourceOptions.Value;
            _resourcePath = $"{_hostingEnvironment.ContentRootPath}\\{_resourcesOptions.Folder}\\{_resourcesOptions.ResourceName}{_resourcesOptions.ResourceExtension}";
        }
        public async Task FetchAssets()
        {
            if (await GetDataFromJsonFile() == null)
            {
                var response = await _client.GetAsync(_assetsOptions.EndPoint);
                if (!response.IsSuccessStatusCode) throw new BussinessException("Error from https://api.coincap.io/v2/assets 'You exceeded you 200 request(s) rate limit of your free plane' PLEASE TRY AGAIN", 400);
                var jsonContent = await response.Content.ReadAsStringAsync();
                await CreateJsonFile(jsonContent);
            }
        }

        public async Task<bool> CreateJsonFile(string jsonContent)
        {
            try
            {
                var directoryPath = $"{_hostingEnvironment.ContentRootPath}\\{_resourcesOptions.Folder}";

                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

                using (var fileStream = File.Open(_resourcePath, FileMode.Create, FileAccess.Write))
                {
                    if (!File.Exists(_resourcePath)) File.Create(_resourcePath);
                    using (var writer = new StreamWriter(fileStream))
                    {
                        await writer.WriteAsync(jsonContent);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BussinessException(ex.Message, 400);
            }
            return true;
        }

        public async Task<List<Asset>> GetDataFromJsonFile()
        {
            if (!File.Exists(_resourcePath)) return null;
            var assets = JObject.Parse(await File.ReadAllTextAsync(_resourcePath))?.SelectToken("data")?.ToObject<List<Asset>>();
            return assets;
        }
    }
}
