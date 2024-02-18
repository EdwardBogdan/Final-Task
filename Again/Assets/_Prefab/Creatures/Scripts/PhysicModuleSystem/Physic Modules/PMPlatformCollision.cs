using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicModuleSystem2D.Modules
{
    internal class PMPlatformCollision : PhysicModule
    {
        [SerializeField] private float _ignoreTime = 1f;
        [SerializeField] private float _rayDistance = 1f;

        private Collider2D _physicBody;

        private readonly HashSet<Collider2D> _activeColliders = new();

        private const string PlatformTag = "Platform";

        protected override void Awake()
        {
            base.Awake();

            _physicBody = GetComponent<PMController2D>().RB.GetComponent<Collider2D>();
        }

        protected override void Modification(Rigidbody2D body, Vector2 direction)
        {
            if (direction.y < 0)
            {
                Vector2 raycastOrigin = body.transform.position;
                RaycastHit2D[] hits = Physics2D.RaycastAll(raycastOrigin, Vector2.down, _rayDistance);

                foreach (RaycastHit2D hit in hits)
                {
                    Collider2D collider = hit.collider;
                    if (collider.CompareTag(PlatformTag) && _activeColliders.Add(collider))
                    {
                        // Если коллайдер успешно добавлен в HashSet (т.е. его раньше не было), игнорируем коллизию
                        Physics2D.IgnoreCollision(collider, _physicBody, true);
                        StartCoroutine(RestoreCollision(collider));
                    }
                }
            }
        }

        private IEnumerator RestoreCollision(Collider2D collider)
        {
            yield return new WaitForSeconds(_ignoreTime);
            if (collider != null)
            {
                Physics2D.IgnoreCollision(collider, _physicBody, false);
            }
            _activeColliders.Remove(collider); // Удаление коллайдера из активных после восстановления коллизии
        }
    }
}