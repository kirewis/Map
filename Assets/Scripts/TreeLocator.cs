using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class TreeLocator : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private bool switchButton = false;
    [SerializeField] private Transform parent;
    
    [SerializeField] private GameObject[] treePrefabs;

    public void InstantiateTree(Vector3 pos, Ray ray)
    {
        //Vector3 mousePosition = Event.current.mousePosition;
        //Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Instantiate(treePrefabs[0], hit.point, Quaternion.identity);
            Debug.Log(hit.point);
        }
        
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //
        //     if (Physics.Raycast(ray, out RaycastHit hit))
        //     {
        //         Instantiate(treePrefabs[0], hit.point, Quaternion.identity);
        //         Debug.Log(hit.point);
        //     }
        // }
    }
}
