using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DescriptionConnectionDb : MonoBehaviour
{
    private DatabaseReference dbReference;

    public GameObject ConnError;
    public GameObject LoadedScene;
    private bool isLoading = true;
    public GameObject SpinImg;

    public TextMeshProUGUI BuildingNameText;
    public RawImage BgImage;
    public RawImage MainImage;

    GoConnectionDb KeyObject;
    string Key;
    GameObject recentCanvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spinner());

        KeyObject = GameObject.FindObjectOfType<GoConnectionDb>();
        Key = KeyObject.GetComponent<GoConnectionDb>().BuildKey;
        recentCanvas = GameObject.Find("GoCanvas");

        if (KeyObject != null && recentCanvas != null)
        {
            recentCanvas.SetActive(false);
            StartCoroutine(StartConnectionTest());
        }
        else
        {
            Debug.Log("no Object");
        }


    }

    IEnumerator StartConnectionTest()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            SpinImg.SetActive(false);
            isLoading = false;
            LoadedScene.SetActive(false);
            ConnError.SetActive(true);
        }
        else
        {
            dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            DisplayBuilding();

            SpinImg.SetActive(false);
            isLoading = false;
            LoadedScene.SetActive(true);
        }
    }

    public IEnumerator GetBuilding(Action<Dictionary<string, object>> onCallback)
    {
        if (Key != null)
        {
            var buildingData = dbReference.Child("Buildings").Child(Key).GetValueAsync();
            Dictionary<string, object> Building = new Dictionary<string, object>();
            yield return new WaitUntil(predicate: () => buildingData.IsCompleted);

            if (buildingData != null)
            {
                DataSnapshot snapshot = buildingData.Result;
                Building = snapshot.Value as Dictionary<string, object>;
                onCallback.Invoke(Building);
            }
        }

    }

    public void DisplayBuilding()
    {
        StartCoroutine(GetBuilding((Dictionary<string, object> Building) =>
        {
            foreach (var buildInfo in Building)
            {
                if (buildInfo.Key == "name")
                {
                    BuildingNameText.text = buildInfo.Value.ToString();
                }
                if (buildInfo.Key == "image")
                {
                    var images = buildInfo.Value as Dictionary<string, object>;
                    foreach (var image in images)
                    {
                        if (image.Key == "main")
                        {
                            Debug.Log(image.Value.ToString());
                            StartCoroutine(LoadImage(image.Value.ToString(), MainImage, BgImage)); 
                        }

                    }
                }
            }
        }));
    }

    public IEnumerator LoadImage(string url, RawImage DisplayImage, RawImage bgImage)
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
            bgImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
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

    public void backButton()
    {
        Destroy(KeyObject);
        SceneManager.LoadScene("GoScene");
    }
}
