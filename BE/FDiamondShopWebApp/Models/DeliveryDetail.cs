﻿namespace FDiamondShop.API.Models
{
    public class DeliveryDetail
    {
        public int DeliverylId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime ReceiveDate { get; set; }
    }
}
