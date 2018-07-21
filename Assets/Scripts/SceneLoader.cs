using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    string[] scenesToLoadAdditively;

    void Start()
    {
        foreach(string sc in scenesToLoadAdditively)
        {
            SceneManager.LoadScene(sc, LoadSceneMode.Additive);
        }
    }
}
