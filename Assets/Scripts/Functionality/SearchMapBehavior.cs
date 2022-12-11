using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchMapBehavior : MonoBehaviour
{
    [SerializeField]
    Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            var StaffKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedStaff;
            var BuildingKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedBuilding;
            var CollegeKey = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().SearchedCollege;

           if(StaffKey != null)
            {
                DisplayStaffDes(StaffKey);
            }
           else if(BuildingKey != null)
            {;
                DisplayBuildingDes(BuildingKey);
            }
           else if(CollegeKey != null)
            {
                DisplayCollegeDes(CollegeKey);
            }
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    public void DisplayStaffDes(string StaffKey)
    {
        var DescriptBox = GameObject.Find("SafeArea").transform.Find("StaffDescriptBox");
        var Staffs = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Staffs;
        if (DescriptBox != null)
        {
            foreach (var offices in Staffs)
            {
                var staffs = offices.Value as Dictionary<string, object>;
                var staff = staffs.GetValueOrDefault(StaffKey);
                if(staff != null)
                {
                    var descript = staff as Dictionary<string, object>;
                    foreach (var des in descript)
                    {
                        if(des.Key == "Name")
                        {
                            var NameText = DescriptBox.Find("Name");
                            if(NameText != null)
                            {
                                NameText.GetComponent<TextMeshProUGUI>().text = des.Value.ToString();
                            }
                        }
                        else if (des.Key == "Building")
                        {
                            var BuildText = DescriptBox.Find("Building");
                            if (BuildText != null)
                            {
                                BuildText.GetComponent<TextMeshProUGUI>().text = des.Value.ToString();
                            }
                        }
                        else if (des.Key == "Position")
                        {
                            var PostText = DescriptBox.Find("Position");
                            if (PostText != null)
                            {
                                PostText.GetComponent<TextMeshProUGUI>().text = des.Value.ToString();
                            }
                        }
                        else if (des.Key == "Office")
                        {
                            var OffText = DescriptBox.Find("Office");
                            if (OffText != null)
                            {
                                OffText.GetComponent<TextMeshProUGUI>().text = des.Value.ToString();
                            }
                        }
                    }
                    ChangeCameraTransform(offices.Key);
                }
            }
            
            DescriptBox.gameObject.SetActive(true);
        }

    }

    public void DisplayCollegeDes(string CollegeKey)
    {
        var DescriptBox = GameObject.Find("SafeArea").transform.Find("CollegeDescriptBox");
        var Colleges = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Colleges;
        var CollegeLogos = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().CollegeLogos;
        if (DescriptBox != null)
        {
            var college = Colleges.GetValueOrDefault(CollegeKey);
            if (college != null)
            {
                var descript = college as Dictionary<string, object>;
                foreach (var des in descript)
                {
                    if (des.Key == "name")
                    {
                        var NameText = DescriptBox.Find("Name");
                        if (NameText != null)
                        {
                            NameText.GetComponent<TextMeshProUGUI>().text = des.Value.ToString();
                        }
                    }
                }
            }

            var logo = CollegeLogos.GetValueOrDefault(CollegeKey);
            if (logo != null)
            {
                var LogoImg = DescriptBox.Find("Logo");
                if (LogoImg != null)
                {
                    LogoImg.GetComponent<RawImage>().texture = logo;
                }
            }
            DescriptBox.gameObject.SetActive(true);
        }
        Debug.Log(CollegeKey + "b");
        ChangeCameraTransform(CollegeKey + "b");
    }
    public void DisplayBuildingDes(string BuildingKey)
    {
        var DescriptBox = GameObject.Find("SafeArea").transform.Find("BuildingDescriptBox");
        var Buildings = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Buildings;
        var BuildingImgs = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().BuildingImages;

        if (DescriptBox != null)
        {
            var building = Buildings.GetValueOrDefault(BuildingKey);
            if (building != null)
            {
                var descript = building as Dictionary<string, object>;
                foreach (var des in descript)
                {
                    if (des.Key == "name")
                    {
                        var NameText = DescriptBox.Find("Name");
                        if (NameText != null)
                        {
                            NameText.GetComponent<TextMeshProUGUI>().text = des.Value.ToString();
                        }
                    }
                }
            }

            var img = BuildingImgs.GetValueOrDefault(BuildingKey);
            if (img != null)
            {
                var Img = DescriptBox.Find("Image");
                if (Img != null)
                {
                    Img.GetComponent<RawImage>().texture = img;
                }
            }
            DescriptBox.gameObject.SetActive(true);
        }



        ChangeCameraTransform(BuildingKey);
    }

    public void ChangeCameraTransform(string key)
    {
        var FindClickable = GameObject.FindGameObjectsWithTag("SearchBuilding");
        foreach (var building in FindClickable)
        {
            var SearchKey = building.GetComponent<BuildingKey>().SearchKey;
            if(SearchKey != null && SearchKey == key)
            {
                var TextBox = building.transform.Find("Name");
                if(TextBox != null)
                {
                    TextBox.gameObject.SetActive(true);
                }
                MainCamera.transform.parent = building.transform;
                MainCamera.transform.localPosition = new Vector3(-3f, 0f, 10f);
                MainCamera.transform.localEulerAngles = new Vector3(0f, 120f, 90f);

                //Display the path from the gate
                string Section = building.transform.parent.name;
                string pathName = "GATE-" + building.name;
                GetGatePath(Section, pathName);
            }
        }

       
    }

    public void GetGatePath(string Section, string pathName)
    {
        //Find the Path
        var pathway = GameObject.Find("Pathing").
            transform.Find("Gate").
            transform.Find(Section). //Find on the Section Object
            transform.Find(pathName); //Find the Gameobject by Name

        if (pathway == true && pathway.gameObject.activeSelf == false)
        {
            pathway.gameObject.SetActive(true);
        }
    }

    public void RemoveDescriptBox()
    {
        var descriptBox = GameObject.FindGameObjectWithTag("SearchDescriptBox");
        if(descriptBox != null)
        {
            descriptBox.SetActive(false);
        }
    }
}
