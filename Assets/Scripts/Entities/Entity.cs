using UnityEngine;

namespace Minecraft
{
    [DisallowMultipleComponent]
    public abstract class Entity : MonoBehaviour
    {
        public abstract AABB BoundingBox { get; }

        public abstract bool IsGrounded { get; }

        public abstract Vector3 Velocity { get; set; }

        public float GravityMultiplier { get; set; }
    }
}