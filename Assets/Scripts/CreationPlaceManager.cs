using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationPlaceManager : MonoBehaviour
{
    [SerializeField] private Text cooldownText;
    [SerializeField] private Image cooldownImage;
    [SerializeField] private MergeManager mergeManager;
    [SerializeField] private List<ObjectController> objectPrefabs;
    private ObjectPool<ObjectController> objectsPool;
    private Vector3 spawnStartPos;
    [SerializeField] private ObjectController firstLevelPrefab;
    [SerializeField] private Transform spawnPointOriginalPos;

    private void Start()
    {
        objectsPool = new ObjectPool<ObjectController>(firstLevelPrefab, 9);
        objectsPool.autoExpand = true;
        spawnStartPos = spawnPointOriginalPos.position;
        CreateSpawnPoints();
        cooldown = true;
    }

    [SerializeField] private List<CreationPlaceScript> freeCreationPlaces;
    [SerializeField] private CreationPlaceScript cps;
    [SerializeField] private GameObject backgroundGameObject;
    [SerializeField] private Transform environment; 
    [SerializeField] private CreationPlaceScript spawnPointPrefab;
    [SerializeField] private int gridRows = 3;
    [SerializeField] private int gridCols = 3;
    [SerializeField] private float offset = 3;

    private void CreateSpawnPoints()
    {
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                if (i == 0 && j == 0)
                {
                    Instantiate(backgroundGameObject, new Vector3(spawnStartPos.x, spawnStartPos.y - 0.95f, spawnStartPos.z), Quaternion.identity, environment);
                }

                else
                {
                    float posX = (offset * i) + spawnStartPos.x;
                    float posY = (offset * j) + spawnStartPos.y;
                    cps = Instantiate(spawnPointPrefab, new Vector3(posX, posY, spawnStartPos.z), Quaternion.identity, environment).GetComponent<CreationPlaceScript>();
                    Instantiate(backgroundGameObject, new Vector3(posX, posY - 0.95f, spawnStartPos.z), Quaternion.identity, environment);
                    if (cps != null)
                        freeCreationPlaces.Add(cps);
                }
            }
        }
    }

    private bool cooldown;
    [SerializeField] private float cooldownTime = 15;
    private float currentCooldownTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateNewObject();
        }

        if (cooldown)
        {
            currentCooldownTime += Time.deltaTime;
            cooldownImage.fillAmount = currentCooldownTime / cooldownTime;
            cooldownText.text = (cooldownTime - currentCooldownTime).ToString("0");
            if (currentCooldownTime >= cooldownTime)
            {
                cooldown = false;
                currentCooldownTime = 0;
                if (freeCreationPlaces.Count != 0)
                    CreateNewObject();
            }
        }
    }

    public int GetPrefabsCount()
    {
        return objectPrefabs.Count;
    }

    public void ReturnFreeElement(Transform parent)
    {
        freeCreationPlaces.Add(parent.GetComponent<CreationPlaceScript>());
        if (!cooldown)
        {
            CreateNewObject();
        }
    }

    private CreationPlaceScript place;
    public delegate void ObjectCreated();
    public event ObjectCreated OnObjectCreated;

    public void CreateNewObject()
    {
        if (freeCreationPlaces.Count != 0)
        {
            place = freeCreationPlaces[Random.Range(0, freeCreationPlaces.Count)];
            place.ObjectSetActive(objectsPool.GetFreeElement());
            OnObjectCreated();
            freeCreationPlaces.Remove(place);
            place = null;
            cooldown = true;
        }
    }

    public void CreateMergedObject(Transform parent, int prefabMergeLvl)
    {
        CreationPlaceScript parentCreationPlace = parent.GetComponent<CreationPlaceScript>();
        parentCreationPlace.CreateObject(objectPrefabs[prefabMergeLvl]);
        OnObjectCreated();
    }

    public void OnCooldownButtonClick()
    {
        if (currentCooldownTime != 0)
        {
            currentCooldownTime += 1;
        }
    }

    public void ChangeCooldownTime()
    {
        if (cooldownTime != 1)
        {
            cooldownTime -= 1;
        }
    }
}
