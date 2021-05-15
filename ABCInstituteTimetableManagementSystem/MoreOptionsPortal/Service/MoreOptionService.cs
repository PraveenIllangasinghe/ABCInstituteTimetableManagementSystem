using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCInstituteTimetableManagementSystem.MoreOptionsPortal.Service
{
    class MoreOptionService
    {
        public SessionInfo fetchAllSessions()
        {
            SessionInfo sessionInfo = new SessionInfo();
            try
            {

                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = $"select * from Session";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());
                cmd1.ExecuteNonQuery();

                SqlDataReader dr = cmd1.ExecuteReader();

                List<string> infoList = new List<string>();
                ListDictionary idMap = new ListDictionary();
                ListDictionary durationMap = new ListDictionary();

                while (dr.Read())
                {
                    string val = $"{dr["SubjectName"]} by {dr["LecturerName"]} for {dr["GroupID"]}";
                    idMap.Add(dr["ID"], val);
                    durationMap.Add(dr["ID"], dr["Duration"]);
                    infoList.Add(val);
                }
                String[] str = infoList.ToArray();
                dr.Close();
                sessionInfo.sessionStringList = infoList;
                sessionInfo.sessionIdMap = idMap;
                sessionInfo.durationMap = durationMap;
                return sessionInfo;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public DataTable fetchConsecutiveSessions(string prop, string value)
        {
            try
            {
                DataTable dt = new DataTable();
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = $"select * from consecutive where {prop} = '{value}'";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());


                SqlDataAdapter ada = new SqlDataAdapter(cmd1);
                ada.Fill(dt);
                con1.closeConnection();
                return dt;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool checkOverlapConditionForParallelSessions(int sessionOneId, int sessionTwoId)
        {
            try
            {

                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = $" select * from NotOverlap where sOneId={sessionOneId} and sTwoId={sessionTwoId} or sOneId={sessionTwoId} and sTwoId={sessionOneId}";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());
                cmd1.ExecuteNonQuery();
                SqlDataReader dr = cmd1.ExecuteReader();

                // if datareader has no rows it is okay to add new parallel sessions
                return !dr.HasRows;

            }
            catch (Exception e)
            {
                return false;

                throw;
            }
        }
        public bool saveConsecutiveOrParallelSessions(ConsecutiveSession cs, string table)
        {

            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

               
                string q1 = $"insert into {table} (sessionOne, sessionTwo, startTime, endTime, classDay) values (@sessionOne, @sessionTwo, @startTime, @endTime, @classDay)";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());

                //DateTime dateTime = DateTime.ParseExact(cs.startTime, "HH:mm:tt",
                //                       CultureInfo.InvariantCulture);
                var dateTime = DateTime.ParseExact(cs.startTime, "hh:mm tt", null);
                DateTime etime = dateTime.AddHours(cs.duration);

                string endTime = etime.ToString("hh:mm tt");

                cmd1.Parameters.AddWithValue("@sessionOne", cs.sessionOne);
                cmd1.Parameters.AddWithValue("@sessionTwo", cs.sessionTwo);
                cmd1.Parameters.AddWithValue("@startTime", cs.startTime);
                cmd1.Parameters.AddWithValue("@endTime", endTime);
                cmd1.Parameters.AddWithValue("@classDay", cs.classDay);

               
                cmd1.ExecuteNonQuery();
                temp1 = true;

            }
            catch (Exception ew)
            {

                throw;
            }


            return temp1;
        }
        public bool saveNonOverlappingSessions(NonOverlap cs)
        {

            bool temp1 = false;
            try
            {
                Database.dbConnect con1 = new Database.dbConnect();
                con1.openConnection();

                string q1 = "insert into NotOverlap (sessionOne, sessionTwo, sOneId, sTwoId) values (@sessionOne, @sessionTwo, @sOneId, @sTwoId)";

                SqlCommand cmd1 = new SqlCommand(q1, con1.getConnection());


                cmd1.Parameters.AddWithValue("@sessionOne", cs.sessionOne);
                cmd1.Parameters.AddWithValue("@sessionTwo", cs.sessionTwo);
                cmd1.Parameters.AddWithValue("@sOneId", cs.s1Id);
                cmd1.Parameters.AddWithValue("@sTwoId", cs.s2Id);


                cmd1.ExecuteNonQuery();
                temp1 = true;

            }
            catch (Exception ew)
            {

                throw;
            }


            return temp1;
        }
    }
}
