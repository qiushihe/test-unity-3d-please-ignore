using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrap : MonoBehaviour {
  private Scene _mainScene;
  private GameObject _mainSceneScriptObject;

  private void Awake() {
    Debug.Log("BootStrap Awake!");
  }

  // Start is called before the first frame update
  private void Start() {
    Debug.Log("BootStrap Start!");

    this._mainScene = SceneManager.CreateScene("Test Scene");
    this._mainSceneScriptObject = new GameObject("Test Scene Script");
    this._mainSceneScriptObject.AddComponent<MainScene>();
    SceneManager.MoveGameObjectToScene(this._mainSceneScriptObject, this._mainScene);
    SceneManager.SetActiveScene(this._mainScene);
  }

  // Update is called once per frame
  private void Update() {
  }
}
