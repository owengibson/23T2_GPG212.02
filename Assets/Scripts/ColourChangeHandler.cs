using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GPG212_02
{
    public class ColourChangeHandler : MonoBehaviour
    {
        public SkinnedMeshRenderer[] items;
        public Graphic graphic;

        private void Awake()
        {
            graphic = GetComponent<Graphic>();
            graphic.color = items[0].material.color;
        }
    }
}