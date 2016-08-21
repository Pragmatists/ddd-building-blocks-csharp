using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBuildingBlocks.Domain
{
    public abstract class ValueObject<T>
    {
        protected T Value { get; set; }

        protected ValueObject(T value)
        {
            this.Value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            ValueObject<T> other = obj as ValueObject<T>;

            return this.Value == null ? other.Value == null : this.Value.Equals(other.Value);
        }

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.Value == null ? this.GetType().GetHashCode() : this.Value.GetHashCode();
        }
    }
}
