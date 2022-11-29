using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingClick : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    [SerializeField]
    GameObject Build3dPrefab;

    [SerializeField]
    Material DefaultColor;

    [SerializeField]
    Material ColorChange;

    void Update()
    {

        ///// PICK UP IF USER TAPS
        if (Input.touchCount > 0 &&  Input.GetTouch(0).phase == TouchPhase.Began)
    {
            ////// CAST RAY TO PICK UP HITS
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.position == transform.position)
                {
                    if(Build3dPrefab != null)
                    {
                        var Last3dPrefab = GameObject.FindGameObjectWithTag("Build3dPrefab");
                        if(Last3dPrefab != null)
                        {
                            var buildingName = Last3dPrefab.transform.Find("BuildName").GetComponent<TextMeshPro>().text;
                            GameObject.Find(buildingName).GetComponent<MeshRenderer>().material = DefaultColor;
                            GameObject.Destroy(Last3dPrefab);
                        }
                        var IBuildPrefab = GameObject.Instantiate(Build3dPrefab,
                        new Vector3(transform.position.x, GetComponent<BoxCollider>().size.z * 9,
                        transform.position.z), Quaternion.identity);

                        IBuildPrefab.transform.Find("BuildName").GetComponent<TextMeshPro>().text = this.name;
                        transform.GetComponent<MeshRenderer>().material = ColorChange;
                    }
                }
            } //// END OF RAYCAST
        }  //// END OF TOUCH BEGAN

    } //// END UP UPDATE
}
