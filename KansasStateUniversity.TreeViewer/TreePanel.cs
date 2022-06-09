/*
 *
 * TreePanel.cs    3/10/12
 * Copyright (c) 2009, 2012, Rod Howell, All Rights Reserved.
 *
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;

namespace KansasStateUniversity.TreeViewer2
{
    /// <summary>
    /// A graphical component displaying a representation of a tree, or some portion of that
    /// representation.
    /// </summary>
    /// <remarks>
    /// This class has been retained as a public class for backwards compatibility -
    /// <c>ScrollingTreePanel</c> is preferred.  Specifically, system limitations restrict
    /// the size of a tree that can be shown by using auto-scrolling with instances of this
    /// class.
    /// <para>
    /// A <c>TreePanel</c> can be constructed quickly from a
    /// <c>TreeDrawing</c>.  Because this
    /// component should not have children, no children are rendered, even if
    /// they are added.  The colors of the contents of the nodes are determined by the
    /// underlying <c>TreeDrawing</c>.  The colors of the lines and rectangles are
    /// determined by the foreground color.  Capability to scroll the view programmatically
    /// is provided via the <c>ScrollTo</c> method.
    /// </para>
    /// </remarks>
    public class TreePanel : Panel
    {
        /// <summary>
        /// The object containing the description of the tree.
        /// </summary>
        private TreeDrawing _tree;

        /// <summary>
        /// An event indicating that the size of the visual representation of the tree
        /// has changed.
        /// </summary>
        public event EventHandler DrawingSizeChanged;

        /// <summary>
        /// Gets or sets the <c>TreeDrawing</c> being displayed.
        /// </summary>
        /// <exception cref="System.NullReferenceException">If given a null TreeDrawing.</exception>
        public TreeDrawing Drawing
        {
            get
            {
                return _tree;
            }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("The Drawing cannot be null.");
                }
                _tree = value;
                SetDrawingSize();
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the Font used to display the contents of the nodes.
        /// </summary>
        /// <exception cref="System.NullReferenceException">If given a null Font.</exception>
        public override Font Font
        {
            get
            {
                
                return base.Font;
            }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("The Font cannot be null.");
                }
                base.Font = value;
                SetDrawingSize();
                Invalidate();
            }
        }

        /// <summary>
        /// The size of the associated <c>TreeDrawing</c>.
        /// </summary>
        /// <remarks>
        /// The <c>SetDrawingSize</c> method is used to set this value in order to ensure that
        /// a <c>DrawingSizeChanged</c> event is signaled.
        /// </remarks>
        private Size _drawingSize;

        /// <summary>
        /// Gets the width in pixels of the tree being displayed.
        /// </summary>
        public int DrawingWidth
        {
            get
            {
                return _drawingSize.Width;
            }
        }

        /// <summary>
        /// Gets the height in pixels of the tree being displayed.
        /// </summary>
        public int DrawingHeight
        {
            get
            {
                return _drawingSize.Height;
            }
        }

        /// <summary>
        /// Controls the alignment of node labels within their boxes.
        /// </summary>
        private StringFormat _format = new StringFormat();

        /// <summary>
        /// The default <c>Font</c>.
        /// </summary>
        private static readonly Font _defaultFont 
            = new Font(FontFamily.GenericMonospace, 12.0F);

        /// <summary>
        /// The distance from the left edge of the drawing to the left edge of the Panel.
        /// </summary>
        private int _horizontalOffset = 0;

        /// <summary>
        /// The distance from the top edge of the drawing to the top edge of the Panel.
        /// </summary>
        private int _verticalOffset = 0;

        /// <summary>
        /// Gets the default <c>Font</c>.
        /// </summary>
        public static new Font DefaultFont
        {
            get
            {
                return _defaultFont;
            }
        }

        /// <summary>
        /// The character used as the standard for determining the character width.
        /// </summary>
        public const string StandardCharacter = "m";

        /// <summary>
        /// Constructs a <c>TreePanel</c> displaying an empty tree.
        /// </summary>
        public TreePanel()
            : this(new TreeDrawing(), DefaultFont)
        {
        }

        /// <summary>
        /// Constructs a new <c>TreePanel</c> displaying the given <c>TreeDrawing</c> using
        /// the default font.
        /// </summary>
        /// <param name="tree">The tree to be rendered.</param>
        /// <exception cref="System.NullReferenceException">If tree is 
        /// <c>null</c>.</exception>
        public TreePanel(TreeDrawing tree)
            : this(tree, DefaultFont)
        {
        }

        /// <summary>
        /// Constructs a new <c>TreePanel</c> displaying the given <c>TreeDrawing</c> using
        /// the given <c>Font</c>.
        /// </summary>
        /// <param name="tree">The tree to be rendered.</param>
        /// <param name="fnt">The font to use.</param>
        /// <exception cref="System.NullReferenceException">If either argument is <c>null</c>.</exception>
        public TreePanel(TreeDrawing tree, Font fnt)
        {
            if (tree == null || fnt == null) throw new NullReferenceException();
            Drawing = tree;
            Font = fnt;
            _format.Alignment = StringAlignment.Center;
            Size = _drawingSize;
            DoubleBuffered = true;
            ForeColor = Color.Black;
        }

        /// <summary>
        /// Scrolls the Panel so that its upper-left corner is at the given pixel coordinates
        /// of the drawing.
        /// </summary>
        /// <param name="x">The horizontal offset in pixels.</param>
        /// <param name="y">The vertical offset in pixels.</param>
        public void ScrollTo(int x, int y)
        {
            _horizontalOffset = x;
            _verticalOffset = y;
            Invalidate();
        }

        /// <summary>
        /// Computes the size, in pixels, of the underlying drawing.  
        /// </summary>
        private void SetDrawingSize()
        {
            int width = _tree.Width * CharacterWidth();
            int height = (int)(_tree.Height * Font.GetHeight()) + 1;
            _drawingSize = new Size(width, height);
            if (DrawingSizeChanged != null)
            {
                DrawingSizeChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Computes the width of the standard character.
        /// </summary>
        /// <returns>The width of the standard character.</returns>
        private int CharacterWidth()
        {
            return TextRenderer.MeasureText(StandardCharacter + StandardCharacter, Font).Width -
                        TextRenderer.MeasureText(StandardCharacter, Font).Width;
        }

        /// <summary>
        /// Renders this component on the graphics context of the given <c>PaintEventArgs</c>.
        /// </summary>
        /// <param name="ea">Encapsulates the graphics context and clip to use for painting.</param>
        protected override void OnPaint(PaintEventArgs ea)
        {
            Graphics g = ea.Graphics;
            Rectangle clip = ea.ClipRectangle;
            g.Clip = new Region(clip);
            int charWidth = CharacterWidth();
            float lineHeight = Font.GetHeight();
            DoPaint(_tree, g, new PointF(-_horizontalOffset, -_verticalOffset), charWidth, lineHeight);
        }

        /// <summary>
        /// Renders the <c>TreeDrawing</c> on the given graphics context.  
        /// </summary>
        /// <param name="t">The tree to be rendered.</param>
        /// <param name="g">The graphics context on which to render t.</param>
        /// <param name="origin">The coordinates within g of the upper left-hand corner of
        /// the rectangle in which t should be rendered.</param>
        /// <param name="charWidth">The width in pixels of the standard character.</param>
        /// <param name="lineHeight">The height in pixels of a single line.</param>
        private void DoPaint(TreeDrawing t, Graphics g, PointF origin, int charWidth, float lineHeight)
        {
            float viewWidth = (float)(charWidth * t.Width);
            float viewHeight = lineHeight * t.Height;
            float viewRootWidth = (float)(charWidth * t.RootWidth);
            float viewChildrenWidth = (float)(charWidth * t.ChildrenWidth);
            RectangleF clip = g.ClipBounds;
            if (clip.IntersectsWith(new RectangleF(origin.X, origin.Y, viewWidth,
                viewHeight)))
            {
                PointF rootLoc =
                    new PointF(origin.X + (t.Width - t.RootWidth) * charWidth / 2.0F,
                        origin.Y);
                Brush brush = new SolidBrush(t.RootColor);
                Pen pen = new Pen(ForeColor);
                g.DrawRectangle(pen, new Rectangle((int)(rootLoc.X), (int)(rootLoc.Y),
                    (int)viewRootWidth - 1, (int)lineHeight - 1));
                RectangleF rootBox = 
                    new RectangleF(rootLoc.X, rootLoc.Y, viewRootWidth - 1, 
                        lineHeight - 1);
                g.DrawString(t.Root, Font, brush, rootBox, _format);
                PointF edgeStart = 
                    new PointF(origin.X + viewWidth / 2.0F, origin.Y + lineHeight);
                PointF loc = 
                    new PointF(origin.X + (viewWidth - viewChildrenWidth) / 2.0F,
                        origin.Y + (TreeDrawing.VerticalSeparation + 1) * lineHeight);
                foreach (TreeDrawing child in t)
                {
                    if (child.Width != 0)
                    {
                        DoPaint(child, g, loc, charWidth, lineHeight);
                        float cw = (float)(child.Width * charWidth);
                        g.DrawLine(pen, edgeStart.X, edgeStart.Y, loc.X + cw / 2.0F, loc.Y);
                        loc = new PointF(loc.X + cw, loc.Y);
                    }
                    loc = 
                        new PointF(loc.X + TreeDrawing.HorizontalSeparation * charWidth, 
                            loc.Y);
                }
            }
        }
    }
}
