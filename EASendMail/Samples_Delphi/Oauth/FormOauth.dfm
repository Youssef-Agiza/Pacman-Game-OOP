object FormOauth: TFormOauth
  Left = 214
  Top = 23
  BorderStyle = bsDialog
  Caption = 'Web Login'
  ClientHeight = 713
  ClientWidth = 572
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCloseQuery = FormCloseQuery
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object OauthBrowser: TWebBrowser
    Left = 0
    Top = 8
    Width = 569
    Height = 700
    TabOrder = 0
    OnTitleChange = OauthBrowserTitleChange
    OnNavigateComplete2 = OauthBrowserNavigateComplete2
    OnDocumentComplete = OauthBrowserDocumentComplete
    ControlData = {
      4C000000CF3A0000594800000000000000000000000000000000000000000000
      000000004C000000000000000000000001000000E0D057007335CF11AE690800
      2B2E12620A000000000000004C0000000114020000000000C000000000000046
      8000000000000000000000000000000000000000000000000000000000000000
      00000000000000000100000000000000000000000000000000000000}
  end
end
