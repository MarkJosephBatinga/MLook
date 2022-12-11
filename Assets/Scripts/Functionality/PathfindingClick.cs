using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathfindingClick : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    [SerializeField]
    Material PFMaterialChange;

    void Update()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(i).tapCount == 2)
                {
                    ////// CAST RAY TO PICK UP HITS
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.position == transform.position)
                        {
                            var FirstBuilding = GameObject.FindGameObjectWithTag("FirstBuilding");
                            if (FirstBuilding != null)
                            {
                                var ActivePath = GameObject.FindGameObjectWithTag("Pathway");
                                if (ActivePath == null)
                                {
                                    FindPath(FirstBuilding.name, this.name);
                                    var SafeArea = GameObject.Find("SafeArea");
                                    var InstructionBox = SafeArea.transform.Find("Instruction");
                                    if (InstructionBox != null)
                                    {
                                        InstructionBox.gameObject.SetActive(false);
                                    }
                                }
                            }
                            else
                            {
                                this.transform.tag = "FirstBuilding";
                                this.GetComponent<MeshRenderer>().material = PFMaterialChange;
                                DisplayUndo();

                                var SafeArea = GameObject.Find("SafeArea");
                                var InstructionBox = SafeArea.transform.Find("Instruction");
                                if (InstructionBox != null)
                                {
                                    var instructText = InstructionBox.gameObject.transform.Find("InstructionText");
                                    if (instructText != null)
                                    {
                                        instructText.GetComponent<TextMeshProUGUI>().text = "Double Click to select the building you want to go";
                                    }
                                }
                            }
                        }
                    } //// END OF RAYCAST
                }
            }
            ///// PICK UP IF USER TAPS
            /*  if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
          {
              ////// CAST RAY TO PICK UP HITS
              ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

              if (Physics.Raycast(ray, out hit, Mathf.Infinity))
              {
                  if (hit.transform.position == transform.position)
                  {

                  }
              } //// END OF RAYCAST
          }  //// END OF TOUCH BEGAN
  */
        } //// END UP UPDATE
    }

    public void FindPath(string FirstBuilding, string SecondBuilding)
    {
        var FirstPath = GameObject.Find("Pathfinding").transform.Find(FirstBuilding + "-" + SecondBuilding);
        if(FirstPath != null)
        {
            FirstPath.gameObject.SetActive(true);
        }
        else
        {
            var secondPath = GameObject.Find("Pathfinding").transform.Find(SecondBuilding + "-" + FirstBuilding);
            if(secondPath != null)
            {
                secondPath.gameObject.SetActive(true);
            }
        }
    }

    public void DisplayUndo()
    {
        var SafeArea = GameObject.Find("ZoomBox");
        var UndoBtn = SafeArea.transform.Find("Undo");

        if(SafeArea != null && UndoBtn != null)
        {
            UndoBtn.gameObject.SetActive(true);
        }
       
    }
}
