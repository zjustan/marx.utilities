using System;
using System.Collections.Generic;
using UnityEngine;

namespace Marx.Utilities
{

    [Serializable]
    public struct SGuid : ISerializationCallbackReceiver, IComparable<SGuid>, IEquatable<SGuid>, IComparable<Guid>, IEquatable<Guid>, IEqualityComparer<SGuid>
    {
        public static SGuid Empty => new(Guid.Empty);

        [SerializeField] private string guidString;

        public Guid Guid => guid;
        private Guid guid;

        public SGuid(Guid guid) : this()
        {
            this.guid = guid;
        }

        public int CompareTo(SGuid obj) => guid.CompareTo(obj.guid);
        public int CompareTo(Guid other) => guid.CompareTo(other);
        public bool Equals(SGuid other) => guid.Equals(other.guid);
        public bool Equals(Guid other) => guid.Equals(other);

        public void OnAfterDeserialize()
        {
            if (!Guid.TryParse(guidString, out guid) || guid == Guid.Empty)
            {
                guid = Guid.NewGuid();
            }
        }

        public void OnBeforeSerialize()
        {
            guidString = guid.ToString();
        }

        public override int GetHashCode()
        {
            return guid.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is SGuid sguid)
            {
                return Equals(sguid);
            }

            if (obj is Guid guid)
            {
                return Equals(guid);
            }
            return false;
        }

        public bool Equals(SGuid x, SGuid y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(SGuid obj)
        {
            return obj.GetHashCode();
        }
    }

}