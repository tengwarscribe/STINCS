using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loadonclick : MonoBehaviour {

	public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
