using UnityEngine;

namespace OctanGames
{
    [CreateAssetMenu]
    public class KitchenObjectData : ScriptableObject
    {
        public string ObjectName;
        public Sprite Sprite;
        public Transform Prefab;
    }
}