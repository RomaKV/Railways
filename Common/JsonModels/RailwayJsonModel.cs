using System;
using System.Collections.Generic;
using System.Text;

namespace JsonModels
{
   public class RailwayJsonModel
   {
      public int ID { get; set; }
      public short Code { get; set; }

      public  string Name { get; set; }
      public string ShortName { get; set; }

      public int CountryID { get; set; }
      public string TelegraphName { get; set; }
      public DateTime DateCreate { get; set; }
      public DateTime DateUpdate { get; set; }
   }
}
