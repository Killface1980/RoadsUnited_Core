using System;
using System.Reflection;

namespace RoadsUnited_Core
{
    public static class RedirectionHelper
    {
        /// <summary>
        /// Redirects all calls from method 'from' to method 'to'.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static RedirectCallsState RedirectCalls(MethodInfo from, MethodInfo to)
        {
            // GetFunctionPointer enforces compilation of the method.
            IntPtr fptr1 = from.MethodHandle.GetFunctionPointer();
            IntPtr fptr2 = to.MethodHandle.GetFunctionPointer();
            return PatchJumpTo(fptr1, fptr2);
        }

        public static void RevertRedirect(MethodInfo from, RedirectCallsState state)
        {
            IntPtr fptr1 = from.MethodHandle.GetFunctionPointer();
            RevertJumpTo(fptr1, state);
        }

        /// <summary>
        /// Primitive patching. Inserts a jump to 'target' at 'site'. Works even if both methods'
        /// callers have already been compiled.
        /// </summary>
        /// <param name="site"></param>
        /// <param name="target"></param>
        private static RedirectCallsState PatchJumpTo(IntPtr site, IntPtr target)
        {
            RedirectCallsState state = new RedirectCallsState();

            // R11 is volatile.
            unsafe
            {
                byte* sitePtr = (byte*)site.ToPointer();
                state.a = *sitePtr;
                state.b = *(sitePtr + 1);
                state.c = *(sitePtr + 10);
                state.d = *(sitePtr + 11);
                state.e = *(sitePtr + 12);
                state.f = *((ulong*)(sitePtr + 2));

                *sitePtr = 0x49; // mov r11, target
                *(sitePtr + 1) = 0xBB;
                *((ulong*)(sitePtr + 2)) = (ulong)target.ToInt64();
                *(sitePtr + 10) = 0x41; // jmp r11
                *(sitePtr + 11) = 0xFF;
                *(sitePtr + 12) = 0xE3;
            }

            return state;
        }

        private static void RevertJumpTo(IntPtr site, RedirectCallsState state)
        {
            unsafe
            {
                byte* sitePtr = (byte*)site.ToPointer();
                *sitePtr = state.a; // mov r11, target
                *(sitePtr + 1) = state.b;
                *((ulong*)(sitePtr + 2)) = state.f;
                *(sitePtr + 10) = state.c; // jmp r11
                *(sitePtr + 11) = state.d;
                *(sitePtr + 12) = state.e;
            }
        }

    }
}
