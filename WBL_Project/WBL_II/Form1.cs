using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace WBL_II
{
    public partial class Form1 : Form
    {
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        DBCollection courses;
        DBCollection programs;
        DBCollection programreqs;
        DBCollection offerings;
        StoredProc sp;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'wBLDB_CollegeDS.Colleges' table. You can move, or remove it, as needed.
            this.collegesTableAdapter.Fill(this.wBLDB_CollegeDS.Colleges);
            CreateClasses();
            PopulatePrograms(cbCollege.Text);
            PopulateTerms();
        }
        private void CreateClasses()
        {
            string classQuery = config.AppSettings.Settings["classQuery"].Value;
            string programQuery = config.AppSettings.Settings["programQuery"].Value;
            string requirementsQuery = config.AppSettings.Settings["requirementsQuery"].Value;
            string offeringsQuery = config.AppSettings.Settings["offeringsQuery"].Value;
            courses = new DBCollection(classQuery);
            programs = new DBCollection(programQuery);
            programreqs = new DBCollection(requirementsQuery);
            offerings = new DBCollection(offeringsQuery);

            GetData();
        }
        private void GetData()
        {
            programs.GetData();
            courses.GetData();
            programreqs.GetData();
            offerings.GetData();
        }
        private void PopulateTerms()
        {

            var query = (from t in offerings.DT.AsEnumerable()
                         select t.Field<string>("Semester")).Distinct().ToList();
            
            foreach(var v in query)
            {
                clbTerm.Items.Add(v);
            }
        }
        private void PopulatePrograms(string College)
        {
            List<string> progList = new List<string>();
            string[] progItem;
            string[] progName;
            string[] progType;

            cbProgram.Items.Clear();

            var query = from p in programs.DT.AsEnumerable()
                        orderby p.Field<string>("ProgramName") ascending
                        where p.Field<string>("College") == College
                        select p.Field<string>("ProgramName");
            progName = query.ToArray();

            var type = from t in programs.DT.AsEnumerable()
                       orderby t.Field<string>("ProgramName") ascending
                       where t.Field<string>("College") == College
                       select t.Field<string>("ProgramType");
            progType = type.ToArray();

            progItem = new string[progName.Count()];

            for (int q = 0; q <= (query.Count()) - 1; q++)
            {
                progItem[q] = progName[q] + "-" + progType[q];
                cbProgram.Items.Add(progItem[q]);
            }

            cbProgram.Text = cbProgram.Items[0].ToString();
            Array.Clear(progName, 0, progName.Count());
            Array.Clear(progType, 0, progType.Count());
            query = null;
            type = null;
        }

        private void cbCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            string college;
            college = cbCollege.Text;
            PopulatePrograms(college);
        }
        private void SaveDestination()
        {

            DialogResult result;
            result = sfd.ShowDialog();
            sfd.DefaultExt = "csv";
            if(result == DialogResult.OK)
            {
                config.AppSettings.Settings["SavePath"].Value = sfd.FileName;
                config.Save(ConfigurationSaveMode.Modified);
            }

            GetResults();
        }
        private string FormatTermString(List<string> terms)
        {
            string formatTerms = "";

           for(int i= 1;i <= terms.Count; i++)
            {
                if(i == terms.Count)
                {
                    formatTerms = formatTerms + terms[i-1];
                }
                else
                {
                    formatTerms = formatTerms + terms[i-1] + " , ";
                }
            }
            return formatTerms;
        }
        private void GetResults()
        {

            List<string> lterms = new List<string>();
            lterms = clbTerm.CheckedItems.OfType<string>().ToList();
            string term = FormatTermString(lterms); 
            sp = new StoredProc(courses.DT, programs.DT, programreqs.DT, offerings.DT,term);
            sp.OnPercentCompleteChanged += PercentCompleteChanged;
            string programName = cbProgram.Text;
            string collegeName = cbCollege.Text;

            sp.GetProgramID(programName, collegeName);
            sp.GetResults();
        }
        private void PercentCompleteChanged(object sender, PercentCompleteEventArgs e)
        {
            pbProgress.Value = e.PercentComplete;
            lblPercent.Text = Convert.ToString(e.PercentComplete) + "%";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDestination();
        }

        private void chkAll_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                chkCollege.Enabled = false;
                chkProgram.Enabled = false;
            }
            else
            {
                chkCollege.Enabled = true;
                chkProgram.Enabled = true;
            }
        }

        private void chkCollege_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkCollege.Checked == true)
            {
                chkAll.Enabled = false;
                chkProgram.Enabled = false;
            }
            else
            {
                chkAll.Enabled = true;
                chkProgram.Enabled = true;
            }
        }

        private void chkProgram_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkProgram.Checked == true)
            {
                chkAll.Enabled = false;
                chkCollege.Enabled = false;
            }
            else
            {
                chkAll.Enabled = true;
                chkCollege.Enabled = true;
            }
        }
    }
}
