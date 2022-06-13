using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreationPlace : MonoBehaviour
{
    public Transform cashedTransform;
    [SerializeField] private GameObject fxPrefab;

    private void Start()
    {
        cashedTransform = transform;
    }

    public void CreateObject(ObjectController obj)
    {
        obj.transform.parent = cashedTransform;
        obj.transform.position = cashedTransform.position;
        Instantiate(fxPrefab, cashedTransform.position, Quaternion.identity);
        obj.gameObject.SetActive(true);
        obj.SetDefaultPosition();
    }
}
