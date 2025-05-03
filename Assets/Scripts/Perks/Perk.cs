using UnityEngine;

namespace Perks
{
    public abstract class Perk : ScriptableObject
    {
        public string perkName;
        [TextArea]
        public string description;
        public Sprite icon;
        public int limit;

        public abstract void Apply(GameObject player);
    }
}
