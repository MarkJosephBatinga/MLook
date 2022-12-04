using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBuildLocate : MonoBehaviour
{
    public void OnLocateClick()
    {
        var BuildingKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().BuildingKey;

        if (BuildingKey != null)
        {
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedBuilding = BuildingKey;
            SceneManager.LoadScene("SearchMapScene");
        }
    }

    public void OnCoLocateClick()
    {
        var CollegeKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeKey;

        if (CollegeKey != null)
        {
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedCollege = CollegeKey;
            SceneManager.LoadScene("SearchMapScene");
        }
    }

    public void ColDesClick()
    {
        var SearchedKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedCollege;

        if (SearchedKey != null)
        {
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeKey = SearchedKey;
            SceneManager.LoadScene("SearchMapScene");
        }
    }

    public void ColBuildClick()
    {
        var SearchedKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedBuilding;

        if (SearchedKey != null)
        {
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().BuildingKey = SearchedKey;
            SceneManager.LoadScene("SearchMapScene");
        }
    }
}
