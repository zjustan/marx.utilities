using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Marx.Utilities
{
    public class UIDropdownOption : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected TextMeshProUGUI textDisplay;
        [SerializeField] protected Image activeToggle;

        private bool clickEnabled = false;
        private new UIDropdownRenderer renderer;
        private Action onClick;

        public void Init(UIDropdownRenderer parent, DropdownOption option)
        {
            textDisplay.text = option.Text;
            clickEnabled = option.Enabled;
            activeToggle.enabled = option.Active;
            renderer = parent;
            onClick = option.OnClick;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!clickEnabled)
                return;

            onClick?.Invoke();
            renderer.Close();
        }
    }
}
