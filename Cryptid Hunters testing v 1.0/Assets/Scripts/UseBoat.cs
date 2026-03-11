using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UseBoat : MonoBehaviour
{
    //public GameObject EnginePartOnPlayer;
    public GameObject InteractText;
    public GameObject EnginePart;
    public GameObject InteractTextSuccess;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //EnginePartOnPlayer.SetActive(false);
        InteractText.SetActive(false);
        InteractTextSuccess.SetActive(false);

    }

    private void OnTriggerStay(Collider other) {

        if (other.gameObject.tag == "Player") {

            //if(partCount == 3) {
            //    InteractTextSuccess.SetActive(true);
            //}
            InteractText.SetActive(true);

            if (Input.GetKey(KeyCode.E)) {

                FpsMovement player = other.GetComponent<FpsMovement>();

                if(player.partCount >= 3) {
                    InteractTextSuccess.SetActive(true);
                    InteractText.SetActive(false);
                    Debug.Log("You Escaped!");
                    SceneManager.LoadScene(3);
                    // add a scene manager and create a win screen
                } else {
                    InteractText.SetActive(true);
                    Debug.Log("Interacted with Boat!");
                }
            }
            
        }
    }
    private void OnTriggerExit(Collider other) {
        InteractText.SetActive(false);
        InteractTextSuccess.SetActive(false);
    }
}
