using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
    public class TaskDispense : Node
    {
        private readonly Transform _transform;
        private BotParameters _parameters;
        private float _elapsedTime;
        private GameObject _prefabToDispense;
        private Transform _dispenser1;
        private Transform _dispenser2;
        

        public TaskDispense(Transform t,BotParameters parameters, GameObject prefabToDispense)
        {
            _transform = t;
            _parameters = parameters;
            _prefabToDispense = prefabToDispense;
        }

        // public override NodeState Evaluate(bool overrideStop = false)
        // {
        //     
        // }
    }
}
