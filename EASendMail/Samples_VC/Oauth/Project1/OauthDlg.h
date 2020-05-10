#pragma once
#include "OauthWrapper.h"
#include "explorer1.h"
#include <atlbase.h>
#include <atlcom.h>
// GoogleOauthDlg dialog

static _ATL_FUNC_INFO OnHttpListenRequest = { CC_STDCALL, VT_EMPTY, 2, { VT_DISPATCH, VT_BSTR } };
static _ATL_FUNC_INFO OnHttpListenerError = { CC_STDCALL, VT_EMPTY, 2, { VT_DISPATCH, VT_BSTR } };

class OauthDlg : public CDialogEx, public IDispEventSimpleImpl<IDD_DIALOG_OAUTH,
	OauthDlg, &__uuidof(_IHttpListenerEvents)>
{
	DECLARE_DYNAMIC(OauthDlg)

public:
	OauthDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~OauthDlg();

	BEGIN_SINK_MAP(OauthDlg)
		SINK_ENTRY_INFO(IDD_DIALOG_OAUTH, __uuidof(_IHttpListenerEvents), 1, OnRequestHandler, &OnHttpListenRequest)
		SINK_ENTRY_INFO(IDD_DIALOG_OAUTH, __uuidof(_IHttpListenerEvents), 2, OnErrorHandler, &OnHttpListenerError)
	END_SINK_MAP()

	HRESULT __stdcall OnRequestHandler(IDispatch* oSender, BSTR Url);
	HRESULT __stdcall OnErrorHandler(IDispatch* oSender, BSTR ErrorMessage);

// Dialog Data
	enum { IDD = IDD_DIALOG_OAUTH };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	virtual BOOL OnInitDialog();
	DECLARE_MESSAGE_MAP()

public:
	OauthWrapper *pOauth;
	IHttpListenerPtr httpListener;

	static CString _parseUrlParameter(const TCHAR* bstrUrl, const TCHAR* name);

private:
	CExplorer1 OauthBrowser;
	DECLARE_EVENTSINK_MAP()
	void DocumentCompleteExplorerOauth(LPDISPATCH pDisp, VARIANT* URL);
public:
	void NavigateComplete2ExplorerOauth(LPDISPATCH pDisp, VARIANT* URL);
	void TitleChangeExplorerOauth(LPCTSTR Text);
};
