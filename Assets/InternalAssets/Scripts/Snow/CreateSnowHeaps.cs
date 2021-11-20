using System.Collections;
using UnityEngine;

public class CreateSnowHeaps : MonoBehaviour
{
    private ICheckTruckLocation _checkTruckLocation;

    private byte _iterator = 0;
    private bool _canclelInstantiation = false;

    Transform snowHeapsPool;
    GameObject snowHeapsPrefab;
    Vector3 snowHeapInstantiationPosition;
    Quaternion snowheapInstantiationRotation;

    private void Start()
    {
        snowHeapsPrefab = Resources.Load<GameObject>("SnowHeaps");
        snowHeapsPool = GameObject.Find("SnowHeaps").transform;
        _checkTruckLocation = GetComponent<ICheckTruckLocation>();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        StartCoroutine(CreateSnowHeapsCoroutine());
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        StopCoroutine(CreateSnowHeapsCoroutine());
    }

    private IEnumerator CreateSnowHeapsCoroutine()
    {
        while (true)
        {
            if (_iterator >= 30)
            {
                _canclelInstantiation = true;
                _iterator = 0;
            }
            snowHeapInstantiationPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            snowheapInstantiationRotation = Quaternion.Euler(0, _checkTruckLocation.TruckRotationY, 0);
            if (!_canclelInstantiation)
            {
                GameObject snowHeapInstance = Instantiate(snowHeapsPrefab, snowHeapInstantiationPosition, snowheapInstantiationRotation, snowHeapsPool);
            }
            else
            {
                snowHeapsPool.GetChild(_iterator).position = snowHeapInstantiationPosition;
                snowHeapsPool.GetChild(_iterator).rotation = snowheapInstantiationRotation;
                snowHeapsPool.GetChild(_iterator).gameObject.SetActive(true);
            }
            _iterator += 1;

            yield return new WaitForSeconds(0.25f);
        }
    }
}
