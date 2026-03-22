using System;


namespace VariableScope
{
    interface ICustomIterator
    {
        bool HasNext();
        int Next();

    }
    class TreeNode(int value, TreeNode? left = null, TreeNode? right = null)
    {
        public int Value = value;
        public TreeNode? Left = left;
        public TreeNode? Right = right;

    }


    class InOrderIterator(TreeNode? root) : ICustomIterator
    {
        Stack<TreeNode> stack = new Stack<TreeNode>();
        TreeNode? current = root;
        public bool HasNext()
        {
            return stack.Count > 0 || current != null;
        }
        public int Next()
        {
            while (current != null)
            {
                stack.Push(current);
                current = current.Left;
            }
            var node = stack.Pop();
            current = node.Right;
            return node.Value;
        }
    }


    class Program
    {

        static void Main(string[] args)
        {

            var t3 = new TreeNode(3);
            var t7 = new TreeNode(7);
            var t20 = new TreeNode(20);
            var t5 = new TreeNode(5, t3, t7);
            var t15 = new TreeNode(15, null, t20);
            var root = new TreeNode(10, t5, t15);

            var iterator = new InOrderIterator(root);

            while (iterator.HasNext())
            {
                Console.Write(iterator.Next() + " -> ");

            }
        }

    }
}