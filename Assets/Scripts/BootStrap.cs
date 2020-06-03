using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrap : MonoBehaviour
{
  private Scene _mainScene;
  private GameObject _mainSceneScriptObject;

  private void Awake()
  {
    Debug.Log("BootStrap Awake!");
  }

  // Start is called before the first frame update
  private void Start()
  {
    Debug.Log("BootStrap Start!");

    _mainScene = SceneManager.CreateScene("Test Scene");
    _mainSceneScriptObject = new GameObject("Test Scene Script");
    _mainSceneScriptObject.AddComponent<MainScene>();
    SceneManager.MoveGameObjectToScene(_mainSceneScriptObject, _mainScene);
    SceneManager.SetActiveScene(_mainScene);
  }

  // Update is called once per frame
  private void Update() {}
}
