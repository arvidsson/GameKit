using UnityEngine;

namespace GameKit.Physics
{
    public class Ragdoll : MonoBehaviour
    {
        [Tooltip("Root transform")]
        public Transform root;
        [Tooltip("If true, ragdoll is disabled to save performance")]
        public bool disableRagdoll = true;
        [Tooltip("Delay until ragdoll is disabled")]
        public float disableRagdollDelay = 10f;

        private void Start()
        {
            if (disableRagdoll)
            {
                Invoke("DisableRagdoll", disableRagdollDelay);
            }
        }

        public void AssignRiggingFrom(Transform source, Transform hitObject, Vector3 direction, Vector3 hitPoint)
        {
            AssignRiggingRecursive(source, root, hitObject, direction, hitPoint);
        }

        private void AssignRiggingRecursive(Transform source, Transform target, Transform hitObject, Vector3 direction, Vector3 hitPoint)
        {
            target.position = source.position;
            target.rotation = source.rotation;

            //if (target.transform.name == "Head_M")
            //{
            //    target.GetComponent<Rigidbody>().AddForceAtPosition(direction * 30, hitPoint, ForceMode.Impulse);
            //}

            for (int i = 0; i < source.childCount; i++)
            {
                var sourceChild = source.GetChild(i);
                var targetChild = target.GetChild(i);

                AssignRiggingRecursive(sourceChild, targetChild, hitObject, direction, hitPoint);
            }
        }

        private void DisableRagdoll()
        {
            foreach (var rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.Sleep();
                rb.isKinematic = true;
                rb.useGravity = false;
                rb.detectCollisions = false;
            }
        }
    }
}