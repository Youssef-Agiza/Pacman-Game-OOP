<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.TextBoxFrom = New System.Windows.Forms.TextBox()
        Me.TextBoxSubject = New System.Windows.Forms.TextBox()
        Me.GroupBoxServer = New System.Windows.Forms.GroupBox()
        Me.NumericUpDownConnections = New System.Windows.Forms.NumericUpDown()
        Me.CheckBoxTestRecipient = New System.Windows.Forms.CheckBox()
        Me.label3 = New System.Windows.Forms.Label()
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
        Me.label9 = New System.Windows.Forms.Label()
        Me.ComboBoxEncoding = New System.Windows.Forms.ComboBox()
        Me.ButtonAddAttachment = New System.Windows.Forms.Button()
        Me.ButtonClearAttachments = New System.Windows.Forms.Button()
        Me.TextBoxBody = New System.Windows.Forms.RichTextBox()
        Me.attachmentDlg = New System.Windows.Forms.OpenFileDialog()
        Me.ListViewTo = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAddress = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ButtonAddRecipient = New System.Windows.Forms.Button()
        Me.ButtonClearRecipients = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonSimpleSend = New System.Windows.Forms.Button()
        Me.StatusBarSend = New System.Windows.Forms.StatusBar()
        Me.GroupBoxServer.SuspendLayout()
        CType(Me.NumericUpDownConnections, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(14, 10)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(44, 18)
        Me.label1.TabIndex = 0
        Me.label1.Text = "From"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(14, 41)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(26, 18)
        Me.label2.TabIndex = 1
        Me.label2.Text = "To"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(14, 283)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(57, 18)
        Me.label4.TabIndex = 3
        Me.label4.Text = "Subject"
        '
        'TextBoxFrom
        '
        Me.TextBoxFrom.Location = New System.Drawing.Point(75, 10)
        Me.TextBoxFrom.Name = "TextBoxFrom"
        Me.TextBoxFrom.Size = New System.Drawing.Size(382, 24)
        Me.TextBoxFrom.TabIndex = 1
        '
        'TextBoxSubject
        '
        Me.TextBoxSubject.Location = New System.Drawing.Point(75, 279)
        Me.TextBoxSubject.Name = "TextBoxSubject"
        Me.TextBoxSubject.Size = New System.Drawing.Size(382, 24)
        Me.TextBoxSubject.TabIndex = 4
        Me.TextBoxSubject.Text = "Test sample"
        '
        'GroupBoxServer
        '
        Me.GroupBoxServer.Controls.Add(Me.NumericUpDownConnections)
        Me.GroupBoxServer.Controls.Add(Me.CheckBoxTestRecipient)
        Me.GroupBoxServer.Controls.Add(Me.label3)
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
        Me.GroupBoxServer.Location = New System.Drawing.Point(478, 1)
        Me.GroupBoxServer.Name = "GroupBoxServer"
        Me.GroupBoxServer.Size = New System.Drawing.Size(306, 338)
        Me.GroupBoxServer.TabIndex = 8
        Me.GroupBoxServer.TabStop = False
        '
        'NumericUpDownConnections
        '
        Me.NumericUpDownConnections.Increment = New Decimal(New Integer() {4, 0, 0, 0})
        Me.NumericUpDownConnections.Location = New System.Drawing.Point(16, 248)
        Me.NumericUpDownConnections.Maximum = New Decimal(New Integer() {128, 0, 0, 0})
        Me.NumericUpDownConnections.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.NumericUpDownConnections.Name = "NumericUpDownConnections"
        Me.NumericUpDownConnections.Size = New System.Drawing.Size(68, 24)
        Me.NumericUpDownConnections.TabIndex = 22
        Me.NumericUpDownConnections.Value = New Decimal(New Integer() {8, 0, 0, 0})
        '
        'CheckBoxTestRecipient
        '
        Me.CheckBoxTestRecipient.AutoSize = True
        Me.CheckBoxTestRecipient.Location = New System.Drawing.Point(16, 288)
        Me.CheckBoxTestRecipient.Name = "CheckBoxTestRecipient"
        Me.CheckBoxTestRecipient.Size = New System.Drawing.Size(158, 22)
        Me.CheckBoxTestRecipient.TabIndex = 21
        Me.CheckBoxTestRecipient.Text = "Test Email Address"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(88, 250)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(161, 18)
        Me.label3.TabIndex = 19
        Me.label3.Text = "Maximum Connections"
        '
        'ComboBoxPorts
        '
        Me.ComboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPorts.FormattingEnabled = True
        Me.ComboBoxPorts.Location = New System.Drawing.Point(240, 18)
        Me.ComboBoxPorts.Name = "ComboBoxPorts"
        Me.ComboBoxPorts.Size = New System.Drawing.Size(53, 26)
        Me.ComboBoxPorts.TabIndex = 16
        '
        'ComboBoxProtocols
        '
        Me.ComboBoxProtocols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxProtocols.FormattingEnabled = True
        Me.ComboBoxProtocols.Location = New System.Drawing.Point(15, 205)
        Me.ComboBoxProtocols.Name = "ComboBoxProtocols"
        Me.ComboBoxProtocols.Size = New System.Drawing.Size(273, 26)
        Me.ComboBoxProtocols.TabIndex = 15
        '
        'CheckBoxAuth
        '
        Me.CheckBoxAuth.AutoSize = True
        Me.CheckBoxAuth.Location = New System.Drawing.Point(13, 58)
        Me.CheckBoxAuth.Name = "CheckBoxAuth"
        Me.CheckBoxAuth.Size = New System.Drawing.Size(247, 22)
        Me.CheckBoxAuth.TabIndex = 11
        Me.CheckBoxAuth.Text = "My server requires authentication"
        '
        'CheckBoxSsl
        '
        Me.CheckBoxSsl.AutoSize = True
        Me.CheckBoxSsl.Location = New System.Drawing.Point(13, 166)
        Me.CheckBoxSsl.Name = "CheckBoxSsl"
        Me.CheckBoxSsl.Size = New System.Drawing.Size(138, 22)
        Me.CheckBoxSsl.TabIndex = 14
        Me.CheckBoxSsl.Text = "SSL Connection"
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Location = New System.Drawing.Point(85, 126)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPassword.Size = New System.Drawing.Size(204, 24)
        Me.TextBoxPassword.TabIndex = 13
        '
        'TextBoxUser
        '
        Me.TextBoxUser.Location = New System.Drawing.Point(85, 92)
        Me.TextBoxUser.Name = "TextBoxUser"
        Me.TextBoxUser.Size = New System.Drawing.Size(204, 24)
        Me.TextBoxUser.TabIndex = 12
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(9, 130)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(75, 18)
        Me.label7.TabIndex = 2
        Me.label7.Text = "Password"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(9, 92)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(40, 18)
        Me.label6.TabIndex = 1
        Me.label6.Text = "User"
        '
        'Server
        '
        Me.Server.AutoSize = True
        Me.Server.Location = New System.Drawing.Point(9, 21)
        Me.Server.Name = "Server"
        Me.Server.Size = New System.Drawing.Size(51, 18)
        Me.Server.TabIndex = 0
        Me.Server.Text = "Server"
        '
        'TextBoxServer
        '
        Me.TextBoxServer.Location = New System.Drawing.Point(85, 19)
        Me.TextBoxServer.Name = "TextBoxServer"
        Me.TextBoxServer.Size = New System.Drawing.Size(153, 24)
        Me.TextBoxServer.TabIndex = 10
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(14, 359)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(90, 18)
        Me.label8.TabIndex = 9
        Me.label8.Text = "Attachments"
        '
        'TextBoxAttachments
        '
        Me.TextBoxAttachments.BackColor = System.Drawing.SystemColors.Info
        Me.TextBoxAttachments.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.TextBoxAttachments.Location = New System.Drawing.Point(110, 356)
        Me.TextBoxAttachments.Name = "TextBoxAttachments"
        Me.TextBoxAttachments.ReadOnly = True
        Me.TextBoxAttachments.Size = New System.Drawing.Size(537, 24)
        Me.TextBoxAttachments.TabIndex = 6
        '
        'ButtonSend
        '
        Me.ButtonSend.Location = New System.Drawing.Point(411, 525)
        Me.ButtonSend.Name = "ButtonSend"
        Me.ButtonSend.Size = New System.Drawing.Size(121, 28)
        Me.ButtonSend.TabIndex = 15
        Me.ButtonSend.TabStop = False
        Me.ButtonSend.Text = "Send"
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(14, 321)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(70, 18)
        Me.label9.TabIndex = 15
        Me.label9.Text = "Encoding"
        '
        'ComboBoxEncoding
        '
        Me.ComboBoxEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEncoding.Location = New System.Drawing.Point(110, 317)
        Me.ComboBoxEncoding.Name = "ComboBoxEncoding"
        Me.ComboBoxEncoding.Size = New System.Drawing.Size(239, 26)
        Me.ComboBoxEncoding.TabIndex = 5
        '
        'ButtonAddAttachment
        '
        Me.ButtonAddAttachment.Location = New System.Drawing.Point(661, 354)
        Me.ButtonAddAttachment.Name = "ButtonAddAttachment"
        Me.ButtonAddAttachment.Size = New System.Drawing.Size(47, 28)
        Me.ButtonAddAttachment.TabIndex = 7
        Me.ButtonAddAttachment.Text = "Add"
        '
        'ButtonClearAttachments
        '
        Me.ButtonClearAttachments.Location = New System.Drawing.Point(717, 354)
        Me.ButtonClearAttachments.Name = "ButtonClearAttachments"
        Me.ButtonClearAttachments.Size = New System.Drawing.Size(61, 28)
        Me.ButtonClearAttachments.TabIndex = 8
        Me.ButtonClearAttachments.Text = "Clear"
        '
        'TextBoxBody
        '
        Me.TextBoxBody.Location = New System.Drawing.Point(13, 398)
        Me.TextBoxBody.Name = "TextBoxBody"
        Me.TextBoxBody.Size = New System.Drawing.Size(771, 114)
        Me.TextBoxBody.TabIndex = 14
        Me.TextBoxBody.Text = ""
        '
        'ListViewTo
        '
        Me.ListViewTo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colAddress, Me.colStatus})
        Me.ListViewTo.FullRowSelect = True
        Me.ListViewTo.GridLines = True
        Me.ListViewTo.Location = New System.Drawing.Point(76, 41)
        Me.ListViewTo.Name = "ListViewTo"
        Me.ListViewTo.Size = New System.Drawing.Size(381, 227)
        Me.ListViewTo.TabIndex = 19
        Me.ListViewTo.UseCompatibleStateImageBehavior = False
        Me.ListViewTo.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 50
        '
        'colAddress
        '
        Me.colAddress.Text = "Address"
        Me.colAddress.Width = 150
        '
        'colStatus
        '
        Me.colStatus.Text = "Status"
        Me.colStatus.Width = 300
        '
        'ButtonAddRecipient
        '
        Me.ButtonAddRecipient.Location = New System.Drawing.Point(14, 68)
        Me.ButtonAddRecipient.Name = "ButtonAddRecipient"
        Me.ButtonAddRecipient.Size = New System.Drawing.Size(56, 28)
        Me.ButtonAddRecipient.TabIndex = 21
        Me.ButtonAddRecipient.Text = "Add"
        '
        'ButtonClearRecipients
        '
        Me.ButtonClearRecipients.Location = New System.Drawing.Point(14, 107)
        Me.ButtonClearRecipients.Name = "ButtonClearRecipients"
        Me.ButtonClearRecipients.Size = New System.Drawing.Size(56, 28)
        Me.ButtonClearRecipients.TabIndex = 22
        Me.ButtonClearRecipients.Text = "Clear"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Enabled = False
        Me.ButtonCancel.Location = New System.Drawing.Point(672, 525)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(112, 28)
        Me.ButtonCancel.TabIndex = 23
        Me.ButtonCancel.Text = "Cancel"
        '
        'ButtonSimpleSend
        '
        Me.ButtonSimpleSend.Location = New System.Drawing.Point(541, 525)
        Me.ButtonSimpleSend.Name = "ButtonSimpleSend"
        Me.ButtonSimpleSend.Size = New System.Drawing.Size(122, 28)
        Me.ButtonSimpleSend.TabIndex = 24
        Me.ButtonSimpleSend.TabStop = False
        Me.ButtonSimpleSend.Text = "Simple Send"
        '
        'StatusBarSend
        '
        Me.StatusBarSend.Location = New System.Drawing.Point(0, 568)
        Me.StatusBarSend.Name = "StatusBarSend"
        Me.StatusBarSend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StatusBarSend.Size = New System.Drawing.Size(799, 32)
        Me.StatusBarSend.TabIndex = 14
        Me.StatusBarSend.Text = "Ready"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(799, 600)
        Me.Controls.Add(Me.StatusBarSend)
        Me.Controls.Add(Me.ButtonSimpleSend)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonClearRecipients)
        Me.Controls.Add(Me.ButtonAddRecipient)
        Me.Controls.Add(Me.ListViewTo)
        Me.Controls.Add(Me.TextBoxBody)
        Me.Controls.Add(Me.ButtonClearAttachments)
        Me.Controls.Add(Me.ButtonAddAttachment)
        Me.Controls.Add(Me.ComboBoxEncoding)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.TextBoxAttachments)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.TextBoxSubject)
        Me.Controls.Add(Me.TextBoxFrom)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.ButtonSend)
        Me.Controls.Add(Me.GroupBoxServer)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBoxServer.ResumeLayout(False)
        Me.GroupBoxServer.PerformLayout()
        CType(Me.NumericUpDownConnections, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxServer As System.Windows.Forms.GroupBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxSsl As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxFrom As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSubject As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxUser As System.Windows.Forms.TextBox
    Friend WithEvents Server As System.Windows.Forms.Label
    Friend WithEvents TextBoxServer As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxAttachments As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSend As System.Windows.Forms.Button
    Friend WithEvents CheckBoxAuth As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonAddAttachment As System.Windows.Forms.Button
    Friend WithEvents ButtonClearAttachments As System.Windows.Forms.Button
    Friend WithEvents ComboBoxEncoding As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxBody As System.Windows.Forms.RichTextBox
    Friend WithEvents ListViewTo As System.Windows.Forms.ListView
    Friend WithEvents ButtonAddRecipient As System.Windows.Forms.Button
    Friend WithEvents ButtonClearRecipients As System.Windows.Forms.Button
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAddress As System.Windows.Forms.ColumnHeader
    Friend WithEvents colStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonSimpleSend As System.Windows.Forms.Button
    Friend WithEvents ComboBoxProtocols As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxPorts As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxTestRecipient As System.Windows.Forms.CheckBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownConnections As System.Windows.Forms.NumericUpDown
    Friend WithEvents StatusBarSend As System.Windows.Forms.StatusBar

    Private attachmentDlg As System.Windows.Forms.OpenFileDialog
End Class
