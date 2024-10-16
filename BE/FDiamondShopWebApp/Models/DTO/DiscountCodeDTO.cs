﻿using System.ComponentModel.DataAnnotations;

namespace FDiamondShop.API.Models.DTO
{
    public class DiscountCodeDTO
    {
        public int DiscountId { get; set; }

        public string DiscountCodeName { get; set; }

        public int DiscountPercent { get; set; } = 0;

        public DateTime StartingDate { get; set; } 

        public DateTime EndDate { get; set; }

        public bool IsExpried { get; set; } = false;
    }
}
