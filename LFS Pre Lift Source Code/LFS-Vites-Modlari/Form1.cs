using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InSimDotNet;
using InSimDotNet.Out;
using InSimDotNet.Helpers;
using WinFormAnimation;
using System.Reflection;
using CircularProgressBar;
using System.Runtime.InteropServices;
using System.Diagnostics;
using SharpUpdate;

namespace LFS_Vites_Modlari
{
    public partial class Form1 : Form, ISharpUpdatable
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        OutGauge outgauge = new OutGauge();

        public int vites = 0;

        private SharpUpdater updater;

        public Form1()
        {
            InitializeComponent();
            updater = new SharpUpdater(this);
            updater.DoUpdate();

            // Start listening for packets
            outgauge.Connect("127.0.0.1", 30000);

            // Attach OutGauge packet event
            outgauge.PacketReceived += (sender, e) => {

                /*
                 Process p = Process.GetProcessesByName("lfs").FirstOrDefault();
                        if (p != null)
                        {
                            IntPtr h = p.MainWindowHandle;
                            SetForegroundWindow(h);
                            SendKeys.SendWait("a");
                            //System.Threading.Thread.Sleep(100);
                        }
                 */



                if (checkBox1.Checked)
                {

                    //Devir
                    label2.Text = Convert.ToInt32(e.RPM).ToString();
                    //Araç
                    label10.Text = Convert.ToString(e.Car);
                    //Hız
                    label9.Text = Convert.ToInt32(e.Speed / 0.27777778).ToString();
                    //Benzin
                    label11.Text = Convert.ToInt32(e.Fuel * 100).ToString();
                    //Vites

                    if (e.Gear == 8)
                    {
                        label12.Text = "7";
                    }
                    else if (e.Gear == 7)
                    {
                        label12.Text = "6";
                    }
                    else if (e.Gear == 6)
                    {
                        label12.Text = "5";
                    }
                    else if (e.Gear == 5)
                    {
                        label12.Text = "4";
                    }
                    else if (e.Gear == 4)
                    {
                        label12.Text = "3";
                    }
                    else if (e.Gear == 3)
                    {
                        label12.Text = "2";
                    }
                    else if (e.Gear == 2)
                    {
                        label12.Text = "1";
                    }
                    else if (e.Gear == 1)
                    {
                        label12.Text = "N";
                    }
                    else if (e.Gear == 0)
                    {
                        label12.Text = "R";
                    }
                    else
                    {
                        label12.Text = Convert.ToInt32(e.Gear).ToString();
                    }

                    //Turbo
                    label13.Text = Convert.ToInt32(e.Turbo * 100).ToString();

                    circularProgressBar4.Value = Convert.ToInt32(e.EngTemp);
                    label19.Text = Convert.ToInt32(e.Packet.EngTemp / 0.27777778).ToString();
                    circularProgressBar5.Value = Convert.ToInt32(e.OilPressure);
                    label20.Text = Convert.ToInt32(e.Packet.OilPressure / 0.27777778).ToString();
                    circularProgressBar6.Value = Convert.ToInt32(e.OilTemp);
                    label21.Text = Convert.ToInt32(e.Packet.OilTemp / 0.27777778).ToString();

                    circularProgressBar1.Value = Convert.ToInt32(e.Throttle * 100);
                    circularProgressBar2.Value = Convert.ToInt32(e.Brake * 100);
                    circularProgressBar3.Value = Convert.ToInt32(e.Clutch * 100);


                    //Görünüm 1
                    textBox1.Text = Convert.ToString(e.Display1);
                    //Görünüm 2
                    textBox2.Text = Convert.ToString(e.Display2);
                }
                else { }
            };
        }

        #region SharpUpdate
        public string ApplicationName
        {
            get { return "LFS-Vites-Modlari"; }
        }

        public string ApplicationID
        {
            get { return "LFS-Vites-Modlari"; }
        }

        public Assembly ApplicationAssembly
        {
            get { return Assembly.GetExecutingAssembly(); }
        }

        public Icon ApplicationIcon
        {
            get { return this.Icon; }
        }

        public Uri UpdateXmlLocation
        {
            get { return new Uri("http://www.lfsturkey.net/vk/vk.xml"); }
        }

        public Form Context
        {
            get { return this; }
        }
        #endregion

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nasıl yapılır ? sayfasının açılması için tamam'a tıklayın!", "BİLGİ");
            nasilyapilir n = new nasilyapilir();
            n.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hesaplama sayfasının açılması için tamam'a tıklayın!", "BİLGİ");
            hesaplama h = new hesaplama();
            h.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("İstatistikler sayfasının açılması için tamam'a tıklayın!", "BİLGİ");
            istatistikler i = new istatistikler();
            i.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test edilen sürümler sayfasının açılması için tamam'a tıklayın!", "BİLGİ");
            testedilensurumler tes = new testedilensurumler();
            tes.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hakkımızda sayfasının açılması için tamam'a tıklayın!", "BİLGİ");
            hakkimizda hkmz = new hakkimizda();
            hkmz.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sport modu pasiftir.", "BİLGİ");
            //MessageBox.Show("Sport modunda vites geçişleri daha üst devirlerde olur.", "BİLGİ");



        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Eco modunda vites geçişleri daha alt devirlerde olur.", "BİLGİ");

            outgauge.PacketReceived += Outgauge_PacketReceived;
            
            timer1.Start();
            timer1.Enabled = true;

        }

        private void Outgauge_PacketReceived(object sender, OutGaugeEventArgs e)
        {
            if (e.Brake >= 0.1)
            {
                if (e.Gear >= 2 && e.Gear <= 8)
                {
                    if (Convert.ToInt32(e.RPM) >= 900)
                    {
                        if (vites <= 2)
                        {
                            Process p = Process.GetProcessesByName("lfs").FirstOrDefault();
                            if (p != null)
                            {
                                IntPtr h = p.MainWindowHandle;
                                SetForegroundWindow(h);
                                SendKeys.Send(textBox4.Text);
                                vites = 3;

                            }

                        }
                        else { }
                    }
                }
                else if (e.Gear == 1)
                {

                }
                else { }
            }
            else if (e.Throttle > 0 && e.Brake < 1)
            {
                if (e.Gear <= 8)
                {
                    if (Convert.ToInt32(e.RPM) >= 3500)
                    {
                        if (vites <= 2)
                        {
                            Process p = Process.GetProcessesByName("lfs").FirstOrDefault();
                            if (p != null)
                            {
                                IntPtr h = p.MainWindowHandle;
                                SetForegroundWindow(h);
                                SendKeys.Send(textBox3.Text);
                                vites = 3;
                            }

                        }
                        else { }
                    }
                }
                else if (e.Gear == 1)
                {

                }
                else { }
            }
            else { }

            if (e.Speed / 0.27777778 <= 20)
            {
                if (e.Gear == 2)
                {

                }
                else
                {
                    if (vites <= 2)
                    {
                        Process p = Process.GetProcessesByName("lfs").FirstOrDefault();
                        if (p != null)
                        {
                            IntPtr h = p.MainWindowHandle;
                            SetForegroundWindow(h);
                            SendKeys.Send(textBox4.Text);
                            vites = 3;

                        }

                    }
                    else { }
                }
            } else { }



        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Form1.ActiveForm.Width = 630;
            }
            else
            {
                Form1.ActiveForm.Width = 315;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (vites >= 0)
            {
                vites -= 3;
            }
        }
    }
}
