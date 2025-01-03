using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS.Model
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Supplier() { }
        public Supplier(int id, string name, string phone, string address)
        {
            this.Id = id;
            this.Name = name;
            this.Phone = phone;
            this.Address = address;
        }
    }
}