using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
                            RemoveLastClick(Last3dPrefab);
                           
                        }

                        //Create New Instance of #dBuildPrefab
                        var IBuildPrefab = GameObject.Instantiate(Build3dPrefab,
                        new Vector3(transform.position.x, GetComponent<BoxCollider>().size.z * 9,
                        transform.position.z), Quaternion.identity);//Set it's transform to the top of the building

                        //Change the BuildName text to the name of the building and change it's color
                        IBuildPrefab.transform.Find("BuildName").GetComponent<TextMeshPro>().text = this.name;
                        transform.GetComponent<MeshRenderer>().material = ColorChange;

                        //Display the path from the gate
                        string Section = transform.parent.name;
                        string pathName = "GATE-" + this.name;
                        GetGatePath(Section, pathName);
                    }
                }
            } //// END OF RAYCAST
        }  //// END OF TOUCH BEGAN

    } //// END UP UPDATE
    public void RemoveLastClick(GameObject Last3dPrefab)
    {
        var buildingName = Last3dPrefab.transform.Find("BuildName").GetComponent<TextMeshPro>().text;
        var lastBuilding = GameObject.Find(buildingName);
        if (lastBuilding != null)
        {
            var lastMaterial = lastBuilding.GetComponent<BuildingKey>().defaultMaterial;
            if (lastMaterial != null)
            {
                lastBuilding.GetComponent<MeshRenderer>().material = lastMaterial;

                //Remove Last Pathing
                string Section = lastBuilding.GetComponent<BuildingKey>().Section;
                string pathName = "GATE-" + lastBuilding.name;
                RemoveGatePath(Section, pathName);
            }

        }
        GameObject.Destroy(Last3dPrefab);
    }

    public void GetGatePath(string Section, string pathName)
    {
        //Find the Path
        var pathway = GameObject.Find("Pathing").
            transform.Find("Gate").
            transform.Find(Section). //Find on the Section Object
            transform.Find(pathName); //Find the Gameobject by Name

        if(pathway == true && pathway.gameObject.activeSelf == false)
        {
            pathway.gameObject.SetActive(true);
        }
    }

   public void RemoveGatePath(string Section, string pathName)
    {
        //Find the Path
        var pathway = GameObject.Find("Pathing").
            transform.Find("Gate").
            transform.Find(Section). //Find on the Section Object
            transform.Find(pathName); //Find the Gameobject by Name

        if (pathway == true && pathway.gameObject.activeSelf == true)
        {
            pathway.gameObject.SetActive(false);
        }
    }
}
