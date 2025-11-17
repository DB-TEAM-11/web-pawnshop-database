using UnityEngine;
using System.Linq;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ActionItemTog : MonoBehaviour
{
    [SerializeField] int itemIndex;
    [SerializeField] ItemActionManager itemActionManager;

    public void OnToggleClicked(bool isOn)
    {
        itemActionManager.OnActionItemClicked(isOn, itemIndex);
    }
}