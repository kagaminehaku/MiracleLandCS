﻿using System;
using System.Collections.Generic;

namespace MiracleLandCS.Models;

public partial class CsOrderDetail
{
    public Guid Odid { get; set; }

    public Guid Orderid { get; set; }

    public Guid Pid { get; set; }

    public int Quantity { get; set; }

    public virtual CsOrder Order { get; set; } = null!;

    public virtual Product OrderNavigation { get; set; } = null!;
}
