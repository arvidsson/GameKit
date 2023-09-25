using UnityEngine;

namespace UnityEngine
{
    public static class FlagsHelper
    {
        // bool susanIsIncluded = FlagsHelper.IsSet(names, Names.Susan);
        public static bool IsSet<T>(T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;
            return (flagsValue & flagValue) != 0;
        }

        // FlagsHelper.Set(ref names, Names.Karen);
        public static void Set<T>(ref T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue | flagValue);
        }

        // FlagsHelper.Unset(ref names, Names.Susan);
        public static void Unset<T>(ref T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue & (~flagValue));
        }
    }
}
