using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OpenDocURL : MonoBehaviour 
{

    // first found local_path by opening in browser
    // -- first version of file with 
    //public string pdf_name = "/DRAFT---Educational-Goals-Objectives-4366-Capstone.pdf";
    public string local_path = "file:///C:/Users/LeonHelios2019/Skele-Pedia/Skele-Pedia/Assets/Educational-Documents/DRAFT---Educational-Goals-Objectives-4366-Capstone.pdf";

    public void DoTheOpen(string uniqueFile)
    {
        // file and script in same place
        // path of files of Unity project (assets)
        string path = Application.dataPath;
        
        //Debug.Log(path);
        Debug.Log(local_path);
        
        // include folder name
        switch(uniqueFile) 
        {
            case :
                // code block
                break;
            case y:
                // code block
                break;
            default:
                // code block
                break;
        }
        local_path = "file:///"+path+"/Educational-Documents"+pdf_name;
        local_path = local_path.Replace(" ", "%20");
        Debug.Log(local_path);
        Application.OpenURL(local_path);
    }
    
}
