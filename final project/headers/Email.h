#pragma once
#include <iostream>
#include <tchar.h>
#include <tchar.h>
#include <Windows.h>
#include <string>
#include "EASendMailObj.tlh"
using namespace EASendMailObjLib;
using namespace std;



class Email
{
private:
    const int ConnectNormal = 0;
    const int ConnectSSLAuto = 1;
    const int ConnectSTARTTLS = 2;
    const int ConnectDirectSSL = 3;
    const int ConnectTryTLS = 4;
public:
    Email()
    {

    }
	void sendEmail() 
	{

            ::CoInitialize(NULL);

            IMailPtr oSmtp = NULL;
            oSmtp.CreateInstance(__uuidof(EASendMailObjLib::Mail));
            oSmtp->LicenseCode = _T("TryIt");
            oSmtp->FromAddr = _T("sstephenson006@gmail.com");
            oSmtp->AddRecipientEx(_T("john.elgallab@aucegypt.edu"), 0);
            oSmtp->Subject = _T("Pacman project updates");
            oSmtp->BodyText = _T("A player has just finished a game");
            oSmtp->ServerAddr = _T("smtp.gmail.com");
            oSmtp->UserName = _T("sstephenson006@gmail.com");
            oSmtp->Password = _T("SoNSAOKiadsowdj85");
            oSmtp->ConnectType = ConnectTryTLS;
            oSmtp->ServerPort = 465; 
            oSmtp->ConnectType = ConnectSSLAuto;
            _tprintf(_T("Start to send email ...\r\n"));
            if (oSmtp->SendMail() == 0)
            {
                _tprintf(_T("email was sent successfully!\r\n"));
            }
            else
            {
                _tprintf(_T("failed to send email with the following error: %s\r\n"),
                    (const TCHAR*)oSmtp->GetLastErrDescription());
            }
	}

};