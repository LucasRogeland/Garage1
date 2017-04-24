using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage1.ViewModel
{
    public class indexViewModel
    {  
        public indexViewModel()
        {
            Success = false;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}