using System;

namespace GameKit
{
    public static class Bitflags
    {
        public static bool IsSet<T>(T flags, T flag) where T : Enum
        {
            long flagsValue = Convert.ToInt64(flags);
            long flagValue = Convert.ToInt64(flag);
            return (flagsValue & flagValue) != 0;
        }

        public static void Set<T>(ref T flags, T flag) where T : Enum
        {
            long flagsValue = Convert.ToInt64(flags);
            long flagValue = Convert.ToInt64(flag);
            flags = (T)Enum.ToObject(typeof(T), flagsValue | flagValue);
        }

        public static void Unset<T>(ref T flags, T flag) where T : Enum
        {
            long flagsValue = Convert.ToInt64(flags);
            long flagValue = Convert.ToInt64(flag);
            flags = (T)Enum.ToObject(typeof(T), flagsValue & ~flagValue);
        }

        public static void Toggle<T>(ref T flags, T flag) where T : Enum
        {
            long flagsValue = Convert.ToInt64(flags);
            long flagValue = Convert.ToInt64(flag);
            flags = (T)Enum.ToObject(typeof(T), flagsValue ^ flagValue);
        }
    }
}