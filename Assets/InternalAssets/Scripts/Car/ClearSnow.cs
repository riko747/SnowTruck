using System.Collections;
using UnityEngine;

/// <summary>
/// Clear snow spawn and reuse mask logic
/// </summary>
public class ClearSnow : MonoBehaviour
{
    private const int MaxMasksAmount = 15;
    private const float MaskSpawnInterval = 0.5f;
    private const int DefaultMaskInstantiationRotationX = 90;
    private const int DefaultMaskInstantiationRotationZ = 0;

    private ICheckTruckLocation _checkTruckLocation;

    private byte _iterator = 0;
    private bool _canclelInstantiation = false;

    private Transform _scratchPool;
    private GameObject _maskPrefab;
    private Vector3 _maskInstantiationPosition;
    private Quaternion _maskInstantiationRotation;



    private void Start()
    {
        _maskPrefab = Resources.Load<GameObject>("Prefabs/SpriteMask");
        _scratchPool = GameObject.Find("Scratch").transform;
        _checkTruckLocation = GetComponent<ICheckTruckLocation>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        StartCoroutine(ClearSnowPart_Coroutine());
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        StopCoroutine(ClearSnowPart_Coroutine());
    }

    private IEnumerator ClearSnowPart_Coroutine()
    {
        while (true)
        {
            RefreshIteratorAndStopMasksSpawn();
            _maskInstantiationPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _maskInstantiationRotation = Quaternion.Euler(DefaultMaskInstantiationRotationX, _checkTruckLocation.TruckRotationY, DefaultMaskInstantiationRotationZ);
            if (!_canclelInstantiation)
            {
                GameObject maskInstance = Instantiate(_maskPrefab, transform.position, _maskInstantiationRotation, _scratchPool);
            }
            else
            {
                ReuseMasks();
            }
            _iterator += 1;

            yield return new WaitForSeconds(MaskSpawnInterval);
        }
    }

    private void RefreshIteratorAndStopMasksSpawn()
    {
        if (_iterator >= MaxMasksAmount)
        {
            _canclelInstantiation = true;
            _iterator = 0;
        }
    }

    private void ReuseMasks()
    {
        _scratchPool.GetChild(_iterator).position = _maskInstantiationPosition;
        _scratchPool.GetChild(_iterator).rotation = _maskInstantiationRotation;
        _scratchPool.GetChild(_iterator).gameObject.SetActive(true);
    }
}
