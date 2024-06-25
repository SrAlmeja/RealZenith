using System.Collections.Generic;
using FMODUnity;
using Lean.Pool;
using Obvious.Soap;
using TMPro;
using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
    public class TaskDispense : Node
    {
        private float _elapsedTime;
        private List<TrackedCollectible> _activeCollectibles = new();
        private int _gadgetSum;
        private bool _alternate;
        
        private readonly GameObject _prefabToDispense;
        private readonly BotDoorController _doorController;
        private readonly BotParameters _parameters;
        private readonly Transform _dispenser1;
        private readonly Transform _dispenser2;
        private readonly StudioEventEmitter _emitter;
        private readonly int _gadgetLimit;
        private readonly ScriptableVariable<int> _gadgetInventory;
        private readonly TextMeshPro _displayText;
        
        public TaskDispense(Transform t,BotParameters parameters, GameObject prefabToDispense, Transform dispenser1, Transform dispenser2,
            StudioEventEmitter emitter, int gadgetLimit, ScriptableVariable<int> gadgetInventory, TextMeshPro displayText )
        {
            _parameters = parameters;
            _prefabToDispense = prefabToDispense;
            _dispenser1 = dispenser1;
            _dispenser2 = dispenser2;
            _emitter = emitter;
            _doorController = t.GetComponent<BotDoorController>();
            _gadgetLimit = gadgetLimit;
            _gadgetInventory = gadgetInventory;
            _displayText = displayText;
        }

        public override NodeState Evaluate(bool overrideStop = false)
        {
            var t = GetData("target");
            if(t == null) return NodeState.Failure;
            
            var sum = _activeCollectibles.Count + _gadgetInventory.Value;
            Debug.Log(sum);
            _displayText.text = Mathf.Abs(sum - _gadgetLimit).ToString();
            if (sum >= _gadgetLimit)
            {
                _displayText.text = "0";
                state = NodeState.Failure;
                return state;
            }
            
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

            if (_alternate)
            { 
                var go = LeanPool.Spawn(_prefabToDispense, _dispenser2.position, _dispenser2.rotation);
                var activeCollectible = go.GetComponent<TrackedCollectible>();
                activeCollectible.ParentList = _activeCollectibles;
                _activeCollectibles.Add(activeCollectible);
                _alternate = !_alternate;
            }
            else
            {
                var go =LeanPool.Spawn(_prefabToDispense, _dispenser1.position, _dispenser1.rotation);
                var activeCollectible = go.GetComponent<TrackedCollectible>();
                _activeCollectibles.Add(activeCollectible);
                activeCollectible.ParentList = _activeCollectibles; 
                _alternate = !_alternate;
            }
        }
    }
}
