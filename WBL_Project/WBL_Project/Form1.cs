using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using MoreLinq;
using System.IO;
using System.Threading;


namespace WBL_Project
{
    public partial class Form1 : Form
    {
        Programs prog = new Programs("Programs");
        CourseRequirements cr = new CourseRequirements("ProgramRequirements");
        Courses crs = new Courses("Classes");
        private string _progID;
        private List<string> _CourseList = new List<string>();
        private string[] _progList;
        private string[] _college;
        private string[][] _collegeProgs;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'wBL_ProjectDBDataSet1.Programs' table. You can move, or remove it, as needed.
            this.programsTableAdapter.Fill(this.wBL_ProjectDBDataSet1.Programs);
            // TODO: This line of code loads data into the 'wBL_ProjectDBDataSet.Colleges' table. You can move, or remove it, as needed.
            this.collegesTableAdapter.Fill(this.wBL_ProjectDBDataSet.Colleges);
            prog.DBConnect();
            cr.DBConnect();
            crs.DBConnect();
            PopulatePrivateCollege();
        }
        private string[] PopulatePrograms(string College)
        {
            string[] progItem;
            string[] progName;
            string[] progType;
                                  
            var query = from p in prog.DT.AsEnumerable()
                        orderby p.Field<string>("ProgramName") ascending
                        where p.Field<string>("College") == College
                        select p.Field<string>("ProgramName");
            progName = query.ToArray();

            var type = from t in prog.DT.AsEnumerable()
                       orderby t.Field<string>("ProgramName") ascending
                       where t.Field<string>("College") == College
                       select t.Field<string>("ProgramType");
            progType = type.ToArray();

            progItem = new string[progName.Count()];

            //cbProgram.Items.Clear();
            for (int q=0; q <= (query.Count()) - 1; q++)
            {
                progItem[q] = progName[q] + "-" + progType[q];
                //cbProgram.Items.Add(progItem);
            }
            
            //cbProgram.Text = "Select a program";
            Array.Clear(progName, 0, progName.Count());
            Array.Clear(progType, 0, progType.Count());
            query = null;
            type = null;
            return progItem;
        }
        private void GetProgramID()
        {
            string[] program;
            program = cbProgram.Text.Split('-');
            var query = from p in prog.DT.AsEnumerable()
                        where p.Field<string>("College").Equals(cbCollege.Text) &&
                        p.Field<string>("ProgramName").Equals(program[0])
                        select p.Field<string>("ProgramID");
            _progID = Convert.ToString(query.First());
        }

        private void SpawnTasks()
        {
            List<Task<string>> taskList = new List<Task<string>>();
            taskList.Add(Task<string>.Factory.StartNew(BuildCollegeProgArray)); 
        }
        private string BuildCollegeProgArray()
        {
            _collegeProgs = new string [_college.Count()][];
            string line = string.Empty;
            string[] programs;
            
            for(int i = 0; i <= _college.Count(); i++)
            {
                programs = PopulatePrograms(_college[i]);
                _collegeProgs[i] = programs;
            }

            foreach (var lbl in this.groupBox1.Controls.OfType<Label>())
            {
                line += lbl.Text + ",";
            }
            line += "\r\n";

            return line;
        }
        private void PopulatePrivateCollege()
        {
            for(int i=0;i<= cbCollege.Items.Count; i++)
            {
                _college[i] = cbCollege.Items[i].ToString();
            }
        }
        private void PopulateDataGrid()
        {
            List<DataRow> classID = new List<DataRow>();
            //cr.DBConnect();
            var query = from pr in cr.DT.AsEnumerable()
                        where pr.Field<string>("ProgramID").Equals(_progID)
                        select pr.Field<string>("CourseID");
            _CourseList = query.ToList();

            //crs.DBConnect();
            var data = from course in crs.DT.AsEnumerable()
                       orderby course.Field<string>("Location") ascending
                       where _CourseList.Contains(course.Field<string>("Class"))
                       select course;
            //dataGridView1.DataSource = data.AsDataView();
            classID = data.ToList();
            getPercents(classID);
        }
        private string getPercents(List<DataRow> varList)
        {
            List<string> classID = new List<String>();
            int tot_count,online_count,main_count,north_count;
            string line, northPer, mainPer, onlinePer;
            var data = varList.DistinctBy(o => o.Field<string>("Class"));
            tot_count = data.Count();
            
            var online = (from v in varList
                         where v.Field<string>("Location") == "Online"
                         select v.Field<string>("Class")).Distinct();
            
            online_count = online.Count();

            var mainCampus = (from v in varList
                             where v.Field<string>("Location") == "Main Campus"
                             select v.Field<string>("Class")).Distinct();
            main_count = mainCampus.Count();

            var northCampus = (from v in varList
                              where v.Field<string>("Location") == "Northern Campus"
                              select v.Field<string>("Class")).Distinct();
            north_count = northCampus.Count();

            onlinePer = Convert.ToString(Math.Round((double)online_count / (double)tot_count * 100)) + "%";
            mainPer = Convert.ToString(Math.Round(((double)main_count / (double)tot_count) * 100)) + "%";
            northPer = Convert.ToString(Math.Round(((double)north_count / (double)tot_count) * 100)) + "%";

            line = onlinePer + "," + mainPer + "," + northPer;
            return line;       
        }
        
        private void cbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProgramID();
            PopulateDataGrid();
        }        
        
        private void cbCollege_SelectedValueChanged(object sender, EventArgs e)
        {
            string college;
            college = cbCollege.Text;
            PopulatePrograms(college);
        }

        private void tbSave_Click(object sender, EventArgs e)
        {
            filePath.InitialDirectory = "C:\\";
            filePath.DefaultExt = ".csv";
            filePath.ShowDialog();            
        }

        private void sfd_FileOk(object sender, CancelEventArgs e)
        {
            string filePath;
            filePath = this.filePath.FileName;
            getProgList();
            WriteToCsv(filePath);
        }
        
        
        private void getProgList()
        {
            for(int i=0;i <= cbProgram.Items.Count; i++)
            {
                _progList[i] = cbProgram.Items[i].ToString();
            }
        }
        

        private void WriteToCsv(string path)
        {

            string line = string.Empty;

            foreach(var lbl in this.groupBox1.Controls.OfType<Label>())
            {
                line += lbl.Text + ",";
            }
            line += "\r\n";

            foreach (var tb in this.groupBox1.Controls.OfType<TextBox>())
            {
                  line += tb.Text + ",";
            }
            line += "\r\n";
            line += "\r\n";

            foreach(DataGridViewColumn c in dataGridView1.Columns)
            {
                line += c.HeaderText + ",";
            }
            line += "\r\n";

            foreach(DataGridViewRow r in dataGridView1.Rows)
            {
                foreach(DataGridViewCell c in r.Cells)
                {
                    if(c.Value != null)
                    {
                        line += c.Value.ToString() + ",";
                    }
                    
                }
                line += "\r\n";

                File.WriteAllText(path, line);
            }
        }
    }
}
