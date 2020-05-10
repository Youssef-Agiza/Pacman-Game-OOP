object Form1: TForm1
  Left = 319
  Top = 129
  Width = 656
  Height = 452
  Caption = 'Form1'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  OnResize = FormResize
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 6
    Top = 10
    Width = 23
    Height = 13
    Caption = 'From'
  end
  object Label2: TLabel
    Left = 6
    Top = 54
    Width = 13
    Height = 13
    Caption = 'To'
  end
  object Label3: TLabel
    Left = 6
    Top = 77
    Width = 13
    Height = 13
    Caption = 'Cc'
  end
  object Label4: TLabel
    Left = 6
    Top = 99
    Width = 36
    Height = 13
    Caption = 'Subject'
  end
  object Label5: TLabel
    Left = 72
    Top = 32
    Width = 260
    Height = 13
    Caption = 'Please separate multiple email addresses with comma(,)'
  end
  object Label9: TLabel
    Left = 6
    Top = 144
    Width = 45
    Height = 13
    Caption = 'Encoding'
  end
  object Label10: TLabel
    Left = 6
    Top = 188
    Width = 59
    Height = 13
    Caption = 'Attachments'
  end
  object textStatus: TLabel
    Left = 8
    Top = 384
    Width = 31
    Height = 13
    Caption = 'Ready'
  end
  object textFrom: TEdit
    Left = 71
    Top = 8
    Width = 281
    Height = 21
    TabOrder = 0
  end
  object textTo: TEdit
    Left = 71
    Top = 50
    Width = 281
    Height = 21
    TabOrder = 1
  end
  object textCc: TEdit
    Left = 71
    Top = 74
    Width = 281
    Height = 21
    TabOrder = 2
  end
  object textSubject: TEdit
    Left = 71
    Top = 100
    Width = 281
    Height = 21
    TabOrder = 3
  end
  object GroupBox1: TGroupBox
    Left = 368
    Top = 0
    Width = 273
    Height = 161
    TabOrder = 4
    object Label6: TLabel
      Left = 9
      Top = 17
      Width = 31
      Height = 13
      Caption = 'Server'
    end
    object Label7: TLabel
      Left = 11
      Top = 56
      Width = 19
      Height = 13
      Caption = 'Port'
    end
    object Label8: TLabel
      Left = 8
      Top = 96
      Width = 39
      Height = 13
      Caption = 'Provider'
    end
    object textServer: TEdit
      Left = 72
      Top = 14
      Width = 193
      Height = 21
      TabOrder = 0
      Text = 'smtp.gmail.com'
    end
    object lstPort: TComboBox
      Left = 72
      Top = 52
      Width = 145
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 1
    end
    object lstProvider: TComboBox
      Left = 72
      Top = 91
      Width = 193
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 2
      OnChange = lstProviderChange
    end
    object chkUseHttpListener: TCheckBox
      Left = 72
      Top = 128
      Width = 129
      Height = 17
      Caption = 'Use Http Listener'
      TabOrder = 3
      OnClick = chkUseHttpListenerClick
    end
  end
  object lstCharset: TComboBox
    Left = 72
    Top = 139
    Width = 201
    Height = 21
    Style = csDropDownList
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ItemHeight = 13
    ParentFont = False
    TabOrder = 5
  end
  object textAttachment: TEdit
    Left = 72
    Top = 184
    Width = 473
    Height = 21
    Color = clYellow
    Enabled = False
    TabOrder = 6
  end
  object btnAdd: TButton
    Left = 551
    Top = 184
    Width = 41
    Height = 21
    Caption = 'Add'
    TabOrder = 7
    OnClick = btnAddClick
  end
  object btnClear: TButton
    Left = 600
    Top = 184
    Width = 41
    Height = 21
    Caption = 'Clear'
    TabOrder = 8
    OnClick = btnClearClick
  end
  object btnSend: TButton
    Left = 376
    Top = 376
    Width = 81
    Height = 25
    Caption = 'Send'
    TabOrder = 9
    OnClick = btnSendClick
  end
  object htmlEditor: TWebBrowser
    Left = 8
    Top = 240
    Width = 633
    Height = 129
    TabOrder = 10
    OnNavigateComplete2 = htmlEditorNavigateComplete2
    ControlData = {
      4C0000006C410000550D00000000000000000000000000000000000000000000
      000000004C000000000000000000000001000000E0D057007335CF11AE690800
      2B2E126208000000000000004C0000000114020000000000C000000000000046
      8000000000000000000000000000000000000000000000000000000000000000
      00000000000000000100000000000000000000000000000000000000}
  end
  object lstFont: TComboBox
    Left = 7
    Top = 214
    Width = 153
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    TabOrder = 11
    OnChange = lstFontChange
  end
  object lstSize: TComboBox
    Left = 168
    Top = 213
    Width = 113
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    TabOrder = 12
    OnChange = lstSizeChange
  end
  object btnB: TButton
    Left = 299
    Top = 211
    Width = 33
    Height = 25
    Caption = 'B'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 13
    OnClick = btnBClick
  end
  object btnI: TButton
    Left = 331
    Top = 211
    Width = 33
    Height = 25
    Caption = 'I'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold, fsItalic]
    ParentFont = False
    TabOrder = 14
    OnClick = btnIClick
  end
  object btnU: TButton
    Left = 364
    Top = 211
    Width = 33
    Height = 25
    Caption = 'U'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold, fsUnderline]
    ParentFont = False
    TabOrder = 15
    OnClick = btnUClick
  end
  object btnC: TButton
    Left = 396
    Top = 211
    Width = 33
    Height = 25
    Caption = 'C'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clRed
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
    TabOrder = 16
    OnClick = btnCClick
  end
  object btnInsert: TButton
    Left = 441
    Top = 210
    Width = 129
    Height = 25
    Caption = 'Insert Image'
    TabOrder = 17
    OnClick = btnInsertClick
  end
  object btnCancel: TButton
    Left = 464
    Top = 376
    Width = 81
    Height = 25
    Caption = 'Cancel'
    Enabled = False
    TabOrder = 18
    OnClick = btnCancelClick
  end
  object btnClearToken: TButton
    Left = 552
    Top = 376
    Width = 89
    Height = 25
    Caption = 'Clear Token'
    Enabled = False
    TabOrder = 19
    OnClick = btnClearTokenClick
  end
  object ColorDialog1: TColorDialog
    Left = 312
    Top = 136
  end
end
