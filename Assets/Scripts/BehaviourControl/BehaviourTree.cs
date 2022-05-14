namespace BehaviourControl
{
    public class BehaviourTree : Node
    {

        public BehaviourTree(string name = "Tree") : base(name) { }

        public override Status Process()
        {
            return Children[CurrentChild].Process();
        }
    }
}