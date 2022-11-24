using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaffDes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject StaffPrefab;

    [SerializeField]
    GameObject StaffBox;

    [SerializeField]
    GameObject StaffArea;

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
            var Colleges = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Colleges;
            var BuildingImages = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().BuildingImages;
            var CollegeLogos = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeLogos;
            var CollegeStaffs = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Staffs;
            var CollegeKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeKey;

            if (CollegeKey != null)
            {
                if (Colleges.ContainsKey(CollegeKey) && BuildingImages.ContainsKey(CollegeKey + "b"))
                {
                    Texture BuildingImg = BuildingImages[CollegeKey + "b"];
                    Texture CollegeLogo = CollegeLogos[CollegeKey];
                    Dictionary<string, object> CollegeDes = Colleges[CollegeKey] as Dictionary<string, object>;
                    if (CollegeStaffs.ContainsKey(CollegeKey + "b"))
                    {
                        Dictionary<string, object> Staffs = CollegeStaffs[CollegeKey + "b"] as Dictionary<string, object>;
                        StartCoroutine(DisplayStaffs(Staffs));
                    }
                   
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
        }

        StaffPrefab.SetActive(true);
        yield return null;
    }

    IEnumerator DisplayStaffs(Dictionary<string, object> CollegeStaffs)
    {
        foreach (var staffs in CollegeStaffs)
        {
            var IStaffBox = GameObject.Instantiate(StaffBox, StaffArea.transform);
            var staff = staffs.Value as Dictionary<string, object>;
            foreach (var des in staff)
            {
                if (des.Key == "Name")
                {
                    IStaffBox.transform.Find("StaffName").GetComponent<TextMeshProUGUI>().text = des.Value.ToString();
                }
                if (des.Key == "Position")
                {
                    IStaffBox.transform.Find("StaffPosition").GetComponent<TextMeshProUGUI>().text = des.Value.ToString();
                }
            }
          
        }
        yield return null;
    }
}
