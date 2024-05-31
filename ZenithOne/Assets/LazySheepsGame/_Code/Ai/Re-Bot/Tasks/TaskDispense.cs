using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
    public class TaskDispense : Node
    {
        private readonly Transform _transform;
        private float _elapsedTime;

        public TaskDispense(Transform t)
        {
            _transform = t;
        }
    }
}
