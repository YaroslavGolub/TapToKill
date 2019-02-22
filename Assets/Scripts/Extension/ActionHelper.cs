using System;

namespace TapToKill.Extensions {
    public static partial class Extension {
        /// Checks if Action can be safely invoked
        public static void SafeInvoke<T>(this Action<T> action, T obj) {
            if(action != null && action.GetInvocationList().Length > 0) {
                action.Invoke(obj);
            }
        }
        /// Checks if Action can be safely invoked
        public static void SafeInvoke(this Action action) {
            if(action != null && action.GetInvocationList().Length > 0) {
                action.Invoke();
            }
        }
        /// Checks if Action can be safely invoked
        public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2) {
            if(action != null && action.GetInvocationList().Length > 0) {
                action.Invoke(arg1, arg2);
            }
        }
        /// Checks if Action can be safely invoked
        public static void SafeInvoke<T1, T2, T3>(this Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3) {
            if(action != null && action.GetInvocationList().Length > 0) {
                action.Invoke(arg1, arg2, arg3);
            }
        }
    }
}
