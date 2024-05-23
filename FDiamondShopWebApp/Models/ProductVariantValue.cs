﻿using System;
using System.Collections.Generic;

namespace FDiamondShopWebApp.Models;

public partial class ProductVariantValue
{
    public int VariantId { get; set; }

    public int ProductId { get; set; }

    public string Value { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual CategoryVariant Variant { get; set; } = null!;
}
