using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiracleLandCS.Models
{
    public class CsProducts
    {
        public Guid Pid { get; set; }

        public string Pname { get; set; } = null!;

        public decimal Pprice { get; set; }

        public int Pquantity { get; set; }

        public string Pinfo { get; set; } = null!;

        public string Pimg { get; set; } = null!;
    }

    public class CsProductsToCart
    {
        public string token { get; set; }
        public Guid Pid { get; set; }
        public int Pquantity { get; set; }
    }
}
