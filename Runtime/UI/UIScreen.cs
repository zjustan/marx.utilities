using UnityEngine;

namespace Marx.Utilities
{
    public class UIScreen : MonoBehaviour
    {
        public new RectTransform transform => (RectTransform)base.transform;

        public bool IsOpened { get; private set; }

        [Header("UI screen")]
        [SerializeField] private CanvasGroup canvasGroup;

        public UIScreenManager Manager { get; private set; }

        public void Init(UIScreenManager manager) => this.Manager = manager;

        public void Open()
        {
            if (IsOpened)
                return;

            IsOpened = true;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            OnOpen();
        }

        public void Close(bool force = false)
        {
            if (!force && !IsOpened)
                return;

            IsOpened = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            OnClose();
        }

        public void Return()
        {
            Manager.Return();
        }
        public void NavigateTo(UIScreen screen)
        {
            Manager.NavigatefromTo(this, screen);
        }
        protected virtual void OnOpen() { }

        protected virtual void OnClose() { }

    }
}