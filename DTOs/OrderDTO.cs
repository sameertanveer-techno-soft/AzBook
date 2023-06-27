using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.DTOs
{
    public class OrderDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public DateTime OrderedOn { get; set; }
        public Boolean Returned { get; set; } = false;
        public DateTime? ReturnedOn { get; set; }
    }
}
