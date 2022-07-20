
﻿using System.ComponentModel.DataAnnotations;
﻿using UnitOfMeasures.Application.Models.Enums;

namespace UnitOfMeasures.Application.Models.DTO
{
    public class UnitsDTO
    {
        [Required]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Required]
        public UnitTypeEnum Type { get; set; }

    }
}
