using System;
using System.Collections.Generic;
using System.Text;

namespace JsonModels
{
    public class StationJsonModel
    {
        public int Code { get; set; }
        public int ID { get; set; }
        public int? RailwayDepartmentID { get; set; }
        public int? RailwayID { get; set; }
        public int? CountryID { get; set; }
        public string Name12Char { get; set; }
        public string Name { get; set; }
        public bool FreightSign { get; set; }
        public string CodeOSGD { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }

}
