using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace porgKorny_WPF_beadando.Model
{
     public class Ad
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
       // public DateTime CreatedAt { get; set; };
        public string UserName { get; set; }
    }
}
