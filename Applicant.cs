using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAgency
{
    //Класс соискателя
    public class Applicant
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Education { get; set; }
        public int ExperienceYears { get; set; }
        public string Contacts { get; set; }
    }
}
