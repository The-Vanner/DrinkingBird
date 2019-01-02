'******************************************************************************************
' How to Add custom states:
' https://technet.microsoft.com/en-us/library/bb963925(v=office.12).aspx
'
' SDK
' http://www.microsoft.com/en-ca/download/details.aspx?id=18898
' https://msdn.microsoft.com/en-us/library/office/gg421054(v=office.14).aspx 
'
'******************************************************************************************

Imports System.Diagnostics.Eventing.Reader
Imports System.IO
Imports System.Runtime.InteropServices
Imports DrinkingBird.My.Resources
Imports Microsoft.Lync.Model
Imports Microsoft.Win32

Public Class Form1

    Private Declare Sub mouse_event Lib "user32.dll" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Declare Function SetActiveWindow Lib "user32.dll" (ByVal hwnd As Integer) As Integer
    Private _lyncClient As LyncClient
    Private Const Logfile As String = "C:\AaronVan\DrinkingBird\DrinkingBird\bin\Release\DBLog.txt"
    Private ReadOnly _random As New Random()
    Private _rotateStatusCounter As Integer = 0
    Public WorkStationLocked As Boolean

    Private Enum ShowWindowEnum
        Hide = 0
        ShowNormal = 1
        ShowMinimized = 2
        ShowMaximized = 3
        Maximize = 3
        ShowNormalNoActivate = 4
        Show = 5
        Minimize = 6
        ShowMinNoActivate = 7
        ShowNoActivate = 8
        Restore = 9
        ShowDefault = 10
        ForceMinimized = 11
    End Enum


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As EventArgs) Handles MyBase.Load
        AddHandler SystemEvents.SessionSwitch, AddressOf CheckWorkStationLockedUnlocked

        ' set the message in case the background working was saying something
        Label1.Text = Startup_message

        ' start the startup timer
        StartupTimer.Enabled = True
        StartupTimer.Start()

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles StartupTimer.Tick
        ' This timer allows the utility to continue and try to connect to lynch if it isn't signed in (e.g. the app starts prior to lync)

        ' keep calling the lync sign in function, until it's signed in
        If LyncSignedIn() Then
            StartupTimer.Stop()
            StartupTimer.Enabled = False
        End If

    End Sub

    Private Function LyncSignedIn() As Boolean

        ' reset the startup message in case the error message is still displaying
        Label1.Text = Startup_message

        Try
            _lyncClient = LyncClient.GetClient()
        Catch ex As AlreadyInitializedException
            ' just here for good measure: https://msdn.microsoft.com/en-us/library/office/hh243703(v=office.14).aspx
        Catch ex As Exception
            ' I guess lync is not running
            Label1.Text = "Lync Error: " + ex.Message
            Me.Show()
            Return False
        End Try

        Select Case _lyncClient.State
            Case ClientState.Uninitialized
                Label1.Text = Lync_is_not_intialized
                Me.Show()
            Case ClientState.SignedOut
                Label1.Text = Lync_is_not_signed_in
                Me.Show()
            Case ClientState.SignedIn
                ' this function will keep our workstation from falling asleep when we aren't allowed to control such settings.
                ' because this function is called multiple times, we need to check to ensure the timer is not already running.
                If Not Timer1.Enabled Then Timer1.Start()
            Case Else
                Label1.Text = Could_not_start_the_timer
                Me.Show()
        End Select

        Return _lyncClient.State = ClientState.SignedIn

    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        ' move the mouse
        mouse_event(&H800, 0, 0, 1, 0)

        ' set lync statuses if it's available
        Try
            If _lyncClient.State = ClientState.SignedIn Then

                ' Set to focussed (do not disturb)
                TimeBlock()

                If PunchInOutToolStripMenuItem.Checked Or PunchInOutContextMenuItem.Checked Then
                    PunchInPunchOut()
                End If

                If LogStatusToolStripMenuItem.Checked Then
                    LogMessage(_lyncClient.Self.Contact.GetContactInformation(ContactInformationType.Activity))
                End If
            Else
                LogMessage(Lync_is_not_signed_in)
                Label1.Text = Lync_is_not_signed_in
                Me.Show()
            End If

            If RotateAppsToolStripMenuItem.Checked Or RotateAppsContextStripMenuItem.Checked Then
                RotateApplication()
            End If

        Catch ex As Exception
            LogMessage(ex.Message)
            Close()
        End Try

    End Sub

    Public Sub LogMessage(message As String)
        ' send a test message
        Dim sw As StreamWriter
        If Not File.Exists(Logfile) Then
            sw = System.IO.File.CreateText(Logfile)
        Else
            sw = File.AppendText(Logfile)
        End If

        sw.WriteLine(Now.ToString + vbTab + "Log Started...")
        sw.WriteLine(Now.ToString + vbTab + message)

        sw.Close()
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            MenuStrip1.Show()
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Show()
        WindowState = Windows.Forms.FormWindowState.Normal
    End Sub

    Private Sub Form2_SizeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.SizeChanged
        If WindowState = Windows.Forms.FormWindowState.Minimized Then
            Hide()
            NotifyIcon1.ShowBalloonTip(3000)
        End If
    End Sub


#Region "Lync Statuses"

    Private Sub TimeBlock()

        ' This function is to set the status in time blocks over the course of the day to prevent multi-taksing, interuptions
        ' and facilitate focussed productivity.  
        ' The time-block strategy will be as follows:
        ' 7am to 12:00pm: foocus time to get shit done!  Take breaks as needed.
        ' 11:30am: status set to available to allow lunch planning
        ' 12pm to 1pm: lunch/break/errands
        ' 1pm to 3pm: emails, open communications, meetings, afternoon planning, adhoc work efforts.
        ' 3pm to 3:30pm: break
        ' 3:30pm to EOD: emails, communction, administration, and next day planning

        Dim focusbegintime As New DateTime(Now.Year, Now.Month, Now.Day, 7, 0, 0)
        Dim focusendtime As New DateTime(Now.Year, Now.Month, Now.Day, 11, 30, 0)

        If Now >= focusbegintime And Now <= focusendtime Then
            SetCustomLyncPresence(4) ' focussed
        Else
            SetCustomLyncPresence(1) ' I can chat
        End If


    End Sub

    Private Sub PunchInPunchOut()
        Dim morningbeginrange As New DateTime(Now.Year, Now.Month, Now.Day, 7, 0, 0)
        Dim morningendrange As New DateTime(Now.Year, Now.Month, Now.Day, 8, 0, 0)
        Dim evening As New DateTime(Now.Year, Now.Month, Now.Day, 19, 0, 0)

        ' check if the workstation is locked before doing anything because we only want to run
        ' this logic if we are away from the workstation

        If Now >= evening Then
            ' set to away if it's late
            SetLyncPresence(ContactAvailability.Away, "Off Work")
            UncheckMenuItems()
        ElseIf Now >= morningbeginrange And Now <= morningendrange Then
            ' reset menu items
            UncheckMenuItems()

            ' set out presence to leave a message if the workstation is locked
            LeaveAMessageToolStripMenuItem.Checked = True
            LeaveAMessageContextMenuItem.Checked = True

            ' turn rotate and drip back on to show activity
            RotateAppsToolStripMenuItem.Checked = True
            RotateAppsContextStripMenuItem.Checked = True

        End If

    End Sub


    Public Sub RotateCustomLyncStatus()

        ' grab a random number to throw off pattern matching
        ' we must keep the upper value under 10 minutes to prevent the computer from sleeping; however,
        ' we'll use a variable to cycle the statuses so they don't change to quickly

        Const randomLowerValue As Integer = 0
        Const randomUpperValue As Integer = 299999 ' 4.9 mins

        _rotateStatusCounter += 1

        ' check if we just want to leave the status
        If _rotateStatusCounter < 3 Then
            Return
        Else
            ' reset the counter and continue to change the status
            _rotateStatusCounter = 0
        End If

        Dim radomIncrement As Integer = _random.Next(randomLowerValue, randomUpperValue)

        ' this needs to align with what has been setup as custom states in lync
        Select Case _lyncClient.Self.Contact.GetContactInformation(ContactInformationType.Activity)
            Case "I can chat"
                SetLyncPresence(ContactAvailability.TemporarilyAway, "Be right back")
                Timer1.Interval = 300000 + radomIncrement
            Case "Be right back"
                ' set to leave a message
                SetCustomLyncPresence(3)
                Timer1.Interval = 300000 + radomIncrement
            Case "Leave a message"
                ' set to I can chat
                SetCustomLyncPresence(1)
                Timer1.Interval = 10000 ' 10 secs
            Case Else
                SetCustomLyncPresence(3)
        End Select

    End Sub

    Private Function CheckWorkStationLockedUnlocked(ByVal sender As Object, ByVal e As SessionSwitchEventArgs)
        ' This method will allow us to set out lync status to something other than "away" when 
        ' we lock our workstations.

        If e.Reason = SessionSwitchReason.SessionLock Then
            If LeaveAMessageToolStripMenuItem.Checked And LeaveAMessageContextMenuItem.Checked Then
                ' set the status to leave a message
                SetCustomLyncPresence(3)
            ElseIf BeRightBackToolStripMenuItem.Checked And BeRightBackContextMenuItem.Checked Then
                SetLyncPresence(ContactAvailability.TemporarilyAway, "Be right back")
            ElseIf InAnotherMeetingToolStripMenuItem.Checked And InAMeetingContextMenuItem.Checked Then
                SetCustomLyncPresence(2)
            ElseIf DripStatusToolStripMenuItem.Checked And DripStatusContextMenuItem.Checked Then
                RotateCustomLyncStatus()
            Else
                SetLyncPresence(ContactAvailability.TemporarilyAway, "Be right back")
            End If
        Else
            SetCustomLyncPresence(1)
        End If



    End Function


    Public Sub SetCustomLyncPresence(customactivityid As Integer)
        Dim newInformation = New Dictionary(Of PublishableContactInformationType, Object)

        newInformation.Add(PublishableContactInformationType.CustomActivityId, customactivityid)
        _lyncClient.Self.BeginPublishContactInformation(newInformation, AddressOf PublishContactInformationCallback, Nothing)

    End Sub

    Private Sub SetLyncPresence(contactAvailability As Integer, activityid As String)
        Dim newInformation = New Dictionary(Of PublishableContactInformationType, Object)

        newInformation.Add(PublishableContactInformationType.Availability, contactAvailability)
        newInformation.Add(PublishableContactInformationType.ActivityId, activityid)
        _lyncClient.Self.BeginPublishContactInformation(newInformation, AddressOf PublishContactInformationCallback, Nothing)

    End Sub


    Private Sub PublishContactInformationCallback(result As IAsyncResult)
        _lyncClient.Self.EndPublishContactInformation(result)
    End Sub

#End Region

#Region "Rotate Applications"

    Private Sub RotateApplication()
        Dim processlist As Process() = Process.GetProcesses()
        Dim processhandles As New List(Of IntPtr)

        For Each p As Process In processlist
            If Not String.IsNullOrEmpty(p.MainWindowTitle) Then
                processhandles.Add(p.MainWindowHandle)
            End If
        Next

        ' use the random number generator to choose the processname
        Dim randomprocesshandle As IntPtr = processhandles(_random.Next(0, processhandles.Count))
        BringWindowToFront(randomprocesshandle)

    End Sub

    <DllImport("user32.dll")> _
    Private Shared Function ShowWindow(hWnd As IntPtr, flags As ShowWindowEnum) As <System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)> Boolean

    End Function

    <DllImport("user32.dll")> _
    Public Shared Function SetForegroundWindow(hwnd As IntPtr) As Integer

    End Function

    Public Sub BringWindowToFront(processhandle As IntPtr)

        If processhandle = IntPtr.Zero Then
            'the window is hidden so try to restore it before setting focus.
            ShowWindow(processhandle, ShowWindowEnum.Restore)
        End If

        'set user the focus to the window
        SetForegroundWindow(processhandle)
        ShowWindow(processhandle, ShowWindowEnum.Show)

    End Sub
#End Region

#Region "Menu Items"
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Close()
    End Sub

    Private Sub PunchInOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PunchInOutToolStripMenuItem.Click
        SetPunchInPunchOutMenuItems()
    End Sub

    Private Sub PunchInOutContextMenuItem_Click(sender As Object, e As EventArgs) Handles PunchInOutContextMenuItem.Click
        SetPunchInPunchOutMenuItems()
    End Sub

    Private Sub DripStatusToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DripStatusToolStripMenuItem.Click
        SetDripStatusMenuItems()
    End Sub

    Private Sub DripStatusToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DripStatusContextMenuItem.Click
        SetDripStatusMenuItems()
    End Sub

    Private Sub LeaveAMessageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeaveAMessageToolStripMenuItem.Click
        SetLeaveAMessageMenuItems()
    End Sub

    Private Sub LeaveAMessageContextMenuItem_Click(sender As Object, e As EventArgs) Handles LeaveAMessageContextMenuItem.Click
        SetLeaveAMessageMenuItems()
    End Sub

    Private Sub BeRightBackContextMenuItem_Click(sender As Object, e As EventArgs) Handles BeRightBackContextMenuItem.Click
        SetBeRightBackMenuItems()
    End Sub

    Private Sub BeRightBackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BeRightBackToolStripMenuItem.Click
        SetBeRightBackMenuItems()
    End Sub

    Private Sub InAMeetingContextMenuItem_Click(sender As Object, e As EventArgs) Handles InAMeetingContextMenuItem.Click
        SetInAMeetingMenuItems()
    End Sub

    Private Sub InAnotherMeetingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InAnotherMeetingToolStripMenuItem.Click
        SetInAMeetingMenuItems()
    End Sub

    Private Sub RotateAppsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RotateAppsToolStripMenuItem.Click
        SetRotateAppsMenuItems()
    End Sub

    Private Sub RotateAppsContextStripMenuItem_Click(sender As Object, e As EventArgs) Handles RotateAppsContextStripMenuItem.Click
        SetRotateAppsMenuItems()
    End Sub

    Private Sub SetDripStatusMenuItems()
        If LyncSignedIn() Then
            If DripStatusToolStripMenuItem.Checked OrElse DripStatusContextMenuItem.Checked Then
                DripStatusToolStripMenuItem.Checked = False
                DripStatusContextMenuItem.Checked = False
            Else
                DripStatusToolStripMenuItem.Checked = True
                DripStatusContextMenuItem.Checked = True
                LeaveAMessageToolStripMenuItem.Checked = False
                LeaveAMessageContextMenuItem.Checked = False
                BeRightBackToolStripMenuItem.Checked = False
                BeRightBackContextMenuItem.Checked = False
                InAnotherMeetingToolStripMenuItem.Checked = False
                InAMeetingContextMenuItem.Checked = False
            End If
        End If
    End Sub

    Private Sub SetLeaveAMessageMenuItems()
        If LyncSignedIn() Then
            If LeaveAMessageToolStripMenuItem.Checked OrElse LeaveAMessageContextMenuItem.Checked Then
                LeaveAMessageToolStripMenuItem.Checked = False
                LeaveAMessageContextMenuItem.Checked = False
            Else
                DripStatusToolStripMenuItem.Checked = False
                DripStatusContextMenuItem.Checked = False
                LeaveAMessageToolStripMenuItem.Checked = True
                LeaveAMessageContextMenuItem.Checked = True
                BeRightBackToolStripMenuItem.Checked = False
                BeRightBackContextMenuItem.Checked = False
                InAnotherMeetingToolStripMenuItem.Checked = False
                InAMeetingContextMenuItem.Checked = False
            End If
        End If
    End Sub

    Private Sub SetBeRightBackMenuItems()
        If LyncSignedIn() Then
            If BeRightBackToolStripMenuItem.Checked OrElse BeRightBackContextMenuItem.Checked Then
                BeRightBackToolStripMenuItem.Checked = False
                BeRightBackContextMenuItem.Checked = False
            Else
                DripStatusToolStripMenuItem.Checked = False
                DripStatusContextMenuItem.Checked = False
                LeaveAMessageToolStripMenuItem.Checked = False
                LeaveAMessageContextMenuItem.Checked = False
                BeRightBackToolStripMenuItem.Checked = True
                BeRightBackContextMenuItem.Checked = True
                InAnotherMeetingToolStripMenuItem.Checked = False
                InAMeetingContextMenuItem.Checked = False
            End If
        End If
    End Sub

    Private Sub SetInAMeetingMenuItems()
        If LyncSignedIn() Then
            If InAnotherMeetingToolStripMenuItem.Checked OrElse InAMeetingContextMenuItem.Checked Then
                InAnotherMeetingToolStripMenuItem.Checked = False
                InAMeetingContextMenuItem.Checked = False
            Else
                DripStatusToolStripMenuItem.Checked = False
                DripStatusContextMenuItem.Checked = False
                LeaveAMessageToolStripMenuItem.Checked = False
                LeaveAMessageContextMenuItem.Checked = False
                BeRightBackToolStripMenuItem.Checked = False
                BeRightBackContextMenuItem.Checked = False
                InAnotherMeetingToolStripMenuItem.Checked = True
                InAMeetingContextMenuItem.Checked = True
            End If
        End If
    End Sub

    Private Sub UncheckMenuItems()
        LeaveAMessageToolStripMenuItem.Checked = False
        LeaveAMessageContextMenuItem.Checked = False
        DripStatusToolStripMenuItem.Checked = False
        DripStatusContextMenuItem.Checked = False
        LeaveAMessageToolStripMenuItem.Checked = False
        LeaveAMessageContextMenuItem.Checked = False
        BeRightBackToolStripMenuItem.Checked = False
        BeRightBackContextMenuItem.Checked = False
        InAnotherMeetingToolStripMenuItem.Checked = False
        InAMeetingContextMenuItem.Checked = False
        RotateAppsToolStripMenuItem.Checked = False
        RotateAppsContextStripMenuItem.Checked = False
    End Sub

    Private Sub SetPunchInPunchOutMenuItems()
        If LyncSignedIn() Then
            If PunchInOutToolStripMenuItem.Checked OrElse PunchInOutContextMenuItem.Checked Then
                PunchInOutToolStripMenuItem.Checked = False
                PunchInOutContextMenuItem.Checked = False
            Else
                PunchInOutToolStripMenuItem.Checked = True
                PunchInOutContextMenuItem.Checked = True
            End If
        End If
    End Sub


    Private Sub LogStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogStatusToolStripMenuItem.Click
        If LyncSignedIn() Then
            If LogStatusToolStripMenuItem.Checked Then
                LogStatusToolStripMenuItem.Checked = False
            Else
                LogStatusToolStripMenuItem.Checked = True
            End If
        End If
    End Sub

    Private Sub SetRotateAppsMenuItems()
        If RotateAppsToolStripMenuItem.Checked OrElse RotateAppsContextStripMenuItem.Checked Then
            RotateAppsToolStripMenuItem.Checked = False
            RotateAppsContextStripMenuItem.Checked = False
        Else
            RotateAppsToolStripMenuItem.Checked = True
            RotateAppsContextStripMenuItem.Checked = True
        End If
    End Sub

    Private Sub LockScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LockScreenToolStripMenuItem.Click
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub LockScreenToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles LockScreenToolStripMenuItem1.Click
        Me.Hide()
        Form2.Show()
    End Sub

#End Region

End Class

