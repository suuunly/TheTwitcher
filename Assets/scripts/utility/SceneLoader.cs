using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject Loading;

    private void Start() {
        Loading.SetActive(false);
    }

    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);

        // AsyncOperation op = SceneManager.LoadSceneAsync(scene);
        // op.completed += LoadComplete;
        // Loading.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadComplete(AsyncOperation op) {
        if(Loading) Loading.SetActive(false);
    }
}
