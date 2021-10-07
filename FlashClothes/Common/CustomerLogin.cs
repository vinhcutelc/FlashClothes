using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlashClothes.Common
{
    [Serializable]
    public class CustomerLogin
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int? Role { get; set; }
        public bool Status { get; set; }
        public bool Remember { get; set; }
    }
}