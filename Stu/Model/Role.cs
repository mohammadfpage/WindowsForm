using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stu.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public ICollection<User> Users { get; set; }
    }
}
