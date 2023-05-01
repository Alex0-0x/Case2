using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2.Code.Model
{
    public class Course : ISearchable
    {
        public string Name { get; set; }
        public Teacher? Teacher { get; set; }
        public Student[]? students { get; set; }

        public Course(string name) 
        {
            Name = name;
        }
    }
}
