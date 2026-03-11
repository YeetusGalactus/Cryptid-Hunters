using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoseMenu : MonoBehaviour
{
    public float JumpscareTime = 2f;

    private void Start() {
        StartCoroutine(LoadScene(JumpscareTime));
    }

    IEnumerator LoadScene(float seconds) {
        yield return new WaitForSeconds(seconds);

        SceneManager.LoadScene(4);
    }
}
