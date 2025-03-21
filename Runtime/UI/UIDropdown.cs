using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Marx.Utilities
{
    public class UIDropdown : MonoBehaviour, IDropdown
    {
        private List<IDropdownOptionProvider> providers = new();

        private List<DropdownOption> registeredOptions;
        private new RectTransform transform => (RectTransform)base.transform;

        public void Register(IDropdownOptionProvider provider)
        {
            providers.Add(provider);
        }

        public void Open()
        {
            registeredOptions ??= new();
            registeredOptions.Clear();
            foreach (IDropdownOptionProvider provider in providers)
                if (provider.enabled)
                    provider.OnDropdownOptionsCreate(this);

            if (registeredOptions.Count > 0)
                UIDropdownRenderer.Instance.Diplay(
                    Mouse.current.position.ReadValue(),
                    registeredOptions);
        }

        public void Add(DropdownOption option)
        {
            registeredOptions.Add(option);
        }
    }

    public interface IDropdown
    {
        void Add(DropdownOption option);
    }

    public interface IDropdownOptionProvider : IUnityComponent
    {
        public void OnDropdownOptionsCreate(IDropdown dropdown);
    }
}