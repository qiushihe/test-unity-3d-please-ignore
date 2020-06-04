using UnityEngine;

namespace Utilities
{
    public class CranePositioner
    {
        public readonly GameObject CranedObject;
        public readonly GameObject OriginObject;
        public float CraneLength;
        public float HorizontalAngle;
        public bool RotateCranedObject;
        public float VerticalAngle;

        public CranePositioner(GameObject originObject, GameObject cranedObject)
        {
            OriginObject = originObject;
            CranedObject = cranedObject;
            CraneLength = 0;
            HorizontalAngle = 0;
            VerticalAngle = 0;
            RotateCranedObject = false;
        }

        public void UpdatePosition()
        {
            CranedObject.transform.position = GetCranedPosition(OriginObject.transform.position, CraneLength,
                VerticalAngle, HorizontalAngle);

            if (RotateCranedObject)
                CranedObject.transform.LookAt(OriginObject.transform.position);
        }

        public static Vector3 GetCranedPosition(Vector3 origin, float distance, float vAngle, float hAngle)
        {
            var groundDistance = distance * Mathf.Cos(vAngle * Mathf.Deg2Rad);
            var xOffset = groundDistance * Mathf.Sin(hAngle * Mathf.Deg2Rad);
            var zOffset = groundDistance * Mathf.Cos(hAngle * Mathf.Deg2Rad);
            var yOffset = distance * Mathf.Sin(vAngle * Mathf.Deg2Rad);
            return new Vector3(origin.x + xOffset, origin.y + yOffset, origin.z + zOffset);
        }
    }
}