using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCInstituteTimetableManagementSystem.MoreOptionsPortal.Service
{
    class NonOverlap
    {
        public string sessionOne { get; set; }
        public string sessionTwo { get; set; }
        public int s1Id { get; set; }
        public int s2Id { get; set; }
        public bool overlap { get; set; }
  
    }
}
