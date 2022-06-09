namespace KansasStateUniversity.TreeViewer2
{
    partial class ScrollingTreePanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            KansasStateUniversity.TreeViewer2.TreeDrawing treeDrawing1 = new KansasStateUniversity.TreeViewer2.TreeDrawing();
            this.uxHorizontalScrollBar = new System.Windows.Forms.HScrollBar();
            this.uxVerticalScrollBar = new System.Windows.Forms.VScrollBar();
            this.uxTree = new KansasStateUniversity.TreeViewer2.TreePanel();
            this.SuspendLayout();
            // 
            // uxHorizontalScrollBar
            // 
            this.uxHorizontalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxHorizontalScrollBar.Location = new System.Drawing.Point(0, 180);
            this.uxHorizontalScrollBar.Name = "uxHorizontalScrollBar";
            this.uxHorizontalScrollBar.Size = new System.Drawing.Size(180, 20);
            this.uxHorizontalScrollBar.SmallChange = 5;
            this.uxHorizontalScrollBar.TabIndex = 0;
            this.uxHorizontalScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.uxHorizontalScrollBar_Scroll);
            // 
            // uxVerticalScrollBar
            // 
            this.uxVerticalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxVerticalScrollBar.Location = new System.Drawing.Point(180, 0);
            this.uxVerticalScrollBar.Name = "uxVerticalScrollBar";
            this.uxVerticalScrollBar.Size = new System.Drawing.Size(20, 180);
            this.uxVerticalScrollBar.SmallChange = 5;
            this.uxVerticalScrollBar.TabIndex = 1;
            this.uxVerticalScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.uxVerticalScrollBar_Scroll);
            // 
            // uxTree
            // 
            this.uxTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uxTree.Drawing = treeDrawing1;
            this.uxTree.Font = new System.Drawing.Font("Courier New", 12F);
            this.uxTree.Location = new System.Drawing.Point(0, 0);
            this.uxTree.Name = "uxTree";
            this.uxTree.Size = new System.Drawing.Size(180, 180);
            this.uxTree.TabIndex = 2;
            this.uxTree.SizeChanged += new System.EventHandler(this.uxTree_SizeChanged);
            // 
            // ScrollingTreePanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.uxTree);
            this.Controls.Add(this.uxVerticalScrollBar);
            this.Controls.Add(this.uxHorizontalScrollBar);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ScrollingTreePanel";
            this.Size = new System.Drawing.Size(200, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar uxHorizontalScrollBar;
        private System.Windows.Forms.VScrollBar uxVerticalScrollBar;
        private TreePanel uxTree;
    }
}
