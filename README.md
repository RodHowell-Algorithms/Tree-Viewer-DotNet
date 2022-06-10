# Tree Viewer

[**KansasStateUniversity.TreeViewer2**](https://github.com/RodHowell-Algorithms/Tree-Viewer-DotNet) is a library of tools for viewing and manipulating trees under the Microsoft® .NET Framework. It includes classes that can be used within code to display trees. This is a port of a portion of a [Java<sup>TM</sup> Tree Viewer](https://github.com/RodHowell-Algorithms/Tree-Viewer).

## Usage

The library is contained within [`KansasStateUniversity.TreeViewer2.dll`](https://github.com/RodHowell-Algorithms/Tree-Viewer-DotNet/raw/main/KansasStateUniversity.TreeViewer2.dll), which must be accessible to your code (e.g., within Microsoft® Visual Studio, define a reference from your project to this DLL). In order to be able to implement a tree that can be displayed using this library, you must write your tree class to implement [**ITree**](https://rodhowell-algorithms.github.io/Tree-Viewer-DotNet/Help/html/68d85729-02b8-db78-4416-945a0e45acfb.htm). If you wish to have colored nodes, you will also need to provide a class that implements [**IColorizer**](https://rodhowell-algorithms.github.io/Tree-Viewer-DotNet/Help/html/662d9a4f-756c-5e6d-e28b-81c1cf584097.htm). You may then use the [**TreeDrawing**](https://rodhowell-algorithms.github.io/Tree-Viewer-DotNet/Help/html/318fe5cb-7ed3-d88a-515f-82753b6dbf3e.htm), [**TreePanel**](https://rodhowell-algorithms.github.io/Tree-Viewer-DotNet/Help/html/bd639a4b-3c76-b534-871f-8c730bacebaa.htm), and/or [**TreeForm**](https://rodhowell-algorithms.github.io/Tree-Viewer-DotNet/Help/html/e15ea583-4b1e-7a0b-ad9c-fc56983ca79b.htm) classes to display your tree. 

The [complete library documentation](https://rodhowell-algorithms.github.io/Tree-Viewer-DotNet/Help/html/4feb08d4-45a9-d5a7-f8c5-964962c586e5.htm), which was built using [Sandcastle Help File Builder](https://github.com/EWSoftware/SHFB), can be found in the `Help` folder.

## Compiling the Code

If you wish to modify the code, you will need to download a copy, either by cloning it with `git` or by downloading and decompressing a [ZIP archive](https://github.com/RodHowell-Algorithms/Tree-Viewer-DotNet/archive/refs/heads/main.zip). The repository is set up as a Visual Studio solution. Open `KansasStateUniversity.TreeViewer2.sln`, and build as usual.



