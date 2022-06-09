/*
 * TreeForm.cs     3/10/12
 *
 * Copyright (c) 2008, 2012, Rod Howell, All Rights Reserved.
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

namespace KansasStateUniversity.TreeViewer2
{
    /// <summary>
    /// A form for displaying a tree.  
    /// </summary>
    /// <remarks>
    /// This class provides a straightforward means
    /// for displaying a tree that implements <c>ITree</c>.
    /// The form contains scroll bars to view the tree. Each constructor requires
    /// a maximum height to be provided so that if the tree contains a cycle, infinite loops
    /// will be avoided.  Once constructed, the <c>TreeDrawing</c> can
    /// be displayed using its <c>Show</c> method. For use in debugging,
    /// the <c>ShowDialog</c> method can be used in order to force the form to be drawn 
    /// prior to a breakpoint.
    /// </remarks>
    public class TreeForm : Form
    {
        /// <summary>
        /// The control displaying the tree.
        /// </summary>
        private ScrollingTreePanel uxTree;

        /// <summary>
        /// Contstructs a <c>TreeForm</c> displaying the given tree.  
        /// </summary>
        /// <param name="t">The tree to be displayed.</param>
        /// <param name="maxHeight">The maximum height to be displayed.</param>
        /// <param name="c">The <c>IColorizer</c> to colorize the tree.</param>
        /// <exception cref="System.NullReferenceException">If t is null</exception>
        public TreeForm(ITree t, int maxHeight, IColorizer c) 
            : this(t, maxHeight, c, TreePanel.DefaultFont)
        {
        }

        /// <summary>
        /// Contstructs a <c>TreeForm</c> displaying the given tree.  
        /// </summary>
        /// <param name="t">The tree to be displayed.</param>
        /// <param name="maxHeight">The maximum height to be displayed.</param>
        public TreeForm(ITree t, int maxHeight) 
            : this(t, maxHeight, null, TreePanel.DefaultFont)
        {
        }

        /// <summary>
        /// Constructs a <c>TreeForm</c> displaying the given tree using the given 
        /// <c>Font</c> and <c>IColorizer</c>. 
        /// </summary>
        /// <param name="t">The tree to be displayed.</param>
        /// <param name="maxHeight">The maximum height to be displayed.</param>
        /// <param name="c">The <c>IColorizer</c> to colorize the tree.</param>
        /// <param name="fnt">The <c>Font</c> to use.</param>
        /// <exception cref="System.NullReferenceException">If fnt is 
        /// <c>null</c>.</exception>
        public TreeForm(ITree t, int maxHeight, IColorizer c, Font fnt) 
        {
            InitializeComponent();
            uxTree.Font = fnt;
            uxTree.Drawing = new TreeDrawing(t, maxHeight, c);
            AutoScroll = false;
        }

        /// <summary>
        /// Constructs a <c>TreeForm</c> displaying the given tree using the given 
        /// <c>Font</c>.
        /// </summary>
        /// <param name="t">The tree to be displayed.</param>
        /// <param name="maxHeight">The maximum height to be displayed.</param>
        /// <param name="fnt">The Font to use.</param>
        /// <exception cref="System.NullReferenceException">If fnt is 
        /// <c>null</c>.</exception>
        public TreeForm(ITree t, int maxHeight, Font fnt) 
            : this(t, maxHeight, null, fnt)
        {
        }

        /// <summary>
        /// Initializes this GUI.
        /// </summary>
        private void InitializeComponent()
        {
            KansasStateUniversity.TreeViewer2.TreeDrawing treeDrawing1 = new KansasStateUniversity.TreeViewer2.TreeDrawing();
            this.uxTree = new KansasStateUniversity.TreeViewer2.ScrollingTreePanel();
            this.SuspendLayout();
            // 
            // uxTree
            // 
            this.uxTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxTree.Drawing = treeDrawing1;
            this.uxTree.ForeColor = System.Drawing.Color.Black;
            this.uxTree.Location = new System.Drawing.Point(0, 0);
            this.uxTree.Name = "uxTree";
            this.uxTree.Size = new System.Drawing.Size(282, 259);
            this.uxTree.TabIndex = 0;
            // 
            // TreeForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.uxTree);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "TreeForm";
            this.FontChanged += new System.EventHandler(this.TreeForm_FontChanged);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Handles the event signaled with this <c>TreeForm</c>'s <c>Font</c> is changed.
        /// </summary>
        /// <param name="sender">The object whose Font was changed.</param>
        /// <param name="e">The event arguments (unused).</param>
        void TreeForm_FontChanged(object sender, EventArgs e)
        {
            uxTree.Font = Font;
        }
    }
}
