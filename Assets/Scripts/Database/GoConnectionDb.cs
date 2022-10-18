using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GoConnectionDb : MonoBehaviour
{
    private DatabaseReference dbReference;

    public TextMeshProUGUI BuildingNameText;
    public GameObject ConnError;
    public GameObject ScrollArea;
    public GameObject BuildingBox;
    public GameObject BoxContainer;
    private RawImage RwImage;

    private bool isLoading = true;
    public GameObject SpinImg;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spinner());
        StartCoroutine(StartConnectionTest());
    }


    IEnumerator StartConnectionTest()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            SpinImg.SetActive(false);
            isLoading = false;
            ScrollArea.SetActive(false);
            ConnError.SetActive(true);
        }
        else
        {
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            GetBuildingInfo();

            SpinImg.SetActive(false);
            isLoading = false;
            ScrollArea.SetActive(true);
        }
    }

    public IEnumerator GetBuildings(Action<Dictionary<string, object>> onCallback)
    {
        var buildingData = dbReference.Child("Buildings").GetValueAsync();
        Dictionary<string, object> Buildings = new Dictionary<string, object>();
        yield return new WaitUntil(predicate: () => buildingData.IsCompleted);

        if (buildingData != null)
        {
            DataSnapshot snapshot = buildingData.Result;
            var buildings = snapshot.Value as Dictionary<string, object>;
            Buildings = buildings;
            onCallback.Invoke(Buildings);
        }
    }

    public void GetBuildingInfo()
    {
        StartCoroutine(GetBuildings((Dictionary<string, object> Buildings) =>
        {
            foreach (var building in Buildings)
            {
                var buildingDes = building.Value as Dictionary<string, object>;
                GameObject buildingBox = Instantiate(BuildingBox, BoxContainer.transform);
                

                foreach (var description in buildingDes)
                {
                    if (description.Key == "image")
                    {
                        var images = description.Value as Dictionary<string, object>;
                        foreach (var image in images)
                        {
                            if(image.Key == "main")
                            {
                                var buildingMainImg = buildingBox.transform.Find("Build_Img");
                                RwImage = buildingMainImg.GetComponent<RawImage>();
                                StartCoroutine(LoadImage(image.Value.ToString(), RwImage));
                            }
                        }
                        
                    }
                    else if (description.Key == "name")
                    {
                        var buildNameText = buildingBox.transform.Find("Build_Name");
                        buildNameText.GetComponent<TextMeshProUGUI>().text = description.Value.ToString();
                    }
                }

            }
        }));
    }

    public IEnumerator LoadImage(string url, RawImage DisplayImage)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            DisplayImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

    private IEnumerator Spinner()
    {
        while (isLoading)
        {
            SpinImg.transform.Rotate(0, 0, -2f);
            yield return null;
        }
    }

    public void tryConnection()
    {
        isLoading = true;
        SpinImg.SetActive(true);
        StartCoroutine(Spinner());


        ConnError.SetActive(false);
        StartCoroutine(StartConnectionTest());
    }
}
