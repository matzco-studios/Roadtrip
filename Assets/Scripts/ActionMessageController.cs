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

    public void Activate() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Function used by inventory controller to tell the user if he want to grab an item.
    /// </summary>
    /// <param name="itemName">The name of the item that the user can take.</param>
    public void GrabItem(string itemName) {
        _key.text = "E";
        _action.text = "to grab";
        _itemName.text = itemName;
        Activate();
    }
}
