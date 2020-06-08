using UnityEngine;

namespace Utilities
{
    public class LookMaintainer
    {
        // ObserverObject refers to the object that's performing the "looking".
        public readonly GameObject ObserverObject;

        // TargetObject refers to the object that's being "looked at".
        public readonly GameObject TargetObject;

        public LookMaintainer(GameObject observerObject, GameObject targetObject)
        {
            ObserverObject = observerObject;
            TargetObject = targetObject;
        }

        public void UpdateLook()
        {
            // TODO: Refactor this to explicitly update ObserverObject's rotation
            //       instead of relying on Transform#LookAt.
            ObserverObject.transform.LookAt(TargetObject.transform.position);
        }
    }
}