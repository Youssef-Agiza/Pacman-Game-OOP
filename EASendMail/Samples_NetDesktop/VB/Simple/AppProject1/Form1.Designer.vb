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
    Friend WithEvents components As System.ComponentModel.IContainer

#Region " Windows Form Designer generated code "
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.TextBoxFrom = New System.Windows.Forms.TextBox()
        Me.TextBoxTo = New System.Windows.Forms.TextBox()
        Me.TextBoxCc = New System.Windows.Forms.TextBox()
        Me.TextBoxSubject = New System.Windows.Forms.TextBox()
        Me.GroupBoxServer = New System.Windows.Forms.GroupBox()
        Me.ComboBoxPorts = New System.Windows.Forms.ComboBox()
        Me.ComboBoxProtocols = New System.Windows.Forms.ComboBox()
        Me.CheckBoxAuth = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSsl = New System.Windows.Forms.CheckBox()
        Me.TextBoxPassword = New System.Windows.Forms.TextBox()
        Me.TextBoxUser = New System.Windows.Forms.TextBox()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.Server = New System.Windows.Forms.Label()
        Me.TextBoxServer = New System.Windows.Forms.TextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.TextBoxAttachments = New System.Windows.Forms.TextBox()
        Me.ButtonSend = New System.Windows.Forms.Button()
        Me.ProgressBarSend = New System.Windows.Forms.ProgressBar()
        Me.StatusBarSend = New System.Windows.Forms.StatusBar()
        Me.label9 = New System.Windows.Forms.Label()
        Me.ComboBoxEncoding = New System.Windows.Forms.ComboBox()
        Me.ButtonAddAttachment = New System.Windows.Forms.Button()
        Me.ButtonClearAttachments = New System.Windows.Forms.Button()
        Me.TextBoxBody = New System.Windows.Forms.RichTextBox()
        Me.openFileDialogAttachment = New System.Windows.Forms.OpenFileDialog()
        Me.CheckBoxHtml = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSignature = New System.Windows.Forms.CheckBox()
        Me.CheckBoxEncrypt = New System.Windows.Forms.CheckBox()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.GroupBoxServer.SuspendLayout()
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(15, 15)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(44, 18)
        Me.label1.TabIndex = 0
        Me.label1.Text = "From"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(15, 55)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(26, 18)
        Me.label2.TabIndex = 1
        Me.label2.Text = "To"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(16, 89)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(27, 18)
        Me.label3.TabIndex = 2
        Me.label3.Text = "Cc"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(15, 124)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(57, 18)
        Me.label4.TabIndex = 3
        Me.label4.Text = "Subject"
        '
        'TextBoxFrom
        '
        Me.TextBoxFrom.Location = New System.Drawing.Point(78, 15)
        Me.TextBoxFrom.Name = "TextBoxFrom"
        Me.TextBoxFrom.Size = New System.Drawing.Size(350, 24)
        Me.TextBoxFrom.TabIndex = 1
        '
        'TextBoxTo
        '
        Me.TextBoxTo.Location = New System.Drawing.Point(78, 51)
        Me.TextBoxTo.Name = "TextBoxTo"
        Me.TextBoxTo.Size = New System.Drawing.Size(350, 24)
        Me.TextBoxTo.TabIndex = 2
        '
        'TextBoxCc
        '
        Me.TextBoxCc.Location = New System.Drawing.Point(78, 85)
        Me.TextBoxCc.Name = "TextBoxCc"
        Me.TextBoxCc.Size = New System.Drawing.Size(350, 24)
        Me.TextBoxCc.TabIndex = 3
        '
        'TextBoxSubject
        '
        Me.TextBoxSubject.Location = New System.Drawing.Point(78, 120)
        Me.TextBoxSubject.Name = "TextBoxSubject"
        Me.TextBoxSubject.Size = New System.Drawing.Size(350, 24)
        Me.TextBoxSubject.TabIndex = 4
        Me.TextBoxSubject.Text = "Test subject"
        '
        'GroupBoxServer
        '
        Me.GroupBoxServer.Controls.Add(Me.ComboBoxPorts)
        Me.GroupBoxServer.Controls.Add(Me.ComboBoxProtocols)
        Me.GroupBoxServer.Controls.Add(Me.CheckBoxAuth)
        Me.GroupBoxServer.Controls.Add(Me.CheckBoxSsl)
        Me.GroupBoxServer.Controls.Add(Me.TextBoxPassword)
        Me.GroupBoxServer.Controls.Add(Me.TextBoxUser)
        Me.GroupBoxServer.Controls.Add(Me.label7)
        Me.GroupBoxServer.Controls.Add(Me.label6)
        Me.GroupBoxServer.Controls.Add(Me.Server)
        Me.GroupBoxServer.Controls.Add(Me.TextBoxServer)
        Me.GroupBoxServer.Location = New System.Drawing.Point(451, 2)
        Me.GroupBoxServer.Name = "GroupBoxServer"
        Me.GroupBoxServer.Size = New System.Drawing.Size(333, 251)
        Me.GroupBoxServer.TabIndex = 8
        Me.GroupBoxServer.TabStop = False
        '
        'ComboBoxPorts
        '
        Me.ComboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPorts.FormattingEnabled = True
        Me.ComboBoxPorts.Location = New System.Drawing.Point(262, 17)
        Me.ComboBoxPorts.Name = "ComboBoxPorts"
        Me.ComboBoxPorts.Size = New System.Drawing.Size(62, 26)
        Me.ComboBoxPorts.TabIndex = 16
        '
        'ComboBoxProtocols
        '
        Me.ComboBoxProtocols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxProtocols.FormattingEnabled = True
        Me.ComboBoxProtocols.Location = New System.Drawing.Point(14, 210)
        Me.ComboBoxProtocols.Name = "ComboBoxProtocols"
        Me.ComboBoxProtocols.Size = New System.Drawing.Size(310, 26)
        Me.ComboBoxProtocols.TabIndex = 15
        '
        'CheckBoxAuth
        '
        Me.CheckBoxAuth.Location = New System.Drawing.Point(14, 56)
        Me.CheckBoxAuth.Name = "CheckBoxAuth"
        Me.CheckBoxAuth.Size = New System.Drawing.Size(282, 27)
        Me.CheckBoxAuth.TabIndex = 11
        Me.CheckBoxAuth.Text = "My server requires user authentication"
        '
        'CheckBoxSsl
        '
        Me.CheckBoxSsl.Location = New System.Drawing.Point(14, 172)
        Me.CheckBoxSsl.Name = "CheckBoxSsl"
        Me.CheckBoxSsl.Size = New System.Drawing.Size(215, 33)
        Me.CheckBoxSsl.TabIndex = 14
        Me.CheckBoxSsl.Text = "SSL Connection"
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Location = New System.Drawing.Point(91, 131)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPassword.Size = New System.Drawing.Size(234, 24)
        Me.TextBoxPassword.TabIndex = 13
        '
        'TextBoxUser
        '
        Me.TextBoxUser.Location = New System.Drawing.Point(91, 92)
        Me.TextBoxUser.Name = "TextBoxUser"
        Me.TextBoxUser.Size = New System.Drawing.Size(234, 24)
        Me.TextBoxUser.TabIndex = 12
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(10, 134)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(75, 18)
        Me.label7.TabIndex = 2
        Me.label7.Text = "Password"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(10, 95)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(40, 18)
        Me.label6.TabIndex = 1
        Me.label6.Text = "User"
        '
        'Server
        '
        Me.Server.AutoSize = True
        Me.Server.Location = New System.Drawing.Point(14, 21)
        Me.Server.Name = "Server"
        Me.Server.Size = New System.Drawing.Size(51, 18)
        Me.Server.TabIndex = 0
        Me.Server.Text = "Server"
        '
        'TextBoxServer
        '
        Me.TextBoxServer.Location = New System.Drawing.Point(80, 18)
        Me.TextBoxServer.Name = "TextBoxServer"
        Me.TextBoxServer.Size = New System.Drawing.Size(181, 24)
        Me.TextBoxServer.TabIndex = 10
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(15, 262)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(90, 18)
        Me.label8.TabIndex = 9
        Me.label8.Text = "Attachments"
        '
        'TextBoxAttachments
        '
        Me.TextBoxAttachments.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxAttachments.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.TextBoxAttachments.Location = New System.Drawing.Point(121, 261)
        Me.TextBoxAttachments.Name = "TextBoxAttachments"
        Me.TextBoxAttachments.ReadOnly = True
        Me.TextBoxAttachments.Size = New System.Drawing.Size(533, 24)
        Me.TextBoxAttachments.TabIndex = 6
        '
        'ButtonSend
        '
        Me.ButtonSend.Location = New System.Drawing.Point(568, 495)
        Me.ButtonSend.Name = "ButtonSend"
        Me.ButtonSend.Size = New System.Drawing.Size(103, 28)
        Me.ButtonSend.TabIndex = 15
        Me.ButtonSend.TabStop = False
        Me.ButtonSend.Text = "Send"
        '
        'ProgressBarSend
        '
        Me.ProgressBarSend.Location = New System.Drawing.Point(18, 505)
        Me.ProgressBarSend.Name = "ProgressBarSend"
        Me.ProgressBarSend.Size = New System.Drawing.Size(536, 12)
        Me.ProgressBarSend.TabIndex = 13
        '
        'StatusBarSend
        '
        Me.StatusBarSend.Location = New System.Drawing.Point(0, 546)
        Me.StatusBarSend.Name = "StatusBarSend"
        Me.StatusBarSend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StatusBarSend.Size = New System.Drawing.Size(801, 27)
        Me.StatusBarSend.TabIndex = 14
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(15, 172)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(70, 18)
        Me.label9.TabIndex = 15
        Me.label9.Text = "Encoding"
        '
        'ComboBoxEncoding
        '
        Me.ComboBoxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEncoding.Location = New System.Drawing.Point(92, 170)
        Me.ComboBoxEncoding.Name = "ComboBoxEncoding"
        Me.ComboBoxEncoding.Size = New System.Drawing.Size(232, 26)
        Me.ComboBoxEncoding.TabIndex = 5
        '
        'ButtonAddAttachment
        '
        Me.ButtonAddAttachment.Location = New System.Drawing.Point(666, 260)
        Me.ButtonAddAttachment.Name = "ButtonAddAttachment"
        Me.ButtonAddAttachment.Size = New System.Drawing.Size(47, 28)
        Me.ButtonAddAttachment.TabIndex = 7
        Me.ButtonAddAttachment.Text = "Add"
        '
        'ButtonClearAttachments
        '
        Me.ButtonClearAttachments.Location = New System.Drawing.Point(720, 260)
        Me.ButtonClearAttachments.Name = "ButtonClearAttachments"
        Me.ButtonClearAttachments.Size = New System.Drawing.Size(57, 28)
        Me.ButtonClearAttachments.TabIndex = 8
        Me.ButtonClearAttachments.Text = "Clear"
        '
        'TextBoxBody
        '
        Me.TextBoxBody.Location = New System.Drawing.Point(19, 305)
        Me.TextBoxBody.Name = "TextBoxBody"
        Me.TextBoxBody.Size = New System.Drawing.Size(765, 180)
        Me.TextBoxBody.TabIndex = 14
        Me.TextBoxBody.Text = ""
        '
        'CheckBoxHtml
        '
        Me.CheckBoxHtml.AutoSize = True
        Me.CheckBoxHtml.Location = New System.Drawing.Point(19, 219)
        Me.CheckBoxHtml.Name = "CheckBoxHtml"
        Me.CheckBoxHtml.Size = New System.Drawing.Size(113, 22)
        Me.CheckBoxHtml.TabIndex = 16
        Me.CheckBoxHtml.Text = "Html Format"
        '
        'CheckBoxSignature
        '
        Me.CheckBoxSignature.AutoSize = True
        Me.CheckBoxSignature.Location = New System.Drawing.Point(149, 219)
        Me.CheckBoxSignature.Name = "CheckBoxSignature"
        Me.CheckBoxSignature.Size = New System.Drawing.Size(139, 22)
        Me.CheckBoxSignature.TabIndex = 17
        Me.CheckBoxSignature.Text = "Digitial Signature"
        '
        'CheckBoxEncrypt
        '
        Me.CheckBoxEncrypt.AutoSize = True
        Me.CheckBoxEncrypt.Location = New System.Drawing.Point(309, 219)
        Me.CheckBoxEncrypt.Name = "CheckBoxEncrypt"
        Me.CheckBoxEncrypt.Size = New System.Drawing.Size(80, 22)
        Me.CheckBoxEncrypt.TabIndex = 18
        Me.CheckBoxEncrypt.Text = "Encrypt"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Enabled = False
        Me.ButtonCancel.Location = New System.Drawing.Point(680, 495)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(103, 28)
        Me.ButtonCancel.TabIndex = 19
        Me.ButtonCancel.Text = "Cancel"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(801, 573)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.CheckBoxEncrypt)
        Me.Controls.Add(Me.CheckBoxSignature)
        Me.Controls.Add(Me.CheckBoxHtml)
        Me.Controls.Add(Me.TextBoxBody)
        Me.Controls.Add(Me.ButtonClearAttachments)
        Me.Controls.Add(Me.ButtonAddAttachment)
        Me.Controls.Add(Me.ComboBoxEncoding)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.TextBoxAttachments)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.TextBoxSubject)
        Me.Controls.Add(Me.TextBoxCc)
        Me.Controls.Add(Me.TextBoxTo)
        Me.Controls.Add(Me.TextBoxFrom)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.StatusBarSend)
        Me.Controls.Add(Me.ProgressBarSend)
        Me.Controls.Add(Me.ButtonSend)
        Me.Controls.Add(Me.GroupBoxServer)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBoxServer.ResumeLayout(False)
        Me.GroupBoxServer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxServer As System.Windows.Forms.GroupBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxSsl As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxFrom As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxTo As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxCc As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSubject As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxUser As System.Windows.Forms.TextBox
    Friend WithEvents Server As System.Windows.Forms.Label
    Friend WithEvents TextBoxServer As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAttachments As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSend As System.Windows.Forms.Button
    Friend WithEvents ProgressBarSend As System.Windows.Forms.ProgressBar
    Friend WithEvents StatusBarSend As System.Windows.Forms.StatusBar
    Friend WithEvents CheckBoxAuth As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonAddAttachment As System.Windows.Forms.Button
    Friend WithEvents ButtonClearAttachments As System.Windows.Forms.Button
    Friend WithEvents ComboBoxEncoding As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxBody As System.Windows.Forms.RichTextBox
    Private openFileDialogAttachment As System.Windows.Forms.OpenFileDialog
    Friend WithEvents CheckBoxHtml As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxSignature As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxEncrypt As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ComboBoxProtocols As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxPorts As System.Windows.Forms.ComboBox

End Class
