

using System;
using System.ComponentModel.DataAnnotations;

namespace EntitySql.Entities
{
    public class Station
    {
       
        public int Id { get; set; }

        [Key]
        public int Code { get; set; }
        public int? RailwayDepartmentID { get; set; }
        public int? RailwayId { get; set; }
        public int? CountryId { get; set; }

        [MaxLength(12)]
        public string Name12Char { get; set; }

        [MaxLength(40)]
        public string Name { get; set; }
        public bool FreightSign { get; set; }
        public string CodeOSGD { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public virtual Railway Railway { get; set; }



    }
}
