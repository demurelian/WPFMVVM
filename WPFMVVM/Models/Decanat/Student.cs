﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMVVM.Models.Decanat
{
    internal class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        //Отчество
        public string Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
    }
    internal class Group
    {
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
        public string Description { get; set; }
    }
}
