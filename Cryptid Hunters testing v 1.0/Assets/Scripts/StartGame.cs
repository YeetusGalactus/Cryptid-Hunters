using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void OnContinueButton() {
        SceneManager.LoadScene(2);
    }

}
