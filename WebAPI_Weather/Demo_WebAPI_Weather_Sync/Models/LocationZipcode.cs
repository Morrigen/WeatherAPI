using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_WebAPI_Weather
{
    public struct LocationZipcode
    {
        public int Zipcode { get; set; }

        public LocationZipcode(int zipcode)
        {
            this.Zipcode = zipcode;
        }
    }
}
