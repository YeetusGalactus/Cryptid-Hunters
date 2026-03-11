using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchPlayer : MonoBehaviour
{

    public void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Player") {

            FpsMovement player = other.GetComponent<FpsMovement>();

            SceneManager.LoadScene(4);
        } 
                    
    }

}
