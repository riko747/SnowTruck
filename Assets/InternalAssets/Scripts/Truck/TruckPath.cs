using System.Collections;
using UnityEngine;

interface ICheckTruckLocation
{
    Vector3[] TruckPosition { get; }
    float[] TruckRotation { get; }
    float TruckRotationY { get; }
    Vector3 NextCarPosition { get; }
}

public class TruckPath : MonoBehaviour, ICheckTruckLocation
{

    private Vector3[] truckPosition = new Vector3[10];
    private float[] truckRotation = new float[10];
    private Vector3 _nextCarPosition;
    private float _truckRotationY;

    private byte _iterator;

    #region Properties
    public Vector3[] TruckPosition
    {
        get { return truckPosition; }
    }
    public float[] TruckRotation
    {
        get { return truckRotation; }
    }
    public float TruckRotationY
    {
        get { return _truckRotationY; }
    }
    public Vector3 NextCarPosition
    {
        get { return _nextCarPosition; }
    }
    #endregion

    private void Start()
    {
        _iterator = 0;
        Invoke("StartPathCheckCoroutine", 1);
    }

    private void StartPathCheckCoroutine() => StartCoroutine(PathCheckCoroutine());

    private IEnumerator PathCheckCoroutine()
    {
        while (_iterator >= 0)
        {
            if (_iterator == truckPosition.Length)
            {
                _iterator = 0;
            }
            truckPosition[_iterator] = transform.position;
            truckRotation[_iterator] = transform.rotation.eulerAngles.y + 90;
            _nextCarPosition = truckPosition[_iterator];
            _truckRotationY = transform.rotation.eulerAngles.y + 90;
            _iterator++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
