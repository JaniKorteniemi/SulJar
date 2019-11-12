using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;

namespace sulatetun_jarjestelman_projekti
{
    public partial class Form1 : Form
    {
        static SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        private MySQLClient mySql = new MySQLClient();
        string[] sValues = new string[3];
        int[] nValues = new int[3];

        //int vrx;
        //int vry;
        //int button;
        public Form1()
        {
            InitializeComponent();
            MySQLClient.InitDB();
        }
        



        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                port.Open();

            }
            catch(Exception)
            {
                textBox1.Text = "Error!!!!";
            }
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                
                backgroundWorker1.ReportProgress(0, port.ReadLine());
                sValues = port.ReadLine().ToString().Split(' ');
                //Thread.Sleep(10);

            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            textBox1.Text = e.UserState.ToString();
            

            stringToInt();
            textBox2.Text = nValues[0].ToString();
            textBox3.Text = nValues[1].ToString();
            textBox4.Text = nValues[2].ToString();

            mySql.loadMySql(nValues[0], nValues[1], nValues[2]);

            textBox5.Text = mySql.getRepeats().ToString();
        }

        private int[] stringToInt()
        {
            for (int i = 0; i < 3; i++)
            {
                if (sValues == null)
                { nValues[i] = 0; }
                else
                { nValues[i] = int.Parse(sValues[i]); }
            }
            return nValues;
        }
    }
}
