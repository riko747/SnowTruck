using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour
{
    GameObject firstCar;

    enum CarMovementState { carIsMoving, carIsNotMoving};

    private int _carSpeed = 8;
    CarMovementState carMovementState;

    private ICheckTruckLocation _checkTruckLocation;

    private byte _iterator;

    private void Start()
    {
        _checkTruckLocation = GameObject.Find("Truck").GetComponent<ICheckTruckLocation>();
        firstCar = GameObject.Find("Car");
        if (gameObject.name == "Car")
        {
            Invoke("MoveCarCoroutine_Start", 2.5f);
        }
        else if (gameObject.name == "Car2")
        {
            Invoke("MoveCarCoroutine_Start", 3.5f);
        }
        carMovementState = CarMovementState.carIsNotMoving;
    }

    private void Update()
    {
        if (gameObject.name == "Car" && Vector3.Distance(transform.position, _checkTruckLocation.TruckPosition[_iterator]) <= 2)
        {
            _carSpeed = 2;
        }
        else if (gameObject.name == "Car" && Vector3.Distance(transform.position, _checkTruckLocation.TruckPosition[_iterator]) > 2)
        {
            _carSpeed = 8;
        }

        if (gameObject.name == "Car2" && Vector3.Distance(transform.position, firstCar.transform.position) <= 5)
        {
            _carSpeed = 0;
        }
        else if (gameObject.name == "Car2" && Vector3.Distance(transform.position, firstCar.transform.position) > 6)
        {
            _carSpeed = 8;
        }

        if (carMovementState == CarMovementState.carIsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _checkTruckLocation.TruckPosition[_iterator], _carSpeed * Time.deltaTime);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, _checkTruckLocation.TruckRotation[_iterator], 0));
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 3);
        }
        if (transform.position == _checkTruckLocation.TruckPosition[_iterator])
        {
            carMovementState = CarMovementState.carIsNotMoving;
        }
    }

    private void MoveCarCoroutine_Start() => StartCoroutine(MoveCarCoroutine());

    private IEnumerator MoveCarCoroutine()
    {
        while (true)
        {
            if (_iterator == _checkTruckLocation.TruckPosition.Length - 1)
            {
                _iterator = 0;
            }
            if (carMovementState != CarMovementState.carIsMoving)
            carMovementState = CarMovementState.carIsMoving;
            _iterator++;
            yield return new WaitForSeconds(0.55f);
        }
    }
}
