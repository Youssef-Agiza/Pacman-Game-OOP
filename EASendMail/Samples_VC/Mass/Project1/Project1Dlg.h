
// Project1Dlg.h : header file
//

#pragma once

#include <atlbase.h>
#include <atlcom.h>
#include "afxwin.h"
#include "afxcmn.h"
#include "explorer1.h"

static _ATL_FUNC_INFO OnSent = { CC_STDCALL, VT_EMPTY, 6, { VT_I4, VT_BSTR, VT_I4, VT_BSTR, VT_BSTR, VT_BSTR } };
static _ATL_FUNC_INFO OnConnected = { CC_STDCALL, VT_EMPTY, 2, { VT_I4, VT_BSTR } };
static _ATL_FUNC_INFO OnAuthenticated = { CC_STDCALL, VT_EMPTY, 2, { VT_I4, VT_BSTR } };
static _ATL_FUNC_INFO OnSending = { CC_STDCALL, VT_EMPTY, 4, { VT_I4, VT_I4, VT_I4, VT_BSTR } };

// CProject1Dlg dialog
class CProject1Dlg : public CDialogEx,
	public IDispEventSimpleImpl<IDD_PROJECT1_DIALOG,
	CProject1Dlg, &__uuidof(_IFastSenderEvents)>
{
// Construction
public:
	CProject1Dlg(CWnd* pParent = NULL);	// standard constructor

	BEGIN_SINK_MAP(CProject1Dlg)
		SINK_ENTRY_INFO(IDD_PROJECT1_DIALOG, __uuidof(_IFastSenderEvents), 1, OnSentHandler, &OnSent)
		SINK_ENTRY_INFO(IDD_PROJECT1_DIALOG, __uuidof(_IFastSenderEvents), 2, OnConnectedHandler, &OnConnected)
		SINK_ENTRY_INFO(IDD_PROJECT1_DIALOG, __uuidof(_IFastSenderEvents), 3, OnAuthenticatedHandler, &OnAuthenticated)
		SINK_ENTRY_INFO(IDD_PROJECT1_DIALOG, __uuidof(_IFastSenderEvents), 4, OnSendingHandler, &OnSending)
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
	
	afx_msg void OnBnClickedSend();
	afx_msg void OnBnClickedCancel();

	afx_msg void OnBnClickedCheckAuth();
	afx_msg void OnBnClickedButtonClear();
	afx_msg void OnBnClickedButtonAdd();

	afx_msg void OnCbnSelchangeComboFont();
	afx_msg void OnCbnSelchangeComboSize();

	afx_msg void OnBnClickedButtonBold();
	afx_msg void OnBnClickedButtonItalic();
	afx_msg void OnBnClickedButtonUnderline();
	afx_msg void OnBnClickedButtonInsert();
	afx_msg void OnBnClickedButtonColor();

	afx_msg void OnBnClickedButtonAddRcpt();
	afx_msg void OnBnClickedButtonClearRcpt();

	afx_msg void OnEnChangeEditFrom();
	afx_msg void OnEnChangeEditServer();

	DECLARE_MESSAGE_MAP()

private:
	HRESULT __stdcall OnSentHandler(long lRet,
		BSTR ErrDesc,
		long nKey,
		BSTR tParam,
		BSTR senderAddr,
		BSTR Recipients);
	HRESULT __stdcall OnConnectedHandler(long nKey, BSTR tParam);
	HRESULT __stdcall OnAuthenticatedHandler(long nKey, BSTR tParam);
	HRESULT __stdcall OnSendingHandler(long nSent, long nTotalSize, long nKey, BSTR tParam);

	DECLARE_EVENTSINK_MAP()
	void NavigateComplete2Explorer1(LPDISPATCH pDisp, VARIANT* URL);

private:
	static void	_parseEmailAddr(LPCTSTR lpszSrc, CString &name, CString &addr);
	static void _splitAddr(LPCTSTR lpszSrc, CStringArray &values);

	void _initCharset();
	void _setStatusEx(int nIndex, LPCTSTR lpszSrc);
	void _changeSettingforWellKnownServer();

private:
	static TCHAR* m_charset[];
	static TCHAR* m_charsetName[];
	CComboBox m_lstCharset;

	CString m_from;
	CListCtrl m_listTo;
	CString m_subject;
	
	CString m_attachments;
	CStringArray m_arAtt;

	CExplorer1 htmlEditor;
	BOOL	m_isBodyInited;

	CString m_server;
	CComboBox m_lstPort;
	CComboBox m_lstProtocol;

	BOOL m_isAuth;
	CString m_user;
	CString m_password;
	BOOL m_isSSL;

	CComboBox m_lstFont;
	CComboBox m_lstSize;

	IFastSenderPtr m_oFastSender;

	void DoEvents();
	void WaitAllTaskFinished();
	void SubmitTask(INT nIndex);
	
	BOOL m_bCancel;
	INT m_nSubmitted;
	INT	m_nCompleted;
	INT m_nSuccess;
	INT m_nFailed;
	INT m_nTotal;

	int m_nWorkThreads;
public:
	afx_msg void OnCbnSelchangeComboProtocol();
};
