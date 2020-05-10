Imports System.Data.Entity

Namespace Models
    
    Public Class WebProject1Context
        Inherits DbContext
    
        ' You can add custom code to this file. Changes will not be overwritten.
        ' 
        ' If you want Entity Framework to drop and regenerate your database
        ' automatically whenever you change your model schema, please use data migrations.
        ' For more information refer to the documentation:
        ' http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        Public Sub New()
            MyBase.New("name=WebProject1Context")
        End Sub

        Public Property DbRecipients As System.Data.Entity.DbSet(Of DbRecipient)

        Public Property DbMailTasks As System.Data.Entity.DbSet(Of DbMailTask)

        Public Property DbRecipientResults As System.Data.Entity.DbSet(Of DbRecipientResult)
    End Class
    
End Namespace
