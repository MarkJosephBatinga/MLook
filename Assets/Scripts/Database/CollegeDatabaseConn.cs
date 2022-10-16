using Firebase.Database;
using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CollegeDatabaseConn : MonoBehaviour
{
    private DatabaseReference dbReference;
    //private StorageReference stReference;

    public TextMeshProUGUI CollegeNameText;
    public GameObject ConnError;
    public GameObject ScrollArea;
    public GameObject CollegeBox;
    public GameObject BoxContainer;
    private RawImage RwImage;

    private void Start()
    {
        StartCoroutine(StartConnectionTest());
    }

    IEnumerator StartConnectionTest()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            ScrollArea.SetActive(false);
            ConnError.SetActive(true);
        }
        else
        {
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            //stReference = FirebaseStorage.DefaultInstance.RootReference;
            GetCollegeInfo();
        }
    }

    public IEnumerator GetCollegeName(Action<Dictionary<string, object>> onCallback)
    {
        var collegeData = dbReference.Child("Colleges").GetValueAsync();
        Dictionary<string, object> College = new Dictionary<string, object>();
        yield return new WaitUntil(predicate: () => collegeData.IsCompleted);

        if (collegeData != null)
        {
            DataSnapshot snapshot = collegeData.Result;
            var colleges = snapshot.Value as Dictionary<string, object>;
            College = colleges;
            onCallback.Invoke(College);
        }
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

    public void GetCollegeInfo()
    {
        StartCoroutine(GetCollegeName((Dictionary<string, object> Colleges) =>
        {
            foreach(var college in Colleges)
            {
                var collegeDes = college.Value as Dictionary<string, object>;
                GameObject collegeBox = Instantiate(CollegeBox, BoxContainer.transform);
                var collegeNameText = collegeBox.transform.Find("College_Name");
                

                foreach (var description in collegeDes)
                {
                    if (description.Key == "logo")
                    {
                        var collegeLogo = collegeBox.transform.Find("Logo");
                        RwImage = collegeLogo.GetComponent<RawImage>();
                        StartCoroutine(LoadImage(description.Value.ToString(), RwImage));
                    }
                    else if (description.Key == "name")
                    {
                        collegeNameText.GetComponent<TextMeshProUGUI>().text = description.Value.ToString();
                    }
                }

            }
        }));
    }
}

