using UnityEngine;

/// <summary>
/// Truck movement and rotation logic
/// </summary>
public class TruckMovement : MonoBehaviour
{
    private const int ReverseFactor = -1;
    private const int SmoothnessFactor = 4;

    private const int TruckSpeed = 5;
    private const int MinClampedAngle = 175;
    private const int MaxClampedAngle = 355;

    private const int DefaultTruckRotationX = 0;
    private const int DefaultTruckRotationZ = 0;


    private float _halfScreenWidth;
    private Vector3 _rotationVector;
    private Vector3 _rotationAmount;

    private void Start()
    {
        _halfScreenWidth = Screen.width / 2;
    }

    private void Update()
    {
        MoveTruck();
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Clamp(transform.eulerAngles.y, MinClampedAngle, MaxClampedAngle), transform.eulerAngles.z);
    }

    private void MoveTruck()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        transform.position += transform.forward * Time.deltaTime * TruckSpeed;
    }

    private void Rotate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            _rotationVector = new Vector3(DefaultTruckRotationX, _halfScreenWidth - touch.position.x, DefaultTruckRotationZ);
            _rotationAmount = _rotationVector * ReverseFactor / SmoothnessFactor * Time.deltaTime;
            transform.Rotate(_rotationAmount);
        }
    }
}
