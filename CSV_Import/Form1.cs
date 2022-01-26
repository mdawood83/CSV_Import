using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSV_Import
{
    public partial class Form1 : Form
    {
        //url to download the CSV
        public string url = "https://twtransfer.energytransfer.com/ipost/capacity/operationally-available?f=csv&extension=csv&asset=TW&gasDay=01%2F24%2F2022&cycle=1&searchType=NOM&searchString=&locType=ALL&locZone=ALL";
        //the filepath to be downloaded in the Downloads folder => (Data_yyyy-MM-dd.csv)
        string filePath = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads\" + @"Data_" + DateTime.Today.ToString("yyyy-MM-dd") + ".csv";

        static SqlConnection con;

        public Form1()
        {
            InitializeComponent();
            ParseCSVFile();
        }

        //// <summary>
        //// Method to download and parse a csv file
        //// </summary>
        public void ParseCSVFile()
        {
            System.Net.WebClient client = new System.Net.WebClient();

            byte[] buffer = client.DownloadData(url);

            Stream stream = new FileStream(filePath, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(buffer);
            stream.Close();

            //optional to see data in Notepad
            Process.Start("Notepad.exe", filePath);
        }

        public static string GetConnSetting(string Conn_name)
        {
            ConnectionStringSettings ret_val = ConfigurationManager.ConnectionStrings[Conn_name];

            if (ret_val == null) throw new Exception("202201240916 ConnectionStrings not found [" + Conn_name + "]");

            return ret_val.ToString();
        }

        public void InsertCSVRecords(DataTable csvDT)
        {
            try
            {
                using (SqlBulkCopy sqlBulk = new SqlBulkCopy(con))
                {
                    sqlBulk.DestinationTableName = "Shipment";

                    foreach (DataColumn column in csvDT.Columns)
                    {
                        sqlBulk.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
                    }

                    con.Open();
                    sqlBulk.WriteToServer(csvDT);
                    con.Close();

                    File.WriteAllText(filePath, csvDT.TableName);

                    MessageBox.Show("CSV data inserted successfully into DB...", "Confirmation");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't insert data, something is wrong!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //// <summary>
        //// Test DB Connection Button
        //// </summary>
        private void test_btn_Click(object sender, EventArgs e)
        {
            string conn = GetConnSetting("NaturalGasDB");
            if (conn.Length > 0)
            {
                MessageBox.Show("DB Connected successfully...", "Confirmation");
                return;
            }
            MessageBox.Show("Failed to connect to DB...", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //// <summary>
        //// Insert into DB Button
        //// </summary>
        private void insert_btn_Click(object sender, EventArgs e)
        {
            DataTable csvDT = new DataTable();
            List<string> list = new List<string>();

            csvDT.Columns.Add("Loc");
            csvDT.Columns.Add("Loc_Zn");
            csvDT.Columns.Add("Loc_Name");
            csvDT.Columns.Add("Loc_Purp_Desc");
            csvDT.Columns.Add("Loc_QTI");
            csvDT.Columns.Add("Flow_Ind");
            csvDT.Columns.Add("DC");
            csvDT.Columns.Add("OPC");
            csvDT.Columns.Add("TSQ");
            csvDT.Columns.Add("OAC");
            csvDT.Columns.Add("IT");
            csvDT.Columns.Add("Auth_Overrun_Ind");
            csvDT.Columns.Add("Nom_Cap_Exceed_Ind");
            csvDT.Columns.Add("All_Qty_Avail");
            csvDT.Columns.Add("Qty_Reason");
            
            string CSVFilePath = Path.GetFullPath(filePath);
            
            string ReadCSV = File.ReadAllText(CSVFilePath);
            
            //Splitting row after new line, skipping the 1st header row
            foreach (string csvRow in ReadCSV.Split('\n').Skip(1))
            {
                if (!String.IsNullOrEmpty(csvRow))
                {
                    csvDT.Rows.Add();
                    list.Add(csvRow);

                    int count = 0;
                    foreach (string record in csvRow.Split(','))
                    {
                        csvDT.Rows[csvDT.Rows.Count - 1][count] = record;
                        count++;
                    }
                }
            }

            DB_REC.InsertDBRecords(list.ToString());

            InsertCSVRecords(csvDT);
        }

        public static class DB_REC
        {
            public static string m_Loc { get; }
            public static string m_Loc_Zn { get; }
            public static string m_Loc_Name { get; }
            public static string m_Loc_Purp_Desc { get; }
            public static string m_Loc_QTI { get; }
            public static string m_Flow_Ind { get; }
            public static string m_DC { get; }
            public static string m_OPC { get; }
            public static string m_TSQ { get; }
            public static string m_OAC { get; }
            public static string m_IT { get; }
            public static string m_Auth_Overrun_Ind { get; }
            public static string m_Nom_Cap_Exceed_Ind { get; }
            public static string m_All_Qty_Avail { get; }
            public static string m_Qty_Reason { get; }

            public static void InsertDBRecords(string db)
            {
                db = GetConnSetting("NaturalGasDB");
                con = new SqlConnection(db);

                var query = con.CreateCommand();

                query.CommandText = string.Format("IF NOT EXISTS (SELECT * FROM Shipment WHERE '{0}' IS NOT NULL) INSERT INTO Shipment VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}')",
                m_Loc, m_Loc_Zn, m_Loc_Name, m_Loc_Purp_Desc, m_Loc_QTI, m_Flow_Ind, m_DC, m_OPC, m_TSQ, m_OAC, m_IT, m_Auth_Overrun_Ind, m_Nom_Cap_Exceed_Ind, m_All_Qty_Avail, m_Qty_Reason);

                con.Open();
                query.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
