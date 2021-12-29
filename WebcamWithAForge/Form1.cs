using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Drawing.Imaging;

namespace WebcamWithAForge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;

        private void Form1_Load(object sender, EventArgs e)
        {
            GetDevices();
        }

        private void gunaPictureBox3_Click(object sender, EventArgs e)
        {
            GetDevices();
        }

        void GetDevices()
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            gunaComboBox1.Items.Clear();
            foreach (FilterInfo item in filterInfoCollection)
            {
                gunaComboBox1.Items.Add(item.Name);
            }
            gunaComboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[gunaComboBox1.SelectedIndex].MonikerString);

                videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                videoCaptureDevice.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            gunaPictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                videoCaptureDevice.Stop();
                gunaPictureBox2.Image = new Bitmap(Application.StartupPath + @"\ImagesAndIcons\aq1BYgp_460s.jpg");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gunaAdvenceButton1_Click(object sender, EventArgs e)
        {
            try
            {
                CheckExist();
                string path = Path.Combine(@"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Substring(System.Security.Principal.WindowsIdentity.GetCurrent().Name.LastIndexOf(@"\"), System.Security.Principal.WindowsIdentity.GetCurrent().Name.Length - System.Security.Principal.WindowsIdentity.GetCurrent().Name.LastIndexOf(@"\")) + @"\Pictures\HasimBeyAppPictures");
                string filePath = path + "\\" + DateTime.Now.Minute + DateTime.Now.Hour + Guid.NewGuid().ToString() + ".png";
                gunaPictureBox2.Image.Save(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CheckExist()
        {
            try
            {
                string path = Path.Combine(@"C:\Users\" + System.Security.Principal.WindowsIdentity.GetCurrent().Name.Substring(System.Security.Principal.WindowsIdentity.GetCurrent().Name.LastIndexOf(@"\"), System.Security.Principal.WindowsIdentity.GetCurrent().Name.Length - System.Security.Principal.WindowsIdentity.GetCurrent().Name.LastIndexOf(@"\")) + @"\Pictures\HasimBeyAppPictures");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
