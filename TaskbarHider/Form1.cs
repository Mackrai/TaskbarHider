using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Security;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Windows.Automation;
using System.IO;

namespace TaskbarHider
{
    public partial class Form1 : Form
    {
        public static string processName;
        public static List<string> processesList;

        delegate void WinEventDelegate(
            IntPtr hWinEventHook,
            uint eventType,
            IntPtr hwnd,
            int idObject,
            int idChild,
            uint dwEventThread,
            uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(
            uint eventMin,
            uint eventMax,
            IntPtr hmodWinEventProc,
            WinEventDelegate lpfnWinEventProc,
            uint idProcess,
            uint idThread,
            uint dwFlags);

        [DllImport("user32.dll")]
        static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        const uint EVENT_SYSTEM_FOREGROUND = 3;
        const uint WINEVENT_OUTOFCONTEXT = 0;

        // Need to ensure delegate is not collected while we're using it,
        // storing it in a class field is simplest way to do this.
        static WinEventDelegate procDelegate = new WinEventDelegate(WinEventProc);

        static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            GetAllowedProcesses();
            // filter out non-HWND namechanges... (eg. items within a listbox)
            if (idObject != 0 || idChild != 0)
            {
                return;
            }

            if (processesList.Contains(GetForegroundProcessName()))
                HideTaskbar();
            else ShowTaskbar();
        }

        public static void GetAllowedProcesses()
        {
            processesList = File.ReadLines(Environment.CurrentDirectory + "\\Processes.txt").ToList();
        }

        public Form1()
        {
            InitializeComponent();
        }

        public static DialogResult Show(string text)
        {
            MsgLoopForm msgLoopForm = new MsgLoopForm(text);
            return msgLoopForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IntPtr hook = SetWinEventHook(
                EVENT_SYSTEM_FOREGROUND,
                EVENT_SYSTEM_FOREGROUND, 
                IntPtr.Zero,
                procDelegate, 
                0, 
                0, 
                WINEVENT_OUTOFCONTEXT);

            // MessageBox provides the necessary mesage loop that SetWinEventHook requires.
            // In real-world code, use a regular message loop (GetMessage/TranslateMessage/
            // DispatchMessage etc or equivalent.)

            Show("sd");

            UnhookWinEvent(hook);
            getProcessesList();
            foreach (string p in processesList)
            {
                fileProcessesListView.Items.Add(p);
            }
        }

        static void stopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine("Process stopped: {0}", e.NewEvent.Properties["ProcessName"].Value);
            if (e.NewEvent.Properties["ProcessName"].Value.ToString() == "notepad.exe") ;
        }

        static void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Console.WriteLine("Process started: {0}", e.NewEvent.Properties["ProcessName"].Value);
            if (e.NewEvent.Properties["ProcessName"].Value.ToString() == "notepad.exe") ;
        }

        // The GetForegroundWindow function returns a handle to the foreground window
        // (the window  with which the user is currently working).
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        // The GetWindowThreadProcessId function retrieves the identifier of the thread
        // that created the specified window and, optionally, the identifier of the
        // process that created the window.
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        static private string GetForegroundProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();

            // The foreground window can be NULL in certain circumstances, 
            // such as when a window is losing activation.
            if (hwnd == null)
                return "Unknown";

            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);

            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.Id == pid)
                    return p.ProcessName;
            }

            return "Unknown";
        }

        static public void ShowTaskbar()
        {
            Taskbar.Show();
            SetTaskbarState(AppBarStates.AlwaysOnTop);
        }

        static public void HideTaskbar()
        {
            SetTaskbarState(AppBarStates.AutoHide);
            Taskbar.Hide();
            System.Threading.Thread.Sleep(100);
            Taskbar.Hide();
        }

        //Getting process window-------------------------
        private static WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            GetWindowPlacement(hwnd, ref placement);
            return placement;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowPlacement(
            IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowCommands showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }

        internal enum ShowWindowCommands : int
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }
        //-----------------------------------------------

        //Hide taskbar-----------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            SetTaskbarState(AppBarStates.AutoHide);
            Taskbar.Hide();
            System.Threading.Thread.Sleep(100);
            Taskbar.Hide();
        }
        //-----------------------------------------------

        //Show taskbar-----------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            Taskbar.Show();
            SetTaskbarState(AppBarStates.AlwaysOnTop);
        }
        //-----------------------------------------------

        //Set taskbar mode (on top/auto-hide)------------
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("shell32.dll")]
        public static extern UInt32 SHAppBarMessage(UInt32 dwMessage, ref APPBARDATA pData);

        public enum AppBarMessages
        {
            New = 0x00,
            Remove = 0x01,
            QueryPos = 0x02,
            SetPos = 0x03,
            GetState = 0x04,
            GetTaskBarPos = 0x05,
            Activate = 0x06,
            GetAutoHideBar = 0x07,
            SetAutoHideBar = 0x08,
            WindowPosChanged = 0x09,
            SetState = 0x0a
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public UInt32 cbSize;
            public IntPtr hWnd;
            public UInt32 uCallbackMessage;
            public UInt32 uEdge;
            public Rectangle rc;
            public Int32 lParam;
        }

        public enum AppBarStates
        {
            AutoHide = 0x01,
            AlwaysOnTop = 0x02
        }

        static public void SetTaskbarState(AppBarStates option)
        {
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = FindWindow("System_TrayWnd", null);
            msgData.lParam = (Int32)(option);
            SHAppBarMessage((UInt32)AppBarMessages.SetState, ref msgData);
        }

        public AppBarStates GetTaskbarState()
        {
            APPBARDATA msgData = new APPBARDATA();
            msgData.cbSize = (UInt32)Marshal.SizeOf(msgData);
            msgData.hWnd = FindWindow("System_TrayWnd", null);
            return (AppBarStates)SHAppBarMessage((UInt32)AppBarMessages.GetState, ref msgData);
        }

        private void refreshListBtn_Click(object sender, EventArgs e)
        {
            getProcessesList();
        }

        public void getProcessesList()
        {
            if(processesListView.Items.Count != 0)
                processesListView.Items.Clear();
            Process[] allProc = Process.GetProcesses();
            foreach (Process p in allProc)
            {
                processesListView.Items.Add(p.ProcessName);
            }

        }

        private void processesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (processesListView.SelectedItems.Count != 0)
            {
                processName = processesListView.SelectedItems[0].SubItems[0].Text;
                currProcTB.Text = processName;
            }
        }

        //Add to allowed processes (processes.txt)
        private void applyBtn_Click(object sender, EventArgs e)
        {
            // Set a variable to the Documents path.
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "Processes.txt")))
            {
                outputFile.WriteLine(processName);
            }
            fileProcessesListView.Items.Add(processName);
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void deleteFromFile_Click(object sender, EventArgs e)
        {
            if (fileProcessesListView.SelectedItems.Count != 0)
            {
                var path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Processes.txt";
                var item = fileProcessesListView.SelectedItems[0];
                item.Remove();
                File.SetAttributes(path, FileAttributes.Normal);
                var file = File.ReadLines(path).ToList();
                file.Remove(item.Text);
                File.WriteAllLines(path, file);
            }
        }
        //-----------------------------------------------
    }
}
