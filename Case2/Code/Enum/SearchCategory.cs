using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case2.Code.Enum;

public enum SearchCategory
{
    [Display( Name = "Fag")]
    course,
    [Display( Name = "Lærer")]
    teacher,
    [Display( Name = "Elev")]
    student
}