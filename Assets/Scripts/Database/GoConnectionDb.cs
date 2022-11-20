using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
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

    public string BuildKey;
    GameObject recentCanvas;
    List<GameObject> CanvasList = new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            var Buildings = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().Buildings;
            var BuildingImages = GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().BuildingImages;
            StartCoroutine(Spinner());
            StartCoroutine(DisplayBuildings(Buildings, BuildingImages));
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    public IEnumerator DisplayBuildings(Dictionary<string, object> BuildingInfo, Dictionary<string, Texture> BuildingImages)
    {
        foreach (var building in BuildingInfo)
        {
            var buildingDes = building.Value as Dictionary<string, object>;
            GameObject buildingBox = Instantiate(BuildingBox, BoxContainer.transform);
            Button boxBtn = buildingBox.GetComponent<Button>();
            if (BuildingImages.ContainsKey(building.Key))
            {
                var buildingMainImg = buildingBox.transform.Find("Build_Img");
                RwImage = buildingMainImg.GetComponent<RawImage>();
                RwImage.texture = BuildingImages[building.Key];
            }

            foreach (var description in buildingDes)
            {
                if (description.Key == "name")
                {
                    var buildNameText = buildingBox.transform.Find("Build_Name");
                    buildNameText.GetComponent<TextMeshProUGUI>().text = description.Value.ToString();
                }
            }

            boxBtn.onClick.AddListener(() =>
            {
                onDataClick(building.Key);
            });

            if (BuildingInfo.Last().Key == building.Key)
            {
                SpinImg.SetActive(false);
                isLoading = false;
                ScrollArea.SetActive(true);
            }
        }

        yield return true;
    }

    private IEnumerator Spinner()
    {
        while (isLoading)
        {
            SpinImg.transform.Rotate(0, 0, -2f);
            yield return null;
        }
    }

    private void onDataClick(string Key)
    {
        if(Key != null)
        {
            GameObject.FindGameObjectWithTag("LoadedData").GetComponent<Data>().BuildingKey = Key;

            SceneManager.LoadScene("BuildingDescriptionScene");
        }
    }
}
