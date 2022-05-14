using System.Collections.Generic;

namespace BehaviourControl
{

    public class Node
    {

        #region Properties

        public List<Node> Children { get; }
        public Status Status { get; }
        public int CurrentChild { get; set; }
        public string Name { get; }

        #endregion

        public Node(string name)
        {
            Children = new List<Node>();
            CurrentChild = 0;
            Name = name;
        }

        public void AddChild(Node node)
        {
            Children.Add(node);
        }
        
        public void AddChildren(IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                Children.Add(node);
            }
        }

        public virtual Status Process()
        {
            return Children[CurrentChild].Process();
        }

    }
}
