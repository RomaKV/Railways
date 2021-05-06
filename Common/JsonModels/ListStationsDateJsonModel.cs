using System;
using System.Collections.Generic;
using System.Text;

namespace JsonModels
{
    public class ListStationsDateJsonModel
    {
        public IEnumerable<StationJsonModel> NewData { get; set; }
       
        public IEnumerable<StationJsonModel> ChangedData { get; set; }
       
        public DateTime DateCreate { get; set; }

        public DateTime DateUpdate { get; set; }

    }
}
