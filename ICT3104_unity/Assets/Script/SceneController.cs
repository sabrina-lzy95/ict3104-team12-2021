using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Press the numpad 1-7 key to start coroutine
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene("1"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene("2"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene("3"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene("4"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene("5"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene("6"));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene("7"));
        }
    }

    IEnumerator LoadYourAsyncScene(string key)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene "+key+"");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
