using System;
using System.Collections.Generic;
using System.Text;

namespace JsonModels
{
   public class ListRailwayDateJsonModel
   {
       public IEnumerable<RailwayJsonModel> NewData { get; set; }

       public IEnumerable<RailwayJsonModel> ChangedData { get; set; }

       public DateTime DateCreate { get; set; }

       public DateTime DateUpdate { get; set; }
    }
}
