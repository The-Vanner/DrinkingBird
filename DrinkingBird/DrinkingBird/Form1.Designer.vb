<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RotateAppsContextStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DripStatusContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LeaveAMessageContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeRightBackContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InAMeetingContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PunchInOutContextMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LockScreenToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.TricksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RotateAppsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DripStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LocksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LeaveAMessageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeRightBackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InAnotherMeetingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PunchInOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogStatusToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LockScreenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.StartupTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 350000
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipText = "I'm still here..."
        Me.NotifyIcon1.BalloonTipTitle = "Drinking Bird"
        Me.NotifyIcon1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Drinking Bird"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RotateAppsContextStripMenuItem, Me.DripStatusContextMenuItem, Me.LeaveAMessageContextMenuItem, Me.BeRightBackContextMenuItem, Me.InAMeetingContextMenuItem, Me.PunchInOutContextMenuItem, Me.LockScreenToolStripMenuItem1, Me.ExitToolStripMenuItem1})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(163, 180)
        '
        'RotateAppsContextStripMenuItem
        '
        Me.RotateAppsContextStripMenuItem.Name = "RotateAppsContextStripMenuItem"
        Me.RotateAppsContextStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.RotateAppsContextStripMenuItem.Text = "Rotate Apps"
        '
        'DripStatusContextMenuItem
        '
        Me.DripStatusContextMenuItem.Name = "DripStatusContextMenuItem"
        Me.DripStatusContextMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.DripStatusContextMenuItem.Text = "Drip Status"
        '
        'LeaveAMessageContextMenuItem
        '
        Me.LeaveAMessageContextMenuItem.Name = "LeaveAMessageContextMenuItem"
        Me.LeaveAMessageContextMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.LeaveAMessageContextMenuItem.Text = "Leave a Message"
        '
        'BeRightBackContextMenuItem
        '
        Me.BeRightBackContextMenuItem.Name = "BeRightBackContextMenuItem"
        Me.BeRightBackContextMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.BeRightBackContextMenuItem.Text = "Be Right Back"
        '
        'InAMeetingContextMenuItem
        '
        Me.InAMeetingContextMenuItem.Name = "InAMeetingContextMenuItem"
        Me.InAMeetingContextMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.InAMeetingContextMenuItem.Text = "In A Meeting"
        '
        'PunchInOutContextMenuItem
        '
        Me.PunchInOutContextMenuItem.Name = "PunchInOutContextMenuItem"
        Me.PunchInOutContextMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.PunchInOutContextMenuItem.Text = "Punch In/Out"
        '
        'LockScreenToolStripMenuItem1
        '
        Me.LockScreenToolStripMenuItem1.Name = "LockScreenToolStripMenuItem1"
        Me.LockScreenToolStripMenuItem1.Size = New System.Drawing.Size(162, 22)
        Me.LockScreenToolStripMenuItem1.Text = "Lock Screen"
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(162, 22)
        Me.ExitToolStripMenuItem1.Text = "Exit"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Bookman Old Style", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "This is set at runtime"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TricksToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(335, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TricksToolStripMenuItem
        '
        Me.TricksToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RotateAppsToolStripMenuItem, Me.DripStatusToolStripMenuItem, Me.LocksToolStripMenuItem, Me.PunchInOutToolStripMenuItem, Me.LogStatusToolStripMenuItem, Me.LockScreenToolStripMenuItem})
        Me.TricksToolStripMenuItem.Image = Global.DrinkingBird.My.Resources.Resources.Magic_Wand_icon
        Me.TricksToolStripMenuItem.Name = "TricksToolStripMenuItem"
        Me.TricksToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
        Me.TricksToolStripMenuItem.Text = "Tricks"
        '
        'RotateAppsToolStripMenuItem
        '
        Me.RotateAppsToolStripMenuItem.Name = "RotateAppsToolStripMenuItem"
        Me.RotateAppsToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.RotateAppsToolStripMenuItem.Text = "Rotate Apps"
        '
        'DripStatusToolStripMenuItem
        '
        Me.DripStatusToolStripMenuItem.Name = "DripStatusToolStripMenuItem"
        Me.DripStatusToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.DripStatusToolStripMenuItem.Text = "Drip Status"
        '
        'LocksToolStripMenuItem
        '
        Me.LocksToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LeaveAMessageToolStripMenuItem, Me.BeRightBackToolStripMenuItem, Me.InAnotherMeetingToolStripMenuItem})
        Me.LocksToolStripMenuItem.Name = "LocksToolStripMenuItem"
        Me.LocksToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.LocksToolStripMenuItem.Text = "Locks"
        '
        'LeaveAMessageToolStripMenuItem
        '
        Me.LeaveAMessageToolStripMenuItem.Name = "LeaveAMessageToolStripMenuItem"
        Me.LeaveAMessageToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.LeaveAMessageToolStripMenuItem.Text = "Leave a Message"
        '
        'BeRightBackToolStripMenuItem
        '
        Me.BeRightBackToolStripMenuItem.Name = "BeRightBackToolStripMenuItem"
        Me.BeRightBackToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.BeRightBackToolStripMenuItem.Text = "Be Right Back"
        '
        'InAnotherMeetingToolStripMenuItem
        '
        Me.InAnotherMeetingToolStripMenuItem.Name = "InAnotherMeetingToolStripMenuItem"
        Me.InAnotherMeetingToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.InAnotherMeetingToolStripMenuItem.Text = "In A Meeting"
        '
        'PunchInOutToolStripMenuItem
        '
        Me.PunchInOutToolStripMenuItem.Name = "PunchInOutToolStripMenuItem"
        Me.PunchInOutToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.PunchInOutToolStripMenuItem.Text = "Punch In/Out"
        '
        'LogStatusToolStripMenuItem
        '
        Me.LogStatusToolStripMenuItem.Name = "LogStatusToolStripMenuItem"
        Me.LogStatusToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.LogStatusToolStripMenuItem.Text = "Log Status"
        '
        'LockScreenToolStripMenuItem
        '
        Me.LockScreenToolStripMenuItem.Name = "LockScreenToolStripMenuItem"
        Me.LockScreenToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.LockScreenToolStripMenuItem.Text = "Lock Screen"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Image = Global.DrinkingBird.My.Resources.Resources.exit_icon
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(53, 20)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'StartupTimer
        '
        Me.StartupTimer.Interval = 2000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(335, 74)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "DrinkingBird"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents TricksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DripStatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DripStatusContextMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LeaveAMessageContextMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PunchInOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PunchInOutContextMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogStatusToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LocksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LeaveAMessageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeRightBackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InAnotherMeetingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeRightBackContextMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InAMeetingContextMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RotateAppsContextStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RotateAppsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LockScreenToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LockScreenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents StartupTimer As System.Windows.Forms.Timer

End Class
