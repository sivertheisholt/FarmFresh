using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmFresh.Api.DTOs
{
    public class NewUserDto
    {
        public required string UUID { get; set; }
        public required string GroupName { get; set; }
    }
}