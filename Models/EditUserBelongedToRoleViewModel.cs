using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class EditUserBelongedToRoleViewModel
    {
        public EditUserBelongedToRoleViewModel()
        {
            Users = new List<string>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public List<String> Users { get; set; }
    }
}
