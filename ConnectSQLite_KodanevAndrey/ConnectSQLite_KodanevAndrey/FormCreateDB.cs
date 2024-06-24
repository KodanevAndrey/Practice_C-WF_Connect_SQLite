using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ConnectSQLite_KodanevAndrey
{
    public partial class FormCreateDB : Form
    {
        private string DBName = null;
        private string TableName = null;
        private List<string> listColumns = new List<string>();
        private DBHelper db = new DBHelper();
        private Form1 Form1 = new Form1();
        public FormCreateDB()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(500, 300);
            this.MaximizeBox = false;
        }

        private void btnCreateDB_Click(object sender, EventArgs e)
        {
            if(txtDBName.Text != "")
            {
                DBName = txtDBName.Text;
                if(txtTableName.Text != "")
                {
                    TableName = txtTableName.Text;
                    if(listColumns.Count > 0)
                    {
                        db.CreateNewDB(lbStatus, DBName, TableName, listColumns);
                    }
                    else lbStatus.Text = "создайте столбцы в таблице!";
                }
                else lbStatus.Text = "введите имя для таблицы!";
            }
            else lbStatus.Text = "введите имя для новой базы данных!";
            Form1.Connect();
            this.Close();
        }

        private void btnAddColum_Click(object sender, EventArgs e)
        {
            string ColumnType = null;
            if (txtColumnName.Text != "")
            {
                bool IsSelectCheckItem = false;
                for (int i = 0; i < checkListColumnType.Items.Count; i++)
                {
                    if (checkListColumnType.GetItemChecked(i) == true)
                    {
                        IsSelectCheckItem = true;
                        ColumnType = checkListColumnType.Items[i].ToString();
                    }
                }
                if (IsSelectCheckItem == true)
                {
                    listStringColums.Items.Add(txtColumnName.Text + " " + ColumnType);
                    listColumns.Add(txtColumnName.Text + " " + ColumnType);
                    txtColumnName.Text = "";
                }
                else lbStatus.Text = "выберите тип данных столбца в таблицые!";
            }
            else lbStatus.Text = "введите имя для создания столбца в таблицые!";
        }

        private void btnDeleteColum_Click(object sender, EventArgs e)
        {
            bool IsError = true;
            for (int i = 0; i < listStringColums.Items.Count; i++)
            {
                if(listStringColums.SelectedItem == listStringColums.Items[i])
                {
                    IsError = false;
                    listColumns.Remove(listColumns[i]);
                }
            }
            if (IsError) MessageBox.Show("выберите пункт в корзине чтобы удрать товар!");
            else listStringColums.Items.Remove(listStringColums.SelectedItem);
        }

        private void checkListColumnType_Click(object sender, EventArgs e)
        {
            if (checkListColumnType.GetItemChecked(0) == false)
            {
                checkListColumnType.SetItemChecked(1, false);
                checkListColumnType.SetItemChecked(2, false);
            }
            if (checkListColumnType.GetItemChecked(1) == false)
            {
                checkListColumnType.SetItemChecked(0, false);
                checkListColumnType.SetItemChecked(2, false);
            }
            if (checkListColumnType.GetItemChecked(2) == false)
            {
                checkListColumnType.SetItemChecked(1, false);
                checkListColumnType.SetItemChecked(0, false);
            }
        }
    }
}
