using System.Collections;
using UnityEngine;

interface ICheckTruckLocation
{
    Vector3[] TruckPosition { get; }
    float TruckRotationY { get; }
    Vector3 NextCarPosition { get; }
    bool MoveCar { get; set; }
}

public class TruckPath : MonoBehaviour, ICheckTruckLocation
{

    private Vector3[] truckPosition = new Vector3[5];
    private Vector3 _nextCarPosition;
    private float _truckRotationY;

    private bool _moveCar = false;

    private byte _iterator;

    #region Properties
    public Vector3[] TruckPosition
    {
        get { return truckPosition; }
    }
    public float TruckRotationY
    {
        get { return _truckRotationY; }
    }
    public Vector3 NextCarPosition
    {
        get { return _nextCarPosition; }
    }
    public bool MoveCar
    {
        get { return _moveCar; }
        set { _moveCar = value; }
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
            _nextCarPosition = truckPosition[_iterator];
            _truckRotationY = transform.rotation.eulerAngles.y + 90;
            _moveCar = true;
            _iterator++;
            yield return new WaitForSeconds(1f);
        }
    }
}
