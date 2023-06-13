using EasyAudioSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GPG212_02
{
    public class BodyItemSelecter : MonoBehaviour
    {
        public BodyItem[] bodyItems;
        [SerializeField] TextMeshProUGUI itemTextField;
        [SerializeField] bool disableBaseMesh;
        [SerializeField] GameObject baseMesh;
        public int startingIndex;
        [Space]

        public int index;

        private BodyItem _currentBodyItem;


        private void Start()
        {
            _currentBodyItem = bodyItems[startingIndex];

            index = startingIndex;
        }

        public void EquipNextBodyItem()
        {
            _currentBodyItem.mesh.SetActive(false);
            _currentBodyItem.isActive = false;
            if (index < bodyItems.Length - 1) index++;
            else index = 0;

            EquipBodyItemAtIndex(index);
        }

        public void EquipPreviousBodyItem()
        {
            _currentBodyItem.mesh.SetActive(false);
            _currentBodyItem.isActive = false;
            if (index > 0) index--;
            else index = bodyItems.Length - 1;

            EquipBodyItemAtIndex(index);
        }

        private void EquipBodyItemAtIndex(int index)
        {
            _currentBodyItem = bodyItems[index];
            _currentBodyItem.isActive = true;
            _currentBodyItem.mesh.SetActive(true);
            itemTextField.text = _currentBodyItem.name;

            if (disableBaseMesh)
            {
                if (this.index == 0) ChangeSkinnedMeshRendererState(true, baseMesh);
                else ChangeSkinnedMeshRendererState(false, baseMesh);
            }

            AudioManager.PlayAudio("ButtonClick");
        }

        private void ChangeSkinnedMeshRendererState(bool targetState, GameObject parent)
        {
            if (parent.GetComponent<SkinnedMeshRenderer>())
            {
                parent.GetComponent<SkinnedMeshRenderer>().enabled = targetState;
            }
            else
            {
                foreach (Transform child in parent.transform)
                {
                    child.GetComponent<SkinnedMeshRenderer>().enabled = targetState;
                }
            }
        }
    }
}