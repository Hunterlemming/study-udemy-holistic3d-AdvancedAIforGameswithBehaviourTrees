using UnityEngine;

namespace BehaviourControl
{
    public class Sequence : Node
    {
        public Sequence(string name) : base(name) { }

        public override Status Process()
        {
            var childStatus = Children[CurrentChild].Process();
            if (childStatus == Status.Running) return Status.Running;
            if (childStatus == Status.Failure) return childStatus;

            CurrentChild++;
            if (CurrentChild >= Children.Count)
            {
                CurrentChild = 0;
                return Status.Success;
            }
            
            
            return Status.Running;
        }
    }
}