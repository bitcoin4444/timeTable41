using System;
using System.Windows.Forms;
using TimetableDesktop.Models.Interfaces;

namespace TimetableDesktop
{
    public partial class Form1 : Form
    {
        private readonly IBusinessLogic _businessLogic;
        public Form1(IBusinessLogic businessLogic)
        {
            InitializeComponent();
            _businessLogic = businessLogic;
        }
        private async void DownloadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog.FileName;
            var cnt = await _businessLogic.CreateTimetableFromFile(filename);

            MessageBox.Show("Insert {0} lesson", cnt.ToString()) ;
        }    

    }
}

