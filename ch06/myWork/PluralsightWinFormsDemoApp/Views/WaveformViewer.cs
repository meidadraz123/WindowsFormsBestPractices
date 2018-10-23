using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluralsightWinFormsDemoApp.Views
{
    public partial class WaveformViewer : UserControl
    {
        private float[] peaks;
        private Brush backBrush;
        private Pen waveformPen;

        public WaveformViewer()
        {
            InitializeComponent();
            DoubleBuffered = true;
            hScrollBar1.Scroll += HScrollBar1_Scroll;
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        public void SetPeaks(float[] newPeaks)
        {
            peaks = newPeaks;
            CalculateScrollBar();
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CalculateScrollBar();
        }

        private void CalculateScrollBar()
        {
            if (peaks != null)
            {
                hScrollBar1.Maximum = peaks.Length;
                hScrollBar1.LargeChange = Width;
                hScrollBar1.SmallChange = Width / 10;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (peaks != null)
            {
                backBrush = backBrush ?? new SolidBrush(BackColor);
                waveformPen = waveformPen ?? new Pen(ForeColor);

                var startPeak = hScrollBar1.Value;

                e.Graphics.FillRectangle(backBrush, ClientRectangle);
                for (int xx = 0; (startPeak + xx) < peaks.Length && xx < Width; xx++)
                {
                    var availableHeight = Height - hScrollBar1.Height;
                    var height = peaks[startPeak + xx] * availableHeight;
                    var top = (availableHeight - height) / 2;
                    e.Graphics.DrawLine(waveformPen, xx, top, xx, top + height);
                }
            }
        }
    }
}
