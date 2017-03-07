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

        /// <summary>
        /// Returns a string representation of the tree to which this node is root.
        /// </summary>
        /// <param name="indentLevel">Amount of characters by which level in the tree is indented. </param>
        /// <param name="maxDepth">Maximum tree depth printed.</param>
        /// <returns></returns>
        public string GetTree(byte indentLevel = 3, byte maxDepth = Byte.MaxValue)
        {
            var sb = new StringBuilder(ToString());
            var isInline = ToString() == "";
            if (!isInline) {sb.AppendLine();}
            if (ChildNodes == null || !ChildNodes.Any() || maxDepth < 1)
            {
                return sb.ToString();
            }
            var last = ChildNodes.Last();
            var first = ChildNodes.First();
            foreach (var childNode in ChildNodes)
            {
                sb.Append(IndentChild(childNode.GetTree(
                    indentLevel, 
                    (byte) (maxDepth-1)),
                    Object.ReferenceEquals(childNode, last), 
                    isInline && Object.ReferenceEquals(childNode, first), 
                    indentLevel));
            }
            return sb.ToString();
        }

        private static string IndentChild(string child, bool isLast, bool isInline, byte indentLevel)
        {
            var lines = child.Split(Separators, Options);
            var sb = new StringBuilder(isInline ? (isLast ? "─" : "┬") : (isLast ? "└" : "├"));
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

        /// <summary>
        /// Prints a string representation of the tree to which this node is root to console.
        /// </summary>
        /// <param name="indentLevel">Amount of characters by which level in the tree is indented. </param>
        /// <param name="maxDepth">Maximum tree depth printed.</param>
        public void PrintTree(byte indentLevel = 3, byte maxDepth = Byte.MaxValue)
        {
            Console.WriteLine(GetTree(indentLevel, maxDepth));
        }
    }
}
