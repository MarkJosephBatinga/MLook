using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollegeDes : MonoBehaviour
{
    [SerializeField]
    GameObject CollegePrefab;

    [SerializeField]
    RawImage MainImg;

    [SerializeField]
    RawImage Logo;

    [SerializeField]
    TextMeshProUGUI CollegeNameText; 
    
    [SerializeField]
    TextMeshProUGUI CollegeDesText;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedStaff = null;
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedBuilding = null;
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedCollege = null;

            var Colleges = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Colleges;
            var BuildingImages = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().BuildingImages;
            var CollegeLogos = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeLogos;
            var CollegeKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeKey;

            if (CollegeKey != null)
            {
                if (Colleges.ContainsKey(CollegeKey) && BuildingImages.ContainsKey(CollegeKey + "b"))
                {
                    Texture BuildingImg = BuildingImages[CollegeKey + "b"];
                    Texture CollegeLogo = CollegeLogos[CollegeKey];
                    Dictionary<string, object> CollegeDes = Colleges[CollegeKey] as Dictionary<string, object>;
                    StartCoroutine(DisplayCollege(CollegeDes, BuildingImg, CollegeLogo));
                }
            }
            else
            {
                SceneManager.LoadScene("CollegeScene");
            }
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    IEnumerator DisplayCollege(Dictionary<string, object> CollegeDes, Texture BuildingImg, Texture CollegeLogo)
    {
        //Set Image Texture;
        MainImg.texture = BuildingImg;

        //Set Image Texture;
        Logo.texture = CollegeLogo;


        //Set Building Name
        foreach (var college in CollegeDes)
        {
            if (college.Key == "name")
            {
                CollegeNameText.text = college.Value.ToString();
            }

            else if (college.Key == "des")
            {
                CollegeDesText.text = college.Value.ToString();
            }
        }

        CollegePrefab.SetActive(true);

        yield return null;
    }

}
