using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationPlaceScript : MonoBehaviour
{
    private Transform cashedTransform;
    [SerializeField] private GameObject fxPrefab;

    private void Start()
    {
        cashedTransform = transform;
    }

    private void CreateFXPrefab()
    {
        Instantiate(fxPrefab, cashedTransform.position, Quaternion.identity);
    }

    public void ObjectSetActive(ObjectController obj)
    {
        obj.transform.parent = cashedTransform;
        obj.transform.position = cashedTransform.position;
        CreateFXPrefab();
        obj.gameObject.SetActive(true);
        obj.SetDefaultPosition();
    }

    public void CreateObject(ObjectController obj)
    {
        CreateFXPrefab();
        Instantiate(obj, cashedTransform.position, Quaternion.identity, cashedTransform);
    }
}
