using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace Nahang.Data.Models
{
    public class User : IdentityUser<int>
    {
        new public int Id { get; set; }
    }
    public class Role : IdentityRole<int>
    {
        new public int Id { get; set; }
    }
}
