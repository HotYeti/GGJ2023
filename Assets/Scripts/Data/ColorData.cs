using JetBrains.Annotations;
using UnityEngine;

namespace Data
{
    public static class ColorData
    {
        public static readonly Color P1Color = new Color(254f / 255f, 122f / 255f, 142f / 255f);
        public static readonly Color P2Color = new Color(137f / 255f, 234f / 255f, 179f / 255f);
        public static readonly Color SelectedTile = new Color(190f / 255f, 192f / 255f, 192f / 255f);
        public static readonly Color MovableTile = new Color(49f / 255f, 191f / 255f, 21f / 255f);
        public static readonly Color AttackableTile = new Color(189f / 255f, 21f / 255f, 49f / 255f);
    }
}