using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EnvLocator))]
    public class TreeLocatorEditor : UnityEditor.Editor
    {
        // public override void OnInspectorGUI()
        // {
        //     DrawDefaultInspector();
        //     TreeLocator myScript = (TreeLocator)target;
        //
        //     if (GUILayout.Button("Instantiate Object"))
        //     {
        //         myScript.InstantiateTree();
        //     }
        // }

        public void OnSceneGUI()
        {
            EnvLocator myScript = (EnvLocator)target;

            Event e = Event.current;

            switch (e.type)
            {
                case EventType.KeyUp:
                {
                    if (e.keyCode == KeyCode.K)
                    {
                        Vector3 mousePosition = Event.current.mousePosition;
                        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
                        
                        myScript.InstantiateTree(mousePosition, ray);
                    }
                    break;

                }
            }
        }
    }
}
