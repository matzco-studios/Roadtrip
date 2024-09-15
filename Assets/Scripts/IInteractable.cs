public interface IInteractable
{
    string InteractionInfo { get; }

    void OnInteract();
}