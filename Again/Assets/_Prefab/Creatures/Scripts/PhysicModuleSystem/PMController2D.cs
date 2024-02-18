using System;
using UnityEngine;

namespace PhysicModuleSystem2D
{
    public class PMController2D : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        internal Rigidbody2D RB => _rigidbody;

        internal event Action<Rigidbody2D,Vector2> ModificationAction;

        private Vector2 Direction;

        private void FixedUpdate()
        {
            ModificationAction.Invoke(_rigidbody, Direction);
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }
    }
}
