﻿namespace Core.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
    }
}
