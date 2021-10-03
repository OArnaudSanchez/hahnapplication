using Hahn.ApplicatonProcess.July2021.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces
{
    public interface IDataAcess
    {
        Task FetchAssets();
        Task<bool> CreateJsonFile(string jsonContent);
        Task<List<Asset>> GetDataFromJsonFile();
    }
}
