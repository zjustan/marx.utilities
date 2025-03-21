using UnityEngine;
using UnityEngine.UI;

namespace Marx.Utilities
{
    public class UIContainer : MonoBehaviour, ILayoutElement
    {
        public float minWidth => _minWidth;
        public float preferredWidth => _preferredWidth;
        public float flexibleWidth => _flexibleWidth;
        public float minHeight => _minHeight;
        public float preferredHeight => _preferredHeight;
        public float flexibleHeight => _flexibleHeight;
        public int layoutPriority => _layoutPriority;

        private float _minWidth, _minHeight;
        private float _preferredWidth, _preferredHeight;
        private float _flexibleWidth, _flexibleHeight;
        private int _layoutPriority;

        [SerializeField] private Component Source;

        public void CalculateLayoutInputHorizontal() { }

        public void CalculateLayoutInputVertical() { }

        private void Update()
        {
            if(Source is not ILayoutElement layout)
            {
                UpdateValue(ref _minWidth, 0);
                UpdateValue(ref _preferredWidth, 0);
                UpdateValue(ref _flexibleWidth, 0);
                UpdateValue(ref _minHeight, 0);
                UpdateValue(ref _preferredHeight, 0);
                UpdateValue(ref _flexibleHeight, 0);
                UpdateValue(ref _layoutPriority, 0);
                return;
            }

            UpdateValue(ref _minWidth, layout.minWidth);
            UpdateValue(ref _preferredWidth, layout.preferredWidth);
            UpdateValue(ref _flexibleWidth, layout.flexibleWidth);
            UpdateValue(ref _minHeight, layout.minHeight);
            UpdateValue(ref _preferredHeight, layout.preferredHeight);
            UpdateValue(ref _flexibleHeight, layout.flexibleHeight);
            UpdateValue(ref _layoutPriority, layout.layoutPriority);
        }

        private void UpdateValue<T>(ref T current, T to)
        {
            if(!current.Equals(to))
            {
                LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
                current = to;
            }
        }
    }
}
