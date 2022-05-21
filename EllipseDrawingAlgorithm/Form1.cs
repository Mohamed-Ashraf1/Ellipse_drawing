using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EllipseDrawingAlgorithm
{
    public partial class Form1 : Form
    {
        float xCenter, yCenter, xRadius, yRadius = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void drawBtn_Click(object sender, EventArgs e)
        {
            xCenter = float.Parse(centerXText.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
            yCenter = float.Parse(centerYText.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
            xRadius = float.Parse(radiusXText.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
            yRadius = float.Parse(radiusYText.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);

            ellipseMidPoint(xCenter, yCenter, xRadius, yRadius);
        }

        private void ellipseMidPoint(float xCenter, float yCenter, float xRadius, float yRadius)
        {
            float x = 0;
            float y = yRadius;

            double powXRadius = Math.Pow(xRadius, 2);
            double powYRadius = Math.Pow(yRadius, 2);

            double fFactor = 2 * powYRadius * x;
            double sFactor = 2 * powXRadius * y;

            double p = powYRadius - (powXRadius * yRadius) + (0.25 * Math.Pow(xRadius, 2));
            double p2 = 0;

            ellipsePlotPoints(xCenter, yCenter, x, y);

            while (y > 0)
            {
                if (fFactor < sFactor)
                {
                    x++;
                    fFactor = 2 * powYRadius * x;

                    if (p < 0)
                    {
                        p += fFactor + powYRadius;
                    }
                    else
                    {
                        y--;
                        sFactor = 2 * powXRadius * y;
                        p += fFactor - sFactor + powYRadius;
                    }
                } else
                {
                    y--;
                    sFactor = 2 * powXRadius * y;
                    p2 = powYRadius * Math.Pow((x + 0.5), 2) + powXRadius * Math.Pow((y - 1), 2) - powXRadius * powYRadius;
                    
                    if (p2 > 0)
                    {
                        p2 -= sFactor + powXRadius;
                    }
                    else
                    {
                        x++;
                        fFactor = 2 * powYRadius * x;
                        p2 += fFactor - sFactor + powXRadius;
                    }
                }
                ellipsePlotPoints(xCenter, yCenter, x, y);
            }
        }

        private void ellipsePlotPoints(float xCenter, float yCenter, float x, float y)
        {
            var brush = Brushes.Black;
            var g = drawingPanel.CreateGraphics();

            g.FillRectangle(brush, xCenter + x, yCenter + y, 1, 1);
            g.FillRectangle(brush, xCenter - x, yCenter + y, 1, 1);
            g.FillRectangle(brush, xCenter + x, yCenter - y, 1, 1);
            g.FillRectangle(brush, xCenter - x, yCenter - y, 1, 1);
        }
    }
}
