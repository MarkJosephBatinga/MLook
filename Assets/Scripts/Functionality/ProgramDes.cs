using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgramDes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject ProgramPrefab;

    [SerializeField]
    GameObject ProgramBox;

    [SerializeField]
    GameObject ProgramArea;

    [SerializeField]
    RawImage MainImg;

    [SerializeField]
    RawImage Logo;

    [SerializeField]
    TextMeshProUGUI CollegeNameText;

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
                SceneManager.LoadScene("CollegeDescriptionScene");
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

            if(college.Key == "programs")
            {
                var programs = college.Value as Dictionary<string, object>;
                foreach (var program in programs)
                {
                    var IProgBox = GameObject.Instantiate(ProgramBox, ProgramArea.transform);
                    var programDes = program.Value as Dictionary<string, object>;
                    foreach (var des in programDes)
                    {
                        if(des.Key == "name")
                        {
                            IProgBox.transform.Find("ProgramName").GetComponent<TextMeshProUGUI>().text = des.Value.ToString();
                        }
                    }
                }
            }
        }

        ProgramPrefab.SetActive(true);
        yield return null;
    }
}
