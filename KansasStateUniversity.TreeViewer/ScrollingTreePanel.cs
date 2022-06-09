/*
 *
 * ScrollingTreePanel.cs    3/10/12
 * Copyright (c) 2012, Rod Howell, All Rights Reserved.
 *
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KansasStateUniversity.TreeViewer2
{
    /// <summary>
    /// A graphical component displaying a representation of a tree with scroll bars.
    /// </summary>
    /// <remarks>
    /// A <c>ScrollingTreePanel</c> can be constructed quickly from a
    /// <c>TreeDrawing</c>.  Because this
    /// component should not have children, no children are rendered, even if
    /// they are added.  The colors of the contents of the nodes are determined by the
    /// underlying <c>TreeDrawing</c>.  The colors of the lines and boxes are determined
    /// by the foreground color.  Currently, both scroll bars are always shown - 
    /// a later version may make them appear when needed.  The underlying <c>TreeDrawing</c>
    /// can be replaced dynamically using the <c>Drawing</c> property.
    /// </remarks>
    public partial class ScrollingTreePanel : UserControl
    {
        /// <summary>
        /// The number of pixels to retain from the previous view when doing a large scroll.
        /// </summary>
        private const int _scrollOverlap = 25;

        /// <summary>
        /// The minimum value of a large scroll.
        /// </summary>
        private const int _minLargeScroll = 5;

        /// <summary>
        /// Constructs a ScrollingTreePanel with an empty tree and default font.
        /// </summary>
        public ScrollingTreePanel() : this(new TreeDrawing(), null)
        {
        }

        /// <summary>
        /// Constructs a <c>ScrollingTreePanel</c> with the given tree and default font.
        /// </summary>
        /// <param name="t">The tree to display.</param>
        /// <exception cref="System.NullReferenceException">If t is null.</exception>
        public ScrollingTreePanel(TreeDrawing t)
            : this(t, null)
        {
        }

        /// <summary>
        /// Constructs a <c>ScrollingTreePanel</c> with the given tree using the given font.
        /// </summary>
        /// <param name="t">The tree to display.</param>
        /// <param name="f">The font to use to display the tree.</param>
        /// <exception cref="System.NullReferenceException">If t is null.</exception>
        public ScrollingTreePanel(TreeDrawing t, Font f)
        {
            InitializeComponent();
            uxTree.Drawing = t;
            if (f != null)
            {
                uxTree.Font = f;
            }
            uxTree.Height = Height - uxHorizontalScrollBar.Height;
            uxTree.Width = Width - uxVerticalScrollBar.Width;
            SetScrollBarMax();
            SetScrollBarChange();
            uxTree.DrawingSizeChanged += new EventHandler(uxTree_DrawingSizeChanged);
            FontChanged += new EventHandler(ScrollingTreePanel_FontChanged);
            ForeColorChanged += new EventHandler(ScrollingTreePanel_ForeColorChanged);
        }

        /// <summary>
        /// Handles the event signaled when this <c>ScrollingTreePanel</c>'s foreground color
        /// is changed.
        /// </summary>
        /// <param name="sender">The object whose foreground color was changed.</param>
        /// <param name="e">The event arguments (unused).</param>
        void ScrollingTreePanel_ForeColorChanged(object sender, EventArgs e)
        {
            uxTree.ForeColor = ForeColor;
        }

        /// <summary>
        /// Handles the event signaled when the underlying <c>TreePanel</c>'s associated
        /// <c>TreeDrawing</c> is changed.
        /// </summary>
        /// <param name="sender">The object whose <c>TreeDrawing</c> was changed.</param>
        /// <param name="e">The event arguments (unused).</param>
        void uxTree_DrawingSizeChanged(object sender, EventArgs e)
        {
            SetScrollBarMax();
        }

        /// <summary>
        /// Handles the event signaled when this <c>ScrollingTreePanel</c>'s <c>Font</c>
        /// is changed.
        /// </summary>
        /// <param name="sender">The object whose <c>Font</c> was changed.</param>
        /// <param name="e">The event arguments (unused).</param>
        void ScrollingTreePanel_FontChanged(object sender, EventArgs e)
        {
            uxTree.Font = Font;
        }

        /// <summary>
        /// Gets or sets the <c>TreeDrawing</c> being shown.
        /// </summary>
        /// <exception cref="System.NullReferenceException">If given a null <c>TreeDrawing</c>.</exception>
        public TreeDrawing Drawing
        {
            get
            {
                return uxTree.Drawing;
            }
            set
            {
                uxTree.Drawing = value;
                SetScrollBarMax();
            }
        }

        /// <summary>
        /// Sets the maximum values of the scroll bars and ensures that the view is
        /// scrolled to within the rectangle bounding the tree.
        /// </summary>
        private void SetScrollBarMax()
        {
            uxHorizontalScrollBar.Maximum = uxTree.DrawingWidth;
            uxVerticalScrollBar.Maximum = uxTree.DrawingHeight;
            uxHorizontalScrollBar.Value = Math.Min(uxHorizontalScrollBar.Value, 
                uxHorizontalScrollBar.Maximum - uxHorizontalScrollBar.LargeChange + 1);
            uxVerticalScrollBar.Value = Math.Min(uxVerticalScrollBar.Value,
                uxVerticalScrollBar.Maximum - uxVerticalScrollBar.LargeChange + 1);
            uxTree.ScrollTo(uxHorizontalScrollBar.Value, uxVerticalScrollBar.Value);
        }

        /// <summary>
        /// Handles the event signaled when the horizontal scrollbar is scrolled.
        /// </summary>
        /// <param name="sender">The object that was scrolled.</param>
        /// <param name="e">The event arguments.</param>
        private void uxHorizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            DoScroll();
        }

        /// <summary>
        /// Handles the event signaled when the vertical scrollbar is scrolled.
        /// </summary>
        /// <param name="sender">The object that was scrolled.</param>
        /// <param name="e">The event arguments.</param>
        private void uxVerticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            DoScroll();
        }

        /// <summary>
        /// Scrolls the view to match the scroll bars.
        /// </summary>
        private void DoScroll()
        {
            uxTree.ScrollTo(uxHorizontalScrollBar.Value, uxVerticalScrollBar.Value);
        }

        /// <summary>
        /// Handles the event signaled when the size of the underlying <c>TreePanel</c>
        /// is changed.
        /// </summary>
        /// <param name="sender">The object whose size was changed.</param>
        /// <param name="e">The event arguments (unused).</param>
        private void uxTree_SizeChanged(object sender, EventArgs e)
        {
            SetScrollBarChange();
        }

        /// <summary>
        /// Adjusts the large change of the scroll bars to be consistent with the size of the view.
        /// </summary>
        private void SetScrollBarChange()
        {
            uxHorizontalScrollBar.LargeChange = Math.Max(uxTree.Width - _scrollOverlap, _minLargeScroll);
            uxVerticalScrollBar.LargeChange = Math.Max(uxTree.Height - _scrollOverlap, _minLargeScroll);
        }
    }
}
