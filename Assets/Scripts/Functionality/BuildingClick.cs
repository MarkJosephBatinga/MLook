using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClick : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    void Update()
    {

        ///// PICK UP IF USER TAPS
        if (Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Stationary)
    {
            ////// CAST RAY TO PICK UP HITS
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.position == transform.position)
                {
                    Debug.Log("building");
                }
            } //// END OF RAYCAST
        }  //// END OF TOUCH BEGAN

    } //// END UP UPDATE
}
