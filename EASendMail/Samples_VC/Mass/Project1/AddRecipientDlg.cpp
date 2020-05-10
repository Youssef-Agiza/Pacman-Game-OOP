// AddRecipientDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Project1.h"
#include "AddRecipientDlg.h"
#include "afxdialogex.h"


// AddRecipientDlg dialog

IMPLEMENT_DYNAMIC(AddRecipientDlg, CDialogEx)

AddRecipientDlg::AddRecipientDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(AddRecipientDlg::IDD, pParent)
	, m_name(_T(""))
	, m_address(_T(""))
{

}

AddRecipientDlg::~AddRecipientDlg()
{
}

void AddRecipientDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);

	DDX_Text(pDX, IDC_EDIT_NAME, m_name);
	DDV_MaxChars(pDX, m_name, 128);
	DDX_Text(pDX, IDC_EDIT_ADDRESS, m_address);
	DDV_MaxChars(pDX, m_address, 256);
}


BEGIN_MESSAGE_MAP(AddRecipientDlg, CDialogEx)
	ON_BN_CLICKED(IDOK, &AddRecipientDlg::OnBnClickedOk)
END_MESSAGE_MAP()

// AddRecipientDlg message handlers

void AddRecipientDlg::OnBnClickedOk()
{
	if (!UpdateData(TRUE))
		return;

	m_address.Trim();
	if (m_address.GetLength() == 0)
	{
		MessageBox(_T("Please input recipient email address!"));
		return;
	}
	OnOK();
}
