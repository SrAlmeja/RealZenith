using UnityEngine;
using UnityEngine.AI;
using CryoStorage;

namespace com.LazyGames.Dz.Ai
{
    public class TaskWander : Node
    {
        
        private float _wanderAngle;
        private Vector3 _deviation;
        private float _elapsedTime;
        private float _actTime;
        private bool _detected;
        private float _deviationForce;
        private bool _doWalk;
        
        
        private readonly BotParameters _parameters;
        private readonly Transform _transform;
        private readonly NavMeshAgent _agent;
        
        public TaskWander(Transform transform, BotParameters parameters)
        {
            _transform = transform;
            _parameters = parameters;
            _agent = transform.GetComponent<NavMeshAgent>();
            _agent.speed = _parameters.movementSpeed;
        }
        
       public override NodeState Evaluate(bool overrideStop = false)
       { 
           ManageWalkState();
           Avoidance(CastRay());
           CountTime();
           
           var t = GetData("target"); 
           _doWalk = t == null;
          if (!_doWalk)
          {
              _agent.isStopped = !_doWalk;
              state = NodeState.Running;
              return state;
          }

          _agent.SetDestination(Wander());
          state = NodeState.Running;
          return state;
       }
       
       private void CountTime()
       {
           _elapsedTime += Time.fixedDeltaTime;
       }
       
       private void Avoidance(RaycastHit hit)
       {
           if(!_detected) return;
           if (hit.distance >= _parameters.detectionRange) return;
           _deviationForce *= 10;
       }

       private RaycastHit CastRay()
       {
           var pos = _transform.position; 
           _detected = Physics.Raycast(pos, _transform.forward, 
               out var hit, _parameters.detectionRange, Physics.DefaultRaycastLayers);
            
           Debug.DrawRay(pos , _transform.forward * _parameters.detectionRange, Color.green);
     
           return hit;
       }
       
       private void ManageWalkState()
       {
           switch (_doWalk)
           {
               case false:
                   if (_elapsedTime < _actTime) return;
                   _actTime = Random.Range(_parameters.minActTime, _parameters.maxActTime);
                   _doWalk = true;
                   _elapsedTime -= _elapsedTime;
                   break;
               case true:
                   if (_elapsedTime < _actTime) return;
                   _actTime = Random.Range(_parameters.minActTime, _parameters.maxActTime);
                   _doWalk = false;
                   _elapsedTime -= _elapsedTime;
                   break;
           }
       }
       
       private Vector3 Wander()
       {
           _deviationForce = Random.Range(_parameters.deviationRange * -1, _parameters.deviationRange);
           _wanderAngle += _deviationForce;
           _deviation = CryoMath.PointOnRadius(GetCircleCenter(), _parameters.circleRadius, _wanderAngle);
           return _deviation;
       }
       
       private Vector3 GetCircleCenter()
       {
           Vector3 result = (_agent.velocity.normalized * _parameters.circleRadius) + _transform.position;
           return result;
       }
    }
}
