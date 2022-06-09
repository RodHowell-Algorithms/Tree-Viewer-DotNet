/*
 * IColorizer.cs     5/14/08
 *
 * Copyright (c) 2008 Rod Howell, All Rights Reserved.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;

namespace KansasStateUniversity.TreeViewer2
{
    /// <summary>
    /// An interface to encapsulate a mechanism for associating colors to
    /// objects.
    /// </summary>
    public interface IColorizer
    {
        /// <summary>
        /// Returns the <c>Color</c> associated with the given object.
        /// </summary>
        /// <param name="obj">The object to color.</param>
        /// <returns>
        /// The <c>Color</c> associated with the given object.
        /// </returns>
        Color GetColor(object obj);
    }
}
