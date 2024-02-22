using UnityEngine;

namespace OctanGames
{
    public class KitchenObject : MonoBehaviour
    {
        [SerializeField] private KitchenObjectData _kitchenObjectData;

        public KitchenObjectData KitchenObjectData => _kitchenObjectData;
    }
}