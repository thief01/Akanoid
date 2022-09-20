using System;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class LoadButton : MonoBehaviour
    {
        private Button button;
        private void Awake()
        {
            button = GetComponent<Button>();
            button.interactable = GameSaveLoad.IsPosibleToLoadLevel();
        }
    }
}
