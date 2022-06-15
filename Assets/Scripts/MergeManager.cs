using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    [SerializeField] private CreationPlaceManager placeManager;
    public delegate void MergeScore(int mergeLevel);
    public event MergeScore onMergeSucces;
    private int prefabLevelLimit;

    private void Start()
    {
        prefabLevelLimit = placeManager.GetPrefabsCount();
    }

    public void Merge(ObjectController obj1, ObjectController obj2, Transform parent, int prefabLevel)
    {
        if (prefabLevel != prefabLevelLimit)
        {
            placeManager.CreateMergedObject(parent, prefabLevel);
            placeManager.ReturnFreeElement(obj1.transform.parent);
            onMergeSucces(prefabLevel);

            if (prefabLevel == 1)
            {
                SetObjectToDefaultState(obj1);
                SetObjectToDefaultState(obj2);
            }

            else if (prefabLevel > 1)
            {
                Destroy(obj1.gameObject);
                Destroy(obj2.gameObject);
            }
        }
    }

    private void SetObjectToDefaultState(ObjectController obj)
    {
        obj.transform.parent = null;
        obj.gameObject.SetActive(false);
    }
}
