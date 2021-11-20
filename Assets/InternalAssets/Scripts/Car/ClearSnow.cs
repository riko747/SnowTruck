using System.Collections;
using UnityEngine;

public class ClearSnow : MonoBehaviour
{
    private ICheckTruckLocation _checkTruckLocation;

    private byte _iterator = 0;
    private bool _canclelInstantiation = false;

    Transform scratch;
    GameObject maskPrefab;
    Vector3 maskInstantiationPosition;
    Quaternion maskInstantiationRotation;



    private void Start()
    {
        maskPrefab = Resources.Load<GameObject>("SpriteMask");
        scratch = GameObject.Find("Scratch").transform;
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
            if (_iterator >= 30)
            {
                _canclelInstantiation = true;
                _iterator = 0;
            }
            maskInstantiationPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            maskInstantiationRotation = Quaternion.Euler(90, _checkTruckLocation.TruckRotationY, 0);
            if (!_canclelInstantiation)
            {
                GameObject maskInstance = Instantiate(maskPrefab, maskInstantiationPosition, maskInstantiationRotation, scratch);
            }
            else
            {
                scratch.GetChild(_iterator).position = maskInstantiationPosition;
                scratch.GetChild(_iterator).rotation = maskInstantiationRotation;
                scratch.GetChild(_iterator).gameObject.SetActive(true);
            }
            _iterator += 1;

            yield return new WaitForSeconds(0.5f);
        }
    }


}
