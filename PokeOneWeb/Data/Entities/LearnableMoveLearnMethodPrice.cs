﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeOneWeb.Data.Entities
{
    [Table("LearnableMoveLearnMethodPrice")]
    public class LearnableMoveLearnMethodPrice
    {
        public int Id { get; set; }

        [ForeignKey("LearnableMoveLearnMethodId")]
        public LearnableMoveLearnMethod LearnableMoveLearnMethod { get; set; }
        public int LearnableMoveLearnMethodId { get; set; }

        [ForeignKey("PriceId")]
        public CurrencyAmount Price { get; set; }
        public int PriceId { get; set; }
    }
}
