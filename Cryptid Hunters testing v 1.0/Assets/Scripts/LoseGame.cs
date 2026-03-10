using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{

    public void Awake() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnRestartButton() {
        SceneManager.LoadScene(0);
    }

    public void OnQuitButton() {
        Application.Quit();
    }
}
