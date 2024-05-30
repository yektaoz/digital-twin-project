#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;



[ExecuteInEditMode]
public class ScreenshotTakerEditor : EditorWindow
{


	[MenuItem("Tools/Carrot./ScreenshotTaker")]
	public static void ShowWindow()
	{
		EditorWindow editorWindow = EditorWindow.GetWindow(typeof(ScreenshotTakerEditor));
		editorWindow.title = "Screenshot Taker";
        editorWindow.autoRepaintOnSceneChange = true;
		editorWindow.Show();
	}


	void OnGUI()
	{
		EditorGUILayout.LabelField("Screenshot Taker", EditorStyles.centeredGreyMiniLabel);

		Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Plugins/ScreenshotTaker/Resources/Textures/logo_sst.png", typeof(Texture));
		GUILayout.Box(banner, GUILayout.Width(200), GUILayout.Height(120));

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		if (GUILayout.Button("INSTALL", GUILayout.MaxWidth(200), GUILayout.MinHeight(40)))
		{
			// Debug.Log("Basic Counter Created");
			if (GameObject.Find("ScreenshotTaker") != null)
			{
				Debug.Log("There's already an ScreenshotTaker object in the scene");
				GameObject _object = GameObject.Find("ScreenshotTaker");
				DestroyImmediate(_object);
				GameObject obj1 = Instantiate(Resources.Load("ScreenshotTaker")) as GameObject;
				obj1.name = "ScreenshotTaker";
			}
			else
			{
				Debug.Log("ScreenshotTaker added to the scene succesfully !");
				GameObject obj1 = Instantiate(Resources.Load("ScreenshotTaker")) as GameObject;
				obj1.name = "ScreenshotTaker";
			}
		}


		EditorGUILayout.Space();

		EditorGUILayout.HelpBox("Click 'INSTALL' to add Screenshot Taker plugin to your scene", MessageType.Info);

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();


		if (GUILayout.Button("Open Screenshots Folder", GUILayout.MaxWidth(300), GUILayout.MinHeight(40)))
		{
			if (!System.IO.Directory.Exists("Screenshots"))  //Create Screenshots folder if not exists;
			{
				System.IO.Directory.CreateDirectory("Screenshots");
			}

			Application.OpenURL("Screenshots");
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.HelpBox("You need to be on 'Play Mode' to take screenshots.", MessageType.Warning);

		EditorGUILayout.Space();
		EditorGUILayout.Space();
        EditorGUILayout.Space();
		EditorGUILayout.HelpBox("Press 'F4' to take screenshot", MessageType.Info);

	}

}

#endif
