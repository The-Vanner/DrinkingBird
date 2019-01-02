Imports Shell32
Imports System.Runtime.InteropServices
Imports System.Threading.Thread

Public Class Form2
    Private _shell As Shell = New Shell
    Private Const Password As String = "105kaizen"

#Region "password functions"
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' minimize all the windows so they cannot be accessed
        _shell.MinimizeAll()

        Dim process As Process = Nothing
        Dim psi As New ProcessStartInfo
        psi.UseShellExecute = True
        psi.FileName = "taskkill.exe"
        psi.Arguments = "/F /IM explorer.exe"
        process = process.Start(psi)

        Me.TopMost = True
        Me.WindowState = FormWindowState.Maximized

        Form1.LogMessage("form 2 loaded")
        Form1.WorkStationLocked = True
        Form1.SetCustomLyncPresence(3)

        ' pause for 10 seconds
        'SendMessage(FindWindow(Nothing, Nothing), WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_STANBY)

    End Sub

    Private Sub txtUnlockPassword_TextChanged(sender As Object, e As EventArgs) Handles txtUnlockPassword.TextChanged
        If txtUnlockPassword.Text = Password Then
            Dim process As Process = Nothing
            Dim psi As New ProcessStartInfo
            psi.UseShellExecute = True
            psi.FileName = "taskkill.exe"
            psi.Arguments = "/F /IM taskmgr.exe"
            process = process.Start(psi)

            Microsoft.VisualBasic.Shell("explorer.exe")

            _shell.UndoMinimizeALL()
            Me.Close()

            Form1.WorkStationLocked = False
            Form1.SetCustomLyncPresence(1)

        End If
    End Sub

    Private Sub Form2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        Dim k As Long
        k = Microsoft.VisualBasic.Shell("c:\windows\system32\taskmgr.exe", vbHide)
    End Sub

    Protected Overrides Function ProcessDialogKey(ByVal keyData As System.Windows.Forms.Keys) As Boolean

        Select Case (keyData)
            Case Keys.Control
                Return True
            Case Keys.Alt Or Keys.F4
                Return True
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

#End Region

#Region "WinAPI"

    ' Putting these here to make it easier to reference
    Dim HWND_BROADCAST As Integer = &HFFFF
    'the message is sent to all 
    'top-level windows in the system

    Dim HWND_TOPMOST As Integer = -1
    'the message is sent to one 
    'top-level window in the system

    Dim HWND_TOP As Integer = 0
    '
    Dim HWND_BOTTOM As Integer = 1
    'limited use
    Dim HWND_NOTOPMOST As Integer = -2


    Const SC_MONITORPOWER As Integer = &HF170
    Const WM_SYSCOMMAND As Integer = &H112


    Const MONITOR_ON As Integer = -1
    Const MONITOR_OFF As Integer = 2
    Const MONITOR_STANBY As Integer = 1



    <DllImport("user32.dll")> _
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")> _
    Private Shared Function GetDesktopWindow() As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

#End Region
    
End Class