// using System.IO;
// using RDTools;
// using RDTools.AutoAttach;
// using UnityEditor;
// using UnityEngine;
//
// public class IconGenerator : MonoBehaviour
// {
//     [SerializeField, Attach] private Camera camera;
//     [SerializeField] private string path = "/Sprites/UI/Icons/InventoryIcons/";
//     [SerializeField] private string name;
//
//     private int width = 512;
//     private int height = 512;
//
//     [Button("Take Screenshot")]
//     public void TakeScreenshot()
//     {
//         var renderTexture = new RenderTexture(width, height, 24);
//         camera.targetTexture = renderTexture;
//         var screenshot = new Texture2D(width, height, TextureFormat.RGBA32, false);
//         camera.Render();
//         RenderTexture.active = renderTexture;
//         screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
//         camera.targetTexture = null;
//         RenderTexture.active = null;
//
//         if (Application.isEditor)
//         {
//             DestroyImmediate(renderTexture);
//         }
//         else
//         {
//             Destroy(renderTexture);
//         }
//
//         Debug.Log("Path " + Application.dataPath + path + name + ".png");
//         byte[] bytes = screenshot.EncodeToPNG();
//         File.WriteAllBytes(Application.dataPath + path + name + ".png", bytes);
//         Debug.Log("Saved success");
//
// #if UNITY_EDITOR
//         AssetDatabase.Refresh();
// #endif
//     }
// }