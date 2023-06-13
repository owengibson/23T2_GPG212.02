using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPG212_02
{
    public class CharacterManager : MonoBehaviour
    {
        public void RandomiseBodyItems()
        {
            foreach (var item in GetComponentsInChildren<BodyItemSelecter>())
            {
                item.index = Random.Range(0, item.bodyItems.Length);
                item.EquipNextBodyItem();
            }
        }

        public void ResetCharacter()
        {
            foreach (var item in GetComponentsInChildren<BodyItemSelecter>())
            {
                item.index = item.startingIndex - 1;
                item.EquipNextBodyItem();
            }
        }
    }
}