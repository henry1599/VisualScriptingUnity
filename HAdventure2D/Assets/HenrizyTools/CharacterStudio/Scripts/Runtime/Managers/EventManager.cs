namespace CharacterStudio
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public EventBus Bus { get; private set; }
    }
}
