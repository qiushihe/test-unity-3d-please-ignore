using UnityEngine;

namespace Utilities
{
    public class RotationLocker
    {
        // TargetObject refers to the object whose rotation will be locked.
        public readonly GameObject TargetObject;

        // LockXAxis indicates if x-axis rotation should be locked or not.
        public bool LockXAxis;

        // LockYAxis indicates if y-axis rotation should be locked or not.
        public bool LockYAxis;

        // LockZAxis indicates if z-axis rotation should be locked or not.
        public bool LockZAxis;

        public RotationLocker(GameObject targetObject)
        {
            TargetObject = targetObject;

            LockXAxis = false;
            LockYAxis = false;
            LockZAxis = false;
        }

        public void UpdateRotation()
        {
            TargetObject.transform.eulerAngles =
                GetLockedRotation(TargetObject.transform.eulerAngles, LockXAxis, LockYAxis, LockZAxis);
        }

        public static Vector3 GetLockedRotation(Vector3 target, bool lockX, bool lockY, bool lockZ)
        {
            var rotation = Vector3.zero;
            rotation.x = lockX ? 0 : target.x;
            rotation.y = lockY ? 0 : target.y;
            rotation.z = lockZ ? 0 : target.z;
            return rotation;
        }
    }
}