using System;

namespace GameKit.Observables
{

    public class BoundedValue<T> where T : IComparable<T>, IConvertible
    {
        T current;
        T max;

        public Action<T, T> OnValueChanged { get; set; } = delegate { };

        public T Current
        {
            get => current;
            set
            {
                T clampedValue = Clamp(value, default, max);

                if (!Equals(current, clampedValue))
                {
                    current = clampedValue;
                    OnValueChanged(Current, Max);
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
                    OnValueChanged(Current, Max);
                }
            }
        }

        public BoundedValue(T initialCurrentValue = default, T initialMaxValue = default)
        {
            max = initialMaxValue;
            Current = initialCurrentValue;
        }

        public void Set(T current, T max)
        {
            Max = max;
            Current = current;
        }

        public void SetToMax()
        {
            Current = Max;
        }

        public bool IsAtLeast(T value)
        {
            return value.CompareTo(Current) <= 0;
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