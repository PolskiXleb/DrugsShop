using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrugsShop.Models
{
    public static class CurrentUser
    {
        public static int Id { get; set; }
        public static Status Status { get; set; }
        public static bool PartialRegistration { get; set; }
        public static List<Position> Cart { get; set; }
    }

    public enum Status
    {
        freeUser = 1,
        loggedIn
        
    }
}