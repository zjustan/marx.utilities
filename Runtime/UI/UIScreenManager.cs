using System.Collections.Generic;
using UnityEngine;

namespace Marx.Utilities
{
    public class UIScreenManager : MonoBehaviour
    {
        [SerializeField] private bool startOpen;
        [SerializeField] private UIScreen startingScreen;
        [SerializeField] private List<UIScreen> screens = new();

        private Stack<UIScreen> history = new();
        private UIScreen current;

        private void Awake()
        {
            foreach (UIScreen screen in screens)
            {
                screen.Init(this);
                screen.Close(true);
            }

            if (startOpen && startingScreen != null)
                Open();
        }

        public void Open()
        {
            startingScreen.Open();
        }


        private void OnValidate()
        {
            screens.Clear();
            screens.AddRange(GetComponentsInChildren<UIScreen>());
        }

        public void Return()
        {
            if (current != null)
                current.Close();

            if (history.TryPop(out current))
                current.Open();
        }

        public void NavigatefromTo(UIScreen from, UIScreen to)
        {
            history.Push(from);
            from.Close();
            current = to;
            to.Open();
        }

        private void CloseAll()
        {
            history.Clear();
            current.Close();
        }
    }
}
