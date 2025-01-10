using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStudio
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public EventBus Bus { get; private set; }
    }
}
