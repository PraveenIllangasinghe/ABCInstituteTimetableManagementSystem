using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCInstituteTimetableManagementSystem.StudentGroupPortal.Service
{
    class YearSemester
    {
        private int Id;
        public int year { get; set; }
        public int semester { get; set; }
        public int id
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
            }
        }
    }
}
