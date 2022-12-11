using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTargetFound : MonoBehaviour
{
    [SerializeField]
    string Key;

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
        var InstructBox = GameObject.Find("Canvas").transform.Find("Instruct");
        if (Key != null && InstructBox != null)
        {
            InstructBox.gameObject.SetActive(false);
            GameObject ICollegeObject = GameObject.Instantiate(CollegePrefab);
            ICollegeObject.transform.Find("Behavior").GetComponent<CollegeArPrefab>().CollegeKey = Key;
        }
    }


    public void onBuildingFound()
    {
        var InstructBox = GameObject.Find("Canvas").transform.Find("Instruct");
        if (InstructBox != null)
        {
            InstructBox.gameObject.SetActive(false);
            GameObject IBuildingObject = GameObject.Instantiate(BuildingPrefab);
        }
    }

    public void onLost()
    {
        /*DestroyImmediate(CollegePrefab, true);
        DestroyImmediate(BuildingPrefab, true);*/
        var InstructBox = GameObject.Find("Canvas").transform.Find("Instruct");
        if (GameObject.FindGameObjectWithTag("ARCollegePrefab") != null && InstructBox != null)
        {
            InstructBox.gameObject.SetActive(true);
            GameObject CollegePrefab = GameObject.FindGameObjectWithTag("ARCollegePrefab");
            Destroy(CollegePrefab);
        }
        else if (GameObject.FindGameObjectWithTag("ARBuildingPrefab") != null)
        {
            InstructBox.gameObject.SetActive(true);
            GameObject BuildingPrefab = GameObject.FindGameObjectWithTag("ARBuildingPrefab");
            Destroy(BuildingPrefab);
        }
    }
}
