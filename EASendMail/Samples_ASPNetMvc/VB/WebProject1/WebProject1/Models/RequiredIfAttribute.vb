Imports System.ComponentModel.DataAnnotations

Public Class RequiredIfAttribute
    Inherits ValidationAttribute
    Implements IClientValidatable

    Private Property PropertyName As String
    Private Property DesiredValue As Object
    Private ReadOnly _innerAttribute As RequiredAttribute

    Public Sub New(ByVal myPropertyName As String, ByVal myDesiredvalue As Object)
        PropertyName = myPropertyName
        DesiredValue = myDesiredvalue
        _innerAttribute = New RequiredAttribute()
    End Sub

    Protected Overrides Function IsValid(ByVal value As Object, ByVal context As ValidationContext) As ValidationResult
        Dim dependentValue = context.ObjectInstance.[GetType]().GetProperty(PropertyName).GetValue(context.ObjectInstance, Nothing)

        If dependentValue IsNot Nothing AndAlso dependentValue.ToString() = DesiredValue.ToString() Then

            If Not _innerAttribute.IsValid(value) Then
                Return New ValidationResult(FormatErrorMessage(context.DisplayName), {context.MemberName})
            End If
        End If

        Return ValidationResult.Success
    End Function

    Public Iterator Function GetClientValidationRules(ByVal metadata As ModelMetadata, ByVal context As ControllerContext) As IEnumerable(Of ModelClientValidationRule) Implements IClientValidatable.GetClientValidationRules
        Dim rule = New ModelClientValidationRule With {
            .ErrorMessage = ErrorMessageString,
            .ValidationType = "requiredif"
        }
        rule.ValidationParameters("dependentproperty") = PropertyName
        rule.ValidationParameters("desiredvalue") = If(TypeOf DesiredValue Is Boolean, DesiredValue.ToString().ToLower(), DesiredValue)
        Yield rule
    End Function

End Class
