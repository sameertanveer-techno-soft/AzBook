using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.Model
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string Username { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public DateTime OrderedOn { get; set; }
        public bool Returned { get; set; }
        public DateTime? ReturnedOn { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
