using System.Collections;
using UnityEngine;

/// <summary>
/// Snow heaps spawn and reuse logic
/// </summary>
public class CreateSnowHeaps : MonoBehaviour
{
    private const int SnowHeapInstantiationRotationX = 0;
    private const int SnowHeapInstantiationRotationZ = 0;
    private const int MaxSnowHeapsAmount = 30;
    private const float SnowHeapsSpawnTime = 0.25f;

    private ICheckTruckLocation _checkTruckLocation;

    private byte _iterator = 0;
    private bool _canclelInstantiation = false;

    Transform snowHeapsPool;
    GameObject snowHeapsPrefab;
    Vector3 snowHeapInstantiationPosition;
    Quaternion snowheapInstantiationRotation;

    private void Start()
    {
        snowHeapsPrefab = Resources.Load<GameObject>("Prefabs/SnowHeaps");
        snowHeapsPool = GameObject.Find("SnowHeaps").transform;
        _checkTruckLocation = GetComponent<ICheckTruckLocation>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        StartCoroutine(CreateSnowHeapCoroutine());
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        StopCoroutine(CreateSnowHeapCoroutine());
    }

    private IEnumerator CreateSnowHeapCoroutine()
    {
        while (true)
        {
            RefreshIteratorAndStopSnowHeapsSparn();
            SpawnSnowHeap();
            _iterator += 1;

            yield return new WaitForSeconds(SnowHeapsSpawnTime);
        }
    }

    private void SpawnSnowHeap()
    {
        snowHeapInstantiationPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        snowheapInstantiationRotation = Quaternion.Euler(SnowHeapInstantiationRotationX, _checkTruckLocation.TruckRotationY, SnowHeapInstantiationRotationZ);
        if (!_canclelInstantiation)
        {
            GameObject snowHeapInstance = Instantiate(snowHeapsPrefab, snowHeapInstantiationPosition, snowheapInstantiationRotation, snowHeapsPool);
        }
        else
        {
            ReuseExistingSnowHeaps();
        }
    }

    private void RefreshIteratorAndStopSnowHeapsSparn()
    {
        if (_iterator >= MaxSnowHeapsAmount)
        {
            _canclelInstantiation = true;
            _iterator = 0;
        }
    }

    private void ReuseExistingSnowHeaps()
    {
        snowHeapsPool.GetChild(_iterator).position = snowHeapInstantiationPosition;
        snowHeapsPool.GetChild(_iterator).rotation = snowheapInstantiationRotation;
        snowHeapsPool.GetChild(_iterator).gameObject.SetActive(true);
    }
}
