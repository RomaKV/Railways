using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntitySql.Entities
{
   public class Railway
   {
        public Railway()
        {

            Stations = new HashSet<Station>();

        }


        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public short Code { get; set; }

        public string ShortName { get; set; }

        public int CountryId { get; set; }
        public string TelegraphName { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public virtual ICollection<Station> Stations { get; set; }
   }
}
