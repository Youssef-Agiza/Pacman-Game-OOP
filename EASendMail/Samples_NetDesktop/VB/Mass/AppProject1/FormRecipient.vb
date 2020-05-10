Public Class FormRecipient
    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Return
    End Sub

    Private Sub ButtonOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOK.Click
        TextBoxName.Text = TextBoxName.Text.Trim()
        TextBoxAddress.Text = TextBoxAddress.Text.Trim()

        If TextBoxAddress.Text.Length = 0 Then
            MessageBox.Show(Me, "Please input a valid email address!")
            TextBoxAddress.Focus()
            Return
        End If

        ButtonOK.DialogResult = DialogResult.OK
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class