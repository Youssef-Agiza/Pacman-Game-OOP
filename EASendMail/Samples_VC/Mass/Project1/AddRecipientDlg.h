#pragma once


// AddRecipientDlg dialog

class AddRecipientDlg : public CDialogEx
{
	DECLARE_DYNAMIC(AddRecipientDlg)

public:
	AddRecipientDlg(CWnd* pParent = NULL);   // standard constructor
	virtual ~AddRecipientDlg();

// Dialog Data
	enum { IDD = IDD_DIALOG1 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
	afx_msg void OnBnClickedOk();

public:
	CString m_name;
	CString m_address;
};
