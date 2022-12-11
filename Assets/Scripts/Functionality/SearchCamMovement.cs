using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCamMovement : MonoBehaviour
{
    //the reate of progression with panning
    public float Speed;

    public float maxPosY = 300f;
    
    public float RotX = 40f;
    public float RotY = 90f;
    public float RotZ = 0f;



    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            this.transform.parent = this.transform.root;
            Vector2 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;

            transform.Translate(-TouchDeltaPosition.x * Speed, -TouchDeltaPosition.y * Speed, 0);

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -315f, 300f),
                Mathf.Clamp(transform.position.y, maxPosY, maxPosY),
                Mathf.Clamp(transform.position.z, -520f, 580f));

            transform.eulerAngles = new Vector3(RotX, RotY, RotZ);
        }
    }
}
