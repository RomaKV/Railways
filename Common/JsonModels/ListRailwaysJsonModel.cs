using System;
using System.Collections.Generic;
using System.Text;

namespace JsonModels
{
   public class ListRailwaysJsonModel
   {
       public IEnumerable<RailwayJsonModel> Data { get; set; }
       public DateTime DateCreate { get; set; }

       public DateTime DateUpdate { get; set; }
   }
}
