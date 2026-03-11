using JetBrains.Annotations;
using UnityEngine;

public class PickUpEnginePart : MonoBehaviour
{

    public GameObject EnginePartOnPlayer;
    public GameObject PickUpText;
    public NavigationArrow arrow;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnginePartOnPlayer.SetActive(false);
        PickUpText.SetActive(false);
        
    }

    private void OnTriggerStay(Collider other) {

        if(other.gameObject.tag == "Player") {

            PickUpText.SetActive(true);

            if (Input.GetKey(KeyCode.E)) {
                FpsMovement player = other.GetComponent<FpsMovement>();

                if(player != null) {
                    player.AddPart();
                    arrow.GoToNextObject();
                }

                this.gameObject.SetActive(false);
                //EnginePartOnPlayer.SetActive(true);

                PickUpText.SetActive(false);
                
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        PickUpText.SetActive(false);
    }

}
