using System;
using UnityEngine;

namespace OctanGames
{
    public class SelectedCounterVisual : MonoBehaviour
    {
        [SerializeField] private ClearCounter _clearCounter;
        [SerializeField] private GameObject _visualGameObject;

        private void Start()
        {
            Player.Instance.SelectedCounterChanged += OnSelectedCounterChanged;
        }

        private void OnDestroy()
        {
            Player.Instance.SelectedCounterChanged -= OnSelectedCounterChanged;
        }

        private void OnSelectedCounterChanged(ClearCounter selectedClearCounter)
        {
            if (selectedClearCounter == _clearCounter)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        private void Show()
        {
            _visualGameObject.SetActive(true);
        }

        private void Hide()
        {
            _visualGameObject.SetActive(false);
        }
    }
}