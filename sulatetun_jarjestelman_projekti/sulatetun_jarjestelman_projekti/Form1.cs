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
//using System.ComponentModel;
using System.Net;


namespace sulatetun_jarjestelman_projekti
{
    public partial class Form1 : Form
    {
        static SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        private MySQLClient mySql = new MySQLClient();

        
        string[] sValues = new string[3];
        //string[] tempS = new string[3];
        int[] nValues = new int[3] { 0, 0, 0 };
        //int breakUpdater = 0;
        string json;

        int tempSpeed = 0;
        int tempTurn = 0;
        int tempButton = 0;

        double x = 0;
        double y = 0;
        int speed = 1;
        int deg = 1;
        int gyroCounter = 0;
        //int recordedSaves = 0;
        //int validationCounter = 0;
        bool recordOn = false;
        bool playOn = false;
        //bool buttonState = true;
        int waitTimer = 0;
        bool deleteTimer = true;

        Client rclient = new Client()
        {
            speedControl = 0,
            turnControl = 0,
            button = 0,
            Deg = 0,
            Speed = 0,
        };

        //int vrx;
        //int vry;
        //int button;
        public Form1()
        {
            InitializeComponent();

            movementPanel.ChartAreas[0].AxisX.Maximum = 5000;
            movementPanel.ChartAreas[0].AxisX.Minimum = -5000;
            movementPanel.ChartAreas[0].AxisY.Maximum = 5000;
            movementPanel.ChartAreas[0].AxisY.Minimum = -5000;

            //chart1.ChartAreas[0].AxisX.Maximum = 15;
            //chart1.ChartAreas[0].AxisX.Minimum = 0;
            /*chart1.ChartAreas[0].AxisY.Maximum = 10;
            chart1.ChartAreas[0].AxisY.Minimum = -10;*/

            RecordBoxGreen.Visible = false;
            playBoxGreen.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            port.Open();
            loop();
        }


        private void loop()
        {
            uiUpdater.DoWork += BackgroundWorker1_DoWork1;
            uiUpdater.RunWorkerAsync();

            ClientUpdater.RunWorkerAsync();
        }

        private void BackgroundWorker1_DoWork1(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void uiUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (uiUpdater.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                uiUpdater.ReportProgress(0, port.ReadLine());
                sValues = port.ReadLine().ToString().Split(' ');
                stringToInt();
            }            
        }

        private void uiUpdater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            updateUI(e);
        }

        private void ClientUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                rclient.speedControl = nValues[0];
                rclient.turnControl = nValues[1];
                //playOn = rclient.playStopped();

                if (recordOn == true && playOn == true)
                {
                    recordOn = false;
                    playOn = false;
                }
                

                switch (nValues[2])
                {
                    case 2:
                        if (recordOn == false && playOn == false)
                        {
                            recordOn = true;
                            tempButton = 2;
                            rclient.repeatControl = 3;
                        }

                        break;
                    case 3:
                        tempButton = 3;
                        if (waitTimer == 0)
                        {
                            if (recordOn == false && playOn == true)
                            {
                                playOn = false;
                                rclient.repeatControl = 2;
                                waitTimer = 50;
                            }
                            else if (recordOn == false && playOn == false)
                            {
                                playOn = true;
                                rclient.repeatControl = 1;
                                waitTimer = 50;
                            }
                            else if (recordOn == true && playOn == false)
                            {
                                recordOn = false;
                                rclient.repeatControl = 0;
                            }
                        }

                        break;
                    case 4:
                        tempButton = 4;
                        rclient.repeatControl = 0;
                        json = rclient.serialize(rclient);
                        rclient.postRequest(json);

                        break;
                    case 5:
                        if (deleteTimer == true)
                        {
                            tempButton = 5;
                            rclient.repeatControl = 0;
                            json = rclient.serialize(rclient);
                            rclient.postRequest(json);
                            deleteTimer = false;
                        }

                        break;
                    case 0:
                        if (recordOn == false && playOn == false)
                        {
                            rclient.repeatControl = 0;
                            tempButton = 0;
                        }
                        else if (recordOn == true && playOn == false)
                        {
                            tempButton = 2;
                            rclient.repeatControl = 3;
                        }
                        break;
                    default:
                        break;
                }

                if (tempButton != 4 || tempButton != 5)
                {
                    rclient.button = tempButton;
                    json = rclient.serialize(rclient);
                    Console.WriteLine("postRequest POST");
                    rclient.postRequest(json);
                    deleteTimer = true;
                    if (waitTimer != 0)
                    {
                        waitTimer--;
                    }
                    
                }
                /*else if (playOn == true)
                {
                    if (gyroCounter < rclient.gyroID)
                    {
                        rclient.getGyro();
                        gyroCounter++;
                    }
                }
                else if (playOn == false && gyroCounter != 0)
                {
                    gyroCounter = 0;
                    rclient.gyroID = 0;
                    //recordedSaves = 0;
                }*/

                Thread.Sleep(300);
            }
        }

        private void ClientUpdater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        public void updateUI(ProgressChangedEventArgs e)
        {

            moveChart();
           /* if (breakUpdater >= 5)
            {
                speedChart();
                breakUpdater = 0;
            }*/
            updateTextBox(e);
        }

        private int[] stringToInt()
        {
            for (int i = 0; i < 3; i++)
            {
                if (sValues == null)
                { nValues[i] = 0; }
                else
                {
                    try { nValues[i] = int.Parse(sValues[i]); }
                    catch { nValues[i] = 0; }
                }
                if (nValues[1] == -10)
                {
                    nValues[1] = 0;
                }

            }
            return nValues;
        }

        private void moveChart()
        {
            //speed = rclient.Speed;
            //deg = rclient.Deg;
            //deg
            //speed;
            //double moveDeg = 0;

            /*try
            {
                moveDeg = Convert.ToDouble(deg);
            }
            catch (Exception)
            {
                moveDeg = 0;
                speed = 0;
                Console.WriteLine("movechart Error");
            }

             x += speed * Math.Cos(moveDeg);
             y += speed * Math.Sin(moveDeg);*/
            int chartSpeed = 5;

            x += -((nValues[0] / 5000) * chartSpeed);
            y += -(nValues[1] / 5000) * chartSpeed;
            movementPanel.Series["move"].Points.AddXY(y, x);
            
        }
        private void updateTextBox(ProgressChangedEventArgs e)
        {
            if (recordOn == true)
            {
                RecordBoxGreen.Visible = true;
                RecordBoxRed.Visible = false;
            }
            else if (recordOn == false)
            {
                RecordBoxRed.Visible = true;
                RecordBoxGreen.Visible = false;
            }

            if (playOn == true)
            {
                playBoxGreen.Visible = true;
                playBoxRed.Visible = false;
            }
            else if (playOn == false)
            {
                playBoxRed.Visible = true;
                playBoxGreen.Visible = false;
            }

            if (playOn == false && recordOn == true)
            {
                textBox6.Text = "pF rT";
            }
            else if (playOn == true && recordOn == false)
            {
                textBox6.Text = "pT rF";
            }
            else if(playOn == false && recordOn == false)
            {
                textBox6.Text = "pF rF";
            }
            else
            {
                textBox6.Text = "?";
            }

            JsonTextbox.Text = json;

            counttextBox.Text = gyroCounter.ToString();
            speedtextBox.Text = speed.ToString();
            degtextBox.Text = deg.ToString();

            textBox1.Text = e.UserState.ToString();

            textBox2.Text = nValues[0].ToString();
            textBox3.Text = nValues[1].ToString();
            textBox4.Text = nValues[2].ToString();

            textBoxX.Text = x.ToString();
            textBoxY.Text = y.ToString();


            
            textBox7.Text = tempButton.ToString();
            textBox8.Text = rclient.repeatControl.ToString();
        }
        
        private bool controllerIdle()
        {
            bool tmpB = false;
            if (tempSpeed == nValues[0] && tempTurn == nValues[1] && tempButton == nValues[2])
            {
                tmpB = true;
            }

            tempSpeed = nValues[0];
            tempTurn = nValues[1];
            tempButton = nValues[2];

            if (tmpB) { return true; }
            else { return false; }
        }
    }
}
