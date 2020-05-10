// GoogleOauthDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Project1.h"
#include "OauthDlg.h"
#include "afxdialogex.h"

/*
Because Web Browser control is used for OAUTH,
Web browser control uses IE7 rendering mode by default,
it doesn't support latest Google Web Login Page.

You should install IE 10/IE11 (recommended) or later version on your current machine,
and then add/mergin the following registry values to use IE 10 mode.

"Project1.exe" is your executable file name.
In current project, it is "Project1.exe"
If you debug it in VS, please also add "Project1.vshost.exe"

Windows Registry Editor Version 5.00
[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
"Project1.exe"=dword:00002AF9
"Project1.vshost.exe"=dword:00002AF9

[HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION]
"Project1.exe"=dword:00002AF9
"Project1.vshost.exe"=dword:00002AF9

Appendix - Web Browser Control Mode:

11001 (0x2AF9) Internet Explorer 11. Webpages are displayed in IE11 Standards mode, regardless of the !DOCTYPE directive.
11000 (0x2AF8) Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 mode.
10001 (0x2711) Internet Explorer 10. Webpages are displayed in IE10 Standards mode, regardless of the !DOCTYPE directive.
10000 (0x2710) Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode.
9999 (0x270F) Internet Explorer 9. Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
9000 (0x2328) Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.
8888 (0x22B8) Webpages are displayed in IE8 Standards mode, regardless of the !DOCTYPE directive.
8000 (0x1F40) Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.
7000 (0x1B58) Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. This mode is kind of pointless since it's the default.

*/

// GoogleOauthDlg dialog

IMPLEMENT_DYNAMIC(OauthDlg, CDialogEx)

OauthDlg::OauthDlg(CWnd* pParent /*=NULL*/)
: CDialogEx(OauthDlg::IDD, pParent)
{
	httpListener = nullptr;
}

OauthDlg::~OauthDlg()
{
	if (httpListener != nullptr)
	{
		DispEventUnadvise(httpListener.GetInterfacePtr());
		httpListener->Close();
		httpListener.Release();
	}
}

void OauthDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EXPLORER_OAUTH, OauthBrowser);
}


BEGIN_MESSAGE_MAP(OauthDlg, CDialogEx)
END_MESSAGE_MAP()


// GoogleOauthDlg message handlers

BOOL OauthDlg::OnInitDialog()
{
	_ASSERT(pOauth != NULL);

	CDialog::OnInitDialog();

	::CoInitialize(NULL);

	pOauth->AuthorizationCode = _T("");
	OauthBrowser.put_Silent(TRUE);
	
	CString  authUri;

	if (!pOauth->UseHttpListener)
	{
		pOauth->ResetLocalRedirectUri();
		authUri = pOauth->GetFullAuthUri();
		OauthBrowser.Navigate(authUri, NULL, NULL, NULL, NULL);
		return TRUE;
	}

	// Http Listener is Google recommended solution for desktop app, 
	// and MS OAUTH supports it as well, but you need to add http://127.0.0.1 to 
	// Azure portal -> Your app -> Authentication -> Mobile and desktop applications: redirect Uri, please check the following URI.

	httpListener.CreateInstance(__uuidof(EASendMailObjLib::HttpListener));

	// 0 means use a random unused ports.
	if (httpListener->Create(L"127.0.0.1", 0) == VARIANT_FALSE ||
		httpListener->BeginGetRequestUrl() == VARIANT_FALSE)
	{
		OnCancel();
	}

	CString redirectUri;
	redirectUri.Format(_T("http://127.0.0.1:%d"), httpListener->ListenPort);
	pOauth->RedirectUri = redirectUri;

	authUri = pOauth->GetFullAuthUri();
	OauthBrowser.Navigate(authUri, NULL, NULL, NULL, NULL);
	DispEventAdvise(httpListener.GetInterfacePtr());
	
	return TRUE;
}

BEGIN_EVENTSINK_MAP(OauthDlg, CDialog)
	ON_EVENT(OauthDlg, IDC_EXPLORER_OAUTH, 259, OauthDlg::DocumentCompleteExplorerOauth, VTS_DISPATCH VTS_PVARIANT)
	ON_EVENT(OauthDlg, IDC_EXPLORER_OAUTH, 252, OauthDlg::NavigateComplete2ExplorerOauth, VTS_DISPATCH VTS_PVARIANT)
	ON_EVENT(OauthDlg, IDC_EXPLORER_OAUTH, 113, OauthDlg::TitleChangeExplorerOauth, VTS_BSTR)
END_EVENTSINK_MAP()

void OauthDlg::DocumentCompleteExplorerOauth(LPDISPATCH pDisp, VARIANT* URL)
{
	if (pOauth->AuthorizationCode.GetLength() > 0)
	{
		CDialog::OnOK();
		return;
	}
	if (!pOauth->ParseAuthorizationCodeInHtml)
	{
		return;
	}

	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)OauthBrowser.get_Document();

	CComPtr<IHTMLDocument3> spHtml;
	spDoc->QueryInterface(IID_IHTMLDocument3, (void**)&spHtml);

	CComPtr<IHTMLElement> spElement = NULL;
	spHtml->getElementById(L"code", &spElement);
	if (spElement == NULL)
	{
		return;
	}
	CComPtr<IHTMLInputElement> spInput;
	spElement->QueryInterface(IID_IHTMLInputElement, (void**)&spInput);
	if (spInput == NULL)
	{
		return;
	}

	BSTR bstr = NULL;
	spInput->get_value(&bstr);

	pOauth->AuthorizationCode = bstr;
	::SysFreeString(bstr);

	CDialog::OnOK();
}

void OauthDlg::NavigateComplete2ExplorerOauth(LPDISPATCH pDisp, VARIANT* URL)
{
	CString uri = URL->bstrVal;

	CString code = _parseUrlParameter(uri, _T("code"));
	if (code.GetLength() > 0)
	{
		pOauth->AuthorizationCode = code;
		// Close form in DocumentCompleteExplorerOauth event instead of here
		// If close form here, Google OAUTH Page may open a default web browser window.
		return;
	}

	code = _parseUrlParameter(uri, _T("approvalCode"));
	if (code.GetLength() > 0)
	{
		pOauth->AuthorizationCode = code;
		// Close form in DocumentCompleteExplorerOauth event instead of here
		// If close form here, Google OAUTH Page may open a default web browser window.
		return;
	}
}

CString 
OauthDlg::_parseUrlParameter(const TCHAR* bstrUrl, const TCHAR* name)
{
	CString value;
	if (bstrUrl == NULL || name == NULL)
	{
		return value;
	}

	CString uri = bstrUrl;
	int pos = uri.Find(_T('?'));
	if (pos != -1)
	{
		uri = uri.Mid(pos + 1);
	}

	int curPos = 0;
	int nameLength = _tcslen(name);

	CString resToken = uri.Tokenize(_T("&"), curPos);
	while (resToken != _T(""))
	{
		if (_tcsnicmp(resToken, name, nameLength) == 0)
		{
			if (resToken.GetLength() > nameLength && resToken[nameLength] == _T('='))
			{
				value = resToken.Mid(nameLength + 1);
			}
		}

		resToken = uri.Tokenize(_T("&"), curPos);
	};

	curPos = value.Find(_T("#"));
	if (curPos != -1)
	{
		value = value.Mid(0, curPos);
	}

	return value;
}


HRESULT __stdcall
OauthDlg::OnRequestHandler(IDispatch* oSender, BSTR Url)
{
	CString requestUrl = Url;
	CString code = _parseUrlParameter(requestUrl, _T("code"));
	if (code.GetLength() == 0)
	{
		CString error = _T("Error with request url ");
		error += _parseUrlParameter(requestUrl, _T("error"));
		MessageBox(error, 0, MB_OK);
		OnCancel();
		return S_OK;
	}

	httpListener->SendResponse(L"200", "text/html; charset=utf8", "Authroziation Code is returned OK, please close window and return to your app!");
	pOauth->AuthorizationCode = code;
	OauthBrowser.Stop();

	OnOK();
	return S_OK;
}

HRESULT __stdcall
OauthDlg::OnErrorHandler(IDispatch* oSender, BSTR ErrorMessage)
{
	MessageBox(ErrorMessage, 0, MB_OK);
	OnCancel();
	return S_OK;
}

void OauthDlg::TitleChangeExplorerOauth(LPCTSTR Text)
{
	this->SetWindowText(Text);
}
