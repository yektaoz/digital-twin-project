using UnityEngine;
using UnityEditor;


public class ScreenshotTaker : MonoBehaviour
{
    public static ScreenshotTaker instance;

    #region Directory name and file name
    public readonly string fileName = "Screenshot";
    public readonly string directoryName = "Screenshots";

    [HideInInspector]
    public int index;
    #endregion

    #region User options
    public KeyCode screenshotKey = KeyCode.F4;
    public enum OutputType {png, jpeg}
    public OutputType outputType;
    //[Range(1, 4)]
    //public int resolutionMultiplier = 1;
    #endregion

    public bool notifyWithDebugWarning = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            TakeScreenshot();
        }
    }
    public void TakeScreenshot()
    {
        if (!System.IO.Directory.Exists(directoryName))  //Create Screenshots folder if not exists;
        {
            System.IO.Directory.CreateDirectory(directoryName);
        }

        for (int i = 0; i < Mathf.Infinity; i++)
        {
            if (!System.IO.File.Exists(directoryName + "/" + fileName +  + i + "." + outputType))
            {
                if (notifyWithDebugWarning)
                {
                    Debug.LogWarning("File will be created as : " + fileName + i + " in to " + "'" + directoryName + "'" + " folder in your project root directory");
                }
                index = i;
                break;
            }
            else
            {
                //Debug.LogWarning("File : " + i + " Exists");
            }
        }
        CaptureScreen();
    }
    public void CaptureScreen()
    {
        ScreenCapture.CaptureScreenshot(directoryName + "/" + fileName + + index + "." + outputType);
        if (notifyWithDebugWarning)
        {
            Debug.LogWarning(fileName + +index + "." + outputType  + " created successfully in to " + "'" + directoryName + "'" + " folder in your project root directory");
        }
    }

    //
    //Carrot.
    //_ny.
}