using System.Collections.Generic;

namespace com.LazyGames.Dz.Ai
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate(bool overrideStop = false)
        {
             if (overrideStop) 
             {
                state = NodeState.Failure;
                return state; 
             } 
             bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        state = NodeState.Failure;
                        return state;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.Success;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.Running : NodeState.Success;
            return state;
        }

    }

}