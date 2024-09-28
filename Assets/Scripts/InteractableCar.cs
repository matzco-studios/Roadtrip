using UnityEngine;

public class InteractableCar : MonoBehaviour, IInteractable
{
    private GameObject _player;
    private GameObject _car;
    private Transform _exitOffset;
    private Transform _seatPosition;
    private Rigidbody _carBody;
    private bool _isInCar;
    private bool _canExit;
    public string InteractionInfo { get { return "Get in car"; } }

    private ActionMessageController _message;
    [SerializeField] private InventoryController _inventory;

    void Start()
    {
        _message = IInteractable.GetActionMessageController();
        _player = GameObject.FindGameObjectWithTag("Player");
        _car = transform.parent.gameObject;
        _car.GetComponent<CarController>().IsPlayerInside = false;
        _carBody = _car.GetComponent<Rigidbody>();
        _exitOffset = transform.GetChild(0);
        _seatPosition = transform.GetChild(1);
        _seatPosition.SetParent(null);
        _isInCar = false;
    }
    private void EnterCar()
    {
        _isInCar = true;
        _player.transform.SetParent(_seatPosition);
        _player.transform.localPosition = Vector3.zero;
        _player.GetComponent<PlayerController>().enabled = false;
        _player.GetComponent<CharacterController>().enabled = false;
        _car.GetComponent<CarController>().IsPlayerInside = true;
        _inventory.Desactivate();
    }
    private void ExitCar()
    {
        if (_canExit)
        {
            _isInCar = false;
            _player.transform.position = _exitOffset.transform.position;
            _player.transform.SetParent(null);
            Vector3 angle = new(0, _car.transform.eulerAngles.y + 10, 0);
            _player.transform.eulerAngles = angle;
            _player.GetComponent<PlayerController>().enabled = true;
            _player.GetComponent<CharacterController>().enabled = true;
            _car.GetComponent<CarController>().IsPlayerInside = false;
            _inventory.Activate();
        }
    }

    void LateUpdate()
    {
        _seatPosition.position = transform.position;
        _seatPosition.rotation = transform.rotation;
    }

    public void OnInteract()
    {
        if (_isInCar) ExitCar();
        else if (!_isInCar) EnterCar();
    }

    public void InteractionMessage()
    {
        if(_canExit) {
            _message.CarInteraction(_isInCar);
        }
    }

    void Update() {
        _canExit = _carBody.velocity.magnitude < 0.1f;

        if(!_canExit && _message.IsActive()) {
            _message.Disable();
            print("Disabling car message.");
        }
    }
}