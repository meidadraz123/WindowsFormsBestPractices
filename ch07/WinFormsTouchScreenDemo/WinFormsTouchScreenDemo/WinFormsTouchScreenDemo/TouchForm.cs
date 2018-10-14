using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    // Base class for multi-touch aware form.
    // Receives touch notifications through Windows messages and converts them
    // to touch events Touchdown, Touchup and Touchmove.

    public partial class TouchForm : WMTouchForm
    {
        // Constructor
        public TouchForm()
        {
            InitializeComponent();

            // Create data members, color generator and stroke collections.
            touchColor = new TouchColor();
            ActiveStrokes = new CollectionOfStrokes();
            FinishedStrokes = new CollectionOfStrokes();

            // Setup handlers
            Touchdown += OnTouchDownHandler;
            Touchup += OnTouchUpHandler;
            TouchMove += OnTouchMoveHandler;
            Paint += new PaintEventHandler(this.OnPaintHandler);

            // Set window background color
            this.BackColor = SystemColors.Window;
        }

        // Touch down event handler.
        // Starts a new stroke and assigns a color to it. 
        // in:
        //      sender      object that has sent the event
        //      e           touch event arguments
        private void OnTouchDownHandler(object sender, WMTouchEventArgs e)
        {
            // We have just started a new stroke, which must have an ID value unique
            // among all the strokes currently being drawn. Check if there is a stroke
            // with the same ID in the collection of the strokes in drawing.
            Debug.Assert(ActiveStrokes.Get(e.Id) == null);

            // Create new stroke, add point and assign a color to it.
            Stroke newStroke = new Stroke();
            newStroke.Color = touchColor.GetColor(e.IsPrimaryContact);
            newStroke.Id = e.Id;

            // Add new stroke to the collection of strokes in drawing.
            ActiveStrokes.Add(newStroke);
        }

        // Touch up event handler.
        // Finishes the stroke and moves it to the collection of finished strokes.
        // in:
        //      sender      object that has sent the event
        //      e           touch event arguments
        private void OnTouchUpHandler(object sender, WMTouchEventArgs e)
        {
            // Find the stroke in the collection of the strokes in drawing
            // and remove it from this collection.
            Stroke stroke = ActiveStrokes.Remove(e.Id);
            Debug.Assert(stroke != null);

            // Add this stroke to the collection of finished strokes.
            FinishedStrokes.Add(stroke);

            // Request full redraw.
            Invalidate();
        }

        // Touch move event handler.
        // Adds a point to the active stroke and draws new stroke segment.
        // in:
        //      sender      object that has sent the event
        //      e           touch event arguments
        private void OnTouchMoveHandler(object sender, WMTouchEventArgs e)
        {
            // Find the stroke in the collection of the strokes in drawing.
            Stroke stroke = ActiveStrokes.Get(e.Id);
            Debug.Assert(stroke != null);

            // Add contact point to the stroke
            stroke.Add(new Point(e.LocationX, e.LocationY));

            // Partial redraw: only the last line segment
            Graphics g = this.CreateGraphics();
            stroke.DrawLast(g);
        }

        // OnPaint event handler.
        // in:
        //      sender      object that has sent the event
        //      e           paint event arguments
        private void OnPaintHandler(object sender, PaintEventArgs e)
        {
            // Full redraw: draw complete collection of finished strokes and
            // also all the strokes that are currently in drawing.
            FinishedStrokes.Draw(e.Graphics);
            ActiveStrokes.Draw(e.Graphics);
        }

        // Attributes
        private TouchColor touchColor;                  // Color generator
        private CollectionOfStrokes FinishedStrokes;    // Collection of finished strokes
        private CollectionOfStrokes ActiveStrokes;      // Collection of active strokes, currently being drawn by the user
    }

    // Color generator: assigns a color to the new stroke.
    public class TouchColor
    {
        // Constructor
        public TouchColor()
        {
        }

        // Returns color for the newly started stroke.
        // in:
        //      primary         boolean, whether the contact is the primary contact
        // returns:
        //      color of the stroke
        public Color GetColor(bool primary)
        {
            if (primary)
            {
                // The primary contact is drawn in black.
                return Color.Black;
            }
            else
            {
                // Take current secondary color.
                Color color = secondaryColors[idx];

                // Move to the next color in the array.
                idx = (idx + 1) % secondaryColors.Length;

                return color;
            }
        }

        // Attributes
        static private Color[] secondaryColors =    // Secondary colors
        {
            Color.Red,
            Color.LawnGreen,
            Color.Blue,
            Color.Cyan,
            Color.Magenta,
            Color.Yellow
        };
        private int idx = 0;                // Rotating secondary color index
    }

    // Stroke object represents a single stroke, trajectory of the finger from
    // touch-down to touch-up. Object has two properties: color of the stroke,
    // and ID used to distinguish strokes coming from different fingers.
    public class Stroke
    {
        // Stroke constructor.
        public Stroke()
        {
            points = new ArrayList();
        }

        // Draws the complete stroke.
        // in:
        //      graphics        the drawing surface
        public void Draw(Graphics graphics)
        {
            if ((points.Count < 2) || (graphics == null))
            {
                return;
            }

            Pen pen = new Pen(color, penWidth);
            graphics.DrawLines(pen, (Point[])points.ToArray(typeof(Point)));
        }

        // Draws only last segment of the stroke.
        // in:
        //      graphics        the drawing surface
        public void DrawLast(Graphics graphics)
        {
            if ((points.Count < 2) || (graphics == null))
            {
                return;
            }

            Pen pen = new Pen(color, penWidth);
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            graphics.DrawLine(pen, (Point)points[points.Count - 2], (Point)points[points.Count - 1]);
        }

        // Access to the property stroke color
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        // Access to the property stroke ID
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        // Adds a point to the stroke.
        // in:
        //      pt          point to be added to the stroke
        public void Add(Point pt)
        {
            points.Add(pt);
        }

        // Attributes
        private ArrayList points;               // the array of stroke points
        private Color color;                    // the color of the stroke
        private int id;                         // stroke ID

        private const float penWidth = 3.0f;    // pen width for drawing the stroke
    }

    // CollectionOfStrokes object represents a collection of the strokes.
    // It supports add/remove stroke operations and finding a stroke by ID.
    public class CollectionOfStrokes
    {
        // CollectionOfStrokes constructor.
        public CollectionOfStrokes()
        {
            strokes = new ArrayList();
        }

        // Draw the collection of the strokes.
        // in:
        //      graphics        the drawing surface
        public void Draw(Graphics graphics)
        {
            foreach (Stroke stroke in strokes)
            {
                stroke.Draw(graphics);
            }
        }

        // Adds the stroke to the collection.
        // in:
        //      stroke          stroke to be added
        public void Add(Stroke stroke)
        {
            strokes.Add(stroke);
        }

        // Returns the stroke with the given ID.
        // in:
        //      id              stroke ID
        // returns:
        //      stroke, if found, or null
        public Stroke Get(int id)
        {
            int i = _IndexFromId(id);
            if (i == -1)
            {
                return null;
            }
            return (Stroke)strokes[i];
        }

        // Remove the stroke from the collection.
        // in:
        //      id              stroke ID
        // returns:
        //      stroke, if found, or null
        public Stroke Remove(int id)
        {
            int i = _IndexFromId(id);
            if (i == -1)
            {
                return null;
            }
            Stroke s = (Stroke)strokes[i];
            strokes.RemoveAt(i);
            return s;
        }

        public void Clear()
        {
            // Removes all the strokes from the collection.
            strokes.Clear();
        }

        // Search the collection for given ID.
        // in:
        //      id      stroke ID
        // returns:
        //      stroke index in the array
        private int _IndexFromId(int id)
        {
            for (int i = 0; i < strokes.Count; ++i)
            {
                Stroke stroke = (Stroke)strokes[i];
                if (id == stroke.Id)
                {
                    return i;
                }
            }
            return -1;
        }

        // Attributes
        private ArrayList strokes;          // the array of strokes
    }
}
