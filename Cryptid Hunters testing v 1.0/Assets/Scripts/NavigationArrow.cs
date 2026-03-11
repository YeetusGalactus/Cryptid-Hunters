using UnityEngine;
using System.Collections.Generic;
public class NavigationArrow : MonoBehaviour
{

    [SerializeField] List<Transform> transforms = new List<Transform>();
    [SerializeField] Transform pointingFrom;

    [SerializeField] Transform currentlyPointingTo;

    [SerializeField] Vector3 offset = new(0, -5, 5);

    [SerializeField] float arrowSpeed = 5.0f;
    private int currentIndex = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(transforms.Count > 0)
            currentlyPointingTo = transforms[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(currentlyPointingTo != null){
          
            LookAt(currentlyPointingTo.position);
        }
        
    }

    private void LookAt(Vector3 position)
    {
        position.y = transform.position.y;
        Vector3 relDirection = position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(relDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, arrowSpeed * Time.deltaTime);

    }


    public void GoToNextObject()
    {
        if(transforms.Count == 0)
        {
            return;
        }

        currentIndex++;
        if(currentIndex < transforms.Count)
            currentlyPointingTo = transforms[currentIndex];
    }
}
