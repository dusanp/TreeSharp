# TreeSharp
A CLI tree visualisation utility.
[Available on NuGet.](https://www.nuget.org/packages/TreeSharp/)

```
Leopardis
├───Lynx
│   ├───Bobcat
│   └───┬───Felis
│       │   ├───Jungle cat
│       │   └───┬───┬───European wild cat
│       │       │   └───Domestic cat
│       │       └───African wild cat
│       └───Puma
│           ├───Cougar
│           └───Cheetah
└───Ocelot
```


## Usage

Inherit the TreeNode class on the class you intend to use as your tree element, then, if desired, override the ToString method to provide a custom description in the tree. Calling GetTree on any element of the tree will provide the string form of the tree below it, PrintTree will output it into the console.


### Parameters

**maxDepth**

The library supports infinite trees through the parameter MaxDepth: specifying it will cut the tree off at the specified level.

**indentLevel**

Specifying the indentLevel will change the amount of characters by which each level of the tree is indented. Default is 3.


### Sample

```C#
class MyNode : TreeNode<MyNode, List<MyNode>>
{
    private string _name;

    public MyNode(string name)
    {
        _name = name;
    }
    
    public override List<MyNode> ChildNodes { get; set; }
    
    public override string ToString()
    {
        return _name;
    }
}

var root = new MyNode("1")
{
    ChildNodes = new List<MyNode>
    {
        new MyNode("1.1"),
        new MyNode("1.2")
    }
};

root.PrintTree();

//  Output:
//
//  1
//  ├───1.1
//  └───1.2

```
