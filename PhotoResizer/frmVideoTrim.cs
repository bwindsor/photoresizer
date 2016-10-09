using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace PhotoResizer
{
    public partial class frmVideoTrim : Form
    {
        private double startPoint = -1;
        private double endPoint = -1;
        private string wmpURL = "";

        public frmVideoTrim()
        {
            InitializeComponent();
        }
        public frmVideoTrim(string filename)
        {
            InitializeComponent();
            wmpURL = filename;
        }

        public Tuple<double, double> GetTimeRange()
        {
            return new Tuple<double, double>(this.startPoint, this.endPoint);
        }

        private void frmVideoTrim_Load(object sender, EventArgs e)
        {
            // this.DialogResult = DialogResult.Cancel; // Set default return
            Console.WriteLine("Form load happening");
            wmp1.URL = this.wmpURL;

            startPoint = 0;
            endPoint = -1;
            lblStartPoint.Text = SecondsToString(startPoint);
            lblEndPoint.Text = "End point not set";

            this.btnSetStartPoint.Click += new System.EventHandler(this.btnSetStartPoint_Click);
            this.btnSetEndPoint.Click += new System.EventHandler(this.btnSetEndPoint_Click);
        }

        private void btnStepLeft_Click(object sender, EventArgs e)
        {
            IWMPControls2 Ctlcontrols2 = (IWMPControls2)wmp1.Ctlcontrols;
            Ctlcontrols2.step(-1);
        }

        private void btnStepRight_Click(object sender, EventArgs e)
        {
            IWMPControls2 Ctlcontrols2 = (IWMPControls2)wmp1.Ctlcontrols;
            Ctlcontrols2.step(1);
        }

        private void btnSetStartPoint_Click(object sender, EventArgs e)
        {
            wmp1.Ctlcontrols.pause();
            if (endPoint >= 0 && wmp1.Ctlcontrols.currentPosition >= endPoint)
            {
                MessageBox.Show("Start point must be before end point.", "Invalid start point", MessageBoxButtons.OK);
                return;
            }
            startPoint = wmp1.Ctlcontrols.currentPosition;
            lblStartPoint.Text = SecondsToString(startPoint);
        }

        private void btnSetEndPoint_Click(object sender, EventArgs e)
        {
            wmp1.Ctlcontrols.pause();
            if (startPoint >= 0 && wmp1.Ctlcontrols.currentPosition <= startPoint)
            {
                MessageBox.Show("End point must be after start point.", "Invalid end point", MessageBoxButtons.OK);
                return;
            }
            endPoint = wmp1.Ctlcontrols.currentPosition;
            lblEndPoint.Text = SecondsToString(endPoint);
            btnDone.Enabled = true;
        }
        private string SecondsToString(double seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {

        }

        private void btnSetEndPoint_Click_1(object sender, EventArgs e)
        {
            btnSetEndPoint_Click(sender, e);
        }
    }
}
