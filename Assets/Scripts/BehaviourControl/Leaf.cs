namespace BehaviourControl
{
    public class Leaf : Node
    {

        public delegate Status Tick();
        public Tick processMethod;

        public Leaf(string name, Tick pm) : base(name)
        {
            processMethod = pm;
        }

        public override Status Process()
        {
            if (processMethod != null)
                return processMethod();
            return Status.Failure;
        }
    }
}