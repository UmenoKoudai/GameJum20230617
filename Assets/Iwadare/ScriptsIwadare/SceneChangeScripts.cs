using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScripts : MonoBehaviour
{
    public static SceneChangeScripts instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void SceneChange(string sceneName)
    {
        StartCoroutine(ChangeTime(sceneName));
    }

    IEnumerator ChangeTime(string sceneName)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(sceneName);
    }
}
