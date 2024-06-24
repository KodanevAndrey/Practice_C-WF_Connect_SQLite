using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectSQLite_KodanevAndrey
{
    public partial class Form1 : Form
    {
        private bool isActive = false;
        private DBHelper db = new DBHelper();
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(500, 300);
            this.MaximizeBox = false;
        }

        public void Connect()
        {
            if (db.ConnectDB(lbStatusText))
            {
                db.LoadCountTables(lbStatusText);
                db.GetTableInfo(lbStatusText);
                db.LoadTableInfo(lbStatusText, dgvViewer);
                db.ReadDB(lbStatusText, dgvViewer);
            }
        }

        private void btnAddDB_Click(object sender, EventArgs e)
        {
            db.AddDB(lbStatusText,lbCommand,dgvViewer);
        }

        private void btnReadDB_Click(object sender, EventArgs e)
        {
            db.ReadDB(lbStatusText,dgvViewer);
        }

        private void btnResetDB_Click(object sender, EventArgs e)
        {
            db.ResetDB(lbStatusText,lbCommand,dgvViewer);
            db.ReadDB(lbStatusText, dgvViewer);
        }

        private void btnDeleteDB_Click(object sender, EventArgs e)
        {
            db.DeleteDB(lbStatusText);
            db.ReadDB(lbStatusText, dgvViewer);
        }

        private void btnDeleteAllDB_Click(object sender, EventArgs e)
        {
            db.DeleteAllDB(lbStatusText);
            db.ReadDB(lbStatusText, dgvViewer);
        }

        private void btnAddImageDB_Click(object sender, EventArgs e)
        {
            db.AddImageToDB(lbStatusText,lbCommand,dgvViewer);
            db.ReadDB(lbStatusText, dgvViewer);
        }

        private void dgvViewer_Click(object sender, EventArgs e)
        {
            db.SelectToTable(lbStatusText,dgvViewer);
        }

        private void btnConnectDB_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void btnCreateNewDB_Click(object sender, EventArgs e)
        {
            FormCreateDB FCDB = new FormCreateDB();
            FCDB.Show();
        }
    }
}
