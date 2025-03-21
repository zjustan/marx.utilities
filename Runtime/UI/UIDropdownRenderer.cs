using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Marx.Utilities
{
    public class UIDropdownRenderer : Singleton<UIDropdownRenderer>
    {
        [SerializeField] private CanvasGroup group;
        [SerializeField] private RectTransform OptionsParent;
        [SerializeField] private UIDropdownOption optionPrefab;
        private new RectTransform transform => (RectTransform)base.transform;

        public void Diplay(Vector2 Position, IEnumerable<DropdownOption> options)
        {
            OptionsParent.DestroyChilderen();


            transform.position = Position;
            if(transform.position.y - transform.rect.height < 0)
            {
                transform.position += Vector3.up * transform.rect.height;
            }
            if (transform.rect.yMax > Screen.height)
            {
                transform.position += Vector3.down * (Screen.height - transform.rect.yMax);
            }

            foreach (DropdownOption option in options)
                Instantiate(optionPrefab, transform).Init(this, option);

            LayoutRebuilder.ForceRebuildLayoutImmediate(transform);

            group.alpha = 1f;
            group.blocksRaycasts = true;
        }

        public void Close()
        {
            group.alpha = 0f;
            group.blocksRaycasts = false;
        }
    }
}
