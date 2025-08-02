using System;

namespace GameKit
{

    public class BoundedValue<T> where T : IComparable<T>, IConvertible
    {
        T current;
        T max;

        public Action<T, T> OnValueChanged { get; private set; } = delegate { };

        public T Current
        {
            get => current;
            set
            {
                T clampedValue = Clamp(value, default, max);

                if (!Equals(current, clampedValue))
                {
                    current = clampedValue;
                    OnValueChanged.Invoke(Current, Max);
                }
            }
        }

        public T Max
        {
            get => max;
            set
            {
                if (!Equals(max, value))
                {
                    max = value;
                    current = Clamp(current, default, max);
                    OnValueChanged.Invoke(Current, Max);
                }
            }
        }

        public BoundedValue(T initialCurrentValue, T initialMaxValue)
        {
            max = initialMaxValue;
            Current = initialCurrentValue;
        }

        public float GetPercentage()
        {
            float current = Convert.ToSingle(this.current);
            float max = Convert.ToSingle(this.max);
            if (max == 0) return 0;
            return (current / max) * 100;
        }

        T Clamp(T value, T min, T max)
        {
            if (value.CompareTo(min) < 0)
            {
                return min;
            }
            else if (value.CompareTo(max) > 0)
            {
                return max;
            }
            return value;
        }
    }

}