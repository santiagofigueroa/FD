﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FD.Models
{
    public interface Price
    {
        public decimal Rate { get; set; }
        public int? Threshold { get; set; }
    }
}
