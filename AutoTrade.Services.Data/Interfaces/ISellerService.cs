﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Services.Data.Interfaces
{
    public interface ISellerService
    {
        Task<bool> SellerExistsByUserIdAsync(string userId);
    }
}
