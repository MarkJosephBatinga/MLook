using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTargetFound : MonoBehaviour
{
    [SerializeField]
    GameObject CollegePrefab;

    [SerializeField]
    GameObject BuildingPrefab;

    [SerializeField]
    GameObject DMMMMSUMap;

    void Start()
    {
        DMMMMSUMap.SetActive(false);
    }


    public void onCollegeFound()
    {
        GameObject ICollegeObject = GameObject.Instantiate(CollegePrefab);
    }


    public void onBuildingFound()
    {
        GameObject IBuildingObject = GameObject.Instantiate(BuildingPrefab);
    }

    public void onLost()
    {
        /*DestroyImmediate(CollegePrefab, true);
        DestroyImmediate(BuildingPrefab, true);*/
        if (GameObject.FindGameObjectWithTag("ARCollegePrefab") != null)
        {
            GameObject CollegePrefab = GameObject.FindGameObjectWithTag("ARCollegePrefab");
            Destroy(CollegePrefab);
        }
        else if (GameObject.FindGameObjectWithTag("ARBuildingPrefab") != null)
        {
            GameObject BuildingPrefab = GameObject.FindGameObjectWithTag("ARBuildingPrefab");
            Destroy(BuildingPrefab);
        }
    }
}
