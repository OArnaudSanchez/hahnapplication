using Hahn.ApplicatonProcess.July2021.Domain.Common;

namespace Hahn.ApplicatonProcess.July2021.Domain.Models
{
    public class UserAsset : BaseEntity
    {
        public string IdAsset { get; set; }
        public int? IdUser { get; set; }
    }
}
