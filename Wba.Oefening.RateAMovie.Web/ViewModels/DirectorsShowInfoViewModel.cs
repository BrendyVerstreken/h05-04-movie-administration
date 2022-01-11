using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.Oefening.RateAMovie.Web.ViewModels
{
    public class DirectorsShowInfoViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Movies { get; set; }
        public IEnumerable<long> MovieIds { get; set; }
    }
}
