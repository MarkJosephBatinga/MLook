using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImgDownloader : MonoBehaviour
{
    
    string Url;
    RawImage Logo;
    private Texture texture;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public IEnumerator LoadImage(string url, RawImage DisplayImage)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://firebasestorage.googleapis.com/v0/b/mlook-2290f.appspot.com/o/Colleges%2FLogo%2FCAS.png?alt=media&token=879f9226-52fa-4060-bb34-7dbd8c98e123");
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
    public void GetImg(string MediaUrl, RawImage logo)
    {
        if(MediaUrl != null && logo != null)
        {
            Debug.Log(Url);
            Debug.Log(Logo.name);
        }
        else
        {
            Debug.Log("No data");
        }
        /*StartCoroutine(LoadImage(MediaUrl, logo));*/
            
        
    }
}

