using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject1.Models;
using EASendMail;
using EASendMail.Oauth;

namespace WebProject1.Controllers
{
    public class DropDownListData
    {
        public static SelectList PortList(int port)
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "25", Value = "25"},
                    new SelectListItem {Text = "587", Value = "587" },
                    new SelectListItem {Text = "465", Value = "465"}
                }, 
                "Value", 
                "Text", port.ToString());
        }

        public static SelectList OauthList(int oauthType)
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "Gmail Oauth + SMTP", Value = "0"},
                    new SelectListItem {Text = "Hotmail Oauth + SMTP", Value = "1" },
                    new SelectListItem {Text = "Office 365 + EWS", Value = "2"},
                    new SelectListItem {Text = "Gmail API + OAUTH", Value = "3"}
                },
                "Value",
                "Text", oauthType.ToString());
        }

        public static SelectList ProtocolList(ServerProtocol protocol)
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "SMTP", Value = "0"},
                    new SelectListItem {Text = "EWS - Exchange 2007-2019/Office 365", Value = "1" },
                    new SelectListItem {Text = "WebDAV - Exchange 2000/2003", Value = "2"}
                },
                "Value",
                "Text", ((int)protocol).ToString());
        }

    }
}