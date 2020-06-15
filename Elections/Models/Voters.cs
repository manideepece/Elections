using System;
using System.Collections.Generic;

namespace Elections.Models
{
    public partial class Voters
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
