
// Project1Dlg.cpp : implementation file
//

#include "stdafx.h"
#include "Project1.h"
#include "Project1Dlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CProject1Dlg dialog

TCHAR* CProject1Dlg::m_charset[] = {
	_T("windows-1256"),
	_T("iso-8859-4"),
	_T("windows-1257"),
	_T("iso-8859-2"),
	_T("windows-1250"),
	_T("GB18030"),
	_T("gb2312"),
	_T("hz-gb-2312"),
	_T("big5"),
	_T("iso-8859-5"),
	_T("koi8-r"),
	_T("koi8-u"),
	_T("windows-1251"),
	_T("iso-8859-7"),
	_T("windows-1253"),
	_T("windows-1255"),
	_T("iso-2022-jp"),
	_T("ks_c_5601-1987"),
	_T("euc-kr"),
	_T("iso-8859-15"),
	_T("windows-874"),
	_T("iso-8859-9"),
	_T("windows-1254"),
	_T("utf-7"),
	_T("utf-8"),
	_T("windows-1258"),
	_T("iso-8859-1"),
	_T("Windows-1252"),
	NULL
};

TCHAR* CProject1Dlg::m_charsetName[] = {
	_T("Arabic(Windows)"),
	_T("Baltic(ISO)"),
	_T("Baltic(Windows)"),
	_T("Central Euporean(ISO)"),
	_T("Central Euporean(Windows)"),
	_T("Chinese Simplified(GB18030)"),
	_T("Chinese Simplified(GB2312)"),
	_T("Chinese Simplified(HZ)"),
	_T("Chinese Traditional(Big5)"),
	_T("Cyrillic(ISO)"),
	_T("Cyrillic(KOI8-R)"),
	_T("Cyrillic(KOI8-U)"),
	_T("Cyrillic(Windows)"),
	_T("Greek(ISO)"),
	_T("Greek(Windows)"),
	_T("Hebrew(Windows)"),
	_T("Japanese(JIS)"),
	_T("Korean"),
	_T("Korean(EUC)"),
	_T("Latin 9(ISO)"),
	_T("Thai(Windows)"),
	_T("Turkish(ISO)"),
	_T("Turkish(Windows)"),
	_T("Unicode(UTF-7)"),
	_T("Unicode(UTF-8)"),
	_T("Vietnames(Windows)"),
	_T("Western European(ISO)"),
	_T("Western European(Windows)"),
	NULL
};

CProject1Dlg::CProject1Dlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CProject1Dlg::IDD, pParent)
	, m_from(_T(""))
	, m_to(_T(""))
	, m_cc(_T(""))
	, m_isSignMessage(FALSE)
	, m_isEncryptMessage(FALSE)
	, m_server(_T(""))
	, m_isAuth(FALSE)
	, m_user(_T(""))
	, m_password(_T(""))
	, m_isSSL(FALSE)
	, m_attachments(_T(""))
	, m_body(_T(""))
	, m_subject(_T(""))
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CProject1Dlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);

	DDX_Text(pDX, IDC_EDIT_FROM, m_from);
	DDV_MaxChars(pDX, m_from, 255);
	DDX_Text(pDX, IDC_EDIT_TO, m_to);
	DDX_Text(pDX, IDC_EDIT_CC, m_cc);
	DDX_Text(pDX, IDC_EDIT_SUBJECT, m_subject);
	DDX_Check(pDX, IDC_CHECK_SIGN, m_isSignMessage);
	DDX_Check(pDX, IDC_CHECK_ENCRYPT, m_isEncryptMessage);
	DDX_Text(pDX, IDC_EDIT_SERVER, m_server);
	DDV_MaxChars(pDX, m_server, 255);
	DDX_Check(pDX, IDC_CHECK_AUTH, m_isAuth);
	DDX_Text(pDX, IDC_EDIT_USER, m_user);
	DDV_MaxChars(pDX, m_user, 255);
	DDX_Text(pDX, IDC_EDIT_PASSWORD, m_password);
	DDV_MaxChars(pDX, m_password, 255);
	DDX_Check(pDX, IDC_CHECK_SSL, m_isSSL);
	DDX_Control(pDX, IDC_COMBO_ENCODING, m_lstCharset);
	DDX_Text(pDX, IDC_EDIT_ATTACHMENTS, m_attachments);
	DDX_Text(pDX, IDC_EDIT_BODY, m_body);
	DDX_Control(pDX, IDC_COMBO_PROTOCOL, m_lstProtocol);
	DDX_Control(pDX, IDC_COMBO_PORT, m_lstPort);
}

BEGIN_MESSAGE_MAP(CProject1Dlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()

	ON_BN_CLICKED(IDC_CHECK_AUTH, &CProject1Dlg::OnBnClickedCheckAuth)
	ON_BN_CLICKED(IDC_BUTTON_CLEAR, &CProject1Dlg::OnBnClickedButtonClear)
	ON_BN_CLICKED(IDC_BUTTON_ADD, &CProject1Dlg::OnBnClickedButtonAdd)
	ON_BN_CLICKED(IDOK, &CProject1Dlg::OnBnClickedOk)
	ON_EN_CHANGE(IDC_EDIT_FROM, &CProject1Dlg::OnEnChangeFrom)
	ON_EN_CHANGE(IDC_EDIT_SERVER, &CProject1Dlg::OnEnChangeServer)

	ON_CBN_SELCHANGE(IDC_COMBO_PROTOCOL, &CProject1Dlg::OnCbnSelchangeComboProtocol)
END_MESSAGE_MAP()


// CProject1Dlg message handlers

BOOL CProject1Dlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	::CoInitialize(NULL); //initialize activex environment

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	m_body = _T("This sample demonstrates how to send simple email.\r\n\r\n");
	m_body += _T("From: [$from]\r\n");
	m_body += _T("To: [$to]\r\n");
	m_body += _T("Subject: [$subject]\r\n\r\n");
	m_body += _T("If no sever address was specified, the email will be delivered to the recipient's server directly. \r\n");
	m_body += _T("However, if you don't have a static IP address, many anti-spam filters will mark it as a junk-email.");
	m_subject = _T("Test sample");

	_initCharset();
	UpdateData(FALSE);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CProject1Dlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CProject1Dlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void
CProject1Dlg::_initCharset()
{
	INT n = 0;
	while (m_charsetName[n] != NULL)
	{
		m_lstCharset.AddString(m_charsetName[n]);
		n++;
	}

	m_lstCharset.SetCurSel(24);

	m_lstProtocol.AddString(_T("SMTP Protocol - Recommended"));
	m_lstProtocol.AddString(_T("Exchange Web Service - 2007-2019/Office365"));
	m_lstProtocol.AddString(_T("Exchange WebDav - 2000/2003"));

	m_lstProtocol.SetCurSel(0);

	m_lstPort.AddString(_T("25"));
	m_lstPort.AddString(_T("587"));
	m_lstPort.AddString(_T("465"));
	m_lstPort.SetCurSel(0);
}

void CProject1Dlg::OnBnClickedCheckAuth()
{
	CButton* pBtn = (CButton*)GetDlgItem(IDC_CHECK_AUTH);
	BOOL b = (pBtn->GetCheck() == BST_CHECKED);
	CWnd *pWnd = GetDlgItem(IDC_EDIT_USER);
	pWnd->EnableWindow(b);
	pWnd = GetDlgItem(IDC_EDIT_PASSWORD);
	pWnd->EnableWindow(b);

}

void CProject1Dlg::OnBnClickedButtonClear()
{
	m_attachments = _T("");
	m_attachmentArray.RemoveAll();
	CWnd *pWnd = GetDlgItem(IDC_EDIT_ATTACHMENTS);
	pWnd->SetWindowText(_T(""));
}

void CProject1Dlg::OnBnClickedButtonAdd()
{
	CFileDialog *pFileDlg = new CFileDialog(TRUE);
	pFileDlg->m_ofn.Flags |= OFN_ALLOWMULTISELECT;
	pFileDlg->m_ofn.Flags |= OFN_FILEMUSTEXIST;

	m_attachments;
	if (pFileDlg->DoModal() == IDOK)
	{
		POSITION pos = pFileDlg->GetStartPosition();
		while (pos != NULL)
		{
			CString fileName = pFileDlg->GetNextPathName(pos);
			m_attachmentArray.Add(fileName);
			INT nIndex = fileName.ReverseFind('\\');
			if (nIndex != -1)
				fileName = fileName.Mid(nIndex + 1);

			m_attachments += fileName;
			m_attachments += ";";
		}
	}

	CWnd *pWnd = GetDlgItem(IDC_EDIT_ATTACHMENTS);
	pWnd->SetWindowText(m_attachments);

	delete pFileDlg;
}

void CProject1Dlg::OnBnClickedOk()
{
	if (!UpdateData(TRUE))
		return;

	m_from = m_from.Trim();
	m_to = m_to.Trim();
	m_cc = m_cc.Trim();

	CWnd *pWnd = NULL;
	if (m_from.GetLength() == 0)
	{
		MessageBox(_T("Please input From email address!"), _T("Error"), MB_OK | MB_ICONERROR);
		pWnd = GetDlgItem(IDC_EDIT_FROM);
		pWnd->SetFocus();
		return;
	}

	if (m_to.GetLength() == 0 &&
		m_cc.GetLength() == 0)
	{
		MessageBox(_T("Please input To or Cc email addresses, please use comma(,) to separate multiple addresses"), _T("Error"), MB_OK | MB_ICONERROR);
		pWnd = GetDlgItem(IDC_EDIT_TO);
		pWnd->SetFocus();
		return;
	}

	if (m_isAuth)
	{
		if (m_user.GetLength() == 0 ||
			m_password.GetLength() == 0)
		{
			MessageBox(_T("Please input user/password for authentication!"), _T("Error"), MB_OK | MB_ICONERROR);
			pWnd = GetDlgItem(IDC_EDIT_USER);
			pWnd->SetFocus();
			return;
		}
	}

	IMailPtr oSmtp = NULL;
	oSmtp.CreateInstance(__uuidof(EASendMailObjLib::Mail));

	if (oSmtp == NULL)
	{
		MessageBox(_T("Please make sure you copied EASendMailObj.dll to your exe folder or installed EASendMail ActiveX Object!"), _T("Error"), MB_OK);
		return;
	}

	try
	{
		//The license code for EASendMail ActiveX Object,
		//for evaluation usage, please use "TryIt" as the license code.
		oSmtp->LicenseCode = _T("TryIt");
	}
	catch (_com_error &ep)
	{
		MessageBox((const TCHAR*)ep.Description(), _T("Error"), MB_OK);
		return;
	}

	oSmtp->Charset = m_charset[m_lstCharset.GetCurSel()];
	//oSmtp.LogFileName = _T("d:\\smtp.txt");  //enable smtp log

	oSmtp->ServerAddr = (LPCTSTR)m_server;
	int arPort[] = { 25, 587, 465 };
	oSmtp->ServerPort = arPort[m_lstPort.GetCurSel()];

	oSmtp->Protocol = m_lstProtocol.GetCurSel();

	const int ConnectNormal = 0;
	const int ConnectSSLAuto = 1;
	const int ConnectSTARTTLS = 2;
	const int ConnectDirectSSL = 3;
	const int ConnectTryTLS = 4;

	// Most mordern SMTP servers require SSL/TLS connection now
    // ConnectTryTLS means if server supports SSL/TLS connection, SSL/TLS is used automatically
	oSmtp->ConnectType = ConnectTryTLS;

	if (m_server.GetLength() > 0)
	{
		if (m_isAuth)
		{
			oSmtp->UserName = (LPCTSTR)m_user;
			oSmtp->Password = (LPCTSTR)m_password;
		}


		if (m_isSSL)
		{
			// Use SSL/TLS based on server port.
			oSmtp->ConnectType = ConnectSSLAuto;
		}
	}

	CString name = _T("");
	CString addr = _T("");

	_parseEmailAddr(m_from, name, addr);

	oSmtp->From = (LPCTSTR)name;
	oSmtp->FromAddr = (LPCTSTR)addr;

	//add digital signature
	oSmtp->SignerCert->Unload();
	if (m_isSignMessage)
	{
		if (oSmtp->SignerCert->FindSubject((LPCTSTR)addr, CERT_SYSTEM_STORE_CURRENT_USER, _T("my")) == VARIANT_FALSE)
		{
			CString error = _T("Error with signer certificate; ");
			error.Append(oSmtp->SignerCert->GetLastError());
			MessageBox(error, _T("Error"), MB_OK | MB_ICONERROR);
			return;
		}

		if (oSmtp->SignerCert->HasPrivateKey == VARIANT_FALSE)
		{
			CString error = _T("Signer certificate does not have a private key, it can not be used to sign email.");
			MessageBox(error, _T("Error"), MB_OK | MB_ICONERROR);
			return;
		}
	}

	oSmtp->AddRecipientEx((LPCTSTR)m_to, 0); // 0, Normal recipient, 1, cc, 2, bcc
	oSmtp->AddRecipientEx((LPCTSTR)m_cc, 0);

	CString rcpts = m_to;
	rcpts.Append(_T(","));
	rcpts.Append(m_cc);
	rcpts = rcpts.Trim(_T(","));

	CStringArray arRcpt;
	_splitAddr(rcpts, arRcpt);

	oSmtp->RecipientsCerts->Clear();
	//encrypt email, lookup certificate in current user store by email address.
	int count = arRcpt.GetSize();
	if (m_isEncryptMessage)
	{
		for (int i = 0; i < count; i++)
		{
			_parseEmailAddr(arRcpt[i], name, addr);
			if (addr.GetLength() > 0)
			{
				ICertificatePtr oCert = NULL;
				oCert.CreateInstance(__uuidof(EASendMailObjLib::Certificate));
				if (oCert->FindSubject((LPCTSTR)addr, CERT_SYSTEM_STORE_CURRENT_USER, _T("AddressBook")) == VARIANT_FALSE)
				{
					if (oCert->FindSubject((LPCTSTR)addr, CERT_SYSTEM_STORE_CURRENT_USER, _T("my")) == VARIANT_FALSE)
					{
						CString error = _T("Encrypting certificate not found; ");
						error.Append(oCert->GetLastError());
						MessageBox(error, _T("Error"), MB_OK | MB_ICONERROR);
						return;
					}
				}

				oSmtp->RecipientsCerts->Add(oCert);
			}
		}
	}

	count = m_attachmentArray.GetSize();
	for (int i = 0; i < count; i++)
	{
		if (oSmtp->AddAttachment((LPCTSTR)m_attachmentArray[i]) != 0)
		{
			CString error = _T("Error with attachment; ");
			error.Append(oSmtp->GetLastErrDescription());
			MessageBox(error, _T("Error"), MB_OK | MB_ICONERROR);
			return;
		}
	}

	oSmtp->Subject = (LPCTSTR)m_subject;
	CString body = m_body;
	body.Replace(_T("[$from]"), m_from);
	body.Replace(_T("[$to]"), rcpts);
	body.Replace(_T("[$subject]"), m_subject);

	oSmtp->BodyText = (LPCTSTR)body;
	//oSmtp->BodyFormat = 1; //' Using HTML FORMAT to send mail

	pWnd = GetDlgItem(IDOK);
	pWnd->EnableWindow(FALSE);
	pWnd = GetDlgItem(IDCANCEL);
	pWnd->EnableWindow(FALSE);

	if (rcpts.Find(_T(",")) > 0 && m_server.GetLength() == 0)
	{
		//To send email without specified smtp server, we have to send the emails one by one
		// to multiple recipients. That is because every recipient has different smtp server.
		_directSend(oSmtp, rcpts);
		pWnd = GetDlgItem(IDOK);
		pWnd->EnableWindow(TRUE);
		pWnd = GetDlgItem(IDCANCEL);
		pWnd->EnableWindow(TRUE);
		return;
	}

	if (oSmtp->SendMail() == 0)
	{
		MessageBox(_T("Message delivered."), _T("OK"), MB_OK);
	}
	else
	{
		CString error = _T("Failed to delivery email; ");
		error.Append(oSmtp->GetLastErrDescription());
		MessageBox(error, _T("Error"), MB_OK | MB_ICONERROR);
	}

	pWnd = GetDlgItem(IDOK);
	pWnd->EnableWindow(TRUE);
	pWnd = GetDlgItem(IDCANCEL);
	pWnd->EnableWindow(TRUE);
}

void CProject1Dlg::_directSend(IMailPtr &oSmtp, LPCTSTR lpszRcpts)
{
	CStringArray arRcpt;
	_splitAddr(lpszRcpts, arRcpt);
	int count = arRcpt.GetSize();
	for (int i = 0; i < count; i++)
	{
		CString addr = arRcpt[i];
		oSmtp->ClearRecipient();
		oSmtp->AddRecipientEx((LPCTSTR)addr, 0);

		if (oSmtp->SendMail() == 0)
		{
			CString s = _T("Message delivered to: ");
			s.Append(addr);
			s.Append(_T(" successfully!"));
			MessageBox(s, _T("OK"), MB_OK);
		}
		else
		{
			CString s = _T("Failed to delivery to: ");
			s.Append(addr);
			s.Append(_T(": "));
			s.Append(oSmtp->GetLastErrDescription());
			MessageBox(s, _T("Error"), MB_OK | MB_ICONERROR);
		}
	}
}


void
CProject1Dlg::_parseEmailAddr(LPCTSTR lpszSrc, CString &name, CString &addr)
{
	name = _T("");
	addr = _T("");

	if (lpszSrc == NULL)
		return;

	LPCTSTR pszBuf = _tcschr(lpszSrc, _T('<'));
	if (pszBuf == NULL)
	{
		addr = lpszSrc;
	}
	else
	{
		name.Append(lpszSrc, pszBuf - lpszSrc);
		addr.Append(pszBuf);
	}

	name = name.Trim(_T(" \"<>"));
	addr = addr.Trim(_T(" \"<>"));
}

// split multiple addresses to an string array 
void
CProject1Dlg::_splitAddr(LPCTSTR lpszSrc, CStringArray &values)
{
	values.RemoveAll();

	if (lpszSrc == NULL)
		return;

	LPCTSTR	pszStart = lpszSrc;
	LPCTSTR	pszBuf = pszStart;
	BOOL	bQuoted = FALSE;

	while ((pszBuf = ::_tcspbrk(pszBuf, _T("\",;"))) != NULL)
	{
		if (*pszBuf == _T('\"'))
		{
			bQuoted = !bQuoted;
			pszBuf++;
			continue;
		}

		if (bQuoted)
		{
			pszBuf++;
			continue;
		}

		CString s;
		s.Append(pszStart, pszBuf - pszStart);

		s = s.Trim(_T(" ;,\r\n\t"));
		if (s.GetLength() > 0)
		{
			values.Add(s);
		}

		pszBuf++;
		pszStart = pszBuf;
	}

	CString s1;
	s1.Append(pszStart);
	s1 = s1.Trim(_T(" ;,\r\n\t"));
	if (s1.GetLength() > 0)
	{
		values.Add(s1);
	}
}

void CProject1Dlg::OnEnChangeFrom()
{
	UpdateData(TRUE);

	CString name, addr, domain;
	_parseEmailAddr(m_from, name, addr);

	int pos = addr.Find(_T('@'));
	if (pos != -1)
		domain = addr.Mid(pos + 1);

	if (domain.CompareNoCase(_T("hotmail.com")) == 0)
	{
		m_server = _T("smtp.live.com");
		m_user = addr;
	}
	else if (domain.CompareNoCase(_T("gmail.com")) == 0)
	{
		m_server = _T("smtp.gmail.com");
		m_user = addr;
	}
	else if (domain.CompareNoCase(_T("yahoo.com")) == 0)
	{
		m_server = _T("smtp.mail.yahoo.com");
		m_user = addr;
	}
	else if (domain.CompareNoCase(_T("aol.com")) == 0)
	{
		m_server = _T("smtp.aol.com");
		m_user = addr;
	}
	else
	{
		m_user = addr;
	}

	UpdateData(FALSE);
	_changeSettingforWellKnownServer();
}

void CProject1Dlg::_changeSettingforWellKnownServer()
{
	UpdateData(TRUE);

	if (m_server.CompareNoCase(_T("smtp.gmail.com")) == 0 ||
		m_server.CompareNoCase(_T("smtp.live.com")) == 0 ||
		m_server.CompareNoCase(_T("smtp.mail.yahoo.com")) == 0 ||
		m_server.CompareNoCase(_T("smtp.office365.com")) == 0 ||
		m_server.CompareNoCase(_T("smtp.aol.com")) == 0)
	{
		m_lstPort.SetCurSel(1);//587 port, you can also use 25, 465
		m_isAuth = TRUE;
		m_isSSL = TRUE;
		CWnd *pWnd = GetDlgItem(IDC_EDIT_USER);
		pWnd->EnableWindow(TRUE);
		pWnd = GetDlgItem(IDC_EDIT_PASSWORD);
		pWnd->EnableWindow(TRUE);
	}
	
	UpdateData(FALSE);
}

void CProject1Dlg::OnEnChangeServer()
{
	_changeSettingforWellKnownServer();
}

void CProject1Dlg::OnCbnSelchangeComboProtocol()
{
	UpdateData(TRUE);
	// enable SSL and user authentication for EWS by default.
	if (m_lstProtocol.GetCurSel() == 1)
	{
		m_isAuth = TRUE;
		m_isSSL = TRUE;

		CWnd *pWnd = GetDlgItem(IDC_EDIT_USER);
		pWnd->EnableWindow(TRUE);

		pWnd = GetDlgItem(IDC_EDIT_PASSWORD);
		pWnd->EnableWindow(TRUE);
	}

	UpdateData(FALSE);
}
