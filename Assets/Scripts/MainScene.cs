using UnityEngine;

public class MainScene : MonoBehaviour {
  private GameObject _cameraObject;
  private Vector3 _cameraOriginAngles;
  private GameObject _cubeObject;

  private Vector3 _cubeOriginAngles;
  private GameObject _directionalLightObject;
  private GameObject _discObject;

  private bool _isPanning;
  private Color _lightColor;
  private Vector3 _mouseOrigin;

  private void Awake() {
    Debug.Log("MainScene Awake!");

    ColorUtility.TryParseHtmlString("#FFF4D6", out this._lightColor);
  }

  // Start is called before the first frame update
  private void Start() {
    Debug.Log("MainScene Start!");

    this.SetupSkybox();
    this.SetupCube();
    this.SetupDisc();
    this.SetupLight();
    this.SetupCamera();
  }

  // Update is called once per frame
  private void Update() {
    float speed = 5f;
    Rigidbody rigidBody = this._cubeObject.GetComponent<Rigidbody>();

    if (Input.GetKey(KeyCode.A)) {
      rigidBody.MovePosition(this._cubeObject.transform.position -
                             this._cubeObject.transform.right * (Time.fixedDeltaTime * speed));
    }

    if (Input.GetKey(KeyCode.D)) {
      rigidBody.MovePosition(this._cubeObject.transform.position +
                             this._cubeObject.transform.right * (Time.fixedDeltaTime * speed));
    }

    if (Input.GetKey(KeyCode.W)) {
      rigidBody.MovePosition(this._cubeObject.transform.position +
                             this._cubeObject.transform.forward * (Time.fixedDeltaTime * speed));
    }

    if (Input.GetKey(KeyCode.S)) {
      rigidBody.MovePosition(this._cubeObject.transform.position -
                             this._cubeObject.transform.forward * (Time.fixedDeltaTime * speed));
    }

    if (Input.GetKey(KeyCode.Space)) {
      rigidBody.AddForce(new Vector3(0, 100, 0));
    }

    if (Input.GetMouseButtonDown(1)) {
      this._isPanning = true;
      this._mouseOrigin = Input.mousePosition;

      this._cubeOriginAngles = this._cubeObject.transform.eulerAngles;
      this._cameraOriginAngles = this._cameraObject.transform.eulerAngles;

      Cursor.visible = false;
    }

    if (!Input.GetMouseButton(1)) {
      this._isPanning = false;
      Cursor.visible = true;
    }

    if (this._isPanning) {
      Vector3 mouseMovement = Input.mousePosition - this._mouseOrigin;

      Vector3 cubeAngles = this._cubeObject.transform.eulerAngles;
      cubeAngles.x = 0;
      cubeAngles.y = this._cubeOriginAngles.y + mouseMovement.x * 0.5f;
      cubeAngles.z = 0;
      this._cubeObject.transform.eulerAngles = cubeAngles;

      Vector3 cameraLookAngle = this._cameraObject.transform.eulerAngles;
      cameraLookAngle.x = this._cameraOriginAngles.x + mouseMovement.y * 0.3f * -1f;
      this._cameraObject.transform.eulerAngles = cameraLookAngle;
    }

    Vector3 cameraPosition = this._cubeObject.transform.position - this._cubeObject.transform.forward * 3;
    cameraPosition.y = this._cubeObject.transform.position.y + 1.5f;
    this._cameraObject.transform.position = cameraPosition;

    Vector3 cameraFollowAngle = this._cameraObject.transform.eulerAngles;
    cameraFollowAngle.y = this._cubeObject.transform.eulerAngles.y;
    cameraFollowAngle.z = this._cubeObject.transform.eulerAngles.z;
    this._cameraObject.transform.eulerAngles = cameraFollowAngle;
  }

  private void SetupSkybox() {
    RenderSettings.skybox = Resources.Load<Material>("Materials/TestSky");
    DynamicGI.UpdateEnvironment();
  }

  private void SetupLight() {
    this._directionalLightObject = new GameObject("Test Directional Light");
    this._directionalLightObject.transform.position = new Vector3(0, 3, 0);
    this._directionalLightObject.transform.Rotate(50.0f, -30.0f, 0.0f, Space.Self);

    Light directionalLight = this._directionalLightObject.AddComponent<Light>();
    directionalLight.type = LightType.Directional;
    directionalLight.shadows = LightShadows.Soft;
    directionalLight.color = this._lightColor;
  }

  private void SetupCamera() {
    this._cameraObject = new GameObject("Test Camera");
    this._cameraObject.transform.position = new Vector3(0, 3, -5);
    this._cameraObject.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

    this._cameraObject.AddComponent<Camera>();
    this._cameraObject.tag = "MainCamera";
  }

  private void SetupDisc() {
    this._discObject = Instantiate(Resources.Load<GameObject>("Prefabs/Grass Disc"));
    this._discObject.transform.position = new Vector3(0, 0, 0);
    this._discObject.transform.localScale = new Vector3(10, 10, 1);

    MeshCollider meshCollider = this._discObject.AddComponent<MeshCollider>();
    meshCollider.convex = true;
  }

  private void SetupCube() {
    this._cubeObject = Instantiate(Resources.Load<GameObject>("Prefabs/Brick Cube"));
    this._cubeObject.transform.position = new Vector3(0, 1, 0);
    this._cubeObject.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);
    this._cubeObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

    MeshCollider cubeMeshCollider = this._cubeObject.AddComponent<MeshCollider>();
    cubeMeshCollider.convex = true;
    this._cubeObject.AddComponent<Rigidbody>();
  }

  private void SetupCube2() {
    this._cubeObject = new GameObject("Test Cube");
    this._cubeObject.transform.position = new Vector3(0, 1, 0);
    this._cubeObject.transform.Rotate(0.0f, 0.0f, 0.0f, Space.Self);

    GameObject tmpGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
    MeshFilter meshFilter = this._cubeObject.AddComponent<MeshFilter>();
    meshFilter.mesh = tmpGameObject.GetComponent<MeshFilter>().sharedMesh;
    MeshRenderer meshRenderer = this._cubeObject.AddComponent<MeshRenderer>();
    meshRenderer.material = Resources.Load<Material>("Materials/TestMat");
    Destroy(tmpGameObject);

    MeshCollider cubeMeshCollider = this._cubeObject.AddComponent<MeshCollider>();
    cubeMeshCollider.convex = true;
    this._cubeObject.AddComponent<Rigidbody>();
  }
}
