using UnityEngine;

public class MainScene : MonoBehaviour
{
  private Color _lightColor;

  private GameObject _cameraObject;
  private GameObject _directionalLightObject;
  private GameObject _cubeObject;
  private GameObject _discObject;

  private bool _isPanning;
  private Vector3 _mouseOrigin;

  private Vector3 _cubeOriginAngles;
  private Vector3 _cameraOriginAngles;

  private void Awake()
  {
    Debug.Log("MainScene Awake!");

    ColorUtility.TryParseHtmlString("#FFF4D6", out _lightColor);
  }

  // Start is called before the first frame update
  private void Start()
  {
    Debug.Log("MainScene Start!");

    SetupSkybox();
    SetupCube();
    SetupDisc();
    SetupLight();
    SetupCamera();
  }

  // Update is called once per frame
  private void Update()
  {
    var speed = 5f;
    var rigidBody = _cubeObject.GetComponent<Rigidbody>();

    if (Input.GetKey(KeyCode.A))
    {
      rigidBody.MovePosition(_cubeObject.transform.position - _cubeObject.transform.right * (Time.fixedDeltaTime * speed));
    }

    if (Input.GetKey(KeyCode.D))
    {
      rigidBody.MovePosition(_cubeObject.transform.position + _cubeObject.transform.right * (Time.fixedDeltaTime * speed));
    }

    if (Input.GetKey(KeyCode.W))
    {
      rigidBody.MovePosition(_cubeObject.transform.position + _cubeObject.transform.forward * (Time.fixedDeltaTime * speed));
    }

    if (Input.GetKey(KeyCode.S))
    {
      rigidBody.MovePosition(_cubeObject.transform.position - _cubeObject.transform.forward * (Time.fixedDeltaTime * speed));
    }

    if (Input.GetKey(KeyCode.Space))
    {
      rigidBody.AddForce(new Vector3(0, 100, 0));
    }

    if (Input.GetMouseButtonDown(1))
    {
      _isPanning = true;
      _mouseOrigin =  Input.mousePosition;

      _cubeOriginAngles = _cubeObject.transform.eulerAngles;
      _cameraOriginAngles = _cameraObject.transform.eulerAngles;

      Cursor.visible = false;
    }

    if (!Input.GetMouseButton(1))
    {
      _isPanning = false;
      Cursor.visible = true;
    }

    if (_isPanning)
    {
      Vector3 mouseMovement = Input.mousePosition - _mouseOrigin;

      var cubeAngles = _cubeObject.transform.eulerAngles;
      cubeAngles.x = 0;
      cubeAngles.y = _cubeOriginAngles.y + mouseMovement.x * 0.5f;
      cubeAngles.z = 0;
      _cubeObject.transform.eulerAngles = cubeAngles;

      var cameraLookAngle = _cameraObject.transform.eulerAngles;
      cameraLookAngle.x = _cameraOriginAngles.x + (mouseMovement.y * 0.3f) * -1f;
      _cameraObject.transform.eulerAngles = cameraLookAngle;
    }

    var cameraPosition = _cubeObject.transform.position - _cubeObject.transform.forward * 3;
    cameraPosition.y = _cubeObject.transform.position.y + 1.5f;
    _cameraObject.transform.position = cameraPosition;

    var cameraFollowAngle = _cameraObject.transform.eulerAngles;
    cameraFollowAngle.y = _cubeObject.transform.eulerAngles.y;
    cameraFollowAngle.z = _cubeObject.transform.eulerAngles.z;
    _cameraObject.transform.eulerAngles = cameraFollowAngle;
  }

  private void SetupSkybox()
  {
    RenderSettings.skybox = Resources.Load<Material>("Materials/TestSky");
    DynamicGI.UpdateEnvironment();
  }

  private void SetupLight()
  {
    _directionalLightObject = new GameObject("Test Directional Light");
    _directionalLightObject.transform.position = new Vector3(0, 3, 0);
    _directionalLightObject.transform.Rotate(50.0f, -30.0f, 0.0f, Space.Self);

    var directionalLight = _directionalLightObject.AddComponent<Light>();
    directionalLight.type = LightType.Directional;
    directionalLight.shadows = LightShadows.Soft;
    directionalLight.color = _lightColor;
  }

  private void SetupCamera()
  {
    _cameraObject = new GameObject("Test Camera");
    _cameraObject.transform.position = new Vector3(0, 3, -5);
    _cameraObject.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

    _cameraObject.AddComponent<Camera>();
    _cameraObject.tag = "MainCamera";
  }

  private void SetupDisc()
  {
    _discObject = Instantiate(Resources.Load<GameObject>("Prefabs/Grass Disc"));
    _discObject.transform.position = new Vector3(0, 0, 0);
    _discObject.transform.localScale = new Vector3(10, 10, 1);

    var meshCollider = _discObject.AddComponent<MeshCollider>();
    meshCollider.convex = true;
  }

  private void SetupCube()
  {
    _cubeObject = Instantiate(Resources.Load<GameObject>("Prefabs/Brick Cube"));
    _cubeObject.transform.position = new Vector3(0, 1, 0);
    _cubeObject.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
    _cubeObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

    var cubeMeshCollider = _cubeObject.AddComponent<MeshCollider>();
    cubeMeshCollider.convex = true;
    _cubeObject.AddComponent<Rigidbody>();
  }

  private void SetupCube2()
  {
    _cubeObject = new GameObject("Test Cube");
    _cubeObject.transform.position = new Vector3(0, 1, 0);
    _cubeObject.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

    var tmpGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
    var meshFilter = _cubeObject.AddComponent<MeshFilter>();
    meshFilter.mesh = tmpGameObject.GetComponent<MeshFilter>().sharedMesh;
    var meshRenderer = _cubeObject.AddComponent<MeshRenderer>();
    meshRenderer.material = Resources.Load<Material>("Materials/TestMat");
    Destroy(tmpGameObject);

    var cubeMeshCollider = _cubeObject.AddComponent<MeshCollider>();
    cubeMeshCollider.convex = true;
    _cubeObject.AddComponent<Rigidbody>();
  }
}
