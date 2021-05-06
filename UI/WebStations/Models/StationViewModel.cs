using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStations.Models
{
    [Serializable]
    public class StationViewModel
    {
        [Display (Name = "Код станции")]
        public int Code { get; set; }

        [Display(Name = "Название станции")]
        public string Name { get; set; }

        [Display(Name = "Название ж/д")]
        public string NameRailway { get; set; }

        [Display(Name = "Дата обновления")]
        public DateTime DateUpdate { get; set; }
    }
}
