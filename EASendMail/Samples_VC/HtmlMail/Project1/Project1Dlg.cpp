
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
	, m_subject(_T(""))
	, m_isBodyInited(FALSE)
	, m_bError(FALSE)
	, m_bCancel(FALSE)
	, m_bIdle(TRUE)
	, m_lastErrDescription(_T(""))
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
	DDX_Text(pDX, IDC_EDIT_ATTACHMENT, m_attachments);
	DDX_Control(pDX, IDC_EXPLORER1, htmlEditor);
	DDX_Control(pDX, IDC_COMBO_FONT, m_lstFont);
	DDX_Control(pDX, IDC_COMBO_SIZE, m_lstSize);
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
	ON_CBN_SELCHANGE(IDC_COMBO_FONT, &CProject1Dlg::OnCbnSelchangeComboFont)
	ON_CBN_SELCHANGE(IDC_COMBO_SIZE, &CProject1Dlg::OnCbnSelchangeComboSize)
	ON_BN_CLICKED(IDC_BUTTON_BOLD, &CProject1Dlg::OnBnClickedButtonBold)
	ON_BN_CLICKED(IDC_BUTTON_ITALIC, &CProject1Dlg::OnBnClickedButtonItalic)
	ON_BN_CLICKED(IDC_BUTTON_UNDERLINE, &CProject1Dlg::OnBnClickedButtonUnderline)
	ON_BN_CLICKED(IDC_BUTTON_INSERT, &CProject1Dlg::OnBnClickedButtonInsert)
	ON_BN_CLICKED(IDC_BUTTON_COLOR, &CProject1Dlg::OnBnClickedButtonColor)
	ON_BN_CLICKED(IDCANCELSEND, &CProject1Dlg::OnBnClickedCancelsend)
	ON_EN_CHANGE(IDC_EDIT_FROM, &CProject1Dlg::OnEnChangeEditFrom)
	ON_EN_CHANGE(IDC_EDIT_SERVER, &CProject1Dlg::OnEnChangeEditServer)
	ON_CBN_SELCHANGE(IDC_COMBO_PROTOCOL, &CProject1Dlg::OnCbnSelchangeComboProtocol)
END_MESSAGE_MAP()

BEGIN_EVENTSINK_MAP(CProject1Dlg, CDialog)
	ON_EVENT(CProject1Dlg, IDC_EXPLORER1, 252, CProject1Dlg::NavigateComplete2Explorer1, VTS_DISPATCH VTS_PVARIANT)
END_EVENTSINK_MAP()

// CProject1Dlg message handlers

BOOL CProject1Dlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	m_subject = _T("Test sample");
	_initCharset();

	m_oSmtp.CreateInstance(__uuidof(EASendMailObjLib::Mail));
	if (m_oSmtp == NULL)
	{
		MessageBox(_T("Please make sure you copied EASendMailObj.dll to your exe folder or installed EASendMail ActiveX Object!"), _T("Error"), MB_OK);
		CDialog::OnCancel();
		return FALSE;
	}

	try
	{
		//The license code for EASendMail ActiveX Object,
		//for evaluation usage, please use "TryIt" as the license code.
		m_oSmtp->LicenseCode = _T("TryIt");
	}
	catch (_com_error &ep)
	{
		MessageBox((const TCHAR*)ep.Description(), _T("Error"), MB_OK);
		CDialog::OnCancel();
		return FALSE;
	}

	DispEventAdvise(m_oSmtp.GetInterfacePtr()); //attach event handler

	TCHAR* arFont[] = {
		_T("Choose Font Style ..."),
		_T("Arial"),
		_T("Calibri"),
		_T("Comic Sans MS"),
		_T("Consolas"),
		_T("Courier New"),
		_T("Helvetica"),
		_T("Times New Roman"),
		_T("Tahoma"),
		_T("Verdana"),
		_T("Segoe UI"),
		NULL };

	INT Index = 0;
	while (arFont[Index] != NULL)
	{
		m_lstFont.AddString(arFont[Index]);
		Index++;
	}
	m_lstFont.SetCurSel(0);


	m_lstSize.AddString(_T("Font Size ... "));
	for (int i = 1; i <= 7; i++)
	{
		CString size;
		size.Format(_T("%d"), i);
		m_lstSize.AddString(size);
	}
	m_lstSize.SetCurSel(0);

	htmlEditor.Navigate(_T("about:blank"), NULL, NULL, NULL, NULL);
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();
	spDoc->put_designMode(L"on");
	spDoc.Release();


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
	m_lstProtocol.AddString(_T("Exchange Web Service - 2007 - 2019/Office365"));
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
	CWnd *pWnd = GetDlgItem(IDC_EDIT_ATTACHMENT);
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

	CWnd *pWnd = GetDlgItem(IDC_EDIT_ATTACHMENT);
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


	m_oSmtp->Reset();
	m_oSmtp->Asynchronous = 1;
	m_oSmtp->Charset = m_charset[m_lstCharset.GetCurSel()];
	//m_oSmtp->LogFileName = _T("d:\\smtp.txt");  //enable smtp log


	m_oSmtp->ServerAddr = (LPCTSTR)m_server;
	m_oSmtp->Protocol = m_lstProtocol.GetCurSel();

	m_oSmtp->UserName = _T("");
	m_oSmtp->Password = _T("");

	const int ConnectNormal = 0;
	const int ConnectSSLAuto = 1;
	const int ConnectSTARTTLS = 2;
	const int ConnectDirectSSL = 3;
	const int ConnectTryTLS = 4;

	// Most mordern SMTP servers require SSL/TLS connection now
    // ConnectTryTLS means if server supports SSL/TLS connection, SSL/TLS is used automatically
	m_oSmtp->ConnectType = ConnectTryTLS;

	if (m_server.GetLength() > 0)
	{
		int arPort[] = { 25, 587, 465 };
		m_oSmtp->ServerPort = arPort[m_lstPort.GetCurSel()];

		if (m_isAuth)
		{
			m_oSmtp->UserName = (LPCTSTR)m_user;
			m_oSmtp->Password = (LPCTSTR)m_password;
		}

		if (m_isSSL)
		{
			// Use SSL/TLS based on server port.
			m_oSmtp->ConnectType = ConnectSSLAuto;
		}
	}

	CString name = _T("");
	CString addr = _T("");

	_parseEmailAddr(m_from, name, addr);

	m_oSmtp->From = (LPCTSTR)name;
	m_oSmtp->FromAddr = (LPCTSTR)addr;

	//add digital signature
	m_oSmtp->SignerCert->Unload();
	if (m_isSignMessage)
	{
		if (m_oSmtp->SignerCert->FindSubject((LPCTSTR)addr, CERT_SYSTEM_STORE_CURRENT_USER, _T("my")) == VARIANT_FALSE)
		{
			CString error = _T("Error with signer certificate; ");
			error.Append(m_oSmtp->SignerCert->GetLastError());
			MessageBox(error, _T("Error"), MB_OK | MB_ICONERROR);
			return;
		}

		if (m_oSmtp->SignerCert->HasPrivateKey == VARIANT_FALSE)
		{
			CString error = _T("Signer certificate does not have a private key, it can not be used to sign email.");
			MessageBox(error, _T("Error"), MB_OK | MB_ICONERROR);
			return;
		}
	}

	m_oSmtp->AddRecipientEx((LPCTSTR)m_to, 0); // 0, Normal recipient, 1, cc, 2, bcc
	m_oSmtp->AddRecipientEx((LPCTSTR)m_cc, 0);

	CString rcpts = m_to;
	rcpts.Append(_T(","));
	rcpts.Append(m_cc);
	rcpts = rcpts.Trim(_T(","));

	CStringArray arRcpt;
	_splitAddr(rcpts, arRcpt);


	m_oSmtp->RecipientsCerts->Clear();
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

				m_oSmtp->RecipientsCerts->Add(oCert);
			}
		}
	}

	count = m_attachmentArray.GetSize();
	for (int i = 0; i < count; i++)
	{
		if (m_oSmtp->AddAttachment((LPCTSTR)m_attachmentArray[i]) != 0)
		{
			CString error = _T("Error with attachment; ");
			error.Append(m_oSmtp->GetLastErrDescription());
			MessageBox(error, _T("Error"), MB_OK | MB_ICONERROR);
			return;
		}
	}

	m_oSmtp->Subject = (LPCTSTR)m_subject;



	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();

	CComPtr<IHTMLElement> spElement;
	spDoc->get_body(&spElement);

	BSTR bstr = NULL;
	spElement->get_innerHTML(&bstr);

	CString body = bstr;
	::SysFreeString(bstr);
	spElement.Release();
	spDoc.Release();

	body.Replace(_T("[$from]"), m_from);
	body.Replace(_T("[$to]"), rcpts);
	body.Replace(_T("[$subject]"), m_subject);

	TCHAR szPath[MAX_PATH + 1];
	memset(szPath, 0, sizeof(szPath));
	::GetModuleFileName(NULL, szPath, MAX_PATH);

	LPCTSTR pszBuf = _tcsrchr(szPath, _T('\\'));
	if (pszBuf != NULL)
	{
		szPath[pszBuf - szPath] = _T('\0');
	}

	//imports html with embedded pictures
	m_oSmtp->ImportHtml((LPCTSTR)body, szPath);
	//m_oSmtp->BodyText = (LPCTSTR)body;

	pWnd = GetDlgItem(IDOK);
	pWnd->EnableWindow(FALSE);
	pWnd = GetDlgItem(IDCANCELSEND);
	pWnd->EnableWindow(TRUE);

	if (rcpts.Find(_T(",")) > 0 && m_server.GetLength() == 0)
	{
		//To send email without specified smtp server, we have to send the emails one by one
		// to multiple recipients. That is because every recipient has different smtp server.
		_directSend(m_oSmtp, rcpts);
		pWnd = GetDlgItem(IDOK);
		pWnd->EnableWindow(TRUE);
		pWnd = GetDlgItem(IDCANCELSEND);
		pWnd->EnableWindow(FALSE);
		return;
	}

	_setStatus(_T("Connecting  ..."));
	m_bIdle = FALSE;
	m_bCancel = FALSE;
	m_bError = FALSE;

	CProgressCtrl *pBar = (CProgressCtrl*)GetDlgItem(IDC_PROGRESS1);
	pBar->SetPos(0);

	m_oSmtp->SendMail();

	while (!m_bIdle)
	{
		MSG msg;
		if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
		{
			::TranslateMessage(&msg);
			::DispatchMessage(&msg);
		}
	}

	if (m_bCancel)
	{
		_setStatus(_T("Operation is cancelled by user."));
		MessageBox(_T("Operation is cancelled by user."), _T("Error"), MB_OK | MB_ICONERROR);
	}
	else if (m_bError)
	{
		_setStatus(m_lastErrDescription);
		MessageBox(m_lastErrDescription, _T("Error"), MB_OK | MB_ICONERROR);
	}
	else
	{
		_setStatus(_T("Message was delivered successfully"));
		MessageBox(_T("Message was delivered successfully"), _T("OK"), MB_OK);
	}

	pWnd = GetDlgItem(IDOK);
	pWnd->EnableWindow(TRUE);
	pWnd = GetDlgItem(IDCANCELSEND);
	pWnd->EnableWindow(FALSE);

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

		CString s;
		s.Format(_T("Connecting server for %s ..."), (LPCTSTR)addr);
		_setStatus(s);
		m_bIdle = FALSE;
		m_bCancel = FALSE;
		m_bError = FALSE;
		CProgressCtrl *pBar = (CProgressCtrl*)GetDlgItem(IDC_PROGRESS1);
		pBar->SetPos(0);

		oSmtp->SendMail();

		//wait the asynchronous call finish.
		while (!m_bIdle)
		{
			MSG msg;
			if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
			{
				::TranslateMessage(&msg);
				::DispatchMessage(&msg);
			}
		}

		s = _T("");
		if (m_bCancel)
		{
			s = _T("Operation is canceled by user.");
		}
		else if (m_bError)
		{
			s.Format(_T("Failed to delivery to %s : %s"), (LPCTSTR)addr, (LPCTSTR)m_lastErrDescription);
		}
		else
		{
			s.Format(_T("Message was delivered to %s successfully"), (LPCTSTR)addr);
		}

		MessageBox(s, _T("Information"), MB_OK | MB_ICONINFORMATION);

		if (m_bCancel)
			break;
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
	BOOL	isQuoted = FALSE;

	while ((pszBuf = ::_tcspbrk(pszBuf, _T("\",;"))) != NULL)
	{
		if (*pszBuf == _T('\"'))
		{
			isQuoted = !isQuoted;
			pszBuf++;
			continue;
		}

		if (isQuoted)
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

void CProject1Dlg::NavigateComplete2Explorer1(LPDISPATCH pDisp, VARIANT* URL)
{
	if (m_isBodyInited)
		return;

	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();

	CComPtr<IHTMLElement> spElement;
	spDoc->put_charset(L"utf-8");
	spDoc->get_body(&spElement);
	spDoc.Release();
	if (spElement == NULL)
	{
		spDoc.Release();
		return;
	}

	m_isBodyInited = TRUE;

	CString  s = _T("");
	s.Append(_T("<div>This sample demonstrates how to send html email.</div><div>&nbsp;</div>"));
	s.Append(_T("<div>From: [$from]</div>"));
	s.Append(_T("<div>To: [$to]</div>"));
	s.Append(_T("<div>Subject: [$subject]</div><div>&nbsp;</div>"));
	s.Append(_T("<div>If no sever address was specified, the email will be delivered to the recipient's server directly,"));
	s.Append(_T("However, if you don't have a static IP address, "));
	s.Append(_T("many anti-spam filters will mark it as a junk-email.</div><div>&nbsp;</div>"));
	s.Append(_T("<div>If \"Digitial Signature\" was checked, please make sure you have the certificate for the sender address installed on "));
	s.Append(_T("Local User Certificate Store.</div><div>&nbsp;</div>"));
	s.Append(_T("<div>If \"Encrypt\" was checked, please make sure you have the certificate for recipient address installed on the Local User Certificate Store.</div>"));

	BSTR bstr = s.AllocSysString();
	spElement->put_innerHTML(bstr);
	::SysFreeString(bstr);

	spElement.Release();
	spDoc.Release();
}

void CProject1Dlg::OnCbnSelchangeComboFont()
{
	INT index = m_lstFont.GetCurSel();
	if (index == 0)
		return;

	CString font = _T("");
	m_lstFont.GetLBText(index, font);

	m_lstFont.SetCurSel(0);

	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();

	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	vt.vt = VT_BSTR;
	vt.bstrVal = font.AllocSysString();
	spDoc->execCommand(_T("fontname"), VARIANT_FALSE, vt, &b);
	::VariantClear(&vt);
	spDoc.Release();
	htmlEditor.SetFocus();
}

void CProject1Dlg::OnCbnSelchangeComboSize()
{
	INT index = m_lstSize.GetCurSel();
	if (index == 0)
		return;

	CString font = _T("");
	m_lstSize.GetLBText(index, font);

	m_lstSize.SetCurSel(0);

	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();

	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	vt.vt = VT_BSTR;
	vt.bstrVal = font.AllocSysString();
	spDoc->execCommand(_T("fontsize"), VARIANT_FALSE, vt, &b);
	::VariantClear(&vt);
	spDoc.Release();
	htmlEditor.SetFocus();

}

void CProject1Dlg::OnBnClickedButtonBold()
{
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();

	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit(&vt);
	spDoc->execCommand(_T("Bold"), VARIANT_FALSE, vt, &b);
	::VariantClear(&vt);
	spDoc.Release();
	htmlEditor.SetFocus();
}

void CProject1Dlg::OnBnClickedButtonItalic()
{
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();

	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit(&vt);
	spDoc->execCommand(_T("Italic"), VARIANT_FALSE, vt, &b);
	::VariantClear(&vt);
	spDoc.Release();
	htmlEditor.SetFocus();
}

void CProject1Dlg::OnBnClickedButtonUnderline()
{
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();

	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit(&vt);
	spDoc->execCommand(_T("Underline"), VARIANT_FALSE, vt, &b);
	::VariantClear(&vt);
	spDoc.Release();
	htmlEditor.SetFocus();
}

void CProject1Dlg::OnBnClickedButtonInsert()
{
	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();

	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit(&vt);
	spDoc->execCommand(_T("InsertImage"), VARIANT_TRUE, vt, &b);
	::VariantClear(&vt);
	spDoc.Release();
	htmlEditor.SetFocus();
}

void CProject1Dlg::OnBnClickedButtonColor()
{
	CColorDialog dlg;
	CString s = _T("");
	if (dlg.DoModal() == IDOK)
	{
		COLORREF color = dlg.GetColor();
		s.Format(_T("#%02x%02x%02x"), GetRValue(color), GetGValue(color), GetBValue(color));
	}
	else
	{
		return;
	}

	CComPtr<IHTMLDocument2> spDoc;
	spDoc = (IHTMLDocument2*)htmlEditor.get_Document();

	VARIANT_BOOL b = VARIANT_FALSE;
	VARIANT vt;
	::VariantInit(&vt);
	vt.vt = VT_BSTR;
	vt.bstrVal = s.AllocSysString();
	spDoc->execCommand(_T("ForeColor"), VARIANT_TRUE, vt, &b);
	::VariantClear(&vt);
	spDoc.Release();
	htmlEditor.SetFocus();
}
/////////////////////////////////////////////////////////////////////////////
// OnClosedHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall
CProject1Dlg::OnClosedHandler()
{
	if (!m_bError)
		_setStatus(_T("Completed"));

	m_bIdle = TRUE;
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// OnSendingHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall
CProject1Dlg::OnSendingHandler(long nSent, long nTotalSize)
{
	TCHAR	szBuf[256];
	memset(szBuf, 0, sizeof(szBuf));
	wsprintf(szBuf, _T("Sending %d/%d ..."), nSent, nTotalSize);
	_setStatus(szBuf);

	CProgressCtrl *pBar = (CProgressCtrl*)GetDlgItem(IDC_PROGRESS1);
	if (nSent == 0)
	{
		pBar->SetRange32(0, nTotalSize);
	}

	pBar->SetPos(nSent);

	if (nSent == nTotalSize)
		_setStatus(_T("Disconnecting ... "));


	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// OnErrorHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall
CProject1Dlg::OnErrorHandler(long nErrorCode, BSTR ErrorMessage)
{
	_setStatus(ErrorMessage);
	m_lastErrDescription = ErrorMessage;
	m_bError = TRUE;
	m_bIdle = TRUE;
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// OnConnectedHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall
CProject1Dlg::OnConnectedHandler()
{
	_setStatus(_T("Connected"));
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// OnAuthenticatedHandler
/////////////////////////////////////////////////////////////////////////////
HRESULT __stdcall
CProject1Dlg::OnAuthenticatedHandler()
{
	_setStatus(_T("Authenticated"));
	return S_OK;
}

void
CProject1Dlg::_setStatus(LPCTSTR lpszSrc)
{
	CWnd *pWnd = GetDlgItem(IDC_STATIC_STATUS);
	pWnd->SetWindowText(lpszSrc);
}

void CProject1Dlg::OnBnClickedCancelsend()
{
	m_oSmtp->Terminate();
	m_bCancel = TRUE;
	m_bIdle = TRUE;
	CWnd *pWnd = GetDlgItem(IDCANCELSEND);
	pWnd->EnableWindow(FALSE);
}

void CProject1Dlg::OnEnChangeEditFrom()
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

void CProject1Dlg::OnEnChangeEditServer()
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
