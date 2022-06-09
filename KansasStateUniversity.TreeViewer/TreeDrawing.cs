/*
 * TreeDrawing.cs     2/28/09
 *
 * Copyright (c) 2009, Rod Howell, All Rights Reserved.
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace KansasStateUniversity.TreeViewer2
{
    /// <summary>
    /// An immutable high-level representation of a drawing of a tree. 
    /// </summary>
    /// <remarks>
    /// <para>
    /// This tree
    /// representation contains a font-independent description of the size of
    /// the drawing along with all the information necessary to draw the tree
    /// on a given graphics context using a given font.  
    /// </para>
    /// <para>
    /// A <c>TreeDrawing</c> can be constructed from an implementation of <c>ITree</c> 
    /// and optionally an implementation of <c>IColorizer</c>.  No
    /// attempt is made to detect cycles or overlapping subtrees; instead,
    /// a parameter to the constructor specifies the maximum height of the tree
    /// drawn.
    /// </para>
    /// <para>
    /// A <c>TreePanel</c> containing a graphical representation
    /// can be efficiently obtained from either the <c>GetDrawing()</c> or the 
    /// <c>GetDrawing(Font)</c> method.  These methods return a new <c>TreePanel</c>
    /// each time they are called, so they can be used in multiple containers.  These
    /// components will all share the same <c>TreeDrawing</c>.
    /// </para>
    /// <para>
    /// Empty trees are not drawn, but if a node contains both empty and nonempty
    /// children, the horizontal padding that would surround a node is included
    /// for empty children.  Consequently, in a binary tree, the line drawn from
    /// a node to its left child always angles to the left, and the line drawn
    /// from a node to its right child always angles to the right, even when the
    /// other child is empty.  On the other hand, it may be more difficult to tell
    /// which children may be empty in trees with other branching factors.
    /// </para>
    /// <para>
    /// The children, each of which is a TreeDrawing, can be obtained using the 
    /// <c>GetEnumerator()</c> method.  This method returns a
    /// <c>System.Collections.IEnumerator</c> of the children.  This method allows a 
    /// C# <c>foreach</c> construct to iterate through the children.
    /// </para>
    /// </remarks>
    public sealed class TreeDrawing
    {
        /// <summary>
        /// A string representation of the root of the tree.
        /// </summary>
        private string _root;

        /// <summary>
        /// The color with which to draw the root.
        /// </summary>
        private Color _color;

        /// <summary>
        /// Drawings of the children of the root.
        /// </summary>
        private TreeDrawing[] _children;

        /// <summary>
        /// The width in characters of this drawing.
        /// </summary>
        private int _width;

        /// <summary>
        /// The width in characters of the root, including padding.
        /// </summary>
        private int _rootWidth;

        /// <summary>
        /// The total width in characters of all children, including padding between
        /// children.
        /// </summary>
        private int _childrenWidth;

        /// <summary>
        /// The height in text lines of this drawing.
        /// </summary>
        private int _height;

        /// <summary>
        /// The width in characters of the horizontal separation between two nodes.
        /// </summary>
        public const int HorizontalSeparation = 1;

        /// <summary>
        /// The height in lines of text of the vertical separation between parents
        /// and children.
        /// </summary>
        public const int VerticalSeparation = 1;

        /// <summary>
        /// Constructs a drawing of an empty tree.
        /// </summary>
        public TreeDrawing()
        {
        }

        /// <summary>
        /// Constructs a drawing of the given tree.  T
        /// </summary>
        /// <param name="tree">The tree to be drawn.</param>
        /// <param name="maxHeight">The maximum height to display.  If this value is
        /// negative, no nodes will be displayed.</param>
        /// <remarks>
        /// he foreground of the drawing 
        /// is black.  The contents of nodes are obtained using the 
        /// <c>ToString()</c> method (<see cref="System.Object.ToString"/>).  If the 
        /// root of the tree is <c>null</c>, the string <c>"null"</c> is used.
        /// </remarks>
        public TreeDrawing(ITree tree, int maxHeight)
            : this(tree, maxHeight, null)
        {
        }

        /// <summary>
        /// Constructs a colorized drawing of the given tree.  
        /// </summary>
        /// <param name="tree">The tree to be drawn.  If <c>null</c>, no tree is 
        /// drawn.</param>
        /// <param name="maxHeight">The maximum height to display.  If this value is
        /// negative, no nodes will be displayed.</param>
        /// <param name="col">The <c>IColorizer</c> used to obtain the color of each node.
        /// If <c>null</c>, each node is colored black.</param>
        /// <remarks>
        /// The contents of nodes
        /// are obtained using the 
        /// <c>ToString()</c> method (<see cref="System.Object.ToString"/>).  If the 
        /// root of the tree is <c>null</c>, the string <c>"null"</c> is used.
        /// </remarks>
        public TreeDrawing(ITree tree, int maxHeight, IColorizer col)
        {
            if (maxHeight-- >= 0 && tree != null && !tree.IsEmpty)
            {
                ITree[] chldr = tree.Children;
                if (chldr == null)
                {
                    _children = new TreeDrawing[0];
                }
                else
                {
                    _children = new TreeDrawing[chldr.Length];
                    for (int i = 0; i < chldr.Length; i++)
                    {
                        _children[i] = new TreeDrawing(chldr[i], maxHeight, col);
                    }
                }
                Object r = tree.Root;
                _root = r == null ? "null" : r.ToString();
                computeSize();
                _color = col == null ? Color.Black : col.GetColor(r);
                if (_color == null)
                {
                    _color = Color.Black;
                }
            }
        }

        /// <summary>
        /// Returns an <c>IEnumerator</c> giving the children, in order.  
        /// </summary>
        /// <returns>An <c>IEnumerator</c> giving the children, in order.</returns>
        /// <remarks>
        /// Each element in the <c>IEnumerator</c> is a <c>TreeDrawing</c>.
        /// </remarks>
        public IEnumerator GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        /// <summary>
        /// Gets the root of the tree.
        /// </summary>
        public string Root
        {
            get
            {
                return _root;
            }
        }

        /// <summary>
        /// Computes the size fields for a nonempty tree.
        /// </summary>
        private void computeSize()
        {
            _rootWidth = _root.Length + 2; // pad both ends
            _childrenWidth = 0;
            if (_children.Length > 0)
            {
                if (_children[0] != null)
                {
                    _childrenWidth += _children[0].Width;
                    _height = _children[0].Height;
                }
                for (int i = 1; i < _children.Length; i++)
                {
                    _childrenWidth += HorizontalSeparation;
                    _childrenWidth += _children[i].Width;
                    _height = Math.Max(_height,
                                      _children[i].Height);
                }
            }
            _width = Math.Max(_rootWidth, _childrenWidth);
            _height += (_height == 0 ? 0 : VerticalSeparation) + 1;
        }

        /// <summary>
        /// Gets the width of this tree in characters.
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// Gets the height of this tree in text lines.
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
        }

        /// <summary>
        /// Gets the width of the root in characters.
        /// </summary>
        public int RootWidth
        {
            get
            {
                return _rootWidth;
            }
        }

        /// <summary>
        /// Gets the total width of the children in characters.
        /// </summary>
        public int ChildrenWidth
        {
            get
            {
                return _childrenWidth;
            }
        }

        /// <summary>
        /// Gets the color of the root.
        /// </summary>
        public Color RootColor
        {
            get
            {
                return _color;
            }
        }

        /// <summary>
        /// Returns a <c>TreePanel</c> displaying this drawing using
        /// <c>TreePanel.DefaultFont</c>.
        /// </summary>
        /// <returns>A <c>TreePanel</c> displaying this drawing using
        /// <c>TreePanel.DefaultFont</c>.</returns>
        public TreePanel GetDrawing()
        {
            return new TreePanel(this);
        }

        /// <summary>
        /// Returns a <c>TreePanel</c> displaying this drawing using the 
        /// given <c>Font</c>.
        /// </summary>
        /// <param name="fnt">The font used to display the tree.</param>
        /// <returns>A <c>TreePanel</c> displaying this drawing using the 
        /// given <c>Font</c>.</returns>
        /// <exception cref="System.NullReferenceException">If fnt is <c>null</c>.
        /// </exception>
        public TreePanel GetDrawing(Font fnt)
        {
            return new TreePanel(this, fnt);
        }

    }
}
