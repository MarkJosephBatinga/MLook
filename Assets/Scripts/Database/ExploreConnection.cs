using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExploreConnection : MonoBehaviour
{

    [SerializeField]
    Material BuildingChangeMaterial;

    public GameObject ConnError;

    private bool isLoading = true;
    public GameObject SpinImg;

    // Start is called before the first frame update
    void Start()
    {
       /* if (GameObject.FindGameObjectWithTag("LoadedData") != null)
        {
            StartCoroutine(Spinner());
            StartCoroutine(ExploreLoad());
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }*/
    }

    IEnumerator ExploreLoad()
    {
        isLoading = false;
        SpinImg.SetActive(false);

        yield return null;
    }

    private IEnumerator Spinner()
    {
        while(isLoading)
        {
            SpinImg.transform.Rotate(0, 0, -2);
            yield return null;
        }
    }


    public void OnPathFindClick()
    {
        HighlightBuildings();
        RemoveActivePathing();
        RemoveClickableBuildings();
/*        UpdateCameraAngle();*/
        DisplayInstruction();
        ChangeButton();
    }

    public void OnCancelPathFind()
    {
        HighlightBuildings();
        RemoveClickableBuildings();
        RemoveActivePathways();
/*        UpdateCameraAngle();*/
        DisplayInstruction();
        ChangeButton();
    }

    public void OnUndoClick()
    {
        var UndoBtn = GameObject.Find("Undo");
        var FirstBuilding = GameObject.FindGameObjectWithTag("FirstBuilding");
        var activePath = GameObject.FindGameObjectWithTag("Pathway");
        var SafeArea = GameObject.Find("SafeArea");
        var InstructionBox = SafeArea.transform.Find("Instruction");
        var InstructionText = InstructionBox.transform.Find("InstructionText");
       

        if (activePath != null)
        {
            if (InstructionBox != null && InstructionText != null)
            {
                InstructionText.gameObject.GetComponent<TextMeshProUGUI>().text = "Select the building you want to go";
                InstructionBox.gameObject.SetActive(true);
            }
            activePath.SetActive(false);
        }
        else if (FirstBuilding != null)
        {
            var defaultColor = FirstBuilding.GetComponent<BuildingKey>().defaultMaterial;
            if (defaultColor != null && UndoBtn != null)
            {
                if (InstructionBox != null && InstructionText != null)
                {
                    InstructionText.gameObject.GetComponent<TextMeshProUGUI>().text = "Select your building location";
                    InstructionBox.gameObject.SetActive(true);
                }
                FirstBuilding.GetComponent<MeshRenderer>().material = defaultColor;
                UndoBtn.gameObject.SetActive(false);
                FirstBuilding.gameObject.transform.tag = "Pathfind";
            }
        }
    }

    public void RemoveClickableBuildings()
    {
        var ClickableBuildings = GameObject.FindGameObjectsWithTag("ClickableBuilding");
        var NonClickableBuildings = GameObject.FindGameObjectsWithTag("NonClickableBuilding");
        var FindPathBtn = GameObject.Find("ZoomBox").transform.Find("FindPath");
        var CancelBtn = GameObject.Find("ZoomBox").transform.Find("Cancel");

        if (FindPathBtn == true && CancelBtn == true && ClickableBuildings != null && ClickableBuildings != null)
        {
            if (FindPathBtn.gameObject.activeSelf == true)
            {
                RemoveBuildingName(ClickableBuildings);
                RemoveBuildingName(NonClickableBuildings);
                foreach (var building in NonClickableBuildings)
                {
                    building.GetComponent<MeshRenderer>().material = BuildingChangeMaterial;
                }

                foreach (var building in ClickableBuildings)
                {
                    var buildingClick = building.GetComponent<BuildingClick>();
                    if (buildingClick != null)
                    {
                        building.GetComponent<MeshRenderer>().material = BuildingChangeMaterial;
                        buildingClick.enabled = false;
                    }
                }
            }
            else
            {
                DisplayBuildingName(ClickableBuildings);
                DisplayBuildingName(NonClickableBuildings);
                foreach (var building in ClickableBuildings)
                {
                    var buildingClick = building.GetComponent<BuildingClick>();
                    if (buildingClick != null)
                    {
                        var dMaterial = building.GetComponent<BuildingKey>().defaultMaterial;
                        building.GetComponent<MeshRenderer>().material = dMaterial;
                        buildingClick.enabled = true;
                    }
                }
                foreach (var building in NonClickableBuildings)
                {
                    var dMaterial = building.GetComponent<BuildingKey>().defaultMaterial;
                    if(dMaterial != null)
                    {
                        building.GetComponent<MeshRenderer>().material = dMaterial;
                    }
                }
            }
        }
    }

    public void HighlightBuildings()
    {
        var PathFindBuildings = GameObject.FindGameObjectsWithTag("Pathfind");

        var FindPathBtn = GameObject.Find("ZoomBox").transform.Find("FindPath");
        var CancelBtn = GameObject.Find("ZoomBox").transform.Find("Cancel");

        if (FindPathBtn == true && CancelBtn == true && PathFindBuildings != null)
        {
            if (FindPathBtn.gameObject.activeSelf == true)
            {
                foreach (var building in PathFindBuildings)
                {
                    
                    var buildingClick = building.GetComponent<BuildingClick>();
                    var pathfindClick = building.GetComponent<PathfindingClick>();
                    if (buildingClick != null && pathfindClick != null)
                    {
                        buildingClick.enabled = false;
                        pathfindClick.enabled = true;
                    }
                }
            }
            else
            {
                var FirstBuilding = GameObject.FindGameObjectWithTag("FirstBuilding");
                if(FirstBuilding != null)
                {
                    var defaultColor = FirstBuilding.GetComponent<BuildingKey>().defaultMaterial;
                    if(defaultColor != null)
                    {
                        FirstBuilding.GetComponent<MeshRenderer>().material = defaultColor;
                    }
                    FirstBuilding.gameObject.transform.tag = "Pathfind";
                    FirstBuilding.GetComponent<BuildingClick>().enabled = true;
                    FirstBuilding.GetComponent<PathfindingClick>().enabled = false;
                }

                foreach (var building in PathFindBuildings)
                {
                    var buildingClick = building.GetComponent<BuildingClick>();
                    var pathfindClick = building.GetComponent<PathfindingClick>();
                    if (buildingClick != null && pathfindClick != null)
                    {
                        buildingClick.enabled = true;
                        pathfindClick.enabled = false;
                    }
                }
            }
        }
    }

/*    public void UpdateCameraAngle()
    {
        var FindPathBtn = GameObject.Find("ZoomBox").transform.Find("FindPath");
        var CancelBtn = GameObject.Find("ZoomBox").transform.Find("Cancel");
        var MainCamera = GameObject.Find("Main Camera");

        if (FindPathBtn == true && CancelBtn == true && MainCamera != null)
        {
            if (FindPathBtn.gameObject.activeSelf == true)
            {
                MainCamera.transform.position = new Vector3(120f, 300f, 180f);
                MainCamera.transform.eulerAngles = new Vector3(70f, 90f, 0);
                MainCamera.GetComponent<Camera>().fieldOfView = 100;
                MainCamera.GetComponent<CameraNavigation>().maxPosY = 500f;
            }
            else
            {
                MainCamera.transform.position = new Vector3(-315f, 300f, 260f);
                MainCamera.transform.eulerAngles = new Vector3(50f, 90f, 0);
                MainCamera.GetComponent<Camera>().fieldOfView = 50;
                MainCamera.GetComponent<CameraNavigation>().maxPosY = 300f;
            }

        }
    }*/

    public void DisplayInstruction()
    {
        var FindPathBtn = GameObject.Find("ZoomBox").transform.Find("FindPath");
        var CancelBtn = GameObject.Find("ZoomBox").transform.Find("Cancel");
        var SafeArea = GameObject.Find("SafeArea");

        if (FindPathBtn == true && CancelBtn == true && SafeArea != null)
        {
            if (FindPathBtn.gameObject.activeSelf == true)
            {
                var InstructionBox = SafeArea.transform.Find("Instruction");
                var InstructionText = InstructionBox.transform.Find("InstructionText");
                if (InstructionBox != null && InstructionText != null)
                {

                    InstructionText.gameObject.GetComponent<TextMeshProUGUI>().text = "Select your building location";
                    InstructionBox.gameObject.SetActive(true);
                }
            }
            else
            {
                var UndoBtn = SafeArea.transform.Find("ZoomBox").transform.Find("Undo");

                var InstructionBox = SafeArea.transform.Find("Instruction");
                if (InstructionBox != null && UndoBtn != null)
                {
                    UndoBtn.gameObject.SetActive(false);
                    InstructionBox.gameObject.SetActive(false);
                }
            }

        }
    }
    public void ChangeButton()
    {
        var FindPathBtn = GameObject.Find("ZoomBox").transform.Find("FindPath");
        var CancelBtn = GameObject.Find("ZoomBox").transform.Find("Cancel");

        if (FindPathBtn == true && CancelBtn == true)
        {
            if (FindPathBtn.gameObject.activeSelf == true)
            {
                FindPathBtn.gameObject.SetActive(false);
                CancelBtn.gameObject.SetActive(true);
            }
            else
            {
                FindPathBtn.gameObject.SetActive(true);
                CancelBtn.gameObject.SetActive(false);
            }
        }
    }

    public void RemoveActivePathing()
    {
        var PathFindBuildings = GameObject.FindGameObjectsWithTag("Pathfind");
        if (PathFindBuildings != null)
        {
            foreach (var building in PathFindBuildings)
            {
                var dMaterial = building.GetComponent<BuildingKey>().defaultMaterial;
                if (dMaterial != null)
                {
                    building.GetComponent<MeshRenderer>().material = dMaterial;
                }
            }
        }


        var activePath = GameObject.FindGameObjectWithTag("GatePathing");
        var activePrefab = GameObject.FindGameObjectWithTag("Build3dPrefab");
        if(activePath != null)
        {
            activePath.SetActive(false);
        }
        if(activePrefab != null)
        {
            activePrefab.SetActive(false);
        }
    }

    public void RemoveActivePathways()
    {
        var activePath = GameObject.FindGameObjectWithTag("Pathway");
 
        if (activePath != null)
        {
            activePath.SetActive(false);
        }
    }

    public void RemoveBuildingName(GameObject[] Buildings)
    {
        foreach (var building in Buildings)
        {
            for (int childCount = 0; childCount < building.transform.childCount; childCount++)
            {
                var buildingName = building.transform.GetChild(childCount);
                if (buildingName != null)
                {
                    buildingName.gameObject.SetActive(false);
                }
            }
           
        }
    }

    public void DisplayBuildingName(GameObject[] Buildings)
    {
        foreach (var building in Buildings)
        {
            for (int childCount = 0; childCount < building.transform.childCount; childCount++)
            {
                var buildingName = building.transform.GetChild(childCount);
                if (buildingName != null)
                {
                    buildingName.gameObject.SetActive(true);
                }
            }

        }
    }

   
}
