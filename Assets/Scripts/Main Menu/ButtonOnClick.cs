using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClick : MonoBehaviour
{
    public GameObject buttonImage;

    public void OnArClick()
    {
        Image Graphic = buttonImage.GetComponent<Image>();
        Graphic.color = Color.black;
    }

}
