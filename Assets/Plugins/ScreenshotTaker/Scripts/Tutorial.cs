using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public GameObject _fileCreated;
    public Text _fileCreatedText;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if(System.IO.File.Exists(ScreenshotTaker.instance.directoryName + "/" + ScreenshotTaker.instance.fileName + (ScreenshotTaker.instance.index) + "." + ScreenshotTaker.instance.outputType))
            {
                _fileCreated.SetActive(true);
                _fileCreatedText.text = ScreenshotTaker.instance.fileName + (ScreenshotTaker.instance.index) + "." + ScreenshotTaker.instance.outputType + " created successfully in to " + "'" + ScreenshotTaker.instance.directoryName + "'" + " folder in your project root directory";

            }
        }
    }

}
