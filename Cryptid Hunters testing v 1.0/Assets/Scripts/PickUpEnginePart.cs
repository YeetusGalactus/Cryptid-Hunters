using JetBrains.Annotations;
using UnityEngine;

public class PickUpEnginePart : MonoBehaviour
{

    public GameObject EnginePartOnPlayer;
    public GameObject PickUpText;
    public float partCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnginePartOnPlayer.SetActive(false);
        PickUpText.SetActive(false);
        partCount = 0f;
        
    }

    private void OnTriggerStay(Collider other) {

        if(other.gameObject.tag == "Player") {

            PickUpText.SetActive(true);

            if (Input.GetKey(KeyCode.E)) {

                this.gameObject.SetActive(false);
                //EnginePartOnPlayer.SetActive(true);

                PickUpText.SetActive(false);
                partCount++;
                Debug.Log("engine part collected! Count: " + partCount);
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        PickUpText.SetActive(false);
    }

}
