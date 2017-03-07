using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeSharp
{
    public abstract class TreeNode<T, TCollection> where T : TreeNode<T, TCollection> where TCollection : IEnumerable<T>
    {
        private const StringSplitOptions Options = StringSplitOptions.RemoveEmptyEntries;
        private static readonly string[] Separators = new[] {"\r\n", "\n"};
        public abstract TCollection ChildNodes { get; set; }

        public string GetTree(byte indentLevel = 3, byte maxDepth = Byte.MaxValue)
        {
            var sb = new StringBuilder(ToString());
            sb.AppendLine();
            if (ChildNodes == null|| maxDepth < 1)
            {
                return sb.ToString();
            }
            var last = ChildNodes.Last();
            foreach (var childNode in ChildNodes)
            {
                sb.Append(IndentChild(childNode.GetTree(indentLevel, (byte) (maxDepth-1)), Object.ReferenceEquals(childNode, last), indentLevel));
            }
            return sb.ToString();
        }

        private static string IndentChild(string child, bool isLast, byte indentLevel)
        {
            var lines = child.Split(Separators, Options);
            var sb = new StringBuilder(isLast?"└": "├");
            sb.Append(new string('─', indentLevel));
            sb.AppendLine(lines.First());
            var indentBlock = (isLast ? " " : "│")+new string(' ', indentLevel);
            foreach (var line in lines.Skip(1))
            {
                sb.Append(indentBlock);
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

        public void PrintTree(byte indentLevel = 3, byte maxDepth = Byte.MaxValue)
        {
            Console.WriteLine(GetTree(indentLevel, maxDepth));
        }
    }
}
