<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
using UnitOfMeasures.Application.Models.Enums;
=======
﻿using UnitOfMeasures.Application.Models.Enums;
>>>>>>> 925b1b0ff0c6466f1652976c58b895b8f73bf17c

namespace UnitOfMeasures.Application.Models.DTO
{
    public class UnitsDTO
    {
<<<<<<< HEAD
        [Required]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Required]
        public UnitTypeEnum Type { get; set; }
=======
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public UnitType Type { get; set; }
>>>>>>> 925b1b0ff0c6466f1652976c58b895b8f73bf17c
    }
}
