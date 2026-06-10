using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAgency
{
    //Класс вакинсии
    public class Vacancy
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public string Requirements { get; set; }
        public string Status { get; set; }
    }
}
