using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    private DatabaseReference dbReference;

    public GameObject SpinImg;
    public GameObject LoadStat;
    public GameObject LoadNum;
    public GameObject ConnError;
    public GameObject LoadBox;
    public GameObject LoadedData;

    private bool isLoading = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spinner());
        StartCoroutine(StartConnectionTest());
    }


    //Check if there is an internet connection
    IEnumerator StartConnectionTest()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            StopAllCoroutines();
            GameObject.Destroy(GameObject.FindGameObjectWithTag("LoadedData"));
            LoadBox.SetActive(false);
            ConnError.SetActive(true);
        }
        else
        {
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            StartCoroutine(GetColleges((Dictionary<string, object> colleges) =>
            {
                var DataBox = GameObject.Instantiate(LoadedData);
                DataBox.GetComponent<Data>().Colleges = colleges;
                StartCoroutine(GetCollegeLogo(DataBox));
            }));
        }

        yield return null;
    }

    //Get all college data in the database and store to a dictionary
    public IEnumerator GetColleges(Action<Dictionary<string, object>> onCallback)
    {
        LoadStat.GetComponent<TextMeshProUGUI>().text = "Loading Colleges";
        LoadNum.GetComponent<TextMeshProUGUI>().text = "";
        var collegeData = dbReference.Child("Colleges").GetValueAsync();
        yield return new WaitUntil(predicate: () => collegeData.IsCompleted);

        if(collegeData.IsCompleted && collegeData.Result != null)
        {
            DataSnapshot snapshot = collegeData.Result;
            var collegeSnap = snapshot.Value as Dictionary<string, object>;
            LoadStat.GetComponent<TextMeshProUGUI>().text = "College Data Retrieved";
            onCallback.Invoke(collegeSnap);
        }
    }

    //Iterate to each college logo url to download the image 
    public IEnumerator GetCollegeLogo(GameObject LoadedCollege)
    {
        LoadStat.GetComponent<TextMeshProUGUI>().text = "Loading College Logos";
        var collegesLoaded = LoadedCollege.GetComponent<Data>().Colleges;
        foreach (var college in collegesLoaded)
        {
            UnityWebRequest request = new UnityWebRequest("http://google.com");
            yield return request.SendWebRequest();

            if (request.error != null)
            {
                StopAllCoroutines();
                GameObject.Destroy(GameObject.FindGameObjectWithTag("LoadedData"));
                LoadBox.SetActive(false);
                ConnError.SetActive(true);
            }
            else
            {
                var collegeDes = college.Value as Dictionary<string, object>;
                foreach (var description in collegeDes)
                {
                    if (description.Key == "logo")
                    {
                        StartCoroutine(LoadLogo(description.Value.ToString(), LoadedCollege, college.Key));
                    }
                }
            }
        }
    }

    //Download the image and store it to the dictionary with a texture
    public IEnumerator LoadLogo(string url, GameObject LoadedCollege, string Key)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            LoadBox.SetActive(false);
            ConnError.SetActive(true);
        }
        else
        {
            var DataScriptLogo = LoadedCollege.GetComponent<Data>().CollegeLogos;
            var DataScript = LoadedCollege.GetComponent<Data>().Colleges;
            DataScriptLogo.Add(Key, ((DownloadHandlerTexture)www.downloadHandler).texture);
            LoadNum.GetComponent<TextMeshProUGUI>().text = DataScriptLogo.Count + "/" + DataScript.Count;
            if (DataScript.Count == DataScriptLogo.Count)
            {
                CollegeLoaded(LoadedCollege);
            }
        }
    }

    //Check if all college data is downloaded
    public void CollegeLoaded(GameObject CollegeLoaded)
    {
        LoadStat.GetComponent<TextMeshProUGUI>().text = "College Logos Retrieved";
        StartCoroutine(GetBuildings((Dictionary<string, object> buildings) =>
        {
            CollegeLoaded.GetComponent<Data>().Buildings = buildings;
            GetBuildingImage(CollegeLoaded);
        }));
    }


    //Get all the building data and store it to a dictionary
    public IEnumerator GetBuildings(Action<Dictionary<string, object>> onCallback)
    {
        LoadStat.GetComponent<TextMeshProUGUI>().text = "Loading Buildings";
        LoadNum.GetComponent<TextMeshProUGUI>().text = "";
        var buildingData = dbReference.Child("Buildings").GetValueAsync();
        yield return new WaitUntil(predicate: () => buildingData.IsCompleted);

        if (buildingData.IsCompleted && buildingData.Result != null)
        {
            DataSnapshot snapshot = buildingData.Result;
            var buildingSnap = snapshot.Value as Dictionary<string, object>;
            LoadStat.GetComponent<TextMeshProUGUI>().text = "Building Data Retrieved";
            onCallback.Invoke(buildingSnap);
        }
    }

    //Iterate to each image object to download the the images
    public void GetBuildingImage(GameObject LoadedBuildings)
    {
        LoadStat.GetComponent<TextMeshProUGUI>().text = "Loading Building Images";
        var buildingsLoaded = LoadedBuildings.GetComponent<Data>().Buildings;
        foreach (var building in buildingsLoaded)
        {
            var buildingDes = building.Value as Dictionary<string, object>;
            foreach (var description in buildingDes)
            {
                if (description.Key == "image")
                {
                    StartCoroutine(LoadImage(description.Value.ToString(), LoadedBuildings, building.Key));
                }
            }

        }
    }

    //Store the image to a dictionary with texture
    public IEnumerator LoadImage(string url, GameObject LoadedData, string Key)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            StopAllCoroutines();
            GameObject.Destroy(GameObject.FindGameObjectWithTag("LoadedData"));
            LoadBox.SetActive(false);
            ConnError.SetActive(true);
        }
        else
        {
            var DataScriptImages = LoadedData.GetComponent<Data>().BuildingImages;
            var DataScript = LoadedData.GetComponent<Data>().Buildings;
            DataScriptImages.Add(Key, ((DownloadHandlerTexture)www.downloadHandler).texture);
            LoadNum.GetComponent<TextMeshProUGUI>().text = DataScriptImages.Count + "/" + DataScript.Count;
            if (DataScript.Count == DataScriptImages.Count)
            {
                BuildingLoaded(LoadedData);
            }
        }
    }


    //Check if all building data are downloaded
    public void BuildingLoaded(GameObject DataLoaded)
    {
        LoadStat.GetComponent<TextMeshProUGUI>().text = "Building Images Retrieved";
        StartCoroutine(GetStaffs((Dictionary<string, object> staffs) =>
        {
            LoadNum.GetComponent<TextMeshProUGUI>().text = "";
            DataLoaded.GetComponent<Data>().Staffs = staffs;
            LoadNum.GetComponent<TextMeshProUGUI>().text = "Load Complete";
            GameObject.DontDestroyOnLoad(DataLoaded);
            SceneManager.LoadScene("ExploreScene");
        }));
    }

    //Get All Staff data and store to dictionary
    public IEnumerator GetStaffs(Action<Dictionary<string, object>> onCallback)
    {
        LoadStat.GetComponent<TextMeshProUGUI>().text = "Loading Staffs";
        LoadNum.GetComponent<TextMeshProUGUI>().text = "";
        var staffData = dbReference.Child("Staffs").GetValueAsync();
        yield return new WaitUntil(predicate: () => staffData.IsCompleted);

        if (staffData.IsCompleted && staffData.Result != null)
        {
            DataSnapshot snapshot = staffData.Result;
            var staffSnap = snapshot.Value as Dictionary<string, object>;
            LoadStat.GetComponent<TextMeshProUGUI>().text = "Staff Data Retrieved";
            onCallback.Invoke(staffSnap);
        }
    }

    public void tryConnection()
    {
        StartCoroutine(Spinner());
        LoadBox.SetActive(true);
        ConnError.SetActive(false);

        StartCoroutine(StartConnectionTest());
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
