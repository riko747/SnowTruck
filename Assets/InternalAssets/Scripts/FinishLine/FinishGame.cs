using UnityEngine;

/// <summary>
/// Finish game logic
/// </summary>
public class FinishGame : MonoBehaviour
{
    private const string FinishScreenName = "FinishScreen";
    private const string TruckName = "Truck";

    private IChangeGameState _changeGameState;
    private Transform _guiTransform;
    private GameObject _finishScreenGameobject;

    private void Start()
    {
        _changeGameState = GameObject.Find("GameManager").GetComponent<IChangeGameState>();
        _guiTransform = GameObject.Find("GUI").transform;

        AccessFinishScreen();
    }

    private void AccessFinishScreen()
    {
        foreach (Transform child in _guiTransform)
        {
            if (child.name == FinishScreenName)
            {
                _finishScreenGameobject = child.gameObject;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == TruckName)
        {
            ShowFinishScreen();
        }
    }

    private void ShowFinishScreen()
    {
        _finishScreenGameobject.SetActive(true);
        _changeGameState.PauseGame();
    }
}
