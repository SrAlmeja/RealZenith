
using System.Collections.Generic;

namespace com.LazyGames.Dz.Ai
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }
        public override NodeStates Evaluate()
        {
            foreach (var node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeStates.Failure:
                        continue;
                    case NodeStates.Success:
                        state = NodeStates.Success;
                        return state;
                    case NodeStates.Running:
                        state = NodeStates.Running;
                        return state;
                    default:
                        continue;
                }
            }
            state = NodeStates.Failure;
            return state;
        }
    }
}
