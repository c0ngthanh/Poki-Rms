// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(SaveLoadSystem))]
// public class SaveManagerEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         SaveLoadSystem saveLoadSystem = (SaveLoadSystem) target;
//         string gameName = saveLoadSystem.gameData.Name;

//         DrawDefaultInspector();

//         if(GUILayout.Button("Save Game")){
//             saveLoadSystem.Save();
//         }
//         if(GUILayout.Button("Load Game")){
//             saveLoadSystem.Load(gameName);
//         }
//     }
// }
