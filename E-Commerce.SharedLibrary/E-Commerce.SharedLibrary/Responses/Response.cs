using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.SharedLibrary.Responses
{
// this Response will be used in all Services
    public record Response(bool Flag = false , string Message = null!);
    
}
