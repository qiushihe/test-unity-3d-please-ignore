using UnityEngine;

namespace ChopSuey
{
    public class PrimitiveObjectCreator
    {
        // materialPath: Path to material relative to `Resources` directory.
        //               For example a value of "Materials/TestMat" would
        //               map to `Resources/Materials/TestMat.mat`. 
        public static GameObject SetupPrimitiveCube(string gameObjectName, string materialPath)
        {
            var cubeObject = new GameObject(gameObjectName);
            cubeObject.transform.position = new Vector3(0, 1, 0);
            cubeObject.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

            var tmpGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var meshFilter = cubeObject.AddComponent<MeshFilter>();
            meshFilter.mesh = tmpGameObject.GetComponent<MeshFilter>().sharedMesh;
            var meshRenderer = cubeObject.AddComponent<MeshRenderer>();
            meshRenderer.material = Resources.Load<Material>(materialPath);
            Object.Destroy(tmpGameObject);

            var cubeMeshCollider = cubeObject.AddComponent<MeshCollider>();
            cubeMeshCollider.convex = true;
            cubeObject.AddComponent<Rigidbody>();

            return cubeObject;
        }
    }
}