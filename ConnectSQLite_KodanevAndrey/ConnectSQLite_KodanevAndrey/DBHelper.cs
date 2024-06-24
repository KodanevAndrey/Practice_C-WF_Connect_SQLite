﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Data.Entity.Infrastructure;
using System.Data.OleDb;
using System.Data.Common;


namespace ConnectSQLite_KodanevAndrey
{
    internal class DBHelper 
    {
        private string TableNameDB;
        private String dbFileName;
        private SQLiteConnection m_dbConn = new SQLiteConnection();
        private SQLiteCommand m_sqlCmd = new SQLiteCommand();
        private SQLiteDataAdapter adapter;
        private string fileLocation = string.Empty;
        private string TableSelectColumnName;
        private string TableSelectCellName;
        private int TableSelectRowIndex;
        private List<int> DBTableColumnsTypeBlob = new List<int>();
        private List<int> DBTableColumnsTypeText = new List<int>();
        private List<int> DBTableColumnsTypeInt = new List<int>();

        public void CreateNewDB(Label lbStatusText, string DBName, string TableName, List<string> listColumns)
        {
            if (DBName != "")
            {
                SQLiteConnection.CreateFile(DBName + ".sqlite");
                try
                {
                    dbFileName = DBName + ".sqlite";
                    m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                    m_dbConn.Open();
                    m_sqlCmd.Connection = m_dbConn;
                    m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TableName + "(";
                    for(int i = 0; i < listColumns.Count; i++)
                    {
                        m_sqlCmd.CommandText += listColumns[i];
                        if (i != listColumns.Count - 1) m_sqlCmd.CommandText += ", ";
                    }
                    m_sqlCmd.CommandText += ")";
                    lbStatusText.Text = m_sqlCmd.CommandText;
                    m_sqlCmd.ExecuteNonQuery();
                    lbStatusText.Text += " База данных создана!";
                    lbStatusText.Text += " Connected";
                    lbStatusText.ForeColor = Color.Green;
                }
                catch (SQLiteException ex)
                {
                    lbStatusText.Text = "Disconnected";
                    lbStatusText.ForeColor = Color.Red;
                    MessageBox.Show("Error CreateDB: " + ex.Message);
                }
            }
            else lbStatusText.Text = "введите имя для новой базы данных!";
        }

        public bool ConnectDB( Label lbStatusText)
        {
            bool Conected = false;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"C\\";
            dlg.RestoreDirectory = true;
            dlg.Filter = "DataBase Files (*.mdb; *.sqlite)|*.mdb; *.sqlite";
            this.FileLocation = dlg.FileName;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileLocation = dlg.FileName;
                if (!File.Exists(FileLocation))
                    MessageBox.Show("Please, create DB and blank table (Push \"Create\" button)");
                else
                    try
                    {
                        m_dbConn = new SQLiteConnection("Data Source=" + FileLocation + ";Version=3;");
                        m_dbConn.Open();
                        m_sqlCmd.Connection = m_dbConn;
                        lbStatusText.Text = "Connected";
                        lbStatusText.ForeColor = Color.Green;
                        Conected = true;
                    }
                    catch (SQLiteException ex)
                    {
                        lbStatusText.Text = "Disconnected";
                        lbStatusText.ForeColor = Color.Red;
                        MessageBox.Show("Error ConnectDB: " + ex.Message);
                    }
            }
            return Conected;
        }

        public void LoadCountTables(Label lbStatusText)
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }
            try
            {
                string query = "SELECT count(*) FROM sqlite_master WHERE type='table';";
                using (var command = new SQLiteCommand(query, m_dbConn))
                {
                    int tableCount = Convert.ToInt32(command.ExecuteScalar());
                    lbStatusText.Text = "Количество таблиц в базе данных: " + tableCount;
                }
                int i = 1;
                query = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";

                using (var command = new SQLiteCommand(query, m_dbConn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lbStatusText.Text += " Имя таблицы №'"+i+"': " + reader["name"];
                            if(i ==1) TableNameDB = reader["name"].ToString();
                            i++;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error LoadCountTables: " + ex.Message);
            }
            DataTable dTable = new DataTable();
            String sqlQuery = "SELECT * FROM " + TableNameDB + " ";
            adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
            adapter.Fill(dTable);
        }

        public void GetTableInfo(Label lbStatusText)
        {
            lbStatusText.Text = "";
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }
            try
            {
                int index;
                m_sqlCmd.CommandText = "pragma table_info('"+TableNameDB+"');";
                SQLiteDataReader sqlite_datareader = m_sqlCmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    index = sqlite_datareader.GetInt32(0);
                    //lbStatusText.Text += sqlite_datareader.GetInt32(0) + ", ";
                    //lbStatusText.Text +=  sqlite_datareader.GetString(1) + ", ";
                    //lbStatusText.Text += sqlite_datareader.GetString(2) + " | ";
                    if (sqlite_datareader.GetString(2) == "BLOB") DBTableColumnsTypeBlob.Add(index);
                    if (sqlite_datareader.GetString(2) == "TEXT") DBTableColumnsTypeText.Add(index);
                    if (sqlite_datareader.GetString(2) == "INTEGER") DBTableColumnsTypeInt.Add(index);
                }
                //lbStatusText.Text += " Type TEXT = ";
                //foreach (int count in DBTableColumnsTypeText) { lbStatusText.Text += count + ","; }
                //lbStatusText.Text += " Type INT = ";
                //foreach (int count in DBTableColumnsTypeInt) { lbStatusText.Text += count + ","; }
                //lbStatusText.Text += " Type BLOB = ";
                //foreach (int count in DBTableColumnsTypeBlob) { lbStatusText.Text += count + ","; }
                sqlite_datareader.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error LoadTable: " + ex.Message);
            }
        }

        public void LoadTableInfo(Label lbStatusText, DataGridView dgvViewer)
        {
            if(dgvViewer.Columns.Count != 0) { dgvViewer.Columns.Clear(); }
            DataTable dTable = new DataTable();

            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }
            try
            {
                m_sqlCmd.CommandText = "select * from " + TableNameDB;
                SQLiteDataReader dr = m_sqlCmd.ExecuteReader();
                    lbStatusText.Text = "";
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if(dr.GetDataTypeName(i) == "BLOB"){dgvViewer.Columns.Add(new DataGridViewTextBoxColumn() { Name = dr.GetName(i), HeaderText = dr.GetName(i), Width = 100, ReadOnly = true });}
                    else{dgvViewer.Columns.Add(new DataGridViewTextBoxColumn() { Name = dr.GetName(i), HeaderText = dr.GetName(i), Width = 100 });}}
                dr.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error LoadTable: " + ex.Message);
            }
        }

        public void SelectToTable(Label lbStatusText, DataGridView dgvViewer)
        {
            int selectedRowCount = dgvViewer.GetCellCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    TableSelectColumnName = dgvViewer.SelectedCells[i].OwningColumn.Name.ToString();
                    TableSelectRowIndex = dgvViewer.SelectedCells[i].RowIndex;
                    sb.Append("Row: " + TableSelectRowIndex);
                    sb.Append(", Column: " + TableSelectColumnName);
                    if (dgvViewer.SelectedCells[i].Value != null) { TableSelectCellName = dgvViewer.SelectedCells[i].Value.ToString(); sb.Append(" Cell: " + TableSelectCellName); }
                    else { sb.Append(" Cell: NULL"); }
                    sb.Append(Environment.NewLine);
                }
                sb.Append("Total: " + selectedRowCount.ToString());
                lbStatusText.Text = sb.ToString() + "Selected Rows";
            }
        }

        public void ReadDB(Label lbStatusText,DataGridView dgvViewer)
        {
            DataTable dTable = new DataTable();
            String sqlQuery;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }
            try
            {
                sqlQuery = "SELECT * FROM "+ TableNameDB +" ";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
                adapter.Fill(dTable);

                if (dTable.Rows.Count > 0)
                {
                    dgvViewer.Rows.Clear();

                    for (int i = 0; i < dTable.Rows.Count; i++)
                        dgvViewer.Rows.Add(dTable.Rows[i].ItemArray);
                }
                else { dgvViewer.Rows.Clear(); lbStatusText.Text = "Database is empty"; lbStatusText.ForeColor = Color.Red; }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error ReadDB: " + ex.Message);
            }
        }
         
        public void AddDB(Label lbStatusText, Label lbCommand, DataGridView dgvViewer)
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }
            string status = "";
            bool IsActive = true;
            DataTable dTable = new DataTable();
            adapter.Fill(dTable);
            int RowCount = dTable.Rows.Count;
            status += " количество записей в дазе данных: "+dTable.Rows.Count.ToString();
            try
            {
                for (int i = 0; i < DBTableColumnsTypeText.Count; i++)
                {
                    status += dgvViewer.Rows[RowCount].Cells[DBTableColumnsTypeText[i]].Value + " | ";
                    if (dgvViewer.Rows[RowCount].Cells[DBTableColumnsTypeText[i]].Value == null)
                    {
                        IsActive = false;
                    }
                }
                if (IsActive == true)
                {
                    //status += " все поля заполнены!";
                    m_sqlCmd.CommandText = "INSERT INTO " + TableNameDB + " (";
                    for (int i = 0; i < DBTableColumnsTypeText.Count; i++)
                    {
                        m_sqlCmd.CommandText += " '" + dgvViewer.Columns[DBTableColumnsTypeText[i]].Name + "'";
                        if (i != DBTableColumnsTypeText.Count -1) m_sqlCmd.CommandText += ", ";
                    }
                    m_sqlCmd.CommandText += ") values (";
                    for (int i = 0; i < DBTableColumnsTypeText.Count; i++)
                    {
                        m_sqlCmd.CommandText += " '" + dgvViewer.Rows[RowCount].Cells[DBTableColumnsTypeText[i]].Value + "'";
                        if (i != DBTableColumnsTypeText.Count - 1) m_sqlCmd.CommandText += ", ";
                    }
                    m_sqlCmd.CommandText += ")";
                    m_sqlCmd.ExecuteNonQuery();
                    //lbStatusText.Text = m_sqlCmd.CommandText;
                    status += "ЗАПИСЬ ВЫПОЛНЕНА!";
                    ReadDB(lbStatusText, dgvViewer);
                }
                else { status += " IsActive = " + IsActive; status += " заполните все поля!" + status; lbStatusText.ForeColor = Color.Red; }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error AddDB: " + ex.Message);
            }
                lbStatusText.Text = status;
                lbCommand.Text = m_sqlCmd.CommandText;
        }

        public void DeleteDB(Label lbStatusText)
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                lbStatusText.Text = "Open connection with database";
                lbStatusText.ForeColor = Color.Red;
                return;
            }
            try
            {
                m_sqlCmd.CommandText = "DELETE FROM "+TableNameDB+" WHERE " +TableSelectColumnName + " = '"+ TableSelectCellName + "' ";
                //lbStatusText.Text = m_sqlCmd.CommandText;
                m_sqlCmd.ExecuteNonQuery();
                lbStatusText.Text = "удаление выполнено! '"+TableSelectColumnName +"' = " + TableSelectCellName.ToString();
                lbStatusText.ForeColor = Color.Green;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error DeleteDB: " + ex.Message);
            }
        }

        public void ResetDB(Label lbStatusText, Label lbCommand, DataGridView dgvViewer)
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                lbStatusText.Text = "Open connection with database";
                return;
            }
            string status = "";
            DataTable dTable = new DataTable();
            adapter.Fill(dTable);
            int RowCount = dTable.Rows.Count;
            try
            {
                m_sqlCmd.CommandText = "UPDATE " + TableNameDB + " SET ";
                m_sqlCmd.CommandText += TableSelectColumnName + " = ";
                for (int j = 0; j < dgvViewer.Columns.Count; j++)
                {
                    if(TableSelectColumnName == dgvViewer.Columns[j].Name)
                    m_sqlCmd.CommandText += "'"+ dgvViewer.Rows[RowCount].Cells[j].Value +"'";
                }
                m_sqlCmd.CommandText += " WHERE " + TableSelectColumnName + " = ";
                if (TableSelectCellName != "NULL") { m_sqlCmd.CommandText += "'" + TableSelectCellName + "'"; m_sqlCmd.ExecuteNonQuery(); status += "ЗАПИСЬ ВЫПОЛНЕНА!"; }
                else { status += "выберите ячейту для изменения!"; }

                    
                lbStatusText.Text = m_sqlCmd.CommandText;
                
                ReadDB(lbStatusText, dgvViewer);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error ResetDB: " + ex.Message);
            }
            lbStatusText.Text = status;
            lbCommand.Text = m_sqlCmd.CommandText;
        }

        public void DeleteAllDB(Label lbStatusText)
        {
            if (m_dbConn.State != ConnectionState.Open)
            {
                lbStatusText.Text = "Open connection with database";
                return;
            }

            try
            {
                m_sqlCmd.CommandText = "DELETE FROM Users";
                m_sqlCmd.ExecuteNonQuery();
                lbStatusText.Text = "удаление выполнено!";
                lbStatusText.ForeColor = Color.Green;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error DeleteAllDB: " + ex.Message);
            }
        }

        private string FileLocation
        {
            get { return fileLocation; }
            set
            {
                fileLocation = value;
            }
        }

        private Image LoadImage()
        {
            Image image = null;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = @"C:\\";
            dlg.Title = "Select Image File";
            dlg.RestoreDirectory = true;
            dlg.Filter = "Image Files  (*.jpg ; *.jpeg ; *.png ; *.gif ; *.tiff ; *.nef) | *.jpg; *.jpeg; *.png; *.gif; *.tiff; *.nef";
            this.FileLocation = dlg.FileName;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileLocation = dlg.FileName;
                var fileStream = dlg.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    image = Image.FromFile(fileLocation);
                }
            }
            return image;
        }

        public void AddImageToDB(Label lbStatusText, Label lbCommand, DataGridView dgvViewer)
        {
            lbStatusText.Text = "";
            string status = "";
            Image image = LoadImage();
            string SelectName = null;
            if (m_dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }


            if (image != null && TableSelectColumnName != null)
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach(int ColumnIndex in DBTableColumnsTypeBlob)
                {
                    if ( TableSelectColumnName == dataTable.Columns[ColumnIndex].ColumnName) 
                    {
                        SelectName = dataTable.Columns[ColumnIndex].ColumnName;    
                    }
                } 
                    try
                    {
                        m_sqlCmd.CommandText = "UPDATE Users SET "+SelectName+" = '" + image + "' WHERE " + TableSelectColumnName + " = ";
                        if (TableSelectCellName != "NULL") 
                        {
                        m_sqlCmd.CommandText += "'" + TableSelectCellName + "'";
                        lbCommand.Text = m_sqlCmd.CommandText;
                        lbStatusText.Text += "SelectColumName = " + SelectName;
                        m_sqlCmd.ExecuteNonQuery(); 
                        //status += "ЗАПИСЬ ВЫПОЛНЕНА!";
                        }
                        else { status = "выберите ячейту для изменения!"; }
                        lbStatusText.Text += "изображение загружено";
                        lbStatusText.ForeColor = Color.Green;
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show("Error AddImageToDB: " + ex.Message);
                    }
            }
            else { lbStatusText.Text = "выберите изображение!"; lbStatusText.ForeColor = Color.Red; }
        }
    }
}
