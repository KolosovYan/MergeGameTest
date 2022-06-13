using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    [SerializeField] private GameObject fxPrefab;
    [SerializeField] private List<GameObject> objectsPrefabs;
    public delegate void MergeScore(int mergeLevel);
    public event MergeScore onMergeSucces;

    public void Merge(GameObject obj1, GameObject obj2, Transform parent, int prefabLevel)
    {
        if (prefabLevel != objectsPrefabs.Count)
        {
            Instantiate(fxPrefab, parent.position, Quaternion.identity);
            Instantiate(objectsPrefabs[prefabLevel], parent.position, Quaternion.identity, parent);
            onMergeSucces(prefabLevel);
            if (prefabLevel == 1)
            {
                SetObjectToDefault(obj1);
                SetObjectToDefault(obj2);
            }
            else
            {
                Destroy(obj1);
                Destroy(obj2);
            }
        }
    }

    private void SetObjectToDefault(GameObject obj)
    {
        obj.transform.parent = null;
        obj.SetActive(false);
    }
}
