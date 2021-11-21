using UnityEngine;
using System.Collections;

/// <summary>
/// Car movement and rotation logic
/// </summary>
public class CarMovement : MonoBehaviour
{
    private const int DefaultCarSpeed = 8;
    private const int SlowedCarSpeed = 2;
    private const int RotationSpeed = 3;
    private const int MinimalDistanceBetweenFirstCarAndTruck = 2;
    private const int MinimalDistanceBetweenFirstCarAndSecondCar = 5;
    private const float FirstCarDelayBeforeStart = 2.5f;
    private const float SecondCarDelayBeforeStart = 3.5f;

    private const string FirstCarGameobjectName = "Car";
    private const string SecondCarGameobjectName = "Car2";

    private const float SearchNextPathPointRefreshTime = .55f;
    
    enum CarMovementState { carIsMoving, carIsNotMoving };
    private CarMovementState _carMovementState;

    private ICheckTruckLocation _checkTruckLocation;


    private Vector3 _carPosition;
    private GameObject _firstCar;

    private int _carSpeed;
    

    private byte _iterator;

    private void Start()
    {
        _checkTruckLocation = GameObject.Find("Truck").GetComponent<ICheckTruckLocation>();
        _firstCar = GameObject.Find("Car");
        _carSpeed = DefaultCarSpeed;
        _carMovementState = CarMovementState.carIsNotMoving;

        LaunchCarsAfterDelay();
    }

    private void LaunchCarsAfterDelay()
    {
        if (gameObject.name == "Car")
        {
            Invoke("StartSearchPathCoroutine", FirstCarDelayBeforeStart);
        }
        else if (gameObject.name == "Car2")
        {
            Invoke("StartSearchPathCoroutine", SecondCarDelayBeforeStart);
        }
    }

    private void Update()
    {
        _carPosition = transform.position;
        LimitCarSpeed();
        MoveCar();
        PrepareToFindNextPathPoint();
    }

    private void PrepareToFindNextPathPoint()
    {
        if (transform.position == _checkTruckLocation.TruckPosition[_iterator])
        {
            _carMovementState = CarMovementState.carIsNotMoving;
        }
    }

    private void MoveCar()
    {
        if (_carMovementState == CarMovementState.carIsMoving)
        {
            transform.position = Vector3.MoveTowards(_carPosition, _checkTruckLocation.TruckPosition[_iterator], _carSpeed * Time.deltaTime);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, _checkTruckLocation.TruckRotation[_iterator], 0));
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
        }
    }

    private void LimitCarSpeed()
    {
        LimitFirstCarSpeed();
        LimitSecondCarSpeed();
    }

    private void LimitFirstCarSpeed()
    {
        if (gameObject.name == FirstCarGameobjectName && Vector3.Distance(_carPosition, _checkTruckLocation.TruckPosition[_iterator]) <= MinimalDistanceBetweenFirstCarAndTruck)
        {
            _carSpeed = SlowedCarSpeed;
        }
        else if (gameObject.name == FirstCarGameobjectName && Vector3.Distance(_carPosition, _checkTruckLocation.TruckPosition[_iterator]) > MinimalDistanceBetweenFirstCarAndTruck)
        {
            _carSpeed = DefaultCarSpeed;
        }
    }

    private void LimitSecondCarSpeed()
    {
        if (gameObject.name == SecondCarGameobjectName && Vector3.Distance(_carPosition, _firstCar.transform.position) <= MinimalDistanceBetweenFirstCarAndSecondCar)
        {
            _carSpeed = SlowedCarSpeed;
        }
        else if (gameObject.name == SecondCarGameobjectName && Vector3.Distance(_carPosition, _firstCar.transform.position) > MinimalDistanceBetweenFirstCarAndSecondCar)
        {
            _carSpeed = DefaultCarSpeed;
        }
    }
    
    private void StartSearchPathCoroutine() => StartCoroutine(SearchPathCoroutine());

    private IEnumerator SearchPathCoroutine()
    {
        while (true)
        {
            RefreshIterator();
            ChangeCarMovementStateToActive();
            _iterator++;
            yield return new WaitForSeconds(SearchNextPathPointRefreshTime);
        }
    }

    private void ChangeCarMovementStateToActive()
    {
        if (_carMovementState != CarMovementState.carIsMoving)
        {
            _carMovementState = CarMovementState.carIsMoving;
        }
    }

    private void RefreshIterator()
    {
        if (_iterator == _checkTruckLocation.TruckPosition.Length - 1)
        {
            _iterator = 0;
        }
    }
}
