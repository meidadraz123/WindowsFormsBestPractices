﻿using System;
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
        private int positionInSeconds;

        public WaveformViewer()
        {
            InitializeComponent();
            DoubleBuffered = true;
            hScrollBar1.Scroll += HScrollBar1_Scroll;
            PositionInSeconds = 0;
            this.MouseClick += OnMouseClick;
        }

        private void OnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (mouseEventArgs.Button == MouseButtons.Left)
            {
                var desiredPosition = mouseEventArgs.X + hScrollBar1.Value;
                PositionInSeconds = desiredPosition;
                OnPositionChanged();
            }
        }

        public event EventHandler PositionChanged;

        protected virtual void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, EventArgs.Empty);
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

        public int PositionInSeconds
        {
            get { return positionInSeconds; }
            set
            {
                if (positionInSeconds != value)
                {
                    positionInSeconds = value;
                    Invalidate();
                }
            }
        }

        private static readonly Pen positionPen = new Pen(Color.FromArgb(80, 80, 80), 2);
        private static readonly Brush positionBrush = new SolidBrush(Color.FromArgb(229, 215, 200));

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

            var positionX = PositionInSeconds - hScrollBar1.Value;
            e.Graphics.DrawLine(positionPen, positionX, 0, positionX, Height);

            var timeString = TimeSpan.FromSeconds(PositionInSeconds).ToString(@"hh\:mm\:ss");
            var timeStringRect = e.Graphics.MeasureString(timeString, Font);
            var timeRect = new Rectangle(positionX, 1, (int)timeStringRect.Width + 6, 15);

            e.Graphics.FillRectangle(positionBrush, timeRect);
            e.Graphics.DrawRectangle(positionPen, timeRect);
            timeRect.Inflate(-2, -2);
            e.Graphics.DrawString(timeString, Font, Brushes.Black, timeRect);
        }
    }
}
