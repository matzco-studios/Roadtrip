using TMPro;
using UnityEngine;

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

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
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
    /// <param name="isExit">If the user is trying to </param>
    public void CarInteraction(bool isExit = false)
    {
        _key.text = "F";
        _action.text = isExit ? "to exit" : "to enter";
        _itemName.text = "car";
        Activate();
    }
}
