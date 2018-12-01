/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadnewscene : MonoBehaviour
{

    // This is scene1.  It loads 3 copies of scene2.
    // Each copy has the same name.

    private Scene scene;
    private void Start()
    {
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        scene = SceneManager.LoadScene("scene_night", parameters);
        Debug.Log("Load 1 of scene_night: " + scene.name);
        scene = SceneManager.LoadScene("scene_night", parameters);
        Debug.Log("Load 2 of scene_night: " + scene.name);
        scene = SceneManager.LoadScene("scene_night", parameters);
        Debug.Log("Load 3 of scene_night: " + scene.name);
    }
}*/