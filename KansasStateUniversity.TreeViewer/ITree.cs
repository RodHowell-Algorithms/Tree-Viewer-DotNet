/*
 * ITree.cs    5/13/08
 *
 * Copyright (c) 2008, Rod Howell, All Rights Reserved.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KansasStateUniversity.TreeViewer2
{
    /// <summary>
    /// This interface may be implemented by a tree in order to allow a
    /// <c>TreeDrawing</c> to be constructed from the tree.  
    /// </summary>
    /// <remarks>
    /// Empty trees may be
    /// represented either by <c>null</c> references or by objects whose
    /// <c>IsEmpty</c> properties are true.  If <c>null</c> references
    /// are used, <c>IsEmpty</c> should always be <c>false</c>.
    /// </remarks>
    public interface ITree
    {
        /// <summary>
        /// Gets the root of the tree.  
        /// </summary>
        /// <remarks>
        /// The <c>TreeDrawing</c> class
        /// converts the returned object to a <c>String</c>.  In
        /// order to control how this conversion is done, its <c>ToString()</c>
        /// method may be overridden.
        /// <see cref="System.Object.ToString"/>
        /// </remarks>
        object Root { get; }

        /// <summary>
        /// Gets the children of the tree.  
        /// </summary>
        /// <remarks>
        /// A <c>null</c> returned value
        /// is treated as equivalent to an array of length 0.
        /// </remarks>
        ITree[] Children { get; }

        /// <summary>
        /// Gets a boolean indicating whether the tree is empty.
        /// </summary>
        bool IsEmpty { get; }
    }
}
