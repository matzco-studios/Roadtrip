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
    public bool Active { get => gameObject.activeSelf; set => SetActive(value); }

    void Start()
    {
        gameObject.SetActive(false);
        _crosshair = transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<Image>();
    }

    /// <summary>
    /// Function to activate or desactivate the ActionMessageController instance.
    /// </summary>
    /// <param name="active">To tell to activate or desactivate.</param>
    public void SetActive(bool active = true)
    {
        gameObject.SetActive(active);
        _crosshair.sprite = active ? _crosshairLook : _crosshairNormal;
    }

    /// <summary>
    /// Default placeholder function for interacting with an object.
    /// </summary>
    public void InteractableItem(string itemName = "with object", string action = "to interact", string key = "F")
    {
        _key.text = key;
        _action.text = action;
        _itemName.text = itemName;
        SetActive();
    }

    /// <summary>
    /// Function used by inventory controller to tell the user if he want to grab an item.
    /// </summary>
    /// <param name="item">The item you want to grab.</param>
    public void GrabItem(GameObject item)
    {
        InteractableItem(itemName: item.GetComponent<GrabbableItem>()?.Name ?? item.name, action: "to grab", key: "E");
    }

    /// <summary>
    /// Function used by inventory controller to tell the user if he want to grab an item.
    /// </summary>
    /// <param name="isExit">If the user is trying to exit or enter the car.</param>
    public void CarInteraction(bool isExit = false)
    {
        InteractableItem(itemName: "car", action: isExit ? "to exit" : "to enter");
    }
}
