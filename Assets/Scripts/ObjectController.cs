using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    public int mergeLevel;
    private bool mouseUp;
    private bool OnDefaultPos;
    private Vector3 defaultPosition;
    private Vector3 offsetZ = new Vector3(0f, 0f, 10f);
    private Vector3 scaleOffset = new Vector3(0.1f, 0.1f, 0.1f);
    private Transform cashedTransform;
    private ObjectController otherGameObjectController;
    private SpriteRenderer otherSpriteRenderer;
    private BoxCollider2D cashedBoxCollider2d;
    private MergeManager mergeManager;

    private void Awake()
    {
        cashedTransform = transform;
        cashedBoxCollider2d = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        mergeManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MergeManager>();
        SetDefaultPosition();
    }

    private void Update()
    {
        if (!OnDefaultPos)
        {
            cashedTransform.position = Vector2.MoveTowards(cashedTransform.position, defaultPosition, (speed * 1) * Time.deltaTime);

            if (cashedTransform.position == defaultPosition)
            {
                OnDefaultPos = true;
                cashedBoxCollider2d.isTrigger = false;
            }
        }
    }

    IEnumerator MouseDelay()
    {
        yield return new WaitForSeconds(0.05f);
        mouseUp = false;
    }

    private void OnMouseUp()
    {
        OnDefaultPos = false;
        mouseUp = true;
        cashedTransform.localScale -= scaleOffset;
        StartCoroutine(MouseDelay());
    }

    private void OnMouseDown()
    {
        cashedTransform.localScale += scaleOffset;
    }

    private void OnMouseDrag()
    {
        cashedTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetZ;
        cashedBoxCollider2d.isTrigger = true;
        mouseUp = false;
    }

    public void SetDefaultPosition()
    {
        defaultPosition = cashedTransform.parent.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cashedBoxCollider2d.isTrigger && OnDefaultPos)
        {
            otherGameObjectController = other.GetComponent<ObjectController>();
            otherSpriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
            otherSpriteRenderer.color = Color.green;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (cashedBoxCollider2d.isTrigger && mouseUp)
        {
            if (otherGameObjectController.mergeLevel == mergeLevel)
            {
                cashedBoxCollider2d.isTrigger = false;
                mouseUp = false;
                otherSpriteRenderer.color = Color.white;
                mergeManager.Merge(gameObject, other.gameObject, otherGameObjectController.cashedTransform.parent, mergeLevel);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            if (otherSpriteRenderer != null)
            otherSpriteRenderer.color = Color.white;
    }
}