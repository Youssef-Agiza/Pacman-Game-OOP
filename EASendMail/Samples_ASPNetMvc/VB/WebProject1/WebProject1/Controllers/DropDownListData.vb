Imports EASendMail

Public Class DropDownListData
    Public Shared Function PortList(ByVal port As Integer) As SelectList
        Return New SelectList(New List(Of SelectListItem) From {
            New SelectListItem With {
                .Text = "25",
                .Value = "25"
            },
            New SelectListItem With {
                .Text = "587",
                .Value = "587"
            },
            New SelectListItem With {
                .Text = "465",
                .Value = "465"
            }
        }, "Value", "Text", port.ToString())
    End Function

    Public Shared Function OauthList(ByVal oauthType As Integer) As SelectList
        Return New SelectList(New List(Of SelectListItem) From {
            New SelectListItem With {
                .Text = "Gmail Oauth + SMTP",
                .Value = "0"
            },
            New SelectListItem With {
                .Text = "Hotmail Oauth + SMTP",
                .Value = "1"
            },
            New SelectListItem With {
                .Text = "Office 365 + EWS",
                .Value = "2"
            },
            New SelectListItem With {
                .Text = "Gmail API + OAUTH",
                .Value = "3"
            }
        }, "Value", "Text", oauthType.ToString())
    End Function

    Public Shared Function ProtocolList(ByVal protocol As ServerProtocol) As SelectList
        Return New SelectList(New List(Of SelectListItem) From {
            New SelectListItem With {
                .Text = "SMTP",
                .Value = "0"
            },
            New SelectListItem With {
                .Text = "EWS - Exchange 2007-2019/Office 365",
                .Value = "1"
            },
            New SelectListItem With {
                .Text = "WebDAV - Exchange 2000/2003",
                .Value = "2"
            }
        }, "Value", "Text", (CInt(protocol)).ToString())
    End Function
End Class
