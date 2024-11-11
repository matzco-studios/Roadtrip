namespace Enemies.Fsm
{
    // Événements d'état pour gérer les transitions
    public enum EventStage
    {
        Enter, // Entrée dans un état
        Update, // Mise à jour pendant un état
        Exit // Sortie d'un état
    };
}