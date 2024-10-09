using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public record UserSession(string? Id, string? Name, string? Email, string? Role);
}