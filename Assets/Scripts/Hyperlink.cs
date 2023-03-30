using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script is for URLS that lead to websites
public class Hyperlink : MonoBehaviour
{
    [SerializeField] private string link;
    public void OpenLink() {
        Application.OpenURL(link);
    }
}
