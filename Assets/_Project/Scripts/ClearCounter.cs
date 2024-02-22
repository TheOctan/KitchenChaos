using UnityEngine;

namespace OctanGames
{
    public class ClearCounter : MonoBehaviour
    {
        [SerializeField] private Transform _counterTopPoint;
        [SerializeField] private KitchenObjectData _kitchenObjectData;

        public void Interact()
        {
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectData.Prefab, _counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
        }
    }
}