using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskbarHider
{
    public sealed class Hook : IDisposable
    {
        public delegate void Win32Event(IntPtr hWnd);

        #region Windows API

        private const uint WINEVENT_OUTOFCONTEXT = 0x0000;


        [DllImport("User32.dll", SetLastError = true)]
        private static extern IntPtr SetWinEventHook(
            uint eventMin,
            uint eventMax,
            IntPtr hmodWinEventProc,
            WinEventDelegate lpfnWinEventProc,
            uint idProcess,
            uint idThread,
            uint dwFlags);


        [DllImport("user32.dll")]
        private static extern bool UnhookWinEvent(
            IntPtr hWinEventHook
            );

        private enum SystemEvents : uint
        {
            EVENT_OBJECT_CREATE = 0x8000,
            EVENT_OBJECT_DESTROY = 0x8001,
            EVENT_SYSTEM_MINIMIZESTART = 0x0016,
            EVENT_SYSTEM_MINIMIZEEND = 0x0017,
            EVENT_SYSTEM_FOREGROUND = 0x0003
        }

        private delegate void WinEventDelegate(
            IntPtr hWinEventHook,
            uint eventType,
            IntPtr hWnd,
            int idObject,
            int idChild,
            uint dwEventThread,
            uint dwmsEventTime);

        #endregion

        public Win32Event OnWindowCreate = delegate { };
        public Win32Event OnWindowDestroy = delegate { };
        public Win32Event OnWindowForegroundChanged = delegate { };
        public Win32Event OnWindowMinimizeEnd = delegate { };
        public Win32Event OnWindowMinimizeStart = delegate { };

        private IntPtr pHook;
        private bool _disposed;


        public Hook(IntPtr hWnd)
        {
            pHook = SetWinEventHook((uint)SystemEvents.EVENT_SYSTEM_FOREGROUND,
                                    (uint)SystemEvents.EVENT_OBJECT_DESTROY,
                                    hWnd,
                                    WinEvent,
                                    0,
                                    0,
                                    WINEVENT_OUTOFCONTEXT
                );
            if (IntPtr.Zero.Equals(pHook))
                throw new Win32Exception();
        }

        public void Dispose()
        {
            Dispose(true);
        }


        private void WinEvent(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild,
                              uint dwEventThread, uint dwmsEventTime)
        {
            switch ((SystemEvents)eventType)
            {
                case SystemEvents.EVENT_OBJECT_DESTROY:
                    OnWindowDestroy(hWnd);
                    break;

                case SystemEvents.EVENT_SYSTEM_FOREGROUND:
                    OnWindowForegroundChanged(hWnd);
                    break;

                case SystemEvents.EVENT_SYSTEM_MINIMIZESTART:
                    OnWindowMinimizeStart(hWnd);
                    break;

                case SystemEvents.EVENT_SYSTEM_MINIMIZEEND:
                    
                    OnWindowMinimizeEnd(hWnd);
                    break;

                case SystemEvents.EVENT_OBJECT_CREATE:
                    OnWindowCreate(hWnd);
                    break;
            }
        }


        ~Hook()
        {
            Dispose(false);
        }

        private void Dispose(bool manual)
        {
            if (_disposed)
                return;
            if (!IntPtr.Zero.Equals(pHook))
                UnhookWinEvent(pHook);

            pHook = IntPtr.Zero;
            OnWindowCreate = null;
            OnWindowDestroy = null;
            OnWindowForegroundChanged = null;
            OnWindowMinimizeStart = null;
            OnWindowMinimizeEnd = null;
            _disposed = true;

            if (manual)
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}
