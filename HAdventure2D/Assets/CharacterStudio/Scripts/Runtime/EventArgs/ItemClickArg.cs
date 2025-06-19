using System;

namespace CharacterStudio
{
    public class ItemClickArg : EventArgs
    {
        public eCharacterPart Part { get; private set; }
        public string Id { get; private set; }
        public ItemClickArg(eCharacterPart part, string id)
        {
            Part = part;
            Id = id;
        }
    }
}
