﻿using UnityEngine;

namespace OctanGames
{
    public class ClearCounter : MonoBehaviour
    {
        public void Interact()
        {
            Debug.Log($"Interact with {gameObject.name}");
        }
    }
}