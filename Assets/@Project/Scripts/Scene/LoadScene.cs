using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene : MonoBehaviour
{ 
    void Start()
    {
        SceneManager.LoadScene(1);
    }
}
