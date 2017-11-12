using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Configuration;


namespace WBL_II
{
    class SaveResult
    {
        public FinalResult fr { get; set; }
        private Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        private string _fileName;
        public SaveResult(FinalResult Fr)
        {
            fr = Fr;
        }

        public void SaveToCSV()
        {
            Application excelApp = new Application();
            Workbook excelWrkBk = excelApp.Workbooks.Add() as Workbook;
            Worksheet excelWrksht = excelWrkBk.Sheets[1] as Worksheet;
            int propCount;
            int j = 1;
            List<string> pr = new List<string>();
            
            Type t = typeof(FinalResult);

            //excelApp.Workbooks.Add();
            //excelWrksht = excelApp.ActiveWorkbook.ActiveSheet();

            excelWrksht.Cells[1, 1].Activate();

            propCount = t.GetProperties().Count();
            foreach (var prop in t.GetProperties()) { pr.Add(prop.Name); }
            for (int i = 1; i <= propCount; i++)
            {
                excelWrksht.Cells[1, i] = pr[i - 1];
                excelWrksht.Cells[1,i].Borders.Weight = XlBorderWeight.xlThick;
            }

            excelWrksht.Cells[2, 1].Activate();
            
            foreach(var prop in fr.GetType().GetProperties())
            {
                excelWrksht.Cells[2, j] = prop.GetValue(fr).ToString();
                j += 1;
            }
            excelApp.Columns.AutoFit();
            GetFileName();
            excelWrksht.SaveAs(_fileName);
            excelApp.Visible = true;
            
        }
        private void GetFileName()
        {
            _fileName = _config.AppSettings.Settings["SavePath"].Value;
        }
    }
    
}
