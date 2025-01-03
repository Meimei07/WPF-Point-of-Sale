using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public double Salary { get; set; }
        public string Address { get; set; }

        public Employee() { }
        public Employee(int id, string name, string username, string password, string phone, double salary, string address)
        {
            this.Id = id;
            this.Name = name;
            this.Username = username;
            this.Password = password;
            this.Phone = phone;
            this.Salary = salary;
            this.Address = address;
        }
    }
}