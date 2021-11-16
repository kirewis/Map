using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(TreeLocator))]
    public class TreeLocatorEditor : UnityEditor.Editor
    {
        private TreeLocator treeLocator;
        
        void Update()
        {
            if (target != null)
            {
                treeLocator = (TreeLocator) target;
            }
        }

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
            TreeLocator myScript = (TreeLocator)target;
            
            // if (Event.current.type == EventType.MouseUp && Event.current.button == 1)
            // {
            //     myScript.InstantiateTree();
            // }
            // else

            // if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.A)
            // {
            //     Debug.Log("sibal");
            //     //myScript.InstantiateTree();
            // }
            //
            // if (Event.current.type == EventType.KeyDown)
            // {
            //     Debug.Log("sibal");
            //     //myScript.InstantiateTree();
            // }
            
            // if (Event.current.keyCode == KeyCode.A)
            // {
            //     Debug.Log("1 pressed!");
            //     // Causes repaint & accepts event has been handled
            //     //Event.current.Use();
            // }
            
            // int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
            //
            // if (Event.current.GetTypeForControl(controlID) == EventType.KeyDown)
            // {
            //     if (Event.current.keyCode == KeyCode.A)
            //     {
            //         Debug.Log("1 pressed!");
            //         // Causes repaint & accepts event has been handled
            //         //Event.current.Use();
            //     }
            // }
            
            Event e = Event.current;

            switch (e.type)
            {
                case EventType.KeyUp:
                {
                    if (e.keyCode == KeyCode.K)
                    {
                        Debug.Log("sibal");

                        Vector3 mousePosition = Event.current.mousePosition;
                        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
                        
                        myScript.InstantiateTree(mousePosition, ray);
                    }

                    break;

                }
            }
        }
        
        private void OnGUI()
        {
            Debug.Log(Event.current);
        }
    }
}
