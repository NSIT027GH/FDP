using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDP.Shared.ResponseModelClass
{
    public class UserDetailsWithAddressResponseModel
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
