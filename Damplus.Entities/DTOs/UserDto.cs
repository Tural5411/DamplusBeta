using Damplus.Shared.Entities.Abstract;
using Damplus.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class UserDto:DtoGetBase
    {
        public User User { get; set; }
    }
}
