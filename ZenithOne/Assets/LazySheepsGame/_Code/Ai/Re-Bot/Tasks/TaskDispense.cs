using System.Collections.Generic;
using FMODUnity;
using Lean.Pool;
using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
    public class TaskDispense : Node
    {
        private float _elapsedTime;
        
        private readonly GameObject _prefabToDispense;
        private readonly BotDoorController _doorController;
        private readonly BotParameters _parameters;
        private readonly Transform _dispenser1;
        private readonly Transform _dispenser2;
        private readonly StudioEventEmitter _emitter;
        
        public TaskDispense(Transform t,BotParameters parameters, GameObject prefabToDispense, Transform dispenser1, Transform dispenser2, StudioEventEmitter emitter)
        {
            _parameters = parameters;
            _prefabToDispense = prefabToDispense;
            _dispenser1 = dispenser1;
            _dispenser2 = dispenser2;
            _emitter = emitter;
            _doorController = t.GetComponent<BotDoorController>();
            _elapsedTime = parameters.dispenserCooldown - (parameters.dispenserCooldown *.05f);
        }

        public override NodeState Evaluate(bool overrideStop = false)
        {
            var t = GetData("target");
            if(t == null) return NodeState.Failure;
            _doorController.OpenDoor();
            _elapsedTime += Time.deltaTime;
            if (!(_elapsedTime >= _parameters.dispenserCooldown)) return NodeState.Running;
            _elapsedTime = 0;
            Dispense();
            return NodeState.Running;
        }

        private void Dispense()
        {
            _emitter.Params[0].Value = Random.Range(1, 7);
            _emitter.Play();
            LeanPool.Spawn(_prefabToDispense, _dispenser1.position, _dispenser1.rotation);
            LeanPool.Spawn(_prefabToDispense, _dispenser2.position, _dispenser2.rotation);
        }
    }
}
