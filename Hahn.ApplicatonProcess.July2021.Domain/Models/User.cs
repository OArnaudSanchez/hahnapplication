using Hahn.ApplicatonProcess.July2021.Domain.Common;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.July2021.Domain.Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Assets = new List<Asset>();
        }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<string> AssetName { get; set; }
        public List<Asset> Assets { get; set; }
    }
}
