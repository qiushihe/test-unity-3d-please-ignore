using UnityEngine;

namespace Utilities
{
    public class CranePositioner
    {
        // CranedObject refers to the object that's being craned around.
        // For example, a "craned object" could be a camera that's being
        // craned around another object.
        public readonly GameObject CranedObject;

        // OriginObject refers to the object that the CranedObject is being
        // craned around. For example, if a camera is being craned around a
        // person, the person is the "origin object".
        public readonly GameObject OriginObject;

        // The direct distance between CranedObject and OriginObject.
        public float CraneLength;

        // HorizontalAngle is the angle of rotation around the vertical axis
        // measured in degrees.
        // For example: when looking side to side.
        public float HorizontalAngle;

        // RotateCranedObject indicates if the local rotation of CranedObject
        // should be updated so that CranedObject always points to OriginObject.
        public bool RotateCranedObject;

        // VerticalAngle is the angle of rotation around the side-way, horizontal
        // axis measured in degrees. For example: when looking up and down.
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