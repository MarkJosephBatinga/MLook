using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class CameraNavigation : MonoBehaviour
{
    //the reate of progression with panning
    public float Speed;

    private void Update()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;

            transform.Translate(-TouchDeltaPosition.x * Speed, -TouchDeltaPosition.y * Speed, 0);

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -315f, 300f),
                Mathf.Clamp(transform.position.y, 200f, 200f),
                Mathf.Clamp(transform.position.z, -355f, 485f));
        }
    }


    public void OnZoomIn()
    {
        
        if (transform.GetComponent<Camera>().fieldOfView >= 30)
        {
            transform.GetComponent<Camera>().fieldOfView -= 10;
        }
    }

    public void OnZoomOut()
    {
        if (transform.GetComponent<Camera>().fieldOfView <= 70)
        {
            transform.GetComponent<Camera>().fieldOfView += 10;
        }
    }
}
