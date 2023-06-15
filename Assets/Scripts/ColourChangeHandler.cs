using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GPG212_02
{
    public class ColourChangeHandler : MonoBehaviour
    {
        enum ColorType { HAIR, SKIN }

        public SkinnedMeshRenderer[] items;
        public Graphic graphic;
        [SerializeField] ColorType colorType;

        private Color defaultSkinColor = new Color32(130, 91, 70, 255);
        private Color defaultHairColor = new Color32(42, 36, 58, 255);

        private void Awake()
        {
            graphic = GetComponent<Graphic>();
            graphic.color = items[0].material.color;
        }

        public void ResetCharacterColors()
        {
            foreach (var item in items)
            {
                if (colorType == ColorType.HAIR) item.material.color = defaultHairColor;
                else item.material.color = defaultSkinColor;
            }

            graphic.color = items[0].material.color;
        }
    }
}