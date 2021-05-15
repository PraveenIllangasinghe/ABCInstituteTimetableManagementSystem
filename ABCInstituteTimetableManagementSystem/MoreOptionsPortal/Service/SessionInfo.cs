using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCInstituteTimetableManagementSystem.MoreOptionsPortal.Service
{
    class SessionInfo
    {
        public List<string> sessionStringList { get; set; }
        public ListDictionary sessionIdMap { get; set; }
        public ListDictionary durationMap { get; set; }
    }

}
