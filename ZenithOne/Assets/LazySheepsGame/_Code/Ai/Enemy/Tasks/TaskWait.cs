using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
    public class TaskWait : Node
    {
        private float _waitTime;
        public TaskWait(float waitTime)
        {
            _waitTime = waitTime;
        }
        public override NodeState Evaluate()
        {
            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0)
            {
                state = NodeState.Success;
                return state;
            }
            Debug.Log("Waiting");
            state = NodeState.Running;
            return state;
        }
    }
}
