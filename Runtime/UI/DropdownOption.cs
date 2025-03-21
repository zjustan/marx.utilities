using System;

namespace Marx.Utilities
{
    public class DropdownOption
    {
        public string Text;
        public bool Enabled = true;
        public bool Active = false;
        public Action OnClick;
    }
}
