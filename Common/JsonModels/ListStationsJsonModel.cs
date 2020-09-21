using System;
using System.Collections.Generic;
using System.Text;

namespace JsonModels
{
   public class ListStationsJsonModel
   {
        public IEnumerable<StationJsonModel> Data { get; set; }
        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }
    }
}
