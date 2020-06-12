using UnityEngine;

namespace Utilities
{
    public class RotationSynchronizer
    {
        // ReferenceObject refers to the object whose rotation properties will be synchronized from.
        public readonly GameObject ReferenceObject;

        // TargetObject refers to the object who will receive rotation properties from ReferenceObject.
        public readonly GameObject TargetObject;

        // SynchronizeXAxis indicates if x-axis rotation should be synchronized or not.
        public bool SynchronizeXAxis;

        // SynchronizeYAxis indicates if y-axis rotation should be synchronized or not.
        public bool SynchronizeYAxis;

        // SynchronizeZAxis indicates if z-axis rotation should be synchronized or not.
        public bool SynchronizeZAxis;

        public RotationSynchronizer(GameObject targetObject, GameObject referenceObject)
        {
            TargetObject = targetObject;
            ReferenceObject = referenceObject;

            SynchronizeXAxis = false;
            SynchronizeYAxis = false;
            SynchronizeZAxis = false;
        }

        public void UpdateRotation()
        {
            TargetObject.transform.eulerAngles = GetSynchronizedRotation(
                TargetObject.transform.eulerAngles,
                ReferenceObject.transform.eulerAngles,
                SynchronizeXAxis,
                SynchronizeYAxis,
                SynchronizeZAxis);
        }

        public static Vector3 GetSynchronizedRotation(Vector3 target, Vector3 reference, bool syncX, bool syncY,
            bool syncZ)
        {
            var rotation = Vector3.zero;
            rotation.x = syncX ? reference.x : target.x;
            rotation.y = syncY ? reference.y : target.y;
            rotation.z = syncZ ? reference.z : target.z;
            return rotation;
        }
    }
}