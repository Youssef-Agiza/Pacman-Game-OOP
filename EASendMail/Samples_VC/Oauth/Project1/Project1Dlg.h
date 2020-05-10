
// Project1Dlg.h : header file
//

#pragma once

#include "afxwin.h"
#include "afxcmn.h"
#include "explorer1.h"
#include "OauthDlg.h"

static _ATL_FUNC_INFO OnClosed = { CC_STDCALL, VT_EMPTY, 0 };
static _ATL_FUNC_INFO OnSending = { CC_STDCALL, VT_EMPTY, 2, { VT_I4, VT_I4 } };
static _ATL_FUNC_INFO OnError = { CC_STDCALL, VT_EMPTY, 2, { VT_I4, VT_BSTR } };
static _ATL_FUNC_INFO OnConnected = { CC_STDCALL, VT_EMPTY, 0 };
static _ATL_FUNC_INFO OnAuthenticated = { CC_STDCALL, VT_EMPTY, 0 };


// CProject1Dlg dialog
class CProject1Dlg : public CDialogEx,
	public IDispEventSimpleImpl<IDD_PROJECT1_DIALOG,
	CProject1Dlg, &__uuidof(_IMailEvents)>
{
// Construction
public:
	CProject1Dlg(CWnd* pParent = NULL);	// standard constructor

	BEGIN_SINK_MAP(CProject1Dlg)
		SINK_ENTRY_INFO(IDD_PROJECT1_DIALOG, __uuidof(_IMailEvents), 1, OnClosedHandler, &OnClosed)
		SINK_ENTRY_INFO(IDD_PROJECT1_DIALOG, __uuidof(_IMailEvents), 2, OnSendingHandler, &OnSending)
		SINK_ENTRY_INFO(IDD_PROJECT1_DIALOG, __uuidof(_IMailEvents), 3, OnErrorHandler, &OnError)
		SINK_ENTRY_INFO(IDD_PROJECT1_DIALOG, __uuidof(_IMailEvents), 4, OnConnectedHandler, &OnConnected)
		SINK_ENTRY_INFO(IDD_PROJECT1_DIALOG, __uuidof(_IMailEvents), 5, OnAuthenticatedHandler, &OnAuthenticated)
	END_SINK_MAP()

// Dialog Data
	enum { IDD = IDD_PROJECT1_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()

	afx_msg void OnBnClickedButtonClear();
	afx_msg void OnBnClickedButtonAdd();
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancelsend();

	afx_msg void OnCbnSelchangeComboFont();
	afx_msg void OnCbnSelchangeComboSize();
	afx_msg void OnBnClickedButtonBold();
	afx_msg void OnBnClickedButtonItalic();
	afx_msg void OnBnClickedButtonUnderline();
	afx_msg void OnBnClickedButtonInsert();
	afx_msg void OnBnClickedButtonColor();
	afx_msg void OnBnClickedButtonCleartoken();
	afx_msg void OnCbnSelchangeComboProviders();

	DECLARE_EVENTSINK_MAP()
	void NavigateComplete2Explorer1(LPDISPATCH pDisp, VARIANT* URL);

private:
	HRESULT __stdcall OnClosedHandler();
	HRESULT __stdcall OnSendingHandler(long nSent, long nTotalSize);
	HRESULT __stdcall OnErrorHandler(long nErrorCode, BSTR ErrorMessage);
	HRESULT __stdcall OnConnectedHandler();
	HRESULT __stdcall OnAuthenticatedHandler();

private:
	static void	_parseEmailAddr(LPCTSTR lpszSrc, CString &name, CString &addr);
	static void _splitAddr(LPCTSTR lpszSrc, CStringArray &values);

	void _initCharset();
	void _setStatus(LPCTSTR lpszSrc);

	BOOL _doOauth();

private:
	static TCHAR* m_charset[];
	static TCHAR* m_charsetName[];
	CComboBox m_lstCharset;

	CString m_from;
	CString m_to;
	CString m_cc;
	CString m_subject;

	CString m_attachments;
	CStringArray m_arAtt;

	CExplorer1 htmlEditor;
	BOOL	m_isBodyInited;

	CComboBox m_lstFont;
	CComboBox m_lstSize;

	CString m_server;
	CComboBox m_lstPort;
	CComboBox m_lstProvider;
	CButton m_useHttpListener;

	BOOL m_bError;
	BOOL m_bCancel;
	BOOL m_bIdle;
	CString m_lastErrDescription;

	IMailPtr	m_oSmtp;
	OauthWrapper m_Oauth;

public:
	afx_msg void OnBnClickedCheckListener();
};
