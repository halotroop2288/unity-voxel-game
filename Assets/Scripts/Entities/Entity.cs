using UnityEngine;

namespace Minecraft
{
    [DisallowMultipleComponent]
    public abstract class Entity : MonoBehaviour {
        protected float JumpVelocity {
            get { return 0.42F; }
        }

        public abstract AABB BoundingBox { get; }

        public abstract bool IsGrounded { get; }

        public abstract Vector3 Velocity { get; set; }

        public float GravityMultiplier { get; set; }

        protected virtual void Jump() {
            this.Velocity = new Vector3(Velocity.x, this.JumpVelocity, Velocity.z); 
        }
    }
}