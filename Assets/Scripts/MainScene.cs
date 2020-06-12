using UnityEngine;
using Utilities;

public class MainScene : MonoBehaviour
{
    private GameObject _cameraObject;
    private CranePositioner _cranePositioner;
    private GameObject _cubeObject;
    private GameObject _directionalLightObject;
    private GameObject _discObject;
    private Color _lightColor;
    private LookMaintainer _lookMaintainer;
    private PanningState _panningState;
    private RotationSynchronizer _rotationSynchronizer;
    private RotationLocker _rotationLocker;

    private void Awake()
    {
        Debug.Log("MainScene Awake!");

        ColorUtility.TryParseHtmlString("#FFF4D6", out _lightColor);

        _panningState = new PanningState {IsPanning = false};
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

        _cranePositioner = new CranePositioner(_cubeObject, _cameraObject)
        {
            CraneLength = 6,
            HorizontalAngle = 180,
            VerticalAngle = 30
        };

        _lookMaintainer = new LookMaintainer(_cameraObject, _cubeObject);

        _rotationSynchronizer = new RotationSynchronizer(_cubeObject, _cameraObject)
        {
            SynchronizeXAxis = false,
            SynchronizeYAxis = true,
            SynchronizeZAxis = false
        };

        _rotationLocker = new RotationLocker(_cubeObject)
        {
            LockXAxis = true,
            LockYAxis = false,
            LockZAxis = true
        };
    }

    // Update is called once per frame
    private void Update()
    {
        var speed = 5f;
        var rigidBody = _cubeObject.GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.A))
            rigidBody.MovePosition(_cubeObject.transform.position -
                                   _cubeObject.transform.right * (Time.fixedDeltaTime * speed));

        if (Input.GetKey(KeyCode.D))
            rigidBody.MovePosition(_cubeObject.transform.position +
                                   _cubeObject.transform.right * (Time.fixedDeltaTime * speed));

        if (Input.GetKey(KeyCode.W))
            rigidBody.MovePosition(_cubeObject.transform.position +
                                   _cubeObject.transform.forward * (Time.fixedDeltaTime * speed));

        if (Input.GetKey(KeyCode.S))
            rigidBody.MovePosition(_cubeObject.transform.position -
                                   _cubeObject.transform.forward * (Time.fixedDeltaTime * speed));

        if (Input.GetKey(KeyCode.Space)) rigidBody.AddForce(new Vector3(0, 100, 0));

        if (Input.GetMouseButtonDown(0))
        {
            _panningState.IsPanning = true;
            _panningState.IsTurning = false;

            _panningState.MouseOrigin = Input.mousePosition;
            _panningState.CraneHorizontalAngle = _cranePositioner.HorizontalAngle;
            _panningState.CraneVerticalAngle = _cranePositioner.VerticalAngle;
            Cursor.visible = false;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            _panningState.IsPanning = true;
            _panningState.IsTurning = true;

            _panningState.MouseOrigin = Input.mousePosition;
            _panningState.CraneHorizontalAngle = _cranePositioner.HorizontalAngle;
            _panningState.CraneVerticalAngle = _cranePositioner.VerticalAngle;
            Cursor.visible = false;
        }

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            _panningState.IsPanning = false;
            Cursor.visible = true;
        }

        if (_panningState.IsPanning)
        {
            var mouseMovement = Input.mousePosition - _panningState.MouseOrigin;
            _cranePositioner.HorizontalAngle = _panningState.CraneHorizontalAngle + mouseMovement.x;
            _cranePositioner.VerticalAngle = _panningState.CraneVerticalAngle - mouseMovement.y;
        }
        
        _cranePositioner.CraneLength += -1 * Input.mouseScrollDelta.y;

        _cranePositioner.UpdatePosition();
        _lookMaintainer.UpdateLook();

        if (_panningState.IsPanning && _panningState.IsTurning) _rotationSynchronizer.UpdateRotation();
        
        _rotationLocker.UpdateRotation();
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

    private struct PanningState
    {
        public bool IsPanning;
        public bool IsTurning;
        public Vector3 MouseOrigin;
        public float CraneHorizontalAngle;
        public float CraneVerticalAngle;
    }
}