
// Project1Dlg.h : header file
//

#pragma once


// CProject1Dlg dialog
class CProject1Dlg : public CDialogEx
{
// Construction
public:
	CProject1Dlg(CWnd* pParent = NULL);	// standard constructor

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

	afx_msg void OnEnChangeFrom();
	afx_msg void OnEnChangeServer();

	afx_msg void OnBnClickedCheckAuth();
	afx_msg void OnBnClickedButtonClear();
	afx_msg void OnBnClickedButtonAdd();

	afx_msg void OnBnClickedOk();


private:
	static void	_parseEmailAddr(LPCTSTR lpszSrc, CString &name, CString &address);
	static void _splitAddr(LPCTSTR lpszSrc, CStringArray &values);

	void _initCharset();
	void _directSend(IMailPtr &oSmtp, LPCTSTR lpszRcpts);
	void _changeSettingforWellKnownServer();

private:
	static TCHAR* m_charset[];
	static TCHAR* m_charsetName[];
	CComboBox m_lstCharset;

	CString m_from;
	CString m_to;
	CString m_cc;
	CString m_subject;

	CString m_attachments;
	CStringArray m_attachmentArray;

	CString m_body;

	BOOL m_isSignMessage;
	BOOL m_isEncryptMessage;
	
	CString m_server;
	CComboBox m_lstPort;
	CComboBox m_lstProtocol;

	BOOL m_isAuth;
	CString m_user;
	CString m_password;
	BOOL m_isSSL;
public:
	afx_msg void OnCbnSelchangeComboProtocol();
};
