using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MonoBehavior to handle the text popup when the user is going to do an action and need confirmation. 
/// Ex: grap an item, enter a car, etc.
/// </summary>
public class ActionMessageController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _press;
    [SerializeField] private TextMeshProUGUI _key;
    [SerializeField] private TextMeshProUGUI _action;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Sprite _crosshairNormal;
    [SerializeField] private Sprite _crosshairLook;
    private Image _crosshair;

    void Start() {
        gameObject.SetActive(false);
        _crosshair = transform.parent.GetChild(transform.GetSiblingIndex()+1).GetComponent<Image>();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        _crosshair.sprite = _crosshairLook;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        _crosshair.sprite = _crosshairNormal;
    }

    public bool IsActive() {
        return gameObject.activeSelf;
    }

    /// <summary>
    /// Function used by inventory controller to tell the user if he want to grab an item.
    /// </summary>
    /// <param name="item">The item you want to grab.</param>
    public void GrabItem(GameObject item)
    {
        var itemScript = item.GetComponent<GrabbableItem>();
        _key.text = "E";
        _action.text = "to grab";
        _itemName.text = itemScript && itemScript.Name.Length != 0 ? itemScript.Name : item.name;
        print($"Displayed the message for {_itemName.text}.");
        Activate();
    }

    /// <summary>
    /// Function used by inventory controller to tell the user if he want to grab an item.
    /// </summary>
    /// <param name="isExit">If the user is trying to exit or enter the car.</param>
    public void CarInteraction(bool isExit = false)
    {
        _key.text = "F";
        _action.text = isExit ? "to exit" : "to enter";
        _itemName.text = "car";
        Activate();
    }

    /// <summary>
    /// Default placeholder function for interacting with an object.
    /// </summary>
    public void InteractableItem()
    {
        _key.text = "F";
        _action.text = "to interact";
        _itemName.text = "with object";
        Activate();
    }
}
