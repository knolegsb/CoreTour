using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CoreTour.Models
{
    public class CoreTourUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}
