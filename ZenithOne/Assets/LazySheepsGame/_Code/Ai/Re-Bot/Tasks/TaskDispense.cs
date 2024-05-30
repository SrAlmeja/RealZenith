using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.LazyGames.Dz.Ai
{
    public class TaskDispense : Node
    {
        private readonly Transform _transform;

        public TaskDispense(Transform t)
        {
            _transform = t;
        }
    }
}
