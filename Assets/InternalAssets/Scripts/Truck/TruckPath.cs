using System.Collections;
using UnityEngine;
/// <summary>
/// Calculation truck path for cars logic
/// </summary>
interface ICheckTruckLocation
{
    Vector3[] TruckPosition { get; }
    float[] TruckRotation { get; }
    float TruckRotationY { get; }
    Vector3 NextCarPosition { get; }
}

public class TruckPath : MonoBehaviour, ICheckTruckLocation
{
    private const int DefaultArrayDataAmount = 10;
    private const float PathCreationInterval = 0.5f;
    private const int FixedTruckAngle = 90;

    private Vector3[] truckPosition = new Vector3[DefaultArrayDataAmount];
    private float[] truckRotation = new float[DefaultArrayDataAmount];
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
            RefreshIterator();
            RegisterNewPathPoint();
            _iterator++;
            yield return new WaitForSeconds(PathCreationInterval);
        }
    }

    private void RefreshIterator()
    {
        if (_iterator == truckPosition.Length)
        {
            _iterator = 0;
        }
    }

    private void RegisterNewPathPoint()
    {
        truckPosition[_iterator] = transform.position;
        truckRotation[_iterator] = transform.rotation.eulerAngles.y + FixedTruckAngle;
        _nextCarPosition = truckPosition[_iterator];
        _truckRotationY = transform.rotation.eulerAngles.y + FixedTruckAngle;
    }
}
