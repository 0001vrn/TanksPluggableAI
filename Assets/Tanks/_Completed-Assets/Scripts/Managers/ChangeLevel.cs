using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

    public int level;
    void OnTriggerEnter(Collider other)
    {
        Invoke("LoadScene", 3f);  
    }

    void LoadScene()
    {
        SceneManager.LoadScene(level);
    }
}
