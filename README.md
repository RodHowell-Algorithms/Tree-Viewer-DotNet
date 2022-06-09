# Tree Viewer

**KansasStateUniversity.TreeViewer2** is a library of tools for viewing and manipulating trees under the Microsoft® .NET Framework. It includes classes that can be used within code to display trees. 

## Usage

The library is contained within [`KansasStateUniversity.TreeViewer2.dll`](https://github.com/RodHowell-Algorithms/Tree-Viewer-DotNet/raw/main/KansasStateUniversity.TreeViewer2.dll), which must be accessible to your code (e.g., within Microsoft® Visual Studio, define a reference from your project to this DLL). In order to be able to implement a tree that can be displayed using this library, you must write your tree class to implement **TreeInterface**. If you wish to have colored nodes, you will also need to provide a class that implements **Colorizer**. You may then use the **TreeDrawing**, **TreeComponent**, and/or **TreeFrame** classes to display your tree. 

The complete library documentation, which was built using [Sandcastle Help File Builder](https://github.com/EWSoftware/SHFB), can be found in the `Help` folder.

## Compiling the Code

If you wish to modify the code, you will need to download a copy, either by cloning it with `git` or by downloading and decompressing a [ZIP archive](https://github.com/RodHowell-Algorithms/Tree-Viewer-DotNet/archive/refs/heads/main.zip). The repository is set up as a Visual Studio solution. Open `KansasStateUniversity.TreeViewer2.sln`, and build as usual.



