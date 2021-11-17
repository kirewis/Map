using UnityEngine;

[ExecuteInEditMode]
public class EnvLocator : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject[] prefabs;

    [SerializeField] private bool isRandomRotation = false;

    public void InstantiateTree(Vector3 pos, Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Quaternion rot = isRandomRotation
                ? Quaternion.Euler(0, Random.Range(0, 360), 0)
                : Quaternion.identity;
            
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)], hit.point, rot);
            obj.transform.SetParent(parent);
        }
    }
}
