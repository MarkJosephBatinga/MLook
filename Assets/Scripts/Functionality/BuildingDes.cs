using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BuildingDes : MonoBehaviour
{
    [SerializeField]
    GameObject SpinImg;

    [SerializeField]
    GameObject BuildingPrefab;

    [SerializeField]
    RawImage MainImg;

    [SerializeField]
    TextMeshProUGUI BuildingNameText;
    
    [SerializeField]
    TextMeshProUGUI DescriptionText;

    bool isLoading = true;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedStaff = null;
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedBuilding = null;
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedCollege = null;


            StartCoroutine(Spinner());
            var Buildings = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Buildings;
            var BuildingImages = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().BuildingImages;
            var BuildingKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().BuildingKey;

            if (BuildingKey != null)
            {
                if (Buildings.ContainsKey(BuildingKey) && BuildingImages.ContainsKey(BuildingKey))
                {
                    Texture BuildingImg = BuildingImages[BuildingKey];
                    Dictionary<string, object> BuildingDes = Buildings[BuildingKey] as Dictionary<string, object>; 
                    StartCoroutine(DisplayBuilding(BuildingDes, BuildingImg));
                }
            }
            else
            {
                SceneManager.LoadScene("GoScene");
            }
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    IEnumerator DisplayBuilding(Dictionary<string, object> BuildingDes, Texture BuildingImg)
    {
        isLoading = false;
        SpinImg.SetActive(false);

        //Set Image Texture;
        MainImg.texture = BuildingImg;

        //Set Building Name
        foreach (var building in BuildingDes)
        {
            if(building.Key == "name")
            {
                BuildingNameText.text = building.Value.ToString();
            }
            else if (building.Key == "purpose")
            {
                DescriptionText.text = building.Value.ToString();
            }
        }

        BuildingPrefab.SetActive(true);
        yield return null;


        yield return null;
    }


    private IEnumerator Spinner()
    {
        while (isLoading)
        {
            SpinImg.transform.Rotate(0, 0, -2f);
            yield return null;
        }
    }
}
