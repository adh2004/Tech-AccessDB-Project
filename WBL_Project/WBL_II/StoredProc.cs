using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace WBL_II
{
    class StoredProc:DataConnection
    {
        DataTable _coursesDT;
        DataTable _programsDT;
        DataTable _programreqsDT;
        DataTable _classOfferingDT;
        DataTable _tempResultDT = new DataTable();
        private string _progID;
        private string _classes;
        private string _college;
        private string _program;
        private string _term;
        private decimal _totalProcedures = 8;
        List<string> _Listcolleges = new List<string>();
        List<string> _Listprograms = new List<string>();
        public event EventHandler<PercentCompleteEventArgs> OnPercentCompleteChanged;
        public StoredProc(DataTable coursesDT, DataTable programsDT, DataTable programreqsDT, DataTable classOfferingDT,string Term)
        {
            _coursesDT = coursesDT;
            _programsDT = programsDT;
            _programreqsDT = programreqsDT;
            _classOfferingDT = classOfferingDT;
            _term = Term;
            
        }
        public void GetProgramID(string program, string college)
        {
            string[] programSplit;
            programSplit = program.Split('-');
            _program = program;
            _college = college;

            var query = from p in _programsDT.AsEnumerable()
                        where p.Field<string>("College").Equals(college) &&
                        p.Field<string>("ProgramName").Equals(programSplit[0])
                        select p.Field<string>("ProgramID");
            _progID = Convert.ToString(query.First());
        }
        private Dictionary<string,int> GetClasses()
        {
            Dictionary<string, int> classDict = new Dictionary<string, int>();
            var classes = from courses in _programreqsDT.AsEnumerable()
                          where courses.Field<string>("ProgramID").Equals(_progID)
                          select new
                          {
                              ClassID = courses.Field<string>("CourseID"),
                              Credits = courses.Field<int>("TotalCreditHrs")

                          };
            classDict = classes.ToDictionary(c => c.ClassID, c => c.Credits);
            return classDict;           

        }
        public void GetResults()
        {
            string classes;
            int totalCreditHrs;
            decimal i = 1;
            decimal perComplete;
            FinalResult result;
            List<ClassOfferings> clOff = new List<ClassOfferings>();
            List<string> classList = new List<string>();
            List<string> termList = new List<string>();
            Dictionary<string, int> classData = new Dictionary<string, int>();
            SaveResult sr;
            classData = GetClasses();
            perComplete = (i / _totalProcedures);
            GetPercentComplete(perComplete);
            i += 1;
            var first = classData.FirstOrDefault();
            classes = first.Key;
            totalCreditHrs = first.Value;
            classList = classes.Split(',').ToList();
            termList = _term.Split(',').ToList();
            CreateTempTable();
            perComplete = (i / _totalProcedures);
            GetPercentComplete(perComplete);
            i += 1;
            clOff = GetClassOfferings(classList,termList);
            perComplete = (i / _totalProcedures);
            GetPercentComplete(perComplete);
            i += 1;
            PopulateTempTable(clOff);
            perComplete = (i / _totalProcedures);
            GetPercentComplete(perComplete);
            i += 1;
            PopulateTempDT();
            perComplete = (i / _totalProcedures);
            GetPercentComplete(perComplete);
            i += 1;
            result = GetFinalResults((decimal)totalCreditHrs);
            perComplete = (i / _totalProcedures);
            GetPercentComplete(perComplete);
            i += 1;
            DeleteTempTable();
            perComplete = (i / _totalProcedures);
            GetPercentComplete(perComplete);
            i += 1;
            sr = new SaveResult(result);
            sr.SaveToCSV();
            perComplete = (i / _totalProcedures);
            GetPercentComplete(perComplete);
            i += 1;
        }
        private void CreateTempTable()
        {
            string query = @"CREATE TABLE TempResult (AID Integer PRIMARY KEY, College char(50),
                            ProgramName char(50), Class char(50), Credits integer, Term char(50), Location char(50))";
            OleDbCommand cmd = new OleDbCommand(query, _con);
            DBConnect();
            cmd.ExecuteNonQuery();
            DBDisconnect();
        }
        private void DeleteTempTable()
        {
            string query = "DROP TABLE TempResult";
            OleDbCommand cmd = new OleDbCommand(query, _con);
            DBConnect();
            cmd.ExecuteNonQuery();
            DBDisconnect();
        }
        private string AlterString(string classStr)
        {
            string[] str;
            string newStr = "";

            str = classStr.Split(',');
            
            for(int i=0;i<= str.Count()-1; i++)
            {
                if (i != str.Count())
                {
                    newStr = newStr + "'" + str[i] + "',";
                }
                else
                {
                    newStr = newStr + "'" + str[i] + "'";
                }
            }
            return newStr;
        }
        private List<ClassOfferings> GetClassOfferings(List<string> classes, List<string> terms)
        {
            List<ClassOfferings> offeringsList = new List<ClassOfferings>();
            var classOfferings = from c in _coursesDT.AsEnumerable()
                                 where classes.Contains(c.Field<string>("Class"))
                                 join o in _classOfferingDT.AsEnumerable()
                                 on c.Field<string>("Class") equals o.Field<string>("Class") 
                                 orderby o.Field<string>("Location")
                                 where o.Field<string>("Semester").Any(p=>terms.Any())
                                 select new ClassOfferings
                                 {
                                     Course = c.Field<string>("Class"),
                                     Location = o.Field<string>("Location"),
                                     Semester = o.Field<string>("Semester"),
                                     Credits = c.Field<int>("Credits")
                                 };
            offeringsList = classOfferings.ToList();
            return offeringsList;
        }
        private void PopulateTempTable(List<ClassOfferings> cl)
        {
            OleDbCommand cmd;
            string query;
            int uid = 0;
            DBConnect();
            foreach (ClassOfferings clo in cl)
            {
                uid += 1;
                query = @"INSERT INTO TempResult (AID, College, ProgramName, Class, Credits, Term, Location)
                          VALUES(" + Convert.ToString(uid) + "," +"'" + _college + "'" +"," +
                          "'" + _program + "'" + "," + "'" + clo.Course + "'" + "," +
                           + clo.Credits  + "," + "'"+ clo.Semester + "'" + 
                          "," + "'" + clo.Location + "'" + ")";

                cmd = new OleDbCommand(query, _con);
                
                cmd.ExecuteNonQuery();
                
            }
            DBDisconnect();
        }
        private void PopulateTempDT()
        {
            string query = "SELECT * FROM TempResult";
            
            OleDbCommand cmd = new OleDbCommand(query, _con);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DBConnect();
            da.Fill(_tempResultDT);
            DBDisconnect();
        }
        private FinalResult GetFinalResults(decimal totalCredits)
        {
            FinalResult fr = new FinalResult();
            List<CourseCredits> cc = new List<CourseCredits>();
            
            int ttlHrs = 0;
            var ttlCreds = (from c in _tempResultDT.AsEnumerable()
                            
                            select new CourseCredits
                            {
                                course = c.Field<string>("Class"),
                                credit = c.Field<int>("Credits")
                            });
            foreach (var a in ttlCreds)
            {
                ttlHrs += a.credit;
            }                    
                

             var result = from r in _tempResultDT.AsEnumerable()
                         select new FinalResult
                         {
                             College = r.Field<string>("College"),
                             Program = r.Field<string>("ProgramName"),
                             Semester = r.Field<string>("Term"),
                             Online = Math.Round((((_tempResultDT.AsEnumerable().Where(row => row.Field<string>("Location").Contains("Online")).Sum(row=>row.Field<int>("Credits")))/Convert.ToDecimal(ttlHrs))*100),2),
                             Main_Campus = Math.Round((((_tempResultDT.AsEnumerable().Where(row => row.Field<string>("Location").Contains("Main Campus")).Sum(row => row.Field<int>("Credits")))/Convert.ToDecimal(ttlHrs))*100),2),
                             North_Campus = Math.Round((((_tempResultDT.AsEnumerable().Where(row => row.Field<string>("Location").Contains("Northern Campus")).Sum(row => row.Field<int>("Credits")))/Convert.ToDecimal(ttlHrs))*100),2),

                         };
            fr = result.FirstOrDefault();
            return fr;
        }
        public void GetPercentComplete(decimal percent)
        {
            int percentComplete;
            percentComplete = (int)Math.Round(percent * 100);
            OnPercentCompleteChanged?.Invoke(this, new PercentCompleteEventArgs(percentComplete));
        }
    }
}
