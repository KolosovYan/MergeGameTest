using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationPlaceManager : MonoBehaviour
{
    [SerializeField] private MergeMusicController mergeMusicController;
    [SerializeField] private MergeManager mergeManager;
    [SerializeField] private List<CreationPlace> creationPlaces;
    [SerializeField] private ObjectController firstLevelPrefab;
    [SerializeField] private float cooldownTime = 2;
    private ObjectPool<ObjectController> objectsPool;

    private void Start()
    {
        objectsPool = new ObjectPool<ObjectController>(firstLevelPrefab, 9);
        objectsPool.autoExpand = true;
        StartCoroutine(SpawnObjects());
    }

    public void TryToCreateObject()
    {
        foreach (CreationPlace place in creationPlaces)
        {
            if (place.cashedTransform.childCount == 0)
            {
                place.CreateObject(objectsPool.GetFreeElement());
                mergeMusicController.PlaySound(0);
                return;
            }
        }
    }

    IEnumerator SpawnObjects()
    {
        while(true)
        {
            yield return new WaitForSeconds(cooldownTime);
            TryToCreateObject();
        }
    }
}
