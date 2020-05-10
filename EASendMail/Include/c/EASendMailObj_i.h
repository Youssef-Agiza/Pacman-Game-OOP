

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 7.00.0555 */
/* at Mon Apr 13 09:53:49 2020
 */
/* Compiler settings for EASendMailObj.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 7.00.0555 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__


#ifndef __EASendMailObj_i_h__
#define __EASendMailObj_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IMail_FWD_DEFINED__
#define __IMail_FWD_DEFINED__
typedef interface IMail IMail;
#endif 	/* __IMail_FWD_DEFINED__ */


#ifndef __IFastSender_FWD_DEFINED__
#define __IFastSender_FWD_DEFINED__
typedef interface IFastSender IFastSender;
#endif 	/* __IFastSender_FWD_DEFINED__ */


#ifndef __ICertificate_FWD_DEFINED__
#define __ICertificate_FWD_DEFINED__
typedef interface ICertificate ICertificate;
#endif 	/* __ICertificate_FWD_DEFINED__ */


#ifndef __ICertificateCollection_FWD_DEFINED__
#define __ICertificateCollection_FWD_DEFINED__
typedef interface ICertificateCollection ICertificateCollection;
#endif 	/* __ICertificateCollection_FWD_DEFINED__ */


#ifndef __ISimpleJsonArray_FWD_DEFINED__
#define __ISimpleJsonArray_FWD_DEFINED__
typedef interface ISimpleJsonArray ISimpleJsonArray;
#endif 	/* __ISimpleJsonArray_FWD_DEFINED__ */


#ifndef __ISimpleJsonParser_FWD_DEFINED__
#define __ISimpleJsonParser_FWD_DEFINED__
typedef interface ISimpleJsonParser ISimpleJsonParser;
#endif 	/* __ISimpleJsonParser_FWD_DEFINED__ */


#ifndef __IBrowserUi_FWD_DEFINED__
#define __IBrowserUi_FWD_DEFINED__
typedef interface IBrowserUi IBrowserUi;
#endif 	/* __IBrowserUi_FWD_DEFINED__ */


#ifndef __IHttpListener_FWD_DEFINED__
#define __IHttpListener_FWD_DEFINED__
typedef interface IHttpListener IHttpListener;
#endif 	/* __IHttpListener_FWD_DEFINED__ */


#ifndef ___IMailEvents_FWD_DEFINED__
#define ___IMailEvents_FWD_DEFINED__
typedef interface _IMailEvents _IMailEvents;
#endif 	/* ___IMailEvents_FWD_DEFINED__ */


#ifndef __Mail_FWD_DEFINED__
#define __Mail_FWD_DEFINED__

#ifdef __cplusplus
typedef class Mail Mail;
#else
typedef struct Mail Mail;
#endif /* __cplusplus */

#endif 	/* __Mail_FWD_DEFINED__ */


#ifndef ___IFastSenderEvents_FWD_DEFINED__
#define ___IFastSenderEvents_FWD_DEFINED__
typedef interface _IFastSenderEvents _IFastSenderEvents;
#endif 	/* ___IFastSenderEvents_FWD_DEFINED__ */


#ifndef __FastSender_FWD_DEFINED__
#define __FastSender_FWD_DEFINED__

#ifdef __cplusplus
typedef class FastSender FastSender;
#else
typedef struct FastSender FastSender;
#endif /* __cplusplus */

#endif 	/* __FastSender_FWD_DEFINED__ */


#ifndef __Certificate_FWD_DEFINED__
#define __Certificate_FWD_DEFINED__

#ifdef __cplusplus
typedef class Certificate Certificate;
#else
typedef struct Certificate Certificate;
#endif /* __cplusplus */

#endif 	/* __Certificate_FWD_DEFINED__ */


#ifndef __CertificateCollection_FWD_DEFINED__
#define __CertificateCollection_FWD_DEFINED__

#ifdef __cplusplus
typedef class CertificateCollection CertificateCollection;
#else
typedef struct CertificateCollection CertificateCollection;
#endif /* __cplusplus */

#endif 	/* __CertificateCollection_FWD_DEFINED__ */


#ifndef __SimpleJsonParser_FWD_DEFINED__
#define __SimpleJsonParser_FWD_DEFINED__

#ifdef __cplusplus
typedef class SimpleJsonParser SimpleJsonParser;
#else
typedef struct SimpleJsonParser SimpleJsonParser;
#endif /* __cplusplus */

#endif 	/* __SimpleJsonParser_FWD_DEFINED__ */


#ifndef __SimpleJsonArray_FWD_DEFINED__
#define __SimpleJsonArray_FWD_DEFINED__

#ifdef __cplusplus
typedef class SimpleJsonArray SimpleJsonArray;
#else
typedef struct SimpleJsonArray SimpleJsonArray;
#endif /* __cplusplus */

#endif 	/* __SimpleJsonArray_FWD_DEFINED__ */


#ifndef __OAuthResponseParser_FWD_DEFINED__
#define __OAuthResponseParser_FWD_DEFINED__

#ifdef __cplusplus
typedef class OAuthResponseParser OAuthResponseParser;
#else
typedef struct OAuthResponseParser OAuthResponseParser;
#endif /* __cplusplus */

#endif 	/* __OAuthResponseParser_FWD_DEFINED__ */


#ifndef __BrowserUi_FWD_DEFINED__
#define __BrowserUi_FWD_DEFINED__

#ifdef __cplusplus
typedef class BrowserUi BrowserUi;
#else
typedef struct BrowserUi BrowserUi;
#endif /* __cplusplus */

#endif 	/* __BrowserUi_FWD_DEFINED__ */


#ifndef ___IHttpListenerEvents_FWD_DEFINED__
#define ___IHttpListenerEvents_FWD_DEFINED__
typedef interface _IHttpListenerEvents _IHttpListenerEvents;
#endif 	/* ___IHttpListenerEvents_FWD_DEFINED__ */


#ifndef __HttpListener_FWD_DEFINED__
#define __HttpListener_FWD_DEFINED__

#ifdef __cplusplus
typedef class HttpListener HttpListener;
#else
typedef struct HttpListener HttpListener;
#endif /* __cplusplus */

#endif 	/* __HttpListener_FWD_DEFINED__ */


#ifndef __IOAuthResponseParser_FWD_DEFINED__
#define __IOAuthResponseParser_FWD_DEFINED__
typedef interface IOAuthResponseParser IOAuthResponseParser;
#endif 	/* __IOAuthResponseParser_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __EASendMailObjLib_LIBRARY_DEFINED__
#define __EASendMailObjLib_LIBRARY_DEFINED__

/* library EASendMailObjLib */
/* [helpstring][version][uuid] */ 










EXTERN_C const IID LIBID_EASendMailObjLib;

#ifndef __IMail_INTERFACE_DEFINED__
#define __IMail_INTERFACE_DEFINED__

/* interface IMail */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IMail;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("1AD28FC9-0C71-4E89-85C9-CAECDE8BE3AB")
    IMail : public IDispatch
    {
    public:
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_BodyFormat( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_BodyFormat( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_BodyText( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_BodyText( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Charset( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Charset( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_From( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_From( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_FromAddr( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_FromAddr( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_LogFileName( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_LogFileName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_LicenseCode( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_LicenseCode( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ServerAddr( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ServerAddr( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ServerPort( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ServerPort( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Subject( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Subject( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ReplyTo( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ReplyTo( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Priority( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Priority( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Timeout( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Timeout( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_UserName( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_UserName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Password( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Password( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Version( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Asynchronous( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Asynchronous( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_AltBody( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_AltBody( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddAttachment( 
            /* [in] */ BSTR strFile,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddRecipient( 
            /* [in] */ BSTR strName,
            /* [in] */ BSTR strAddress,
            /* [in] */ long Flags,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ClearAttachment( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ClearRecipient( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ConvertHTML( 
            /* [in] */ long Flags) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ImportMail( 
            /* [in] */ BSTR strFile,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Reset( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SendMail( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddAttachmentEx( 
            /* [in] */ BSTR strFile,
            /* [in] */ BSTR strAlt,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddInline( 
            /* [in] */ BSTR strFile,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddInlineEx( 
            /* [in] */ BSTR strFile,
            /* [in] */ BSTR strAlt,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ClearInline( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SaveMail( 
            /* [in] */ BSTR strFile,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddHeader( 
            /* [in] */ BSTR strHeader,
            /* [in] */ BSTR strValue,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ClearHeader( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Terminate( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetLastError( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetLastErrDescription( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Anonymous( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Anonymous( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SetMailer( 
            /* [in] */ BSTR Mailer) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_KeepConnection( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_KeepConnection( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ImportMailEx( 
            /* [in] */ BSTR strFile,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_TransferEncoding( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_TransferEncoding( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetEmailServer( 
            /* [in] */ BSTR EmailAddr,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddRecipientEx( 
            /* [in] */ BSTR AddressList,
            /* [in] */ long Flags,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddAttachments( 
            /* [in] */ BSTR sPath,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ComputerName( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ComputerName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_BodyFormatEx( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_BodyFormatEx( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_HeaderEncoding( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_HeaderEncoding( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SaveMailEx( 
            /* [defaultvalue][optional][in] */ BSTR PickupPath,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE TestEmailAddr( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetAllEmailServers( 
            /* [in] */ BSTR EmailAddr,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetEmailContent( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetEmailHeaders( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetAllRecipients( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetSenderAddr( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_TryAllSmtpServers( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_TryAllSmtpServers( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE CreateFolder( 
            /* [in] */ BSTR FolderName,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE DeleteFile( 
            /* [in] */ BSTR FileName,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_RawModeEnable( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_RawModeEnable( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_WrapEmailAddr( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_WrapEmailAddr( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_DeliveryNotification( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_DeliveryNotification( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get__Idle( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SSL_init( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SSL_ignorecerterror( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SSL_ignorecerterror( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SSL_starttls( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SSL_starttls( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SSL_uninit( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SSL_enabled( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_raw_Content( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_raw_Content( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_LogLevel( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_LogLevel( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SignerCert( 
            /* [retval][out] */ ICertificate **pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SignerCert( 
            /* [in] */ ICertificate *newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_RecipientsCerts( 
            /* [retval][out] */ ICertificateCollection **pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE WriteLog( 
            /* [in] */ BSTR LogContent) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ReturnPath( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ReturnPath( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_LocalIP( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_LocalIP( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ImportHtml( 
            /* [in] */ BSTR html,
            /* [in] */ BSTR BasePath,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddAttachment1( 
            /* [in] */ BSTR FileName,
            /* [in] */ VARIANT Stream,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_AuthType( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_AuthType( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SpecialFlags( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SpecialFlags( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_DisplayTo( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_DisplayTo( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Date( 
            /* [retval][out] */ DATE *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Date( 
            /* [in] */ DATE newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_MessageID( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_MessageID( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AppendBody( 
            /* [in] */ BSTR BodyText,
            /* [in] */ long bAlt) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddInline1( 
            /* [in] */ BSTR FileName,
            /* [in] */ VARIANT Stream,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SendMailToQueue( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_NoWrapBody( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_NoWrapBody( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_EncryptionAlgorithm( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_EncryptionAlgorithm( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ClearHeaderEx( 
            /* [in] */ BSTR HeaderName) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetEmailChunk( 
            /* [retval][out] */ VARIANT *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE AddAttachmentCT( 
            /* [in] */ BSTR FileName,
            /* [in] */ BSTR ContentType,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SocksProxyServer( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SocksProxyServer( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SocksProxyUser( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SocksProxyUser( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SocksProxyPassword( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SocksProxyPassword( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SocksProxyPort( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SocksProxyPort( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ProxyProtocol( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ProxyProtocol( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_DK_PublicKey( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LoadMessage( 
            /* [in] */ BSTR FileName,
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ReadReceipt( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ReadReceipt( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LoadMessageChunk( 
            /* [in] */ VARIANT newVal,
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Recipients( 
            /* [retval][out] */ VARIANT *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Style( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Style( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SetAttHeader( 
            /* [in] */ LONG Index,
            /* [in] */ BSTR HeaderKey,
            /* [in] */ BSTR HeaderValue) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_AutoCalendar( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_AutoCalendar( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_AttachmentCount( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_DnsServerIP( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_DnsServerIP( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SendMailToQueueEx( 
            /* [in] */ BSTR Instant,
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LoadRawMessage( 
            /* [in] */ BSTR FileName,
            /* [in] */ LONG Flag,
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Protocol( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Protocol( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Alias( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Alias( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Drafts( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Drafts( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Sender( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Sender( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Quit( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Close( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_HttpProxyAuthType( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_HttpProxyAuthType( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SMIMERFCCompatibility( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SMIMERFCCompatibility( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_PIPELINING( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_PIPELINING( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_IgnoreDeliveryNotificationError( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_IgnoreDeliveryNotificationError( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_IPv6Policy( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_IPv6Policy( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_LocalIP6( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_LocalIP6( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE PostToRemoteQueue( 
            /* [in] */ BSTR Instance,
            /* [in] */ BSTR URL,
            /* [in] */ BSTR User,
            /* [in] */ BSTR Password,
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_MimeSplitor( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_MimeSplitor( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SaveCopy( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SaveCopy( 
            /* [in] */ VARIANT_BOOL newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SignatureHashAlgorithm( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SignatureHashAlgorithm( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_AttachmentEncoding( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_AttachmentEncoding( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SignatureEncryptionAlgorithm( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SignatureEncryptionAlgorithm( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_OaepHashAlgorithm( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_OaepHashAlgorithm( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ConnectType( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ConnectType( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_EWSImpersonatedUser( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_EWSImpersonatedUser( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IMailVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IMail * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IMail * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IMail * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IMail * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IMail * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IMail * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IMail * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_BodyFormat )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_BodyFormat )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_BodyText )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_BodyText )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Charset )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Charset )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_From )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_From )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_FromAddr )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_FromAddr )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_LogFileName )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_LogFileName )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_LicenseCode )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_LicenseCode )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ServerAddr )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ServerAddr )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ServerPort )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ServerPort )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Subject )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Subject )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ReplyTo )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ReplyTo )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Priority )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Priority )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Timeout )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Timeout )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_UserName )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_UserName )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Password )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Password )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Version )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Asynchronous )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Asynchronous )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_AltBody )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_AltBody )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddAttachment )( 
            IMail * This,
            /* [in] */ BSTR strFile,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddRecipient )( 
            IMail * This,
            /* [in] */ BSTR strName,
            /* [in] */ BSTR strAddress,
            /* [in] */ long Flags,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ClearAttachment )( 
            IMail * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ClearRecipient )( 
            IMail * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ConvertHTML )( 
            IMail * This,
            /* [in] */ long Flags);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ImportMail )( 
            IMail * This,
            /* [in] */ BSTR strFile,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Reset )( 
            IMail * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SendMail )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddAttachmentEx )( 
            IMail * This,
            /* [in] */ BSTR strFile,
            /* [in] */ BSTR strAlt,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddInline )( 
            IMail * This,
            /* [in] */ BSTR strFile,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddInlineEx )( 
            IMail * This,
            /* [in] */ BSTR strFile,
            /* [in] */ BSTR strAlt,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ClearInline )( 
            IMail * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SaveMail )( 
            IMail * This,
            /* [in] */ BSTR strFile,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddHeader )( 
            IMail * This,
            /* [in] */ BSTR strHeader,
            /* [in] */ BSTR strValue,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ClearHeader )( 
            IMail * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Terminate )( 
            IMail * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetLastError )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetLastErrDescription )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Anonymous )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Anonymous )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SetMailer )( 
            IMail * This,
            /* [in] */ BSTR Mailer);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_KeepConnection )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_KeepConnection )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ImportMailEx )( 
            IMail * This,
            /* [in] */ BSTR strFile,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_TransferEncoding )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_TransferEncoding )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetEmailServer )( 
            IMail * This,
            /* [in] */ BSTR EmailAddr,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddRecipientEx )( 
            IMail * This,
            /* [in] */ BSTR AddressList,
            /* [in] */ long Flags,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddAttachments )( 
            IMail * This,
            /* [in] */ BSTR sPath,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ComputerName )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ComputerName )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_BodyFormatEx )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_BodyFormatEx )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_HeaderEncoding )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_HeaderEncoding )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SaveMailEx )( 
            IMail * This,
            /* [defaultvalue][optional][in] */ BSTR PickupPath,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *TestEmailAddr )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetAllEmailServers )( 
            IMail * This,
            /* [in] */ BSTR EmailAddr,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetEmailContent )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetEmailHeaders )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetAllRecipients )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetSenderAddr )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_TryAllSmtpServers )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_TryAllSmtpServers )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *CreateFolder )( 
            IMail * This,
            /* [in] */ BSTR FolderName,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *DeleteFile )( 
            IMail * This,
            /* [in] */ BSTR FileName,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_RawModeEnable )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_RawModeEnable )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_WrapEmailAddr )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_WrapEmailAddr )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_DeliveryNotification )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_DeliveryNotification )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get__Idle )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SSL_init )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SSL_ignorecerterror )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SSL_ignorecerterror )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SSL_starttls )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SSL_starttls )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SSL_uninit )( 
            IMail * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SSL_enabled )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_raw_Content )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_raw_Content )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_LogLevel )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_LogLevel )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SignerCert )( 
            IMail * This,
            /* [retval][out] */ ICertificate **pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SignerCert )( 
            IMail * This,
            /* [in] */ ICertificate *newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_RecipientsCerts )( 
            IMail * This,
            /* [retval][out] */ ICertificateCollection **pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *WriteLog )( 
            IMail * This,
            /* [in] */ BSTR LogContent);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ReturnPath )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ReturnPath )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_LocalIP )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_LocalIP )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ImportHtml )( 
            IMail * This,
            /* [in] */ BSTR html,
            /* [in] */ BSTR BasePath,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddAttachment1 )( 
            IMail * This,
            /* [in] */ BSTR FileName,
            /* [in] */ VARIANT Stream,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_AuthType )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_AuthType )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SpecialFlags )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SpecialFlags )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_DisplayTo )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_DisplayTo )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Date )( 
            IMail * This,
            /* [retval][out] */ DATE *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Date )( 
            IMail * This,
            /* [in] */ DATE newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_MessageID )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_MessageID )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AppendBody )( 
            IMail * This,
            /* [in] */ BSTR BodyText,
            /* [in] */ long bAlt);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddInline1 )( 
            IMail * This,
            /* [in] */ BSTR FileName,
            /* [in] */ VARIANT Stream,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SendMailToQueue )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_NoWrapBody )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_NoWrapBody )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_EncryptionAlgorithm )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_EncryptionAlgorithm )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ClearHeaderEx )( 
            IMail * This,
            /* [in] */ BSTR HeaderName);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetEmailChunk )( 
            IMail * This,
            /* [retval][out] */ VARIANT *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *AddAttachmentCT )( 
            IMail * This,
            /* [in] */ BSTR FileName,
            /* [in] */ BSTR ContentType,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SocksProxyServer )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SocksProxyServer )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SocksProxyUser )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SocksProxyUser )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SocksProxyPassword )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SocksProxyPassword )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SocksProxyPort )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SocksProxyPort )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ProxyProtocol )( 
            IMail * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ProxyProtocol )( 
            IMail * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_DK_PublicKey )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LoadMessage )( 
            IMail * This,
            /* [in] */ BSTR FileName,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ReadReceipt )( 
            IMail * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ReadReceipt )( 
            IMail * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LoadMessageChunk )( 
            IMail * This,
            /* [in] */ VARIANT newVal,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Recipients )( 
            IMail * This,
            /* [retval][out] */ VARIANT *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Style )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Style )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SetAttHeader )( 
            IMail * This,
            /* [in] */ LONG Index,
            /* [in] */ BSTR HeaderKey,
            /* [in] */ BSTR HeaderValue);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_AutoCalendar )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_AutoCalendar )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_AttachmentCount )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_DnsServerIP )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_DnsServerIP )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SendMailToQueueEx )( 
            IMail * This,
            /* [in] */ BSTR Instant,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LoadRawMessage )( 
            IMail * This,
            /* [in] */ BSTR FileName,
            /* [in] */ LONG Flag,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Protocol )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Protocol )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Alias )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Alias )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Drafts )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Drafts )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Sender )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Sender )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Quit )( 
            IMail * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Close )( 
            IMail * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_HttpProxyAuthType )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_HttpProxyAuthType )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SMIMERFCCompatibility )( 
            IMail * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SMIMERFCCompatibility )( 
            IMail * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_PIPELINING )( 
            IMail * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_PIPELINING )( 
            IMail * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_IgnoreDeliveryNotificationError )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_IgnoreDeliveryNotificationError )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_IPv6Policy )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_IPv6Policy )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_LocalIP6 )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_LocalIP6 )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *PostToRemoteQueue )( 
            IMail * This,
            /* [in] */ BSTR Instance,
            /* [in] */ BSTR URL,
            /* [in] */ BSTR User,
            /* [in] */ BSTR Password,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_MimeSplitor )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_MimeSplitor )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SaveCopy )( 
            IMail * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SaveCopy )( 
            IMail * This,
            /* [in] */ VARIANT_BOOL newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SignatureHashAlgorithm )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SignatureHashAlgorithm )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_AttachmentEncoding )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_AttachmentEncoding )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SignatureEncryptionAlgorithm )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SignatureEncryptionAlgorithm )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_OaepHashAlgorithm )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_OaepHashAlgorithm )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ConnectType )( 
            IMail * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ConnectType )( 
            IMail * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_EWSImpersonatedUser )( 
            IMail * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_EWSImpersonatedUser )( 
            IMail * This,
            /* [retval][out] */ BSTR *pVal);
        
        END_INTERFACE
    } IMailVtbl;

    interface IMail
    {
        CONST_VTBL struct IMailVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IMail_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IMail_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IMail_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IMail_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IMail_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IMail_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IMail_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IMail_get_BodyFormat(This,pVal)	\
    ( (This)->lpVtbl -> get_BodyFormat(This,pVal) ) 

#define IMail_put_BodyFormat(This,newVal)	\
    ( (This)->lpVtbl -> put_BodyFormat(This,newVal) ) 

#define IMail_get_BodyText(This,pVal)	\
    ( (This)->lpVtbl -> get_BodyText(This,pVal) ) 

#define IMail_put_BodyText(This,newVal)	\
    ( (This)->lpVtbl -> put_BodyText(This,newVal) ) 

#define IMail_get_Charset(This,pVal)	\
    ( (This)->lpVtbl -> get_Charset(This,pVal) ) 

#define IMail_put_Charset(This,newVal)	\
    ( (This)->lpVtbl -> put_Charset(This,newVal) ) 

#define IMail_get_From(This,pVal)	\
    ( (This)->lpVtbl -> get_From(This,pVal) ) 

#define IMail_put_From(This,newVal)	\
    ( (This)->lpVtbl -> put_From(This,newVal) ) 

#define IMail_get_FromAddr(This,pVal)	\
    ( (This)->lpVtbl -> get_FromAddr(This,pVal) ) 

#define IMail_put_FromAddr(This,newVal)	\
    ( (This)->lpVtbl -> put_FromAddr(This,newVal) ) 

#define IMail_get_LogFileName(This,pVal)	\
    ( (This)->lpVtbl -> get_LogFileName(This,pVal) ) 

#define IMail_put_LogFileName(This,newVal)	\
    ( (This)->lpVtbl -> put_LogFileName(This,newVal) ) 

#define IMail_get_LicenseCode(This,pVal)	\
    ( (This)->lpVtbl -> get_LicenseCode(This,pVal) ) 

#define IMail_put_LicenseCode(This,newVal)	\
    ( (This)->lpVtbl -> put_LicenseCode(This,newVal) ) 

#define IMail_get_ServerAddr(This,pVal)	\
    ( (This)->lpVtbl -> get_ServerAddr(This,pVal) ) 

#define IMail_put_ServerAddr(This,newVal)	\
    ( (This)->lpVtbl -> put_ServerAddr(This,newVal) ) 

#define IMail_get_ServerPort(This,pVal)	\
    ( (This)->lpVtbl -> get_ServerPort(This,pVal) ) 

#define IMail_put_ServerPort(This,newVal)	\
    ( (This)->lpVtbl -> put_ServerPort(This,newVal) ) 

#define IMail_get_Subject(This,pVal)	\
    ( (This)->lpVtbl -> get_Subject(This,pVal) ) 

#define IMail_put_Subject(This,newVal)	\
    ( (This)->lpVtbl -> put_Subject(This,newVal) ) 

#define IMail_get_ReplyTo(This,pVal)	\
    ( (This)->lpVtbl -> get_ReplyTo(This,pVal) ) 

#define IMail_put_ReplyTo(This,newVal)	\
    ( (This)->lpVtbl -> put_ReplyTo(This,newVal) ) 

#define IMail_get_Priority(This,pVal)	\
    ( (This)->lpVtbl -> get_Priority(This,pVal) ) 

#define IMail_put_Priority(This,newVal)	\
    ( (This)->lpVtbl -> put_Priority(This,newVal) ) 

#define IMail_get_Timeout(This,pVal)	\
    ( (This)->lpVtbl -> get_Timeout(This,pVal) ) 

#define IMail_put_Timeout(This,newVal)	\
    ( (This)->lpVtbl -> put_Timeout(This,newVal) ) 

#define IMail_get_UserName(This,pVal)	\
    ( (This)->lpVtbl -> get_UserName(This,pVal) ) 

#define IMail_put_UserName(This,newVal)	\
    ( (This)->lpVtbl -> put_UserName(This,newVal) ) 

#define IMail_get_Password(This,pVal)	\
    ( (This)->lpVtbl -> get_Password(This,pVal) ) 

#define IMail_put_Password(This,newVal)	\
    ( (This)->lpVtbl -> put_Password(This,newVal) ) 

#define IMail_get_Version(This,pVal)	\
    ( (This)->lpVtbl -> get_Version(This,pVal) ) 

#define IMail_get_Asynchronous(This,pVal)	\
    ( (This)->lpVtbl -> get_Asynchronous(This,pVal) ) 

#define IMail_put_Asynchronous(This,newVal)	\
    ( (This)->lpVtbl -> put_Asynchronous(This,newVal) ) 

#define IMail_get_AltBody(This,pVal)	\
    ( (This)->lpVtbl -> get_AltBody(This,pVal) ) 

#define IMail_put_AltBody(This,newVal)	\
    ( (This)->lpVtbl -> put_AltBody(This,newVal) ) 

#define IMail_AddAttachment(This,strFile,pVal)	\
    ( (This)->lpVtbl -> AddAttachment(This,strFile,pVal) ) 

#define IMail_AddRecipient(This,strName,strAddress,Flags,pVal)	\
    ( (This)->lpVtbl -> AddRecipient(This,strName,strAddress,Flags,pVal) ) 

#define IMail_ClearAttachment(This)	\
    ( (This)->lpVtbl -> ClearAttachment(This) ) 

#define IMail_ClearRecipient(This)	\
    ( (This)->lpVtbl -> ClearRecipient(This) ) 

#define IMail_ConvertHTML(This,Flags)	\
    ( (This)->lpVtbl -> ConvertHTML(This,Flags) ) 

#define IMail_ImportMail(This,strFile,pVal)	\
    ( (This)->lpVtbl -> ImportMail(This,strFile,pVal) ) 

#define IMail_Reset(This)	\
    ( (This)->lpVtbl -> Reset(This) ) 

#define IMail_SendMail(This,pVal)	\
    ( (This)->lpVtbl -> SendMail(This,pVal) ) 

#define IMail_AddAttachmentEx(This,strFile,strAlt,pVal)	\
    ( (This)->lpVtbl -> AddAttachmentEx(This,strFile,strAlt,pVal) ) 

#define IMail_AddInline(This,strFile,pVal)	\
    ( (This)->lpVtbl -> AddInline(This,strFile,pVal) ) 

#define IMail_AddInlineEx(This,strFile,strAlt,pVal)	\
    ( (This)->lpVtbl -> AddInlineEx(This,strFile,strAlt,pVal) ) 

#define IMail_ClearInline(This)	\
    ( (This)->lpVtbl -> ClearInline(This) ) 

#define IMail_SaveMail(This,strFile,pVal)	\
    ( (This)->lpVtbl -> SaveMail(This,strFile,pVal) ) 

#define IMail_AddHeader(This,strHeader,strValue,pVal)	\
    ( (This)->lpVtbl -> AddHeader(This,strHeader,strValue,pVal) ) 

#define IMail_ClearHeader(This)	\
    ( (This)->lpVtbl -> ClearHeader(This) ) 

#define IMail_Terminate(This)	\
    ( (This)->lpVtbl -> Terminate(This) ) 

#define IMail_GetLastError(This,pVal)	\
    ( (This)->lpVtbl -> GetLastError(This,pVal) ) 

#define IMail_GetLastErrDescription(This,pVal)	\
    ( (This)->lpVtbl -> GetLastErrDescription(This,pVal) ) 

#define IMail_get_Anonymous(This,pVal)	\
    ( (This)->lpVtbl -> get_Anonymous(This,pVal) ) 

#define IMail_put_Anonymous(This,newVal)	\
    ( (This)->lpVtbl -> put_Anonymous(This,newVal) ) 

#define IMail_SetMailer(This,Mailer)	\
    ( (This)->lpVtbl -> SetMailer(This,Mailer) ) 

#define IMail_get_KeepConnection(This,pVal)	\
    ( (This)->lpVtbl -> get_KeepConnection(This,pVal) ) 

#define IMail_put_KeepConnection(This,newVal)	\
    ( (This)->lpVtbl -> put_KeepConnection(This,newVal) ) 

#define IMail_ImportMailEx(This,strFile,pVal)	\
    ( (This)->lpVtbl -> ImportMailEx(This,strFile,pVal) ) 

#define IMail_get_TransferEncoding(This,pVal)	\
    ( (This)->lpVtbl -> get_TransferEncoding(This,pVal) ) 

#define IMail_put_TransferEncoding(This,newVal)	\
    ( (This)->lpVtbl -> put_TransferEncoding(This,newVal) ) 

#define IMail_GetEmailServer(This,EmailAddr,pVal)	\
    ( (This)->lpVtbl -> GetEmailServer(This,EmailAddr,pVal) ) 

#define IMail_AddRecipientEx(This,AddressList,Flags,pVal)	\
    ( (This)->lpVtbl -> AddRecipientEx(This,AddressList,Flags,pVal) ) 

#define IMail_AddAttachments(This,sPath,pVal)	\
    ( (This)->lpVtbl -> AddAttachments(This,sPath,pVal) ) 

#define IMail_get_ComputerName(This,pVal)	\
    ( (This)->lpVtbl -> get_ComputerName(This,pVal) ) 

#define IMail_put_ComputerName(This,newVal)	\
    ( (This)->lpVtbl -> put_ComputerName(This,newVal) ) 

#define IMail_get_BodyFormatEx(This,pVal)	\
    ( (This)->lpVtbl -> get_BodyFormatEx(This,pVal) ) 

#define IMail_put_BodyFormatEx(This,newVal)	\
    ( (This)->lpVtbl -> put_BodyFormatEx(This,newVal) ) 

#define IMail_get_HeaderEncoding(This,pVal)	\
    ( (This)->lpVtbl -> get_HeaderEncoding(This,pVal) ) 

#define IMail_put_HeaderEncoding(This,newVal)	\
    ( (This)->lpVtbl -> put_HeaderEncoding(This,newVal) ) 

#define IMail_SaveMailEx(This,PickupPath,pVal)	\
    ( (This)->lpVtbl -> SaveMailEx(This,PickupPath,pVal) ) 

#define IMail_TestEmailAddr(This,pVal)	\
    ( (This)->lpVtbl -> TestEmailAddr(This,pVal) ) 

#define IMail_GetAllEmailServers(This,EmailAddr,pVal)	\
    ( (This)->lpVtbl -> GetAllEmailServers(This,EmailAddr,pVal) ) 

#define IMail_GetEmailContent(This,pVal)	\
    ( (This)->lpVtbl -> GetEmailContent(This,pVal) ) 

#define IMail_GetEmailHeaders(This,pVal)	\
    ( (This)->lpVtbl -> GetEmailHeaders(This,pVal) ) 

#define IMail_GetAllRecipients(This,pVal)	\
    ( (This)->lpVtbl -> GetAllRecipients(This,pVal) ) 

#define IMail_GetSenderAddr(This,pVal)	\
    ( (This)->lpVtbl -> GetSenderAddr(This,pVal) ) 

#define IMail_get_TryAllSmtpServers(This,pVal)	\
    ( (This)->lpVtbl -> get_TryAllSmtpServers(This,pVal) ) 

#define IMail_put_TryAllSmtpServers(This,newVal)	\
    ( (This)->lpVtbl -> put_TryAllSmtpServers(This,newVal) ) 

#define IMail_CreateFolder(This,FolderName,pVal)	\
    ( (This)->lpVtbl -> CreateFolder(This,FolderName,pVal) ) 

#define IMail_DeleteFile(This,FileName,pVal)	\
    ( (This)->lpVtbl -> DeleteFile(This,FileName,pVal) ) 

#define IMail_get_RawModeEnable(This,pVal)	\
    ( (This)->lpVtbl -> get_RawModeEnable(This,pVal) ) 

#define IMail_put_RawModeEnable(This,newVal)	\
    ( (This)->lpVtbl -> put_RawModeEnable(This,newVal) ) 

#define IMail_get_WrapEmailAddr(This,pVal)	\
    ( (This)->lpVtbl -> get_WrapEmailAddr(This,pVal) ) 

#define IMail_put_WrapEmailAddr(This,newVal)	\
    ( (This)->lpVtbl -> put_WrapEmailAddr(This,newVal) ) 

#define IMail_get_DeliveryNotification(This,pVal)	\
    ( (This)->lpVtbl -> get_DeliveryNotification(This,pVal) ) 

#define IMail_put_DeliveryNotification(This,newVal)	\
    ( (This)->lpVtbl -> put_DeliveryNotification(This,newVal) ) 

#define IMail_get__Idle(This,pVal)	\
    ( (This)->lpVtbl -> get__Idle(This,pVal) ) 

#define IMail_SSL_init(This,pVal)	\
    ( (This)->lpVtbl -> SSL_init(This,pVal) ) 

#define IMail_get_SSL_ignorecerterror(This,pVal)	\
    ( (This)->lpVtbl -> get_SSL_ignorecerterror(This,pVal) ) 

#define IMail_put_SSL_ignorecerterror(This,newVal)	\
    ( (This)->lpVtbl -> put_SSL_ignorecerterror(This,newVal) ) 

#define IMail_get_SSL_starttls(This,pVal)	\
    ( (This)->lpVtbl -> get_SSL_starttls(This,pVal) ) 

#define IMail_put_SSL_starttls(This,newVal)	\
    ( (This)->lpVtbl -> put_SSL_starttls(This,newVal) ) 

#define IMail_SSL_uninit(This)	\
    ( (This)->lpVtbl -> SSL_uninit(This) ) 

#define IMail_get_SSL_enabled(This,pVal)	\
    ( (This)->lpVtbl -> get_SSL_enabled(This,pVal) ) 

#define IMail_get_raw_Content(This,pVal)	\
    ( (This)->lpVtbl -> get_raw_Content(This,pVal) ) 

#define IMail_put_raw_Content(This,newVal)	\
    ( (This)->lpVtbl -> put_raw_Content(This,newVal) ) 

#define IMail_get_LogLevel(This,pVal)	\
    ( (This)->lpVtbl -> get_LogLevel(This,pVal) ) 

#define IMail_put_LogLevel(This,newVal)	\
    ( (This)->lpVtbl -> put_LogLevel(This,newVal) ) 

#define IMail_get_SignerCert(This,pVal)	\
    ( (This)->lpVtbl -> get_SignerCert(This,pVal) ) 

#define IMail_put_SignerCert(This,newVal)	\
    ( (This)->lpVtbl -> put_SignerCert(This,newVal) ) 

#define IMail_get_RecipientsCerts(This,pVal)	\
    ( (This)->lpVtbl -> get_RecipientsCerts(This,pVal) ) 

#define IMail_WriteLog(This,LogContent)	\
    ( (This)->lpVtbl -> WriteLog(This,LogContent) ) 

#define IMail_get_ReturnPath(This,pVal)	\
    ( (This)->lpVtbl -> get_ReturnPath(This,pVal) ) 

#define IMail_put_ReturnPath(This,newVal)	\
    ( (This)->lpVtbl -> put_ReturnPath(This,newVal) ) 

#define IMail_get_LocalIP(This,pVal)	\
    ( (This)->lpVtbl -> get_LocalIP(This,pVal) ) 

#define IMail_put_LocalIP(This,newVal)	\
    ( (This)->lpVtbl -> put_LocalIP(This,newVal) ) 

#define IMail_ImportHtml(This,html,BasePath,pVal)	\
    ( (This)->lpVtbl -> ImportHtml(This,html,BasePath,pVal) ) 

#define IMail_AddAttachment1(This,FileName,Stream,pVal)	\
    ( (This)->lpVtbl -> AddAttachment1(This,FileName,Stream,pVal) ) 

#define IMail_get_AuthType(This,pVal)	\
    ( (This)->lpVtbl -> get_AuthType(This,pVal) ) 

#define IMail_put_AuthType(This,newVal)	\
    ( (This)->lpVtbl -> put_AuthType(This,newVal) ) 

#define IMail_get_SpecialFlags(This,pVal)	\
    ( (This)->lpVtbl -> get_SpecialFlags(This,pVal) ) 

#define IMail_put_SpecialFlags(This,newVal)	\
    ( (This)->lpVtbl -> put_SpecialFlags(This,newVal) ) 

#define IMail_get_DisplayTo(This,pVal)	\
    ( (This)->lpVtbl -> get_DisplayTo(This,pVal) ) 

#define IMail_put_DisplayTo(This,newVal)	\
    ( (This)->lpVtbl -> put_DisplayTo(This,newVal) ) 

#define IMail_get_Date(This,pVal)	\
    ( (This)->lpVtbl -> get_Date(This,pVal) ) 

#define IMail_put_Date(This,newVal)	\
    ( (This)->lpVtbl -> put_Date(This,newVal) ) 

#define IMail_get_MessageID(This,pVal)	\
    ( (This)->lpVtbl -> get_MessageID(This,pVal) ) 

#define IMail_put_MessageID(This,newVal)	\
    ( (This)->lpVtbl -> put_MessageID(This,newVal) ) 

#define IMail_AppendBody(This,BodyText,bAlt)	\
    ( (This)->lpVtbl -> AppendBody(This,BodyText,bAlt) ) 

#define IMail_AddInline1(This,FileName,Stream,pVal)	\
    ( (This)->lpVtbl -> AddInline1(This,FileName,Stream,pVal) ) 

#define IMail_SendMailToQueue(This,pVal)	\
    ( (This)->lpVtbl -> SendMailToQueue(This,pVal) ) 

#define IMail_get_NoWrapBody(This,pVal)	\
    ( (This)->lpVtbl -> get_NoWrapBody(This,pVal) ) 

#define IMail_put_NoWrapBody(This,newVal)	\
    ( (This)->lpVtbl -> put_NoWrapBody(This,newVal) ) 

#define IMail_get_EncryptionAlgorithm(This,pVal)	\
    ( (This)->lpVtbl -> get_EncryptionAlgorithm(This,pVal) ) 

#define IMail_put_EncryptionAlgorithm(This,newVal)	\
    ( (This)->lpVtbl -> put_EncryptionAlgorithm(This,newVal) ) 

#define IMail_ClearHeaderEx(This,HeaderName)	\
    ( (This)->lpVtbl -> ClearHeaderEx(This,HeaderName) ) 

#define IMail_GetEmailChunk(This,pVal)	\
    ( (This)->lpVtbl -> GetEmailChunk(This,pVal) ) 

#define IMail_AddAttachmentCT(This,FileName,ContentType,pVal)	\
    ( (This)->lpVtbl -> AddAttachmentCT(This,FileName,ContentType,pVal) ) 

#define IMail_get_SocksProxyServer(This,pVal)	\
    ( (This)->lpVtbl -> get_SocksProxyServer(This,pVal) ) 

#define IMail_put_SocksProxyServer(This,newVal)	\
    ( (This)->lpVtbl -> put_SocksProxyServer(This,newVal) ) 

#define IMail_get_SocksProxyUser(This,pVal)	\
    ( (This)->lpVtbl -> get_SocksProxyUser(This,pVal) ) 

#define IMail_put_SocksProxyUser(This,newVal)	\
    ( (This)->lpVtbl -> put_SocksProxyUser(This,newVal) ) 

#define IMail_get_SocksProxyPassword(This,pVal)	\
    ( (This)->lpVtbl -> get_SocksProxyPassword(This,pVal) ) 

#define IMail_put_SocksProxyPassword(This,newVal)	\
    ( (This)->lpVtbl -> put_SocksProxyPassword(This,newVal) ) 

#define IMail_get_SocksProxyPort(This,pVal)	\
    ( (This)->lpVtbl -> get_SocksProxyPort(This,pVal) ) 

#define IMail_put_SocksProxyPort(This,newVal)	\
    ( (This)->lpVtbl -> put_SocksProxyPort(This,newVal) ) 

#define IMail_get_ProxyProtocol(This,pVal)	\
    ( (This)->lpVtbl -> get_ProxyProtocol(This,pVal) ) 

#define IMail_put_ProxyProtocol(This,newVal)	\
    ( (This)->lpVtbl -> put_ProxyProtocol(This,newVal) ) 

#define IMail_get_DK_PublicKey(This,pVal)	\
    ( (This)->lpVtbl -> get_DK_PublicKey(This,pVal) ) 

#define IMail_LoadMessage(This,FileName,pVal)	\
    ( (This)->lpVtbl -> LoadMessage(This,FileName,pVal) ) 

#define IMail_get_ReadReceipt(This,pVal)	\
    ( (This)->lpVtbl -> get_ReadReceipt(This,pVal) ) 

#define IMail_put_ReadReceipt(This,newVal)	\
    ( (This)->lpVtbl -> put_ReadReceipt(This,newVal) ) 

#define IMail_LoadMessageChunk(This,newVal,pVal)	\
    ( (This)->lpVtbl -> LoadMessageChunk(This,newVal,pVal) ) 

#define IMail_get_Recipients(This,pVal)	\
    ( (This)->lpVtbl -> get_Recipients(This,pVal) ) 

#define IMail_get_Style(This,pVal)	\
    ( (This)->lpVtbl -> get_Style(This,pVal) ) 

#define IMail_put_Style(This,newVal)	\
    ( (This)->lpVtbl -> put_Style(This,newVal) ) 

#define IMail_SetAttHeader(This,Index,HeaderKey,HeaderValue)	\
    ( (This)->lpVtbl -> SetAttHeader(This,Index,HeaderKey,HeaderValue) ) 

#define IMail_get_AutoCalendar(This,pVal)	\
    ( (This)->lpVtbl -> get_AutoCalendar(This,pVal) ) 

#define IMail_put_AutoCalendar(This,newVal)	\
    ( (This)->lpVtbl -> put_AutoCalendar(This,newVal) ) 

#define IMail_get_AttachmentCount(This,pVal)	\
    ( (This)->lpVtbl -> get_AttachmentCount(This,pVal) ) 

#define IMail_get_DnsServerIP(This,pVal)	\
    ( (This)->lpVtbl -> get_DnsServerIP(This,pVal) ) 

#define IMail_put_DnsServerIP(This,newVal)	\
    ( (This)->lpVtbl -> put_DnsServerIP(This,newVal) ) 

#define IMail_SendMailToQueueEx(This,Instant,pVal)	\
    ( (This)->lpVtbl -> SendMailToQueueEx(This,Instant,pVal) ) 

#define IMail_LoadRawMessage(This,FileName,Flag,pVal)	\
    ( (This)->lpVtbl -> LoadRawMessage(This,FileName,Flag,pVal) ) 

#define IMail_get_Protocol(This,pVal)	\
    ( (This)->lpVtbl -> get_Protocol(This,pVal) ) 

#define IMail_put_Protocol(This,newVal)	\
    ( (This)->lpVtbl -> put_Protocol(This,newVal) ) 

#define IMail_get_Alias(This,pVal)	\
    ( (This)->lpVtbl -> get_Alias(This,pVal) ) 

#define IMail_put_Alias(This,newVal)	\
    ( (This)->lpVtbl -> put_Alias(This,newVal) ) 

#define IMail_get_Drafts(This,pVal)	\
    ( (This)->lpVtbl -> get_Drafts(This,pVal) ) 

#define IMail_put_Drafts(This,newVal)	\
    ( (This)->lpVtbl -> put_Drafts(This,newVal) ) 

#define IMail_get_Sender(This,pVal)	\
    ( (This)->lpVtbl -> get_Sender(This,pVal) ) 

#define IMail_put_Sender(This,newVal)	\
    ( (This)->lpVtbl -> put_Sender(This,newVal) ) 

#define IMail_Quit(This)	\
    ( (This)->lpVtbl -> Quit(This) ) 

#define IMail_Close(This)	\
    ( (This)->lpVtbl -> Close(This) ) 

#define IMail_get_HttpProxyAuthType(This,pVal)	\
    ( (This)->lpVtbl -> get_HttpProxyAuthType(This,pVal) ) 

#define IMail_put_HttpProxyAuthType(This,newVal)	\
    ( (This)->lpVtbl -> put_HttpProxyAuthType(This,newVal) ) 

#define IMail_get_SMIMERFCCompatibility(This,pVal)	\
    ( (This)->lpVtbl -> get_SMIMERFCCompatibility(This,pVal) ) 

#define IMail_put_SMIMERFCCompatibility(This,newVal)	\
    ( (This)->lpVtbl -> put_SMIMERFCCompatibility(This,newVal) ) 

#define IMail_get_PIPELINING(This,pVal)	\
    ( (This)->lpVtbl -> get_PIPELINING(This,pVal) ) 

#define IMail_put_PIPELINING(This,newVal)	\
    ( (This)->lpVtbl -> put_PIPELINING(This,newVal) ) 

#define IMail_get_IgnoreDeliveryNotificationError(This,pVal)	\
    ( (This)->lpVtbl -> get_IgnoreDeliveryNotificationError(This,pVal) ) 

#define IMail_put_IgnoreDeliveryNotificationError(This,newVal)	\
    ( (This)->lpVtbl -> put_IgnoreDeliveryNotificationError(This,newVal) ) 

#define IMail_get_IPv6Policy(This,pVal)	\
    ( (This)->lpVtbl -> get_IPv6Policy(This,pVal) ) 

#define IMail_put_IPv6Policy(This,newVal)	\
    ( (This)->lpVtbl -> put_IPv6Policy(This,newVal) ) 

#define IMail_get_LocalIP6(This,pVal)	\
    ( (This)->lpVtbl -> get_LocalIP6(This,pVal) ) 

#define IMail_put_LocalIP6(This,newVal)	\
    ( (This)->lpVtbl -> put_LocalIP6(This,newVal) ) 

#define IMail_PostToRemoteQueue(This,Instance,URL,User,Password,pVal)	\
    ( (This)->lpVtbl -> PostToRemoteQueue(This,Instance,URL,User,Password,pVal) ) 

#define IMail_get_MimeSplitor(This,pVal)	\
    ( (This)->lpVtbl -> get_MimeSplitor(This,pVal) ) 

#define IMail_put_MimeSplitor(This,newVal)	\
    ( (This)->lpVtbl -> put_MimeSplitor(This,newVal) ) 

#define IMail_get_SaveCopy(This,pVal)	\
    ( (This)->lpVtbl -> get_SaveCopy(This,pVal) ) 

#define IMail_put_SaveCopy(This,newVal)	\
    ( (This)->lpVtbl -> put_SaveCopy(This,newVal) ) 

#define IMail_get_SignatureHashAlgorithm(This,pVal)	\
    ( (This)->lpVtbl -> get_SignatureHashAlgorithm(This,pVal) ) 

#define IMail_put_SignatureHashAlgorithm(This,newVal)	\
    ( (This)->lpVtbl -> put_SignatureHashAlgorithm(This,newVal) ) 

#define IMail_get_AttachmentEncoding(This,pVal)	\
    ( (This)->lpVtbl -> get_AttachmentEncoding(This,pVal) ) 

#define IMail_put_AttachmentEncoding(This,newVal)	\
    ( (This)->lpVtbl -> put_AttachmentEncoding(This,newVal) ) 

#define IMail_get_SignatureEncryptionAlgorithm(This,pVal)	\
    ( (This)->lpVtbl -> get_SignatureEncryptionAlgorithm(This,pVal) ) 

#define IMail_put_SignatureEncryptionAlgorithm(This,newVal)	\
    ( (This)->lpVtbl -> put_SignatureEncryptionAlgorithm(This,newVal) ) 

#define IMail_get_OaepHashAlgorithm(This,pVal)	\
    ( (This)->lpVtbl -> get_OaepHashAlgorithm(This,pVal) ) 

#define IMail_put_OaepHashAlgorithm(This,newVal)	\
    ( (This)->lpVtbl -> put_OaepHashAlgorithm(This,newVal) ) 

#define IMail_put_ConnectType(This,newVal)	\
    ( (This)->lpVtbl -> put_ConnectType(This,newVal) ) 

#define IMail_get_ConnectType(This,pVal)	\
    ( (This)->lpVtbl -> get_ConnectType(This,pVal) ) 

#define IMail_put_EWSImpersonatedUser(This,newVal)	\
    ( (This)->lpVtbl -> put_EWSImpersonatedUser(This,newVal) ) 

#define IMail_get_EWSImpersonatedUser(This,pVal)	\
    ( (This)->lpVtbl -> get_EWSImpersonatedUser(This,pVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */



/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_SendMailToQueue_Proxy( 
    IMail * This,
    /* [retval][out] */ long *pVal);


void __RPC_STUB IMail_SendMailToQueue_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_NoWrapBody_Proxy( 
    IMail * This,
    /* [retval][out] */ long *pVal);


void __RPC_STUB IMail_get_NoWrapBody_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_NoWrapBody_Proxy( 
    IMail * This,
    /* [in] */ long newVal);


void __RPC_STUB IMail_put_NoWrapBody_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_EncryptionAlgorithm_Proxy( 
    IMail * This,
    /* [retval][out] */ long *pVal);


void __RPC_STUB IMail_get_EncryptionAlgorithm_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_EncryptionAlgorithm_Proxy( 
    IMail * This,
    /* [in] */ long newVal);


void __RPC_STUB IMail_put_EncryptionAlgorithm_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_ClearHeaderEx_Proxy( 
    IMail * This,
    /* [in] */ BSTR HeaderName);


void __RPC_STUB IMail_ClearHeaderEx_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_GetEmailChunk_Proxy( 
    IMail * This,
    /* [retval][out] */ VARIANT *pVal);


void __RPC_STUB IMail_GetEmailChunk_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_AddAttachmentCT_Proxy( 
    IMail * This,
    /* [in] */ BSTR FileName,
    /* [in] */ BSTR ContentType,
    /* [retval][out] */ long *pVal);


void __RPC_STUB IMail_AddAttachmentCT_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_SocksProxyServer_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_SocksProxyServer_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_SocksProxyServer_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_SocksProxyServer_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_SocksProxyUser_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_SocksProxyUser_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_SocksProxyUser_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_SocksProxyUser_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_SocksProxyPassword_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_SocksProxyPassword_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_SocksProxyPassword_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_SocksProxyPassword_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_SocksProxyPort_Proxy( 
    IMail * This,
    /* [retval][out] */ long *pVal);


void __RPC_STUB IMail_get_SocksProxyPort_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_SocksProxyPort_Proxy( 
    IMail * This,
    /* [in] */ long newVal);


void __RPC_STUB IMail_put_SocksProxyPort_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_ProxyProtocol_Proxy( 
    IMail * This,
    /* [retval][out] */ long *pVal);


void __RPC_STUB IMail_get_ProxyProtocol_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_ProxyProtocol_Proxy( 
    IMail * This,
    /* [in] */ long newVal);


void __RPC_STUB IMail_put_ProxyProtocol_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_DK_PublicKey_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_DK_PublicKey_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_LoadMessage_Proxy( 
    IMail * This,
    /* [in] */ BSTR FileName,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_LoadMessage_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_ReadReceipt_Proxy( 
    IMail * This,
    /* [retval][out] */ VARIANT_BOOL *pVal);


void __RPC_STUB IMail_get_ReadReceipt_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_ReadReceipt_Proxy( 
    IMail * This,
    /* [in] */ VARIANT_BOOL newVal);


void __RPC_STUB IMail_put_ReadReceipt_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_LoadMessageChunk_Proxy( 
    IMail * This,
    /* [in] */ VARIANT newVal,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_LoadMessageChunk_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_Recipients_Proxy( 
    IMail * This,
    /* [retval][out] */ VARIANT *pVal);


void __RPC_STUB IMail_get_Recipients_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_Style_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_Style_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_Style_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_Style_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_SetAttHeader_Proxy( 
    IMail * This,
    /* [in] */ LONG Index,
    /* [in] */ BSTR HeaderKey,
    /* [in] */ BSTR HeaderValue);


void __RPC_STUB IMail_SetAttHeader_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_AutoCalendar_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_AutoCalendar_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_AutoCalendar_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_AutoCalendar_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_AttachmentCount_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_AttachmentCount_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_DnsServerIP_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_DnsServerIP_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_DnsServerIP_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_DnsServerIP_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_SendMailToQueueEx_Proxy( 
    IMail * This,
    /* [in] */ BSTR Instant,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_SendMailToQueueEx_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_LoadRawMessage_Proxy( 
    IMail * This,
    /* [in] */ BSTR FileName,
    /* [in] */ LONG Flag,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_LoadRawMessage_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_Protocol_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_Protocol_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_Protocol_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_Protocol_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_Alias_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_Alias_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_Alias_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_Alias_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_Drafts_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_Drafts_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_Drafts_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_Drafts_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_Sender_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_Sender_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_Sender_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_Sender_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_Quit_Proxy( 
    IMail * This);


void __RPC_STUB IMail_Quit_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_Close_Proxy( 
    IMail * This);


void __RPC_STUB IMail_Close_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_HttpProxyAuthType_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_HttpProxyAuthType_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_HttpProxyAuthType_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_HttpProxyAuthType_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_SMIMERFCCompatibility_Proxy( 
    IMail * This,
    /* [retval][out] */ VARIANT_BOOL *pVal);


void __RPC_STUB IMail_get_SMIMERFCCompatibility_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_SMIMERFCCompatibility_Proxy( 
    IMail * This,
    /* [in] */ VARIANT_BOOL newVal);


void __RPC_STUB IMail_put_SMIMERFCCompatibility_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_PIPELINING_Proxy( 
    IMail * This,
    /* [retval][out] */ VARIANT_BOOL *pVal);


void __RPC_STUB IMail_get_PIPELINING_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_PIPELINING_Proxy( 
    IMail * This,
    /* [in] */ VARIANT_BOOL newVal);


void __RPC_STUB IMail_put_PIPELINING_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_IgnoreDeliveryNotificationError_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_IgnoreDeliveryNotificationError_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_IgnoreDeliveryNotificationError_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_IgnoreDeliveryNotificationError_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_IPv6Policy_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_IPv6Policy_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_IPv6Policy_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_IPv6Policy_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_LocalIP6_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_LocalIP6_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_LocalIP6_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_LocalIP6_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IMail_PostToRemoteQueue_Proxy( 
    IMail * This,
    /* [in] */ BSTR Instance,
    /* [in] */ BSTR URL,
    /* [in] */ BSTR User,
    /* [in] */ BSTR Password,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_PostToRemoteQueue_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_MimeSplitor_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_MimeSplitor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_MimeSplitor_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_MimeSplitor_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_SaveCopy_Proxy( 
    IMail * This,
    /* [retval][out] */ VARIANT_BOOL *pVal);


void __RPC_STUB IMail_get_SaveCopy_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_SaveCopy_Proxy( 
    IMail * This,
    /* [in] */ VARIANT_BOOL newVal);


void __RPC_STUB IMail_put_SaveCopy_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_SignatureHashAlgorithm_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_SignatureHashAlgorithm_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_SignatureHashAlgorithm_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_SignatureHashAlgorithm_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_AttachmentEncoding_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_AttachmentEncoding_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_AttachmentEncoding_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_AttachmentEncoding_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_SignatureEncryptionAlgorithm_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_SignatureEncryptionAlgorithm_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_SignatureEncryptionAlgorithm_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_SignatureEncryptionAlgorithm_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_OaepHashAlgorithm_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_OaepHashAlgorithm_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_OaepHashAlgorithm_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_OaepHashAlgorithm_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_ConnectType_Proxy( 
    IMail * This,
    /* [in] */ LONG newVal);


void __RPC_STUB IMail_put_ConnectType_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_ConnectType_Proxy( 
    IMail * This,
    /* [retval][out] */ LONG *pVal);


void __RPC_STUB IMail_get_ConnectType_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE IMail_put_EWSImpersonatedUser_Proxy( 
    IMail * This,
    /* [in] */ BSTR newVal);


void __RPC_STUB IMail_put_EWSImpersonatedUser_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);


/* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE IMail_get_EWSImpersonatedUser_Proxy( 
    IMail * This,
    /* [retval][out] */ BSTR *pVal);


void __RPC_STUB IMail_get_EWSImpersonatedUser_Stub(
    IRpcStubBuffer *This,
    IRpcChannelBuffer *_pRpcChannelBuffer,
    PRPC_MESSAGE _pRpcMessage,
    DWORD *_pdwStubPhase);



#endif 	/* __IMail_INTERFACE_DEFINED__ */


#ifndef __IFastSender_INTERFACE_DEFINED__
#define __IFastSender_INTERFACE_DEFINED__

/* interface IFastSender */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IFastSender;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("92298BE3-ADEC-438F-800C-CF6311A7DF1D")
    IFastSender : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Send( 
            /* [in] */ IMail *pSmtp,
            /* [in] */ long nKey,
            /* [in] */ BSTR tParam,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Test( 
            /* [in] */ IMail *pSmtp,
            /* [in] */ long nKey,
            /* [in] */ BSTR tParam,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_MaxThreads( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_MaxThreads( 
            /* [in] */ long newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetCurrentThreads( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetQueuedCount( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ClearQueuedMails( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE StopAllThreads( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetIdleThreads( 
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SendByPickup( 
            /* [in] */ BSTR PickupPath,
            /* [in] */ IMail *pSmtp,
            /* [in] */ long nKey,
            /* [in] */ BSTR tParam,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SendEmlFile( 
            /* [in] */ BSTR fileName,
            /* [in] */ BSTR senderAddr,
            /* [in] */ BSTR recipientAddrs,
            /* [in] */ long nKey,
            /* [in] */ BSTR tParam,
            /* [in] */ BSTR RegisterKey,
            /* [retval][out] */ long *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ComputerName( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_ComputerName( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LockEvent( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE UnlockEvent( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ClearAllMails( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Pause( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Resume( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_KeepConnection( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_KeepConnection( 
            /* [in] */ LONG newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_MaxMessagePerConnection( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_MaxMessagePerConnection( 
            /* [in] */ LONG newVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IFastSenderVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IFastSender * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IFastSender * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IFastSender * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IFastSender * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IFastSender * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IFastSender * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IFastSender * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Send )( 
            IFastSender * This,
            /* [in] */ IMail *pSmtp,
            /* [in] */ long nKey,
            /* [in] */ BSTR tParam,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Test )( 
            IFastSender * This,
            /* [in] */ IMail *pSmtp,
            /* [in] */ long nKey,
            /* [in] */ BSTR tParam,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_MaxThreads )( 
            IFastSender * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_MaxThreads )( 
            IFastSender * This,
            /* [in] */ long newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetCurrentThreads )( 
            IFastSender * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetQueuedCount )( 
            IFastSender * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ClearQueuedMails )( 
            IFastSender * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *StopAllThreads )( 
            IFastSender * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetIdleThreads )( 
            IFastSender * This,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SendByPickup )( 
            IFastSender * This,
            /* [in] */ BSTR PickupPath,
            /* [in] */ IMail *pSmtp,
            /* [in] */ long nKey,
            /* [in] */ BSTR tParam,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SendEmlFile )( 
            IFastSender * This,
            /* [in] */ BSTR fileName,
            /* [in] */ BSTR senderAddr,
            /* [in] */ BSTR recipientAddrs,
            /* [in] */ long nKey,
            /* [in] */ BSTR tParam,
            /* [in] */ BSTR RegisterKey,
            /* [retval][out] */ long *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ComputerName )( 
            IFastSender * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_ComputerName )( 
            IFastSender * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LockEvent )( 
            IFastSender * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *UnlockEvent )( 
            IFastSender * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ClearAllMails )( 
            IFastSender * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Pause )( 
            IFastSender * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Resume )( 
            IFastSender * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_KeepConnection )( 
            IFastSender * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_KeepConnection )( 
            IFastSender * This,
            /* [in] */ LONG newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_MaxMessagePerConnection )( 
            IFastSender * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_MaxMessagePerConnection )( 
            IFastSender * This,
            /* [in] */ LONG newVal);
        
        END_INTERFACE
    } IFastSenderVtbl;

    interface IFastSender
    {
        CONST_VTBL struct IFastSenderVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IFastSender_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IFastSender_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IFastSender_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IFastSender_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IFastSender_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IFastSender_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IFastSender_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IFastSender_Send(This,pSmtp,nKey,tParam,pVal)	\
    ( (This)->lpVtbl -> Send(This,pSmtp,nKey,tParam,pVal) ) 

#define IFastSender_Test(This,pSmtp,nKey,tParam,pVal)	\
    ( (This)->lpVtbl -> Test(This,pSmtp,nKey,tParam,pVal) ) 

#define IFastSender_get_MaxThreads(This,pVal)	\
    ( (This)->lpVtbl -> get_MaxThreads(This,pVal) ) 

#define IFastSender_put_MaxThreads(This,newVal)	\
    ( (This)->lpVtbl -> put_MaxThreads(This,newVal) ) 

#define IFastSender_GetCurrentThreads(This,pVal)	\
    ( (This)->lpVtbl -> GetCurrentThreads(This,pVal) ) 

#define IFastSender_GetQueuedCount(This,pVal)	\
    ( (This)->lpVtbl -> GetQueuedCount(This,pVal) ) 

#define IFastSender_ClearQueuedMails(This)	\
    ( (This)->lpVtbl -> ClearQueuedMails(This) ) 

#define IFastSender_StopAllThreads(This)	\
    ( (This)->lpVtbl -> StopAllThreads(This) ) 

#define IFastSender_GetIdleThreads(This,pVal)	\
    ( (This)->lpVtbl -> GetIdleThreads(This,pVal) ) 

#define IFastSender_SendByPickup(This,PickupPath,pSmtp,nKey,tParam,pVal)	\
    ( (This)->lpVtbl -> SendByPickup(This,PickupPath,pSmtp,nKey,tParam,pVal) ) 

#define IFastSender_SendEmlFile(This,fileName,senderAddr,recipientAddrs,nKey,tParam,RegisterKey,pVal)	\
    ( (This)->lpVtbl -> SendEmlFile(This,fileName,senderAddr,recipientAddrs,nKey,tParam,RegisterKey,pVal) ) 

#define IFastSender_get_ComputerName(This,pVal)	\
    ( (This)->lpVtbl -> get_ComputerName(This,pVal) ) 

#define IFastSender_put_ComputerName(This,newVal)	\
    ( (This)->lpVtbl -> put_ComputerName(This,newVal) ) 

#define IFastSender_LockEvent(This)	\
    ( (This)->lpVtbl -> LockEvent(This) ) 

#define IFastSender_UnlockEvent(This)	\
    ( (This)->lpVtbl -> UnlockEvent(This) ) 

#define IFastSender_ClearAllMails(This)	\
    ( (This)->lpVtbl -> ClearAllMails(This) ) 

#define IFastSender_Pause(This)	\
    ( (This)->lpVtbl -> Pause(This) ) 

#define IFastSender_Resume(This)	\
    ( (This)->lpVtbl -> Resume(This) ) 

#define IFastSender_get_KeepConnection(This,pVal)	\
    ( (This)->lpVtbl -> get_KeepConnection(This,pVal) ) 

#define IFastSender_put_KeepConnection(This,newVal)	\
    ( (This)->lpVtbl -> put_KeepConnection(This,newVal) ) 

#define IFastSender_get_MaxMessagePerConnection(This,pVal)	\
    ( (This)->lpVtbl -> get_MaxMessagePerConnection(This,pVal) ) 

#define IFastSender_put_MaxMessagePerConnection(This,newVal)	\
    ( (This)->lpVtbl -> put_MaxMessagePerConnection(This,newVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IFastSender_INTERFACE_DEFINED__ */


#ifndef __ICertificate_INTERFACE_DEFINED__
#define __ICertificate_INTERFACE_DEFINED__

/* interface ICertificate */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ICertificate;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A2809780-C98E-4C6D-A552-DAB146D4AD12")
    ICertificate : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FindSubject( 
            /* [in] */ BSTR FindKey,
            /* [in] */ LONG StoreLocation,
            /* [in] */ BSTR StoreName,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LoadPFX( 
            /* [in] */ VARIANT PFXContent,
            /* [in] */ BSTR Password,
            /* [in] */ LONG KeyLocation,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LoadPFXFromFile( 
            /* [in] */ BSTR PFXFile,
            /* [in] */ BSTR Password,
            /* [in] */ LONG KeyLocation,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LoadCert( 
            /* [in] */ VARIANT CERTContent,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE LoadCertFromFile( 
            /* [in] */ BSTR CERTFile,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Unload( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_HasCertificate( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [restricted][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Store( 
            /* [retval][out] */ ULONGLONG *pVal) = 0;
        
        virtual /* [restricted][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Store( 
            /* [in] */ ULONGLONG newVal) = 0;
        
        virtual /* [restricted][helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Handle( 
            /* [retval][out] */ ULONGLONG *pVal) = 0;
        
        virtual /* [restricted][helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Handle( 
            /* [in] */ ULONGLONG newVal) = 0;
        
        virtual /* [restricted][helpstring][id] */ HRESULT STDMETHODCALLTYPE SignMessage( 
            /* [in] */ VARIANT Content,
            /* [in] */ LONG SignatureHashAlgorithm,
            /* [retval][out] */ VARIANT *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_HasPrivateKey( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetLastError( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Issuer( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Issuer( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_PublicKey( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_PublicKey( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Subject( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_Subject( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_NotAfter( 
            /* [retval][out] */ DATE *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_NotAfter( 
            /* [in] */ DATE newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_NotBefore( 
            /* [retval][out] */ DATE *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_NotBefore( 
            /* [in] */ DATE newVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_SerialNumber( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propput] */ HRESULT STDMETHODCALLTYPE put_SerialNumber( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE FindCertificates( 
            /* [in] */ BSTR FindKey,
            /* [in] */ LONG StoreLocation,
            /* [in] */ BSTR StoreName,
            /* [retval][out] */ ICertificateCollection **pVal) = 0;
        
        virtual /* [restricted][helpstring][id] */ HRESULT STDMETHODCALLTYPE SignMessageEx( 
            /* [in] */ VARIANT Content,
            /* [in] */ LONG SignatureHashAlgorithm,
            /* [in] */ LONG SignatureHashEncryption,
            /* [retval][out] */ VARIANT *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Base64Thumbprint( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Thumbprint( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICertificateVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ICertificate * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ICertificate * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ICertificate * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ICertificate * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ICertificate * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ICertificate * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ICertificate * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *FindSubject )( 
            ICertificate * This,
            /* [in] */ BSTR FindKey,
            /* [in] */ LONG StoreLocation,
            /* [in] */ BSTR StoreName,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LoadPFX )( 
            ICertificate * This,
            /* [in] */ VARIANT PFXContent,
            /* [in] */ BSTR Password,
            /* [in] */ LONG KeyLocation,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LoadPFXFromFile )( 
            ICertificate * This,
            /* [in] */ BSTR PFXFile,
            /* [in] */ BSTR Password,
            /* [in] */ LONG KeyLocation,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LoadCert )( 
            ICertificate * This,
            /* [in] */ VARIANT CERTContent,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *LoadCertFromFile )( 
            ICertificate * This,
            /* [in] */ BSTR CERTFile,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Unload )( 
            ICertificate * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_HasCertificate )( 
            ICertificate * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [restricted][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Store )( 
            ICertificate * This,
            /* [retval][out] */ ULONGLONG *pVal);
        
        /* [restricted][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Store )( 
            ICertificate * This,
            /* [in] */ ULONGLONG newVal);
        
        /* [restricted][helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Handle )( 
            ICertificate * This,
            /* [retval][out] */ ULONGLONG *pVal);
        
        /* [restricted][helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Handle )( 
            ICertificate * This,
            /* [in] */ ULONGLONG newVal);
        
        /* [restricted][helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SignMessage )( 
            ICertificate * This,
            /* [in] */ VARIANT Content,
            /* [in] */ LONG SignatureHashAlgorithm,
            /* [retval][out] */ VARIANT *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_HasPrivateKey )( 
            ICertificate * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetLastError )( 
            ICertificate * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Issuer )( 
            ICertificate * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Issuer )( 
            ICertificate * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_PublicKey )( 
            ICertificate * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_PublicKey )( 
            ICertificate * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Subject )( 
            ICertificate * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_Subject )( 
            ICertificate * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_NotAfter )( 
            ICertificate * This,
            /* [retval][out] */ DATE *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_NotAfter )( 
            ICertificate * This,
            /* [in] */ DATE newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_NotBefore )( 
            ICertificate * This,
            /* [retval][out] */ DATE *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_NotBefore )( 
            ICertificate * This,
            /* [in] */ DATE newVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_SerialNumber )( 
            ICertificate * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propput] */ HRESULT ( STDMETHODCALLTYPE *put_SerialNumber )( 
            ICertificate * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *FindCertificates )( 
            ICertificate * This,
            /* [in] */ BSTR FindKey,
            /* [in] */ LONG StoreLocation,
            /* [in] */ BSTR StoreName,
            /* [retval][out] */ ICertificateCollection **pVal);
        
        /* [restricted][helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SignMessageEx )( 
            ICertificate * This,
            /* [in] */ VARIANT Content,
            /* [in] */ LONG SignatureHashAlgorithm,
            /* [in] */ LONG SignatureHashEncryption,
            /* [retval][out] */ VARIANT *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Base64Thumbprint )( 
            ICertificate * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Thumbprint )( 
            ICertificate * This,
            /* [retval][out] */ BSTR *pVal);
        
        END_INTERFACE
    } ICertificateVtbl;

    interface ICertificate
    {
        CONST_VTBL struct ICertificateVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICertificate_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ICertificate_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ICertificate_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ICertificate_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ICertificate_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ICertificate_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ICertificate_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define ICertificate_FindSubject(This,FindKey,StoreLocation,StoreName,pVal)	\
    ( (This)->lpVtbl -> FindSubject(This,FindKey,StoreLocation,StoreName,pVal) ) 

#define ICertificate_LoadPFX(This,PFXContent,Password,KeyLocation,pVal)	\
    ( (This)->lpVtbl -> LoadPFX(This,PFXContent,Password,KeyLocation,pVal) ) 

#define ICertificate_LoadPFXFromFile(This,PFXFile,Password,KeyLocation,pVal)	\
    ( (This)->lpVtbl -> LoadPFXFromFile(This,PFXFile,Password,KeyLocation,pVal) ) 

#define ICertificate_LoadCert(This,CERTContent,pVal)	\
    ( (This)->lpVtbl -> LoadCert(This,CERTContent,pVal) ) 

#define ICertificate_LoadCertFromFile(This,CERTFile,pVal)	\
    ( (This)->lpVtbl -> LoadCertFromFile(This,CERTFile,pVal) ) 

#define ICertificate_Unload(This)	\
    ( (This)->lpVtbl -> Unload(This) ) 

#define ICertificate_get_HasCertificate(This,pVal)	\
    ( (This)->lpVtbl -> get_HasCertificate(This,pVal) ) 

#define ICertificate_get_Store(This,pVal)	\
    ( (This)->lpVtbl -> get_Store(This,pVal) ) 

#define ICertificate_put_Store(This,newVal)	\
    ( (This)->lpVtbl -> put_Store(This,newVal) ) 

#define ICertificate_get_Handle(This,pVal)	\
    ( (This)->lpVtbl -> get_Handle(This,pVal) ) 

#define ICertificate_put_Handle(This,newVal)	\
    ( (This)->lpVtbl -> put_Handle(This,newVal) ) 

#define ICertificate_SignMessage(This,Content,SignatureHashAlgorithm,pVal)	\
    ( (This)->lpVtbl -> SignMessage(This,Content,SignatureHashAlgorithm,pVal) ) 

#define ICertificate_get_HasPrivateKey(This,pVal)	\
    ( (This)->lpVtbl -> get_HasPrivateKey(This,pVal) ) 

#define ICertificate_GetLastError(This,pVal)	\
    ( (This)->lpVtbl -> GetLastError(This,pVal) ) 

#define ICertificate_get_Issuer(This,pVal)	\
    ( (This)->lpVtbl -> get_Issuer(This,pVal) ) 

#define ICertificate_put_Issuer(This,newVal)	\
    ( (This)->lpVtbl -> put_Issuer(This,newVal) ) 

#define ICertificate_get_PublicKey(This,pVal)	\
    ( (This)->lpVtbl -> get_PublicKey(This,pVal) ) 

#define ICertificate_put_PublicKey(This,newVal)	\
    ( (This)->lpVtbl -> put_PublicKey(This,newVal) ) 

#define ICertificate_get_Subject(This,pVal)	\
    ( (This)->lpVtbl -> get_Subject(This,pVal) ) 

#define ICertificate_put_Subject(This,newVal)	\
    ( (This)->lpVtbl -> put_Subject(This,newVal) ) 

#define ICertificate_get_NotAfter(This,pVal)	\
    ( (This)->lpVtbl -> get_NotAfter(This,pVal) ) 

#define ICertificate_put_NotAfter(This,newVal)	\
    ( (This)->lpVtbl -> put_NotAfter(This,newVal) ) 

#define ICertificate_get_NotBefore(This,pVal)	\
    ( (This)->lpVtbl -> get_NotBefore(This,pVal) ) 

#define ICertificate_put_NotBefore(This,newVal)	\
    ( (This)->lpVtbl -> put_NotBefore(This,newVal) ) 

#define ICertificate_get_SerialNumber(This,pVal)	\
    ( (This)->lpVtbl -> get_SerialNumber(This,pVal) ) 

#define ICertificate_put_SerialNumber(This,newVal)	\
    ( (This)->lpVtbl -> put_SerialNumber(This,newVal) ) 

#define ICertificate_FindCertificates(This,FindKey,StoreLocation,StoreName,pVal)	\
    ( (This)->lpVtbl -> FindCertificates(This,FindKey,StoreLocation,StoreName,pVal) ) 

#define ICertificate_SignMessageEx(This,Content,SignatureHashAlgorithm,SignatureHashEncryption,pVal)	\
    ( (This)->lpVtbl -> SignMessageEx(This,Content,SignatureHashAlgorithm,SignatureHashEncryption,pVal) ) 

#define ICertificate_get_Base64Thumbprint(This,pVal)	\
    ( (This)->lpVtbl -> get_Base64Thumbprint(This,pVal) ) 

#define ICertificate_get_Thumbprint(This,pVal)	\
    ( (This)->lpVtbl -> get_Thumbprint(This,pVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ICertificate_INTERFACE_DEFINED__ */


#ifndef __ICertificateCollection_INTERFACE_DEFINED__
#define __ICertificateCollection_INTERFACE_DEFINED__

/* interface ICertificateCollection */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ICertificateCollection;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("DC8D5635-B8E7-441E-B550-CE1BF3BA5C55")
    ICertificateCollection : public IDispatch
    {
    public:
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Count( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Item( 
            /* [in] */ LONG Index,
            /* [retval][out] */ ICertificate **pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Add( 
            /* [in] */ ICertificate *oCert) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Insert( 
            /* [in] */ LONG Index,
            /* [in] */ ICertificate *oCert) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Clear( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE RemoveAt( 
            /* [in] */ LONG Index) = 0;
        
        virtual /* [restricted][helpstring][id] */ HRESULT STDMETHODCALLTYPE EncryptMessage( 
            /* [in] */ LONG EncryptionAlgorithm,
            /* [in] */ VARIANT Content,
            /* [retval][out] */ VARIANT *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_HasEncryptCert( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetLastError( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [restricted][helpstring][id] */ HRESULT STDMETHODCALLTYPE EncryptMessageEx( 
            /* [in] */ LONG EncryptionAlgorithm,
            /* [in] */ LONG OaepHashAlgorithm,
            /* [in] */ VARIANT Content,
            /* [retval][out] */ VARIANT *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ICertificateCollectionVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ICertificateCollection * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ICertificateCollection * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ICertificateCollection * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ICertificateCollection * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ICertificateCollection * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ICertificateCollection * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ICertificateCollection * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Count )( 
            ICertificateCollection * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Item )( 
            ICertificateCollection * This,
            /* [in] */ LONG Index,
            /* [retval][out] */ ICertificate **pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Add )( 
            ICertificateCollection * This,
            /* [in] */ ICertificate *oCert);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Insert )( 
            ICertificateCollection * This,
            /* [in] */ LONG Index,
            /* [in] */ ICertificate *oCert);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Clear )( 
            ICertificateCollection * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *RemoveAt )( 
            ICertificateCollection * This,
            /* [in] */ LONG Index);
        
        /* [restricted][helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *EncryptMessage )( 
            ICertificateCollection * This,
            /* [in] */ LONG EncryptionAlgorithm,
            /* [in] */ VARIANT Content,
            /* [retval][out] */ VARIANT *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_HasEncryptCert )( 
            ICertificateCollection * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetLastError )( 
            ICertificateCollection * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [restricted][helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *EncryptMessageEx )( 
            ICertificateCollection * This,
            /* [in] */ LONG EncryptionAlgorithm,
            /* [in] */ LONG OaepHashAlgorithm,
            /* [in] */ VARIANT Content,
            /* [retval][out] */ VARIANT *pVal);
        
        END_INTERFACE
    } ICertificateCollectionVtbl;

    interface ICertificateCollection
    {
        CONST_VTBL struct ICertificateCollectionVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ICertificateCollection_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ICertificateCollection_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ICertificateCollection_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ICertificateCollection_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ICertificateCollection_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ICertificateCollection_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ICertificateCollection_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define ICertificateCollection_get_Count(This,pVal)	\
    ( (This)->lpVtbl -> get_Count(This,pVal) ) 

#define ICertificateCollection_Item(This,Index,pVal)	\
    ( (This)->lpVtbl -> Item(This,Index,pVal) ) 

#define ICertificateCollection_Add(This,oCert)	\
    ( (This)->lpVtbl -> Add(This,oCert) ) 

#define ICertificateCollection_Insert(This,Index,oCert)	\
    ( (This)->lpVtbl -> Insert(This,Index,oCert) ) 

#define ICertificateCollection_Clear(This)	\
    ( (This)->lpVtbl -> Clear(This) ) 

#define ICertificateCollection_RemoveAt(This,Index)	\
    ( (This)->lpVtbl -> RemoveAt(This,Index) ) 

#define ICertificateCollection_EncryptMessage(This,EncryptionAlgorithm,Content,pVal)	\
    ( (This)->lpVtbl -> EncryptMessage(This,EncryptionAlgorithm,Content,pVal) ) 

#define ICertificateCollection_get_HasEncryptCert(This,pVal)	\
    ( (This)->lpVtbl -> get_HasEncryptCert(This,pVal) ) 

#define ICertificateCollection_GetLastError(This,pVal)	\
    ( (This)->lpVtbl -> GetLastError(This,pVal) ) 

#define ICertificateCollection_EncryptMessageEx(This,EncryptionAlgorithm,OaepHashAlgorithm,Content,pVal)	\
    ( (This)->lpVtbl -> EncryptMessageEx(This,EncryptionAlgorithm,OaepHashAlgorithm,Content,pVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ICertificateCollection_INTERFACE_DEFINED__ */


#ifndef __ISimpleJsonArray_INTERFACE_DEFINED__
#define __ISimpleJsonArray_INTERFACE_DEFINED__

/* interface ISimpleJsonArray */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISimpleJsonArray;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("B62298B7-A091-4A42-8D50-B7F0194DE25A")
    ISimpleJsonArray : public IDispatch
    {
    public:
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Length( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_Count( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Add( 
            /* [in] */ BSTR newVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE RemoveAt( 
            /* [in] */ LONG Index) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Clear( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Item( 
            /* [in] */ LONG Index,
            /* [retval][out] */ BSTR *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISimpleJsonArrayVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISimpleJsonArray * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISimpleJsonArray * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISimpleJsonArray * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISimpleJsonArray * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISimpleJsonArray * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISimpleJsonArray * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISimpleJsonArray * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Length )( 
            ISimpleJsonArray * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_Count )( 
            ISimpleJsonArray * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Add )( 
            ISimpleJsonArray * This,
            /* [in] */ BSTR newVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *RemoveAt )( 
            ISimpleJsonArray * This,
            /* [in] */ LONG Index);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Clear )( 
            ISimpleJsonArray * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Item )( 
            ISimpleJsonArray * This,
            /* [in] */ LONG Index,
            /* [retval][out] */ BSTR *pVal);
        
        END_INTERFACE
    } ISimpleJsonArrayVtbl;

    interface ISimpleJsonArray
    {
        CONST_VTBL struct ISimpleJsonArrayVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISimpleJsonArray_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISimpleJsonArray_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISimpleJsonArray_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISimpleJsonArray_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ISimpleJsonArray_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ISimpleJsonArray_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ISimpleJsonArray_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define ISimpleJsonArray_get_Length(This,pVal)	\
    ( (This)->lpVtbl -> get_Length(This,pVal) ) 

#define ISimpleJsonArray_get_Count(This,pVal)	\
    ( (This)->lpVtbl -> get_Count(This,pVal) ) 

#define ISimpleJsonArray_Add(This,newVal)	\
    ( (This)->lpVtbl -> Add(This,newVal) ) 

#define ISimpleJsonArray_RemoveAt(This,Index)	\
    ( (This)->lpVtbl -> RemoveAt(This,Index) ) 

#define ISimpleJsonArray_Clear(This)	\
    ( (This)->lpVtbl -> Clear(This) ) 

#define ISimpleJsonArray_Item(This,Index,pVal)	\
    ( (This)->lpVtbl -> Item(This,Index,pVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISimpleJsonArray_INTERFACE_DEFINED__ */


#ifndef __ISimpleJsonParser_INTERFACE_DEFINED__
#define __ISimpleJsonParser_INTERFACE_DEFINED__

/* interface ISimpleJsonParser */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_ISimpleJsonParser;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("08088DBB-A031-4DCE-A4DF-7683CBE706AF")
    ISimpleJsonParser : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetJsonValue( 
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Key,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ParseArray( 
            /* [in] */ BSTR Source,
            /* [retval][out] */ ISimpleJsonArray **pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Trim( 
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Trimer,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetStringValue( 
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Key,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetIntValue( 
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Key,
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetBoolValue( 
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Key,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE JwtBase64UrlEncode( 
            /* [in] */ BSTR Input,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE JwtBase64UrlDecode( 
            /* [in] */ BSTR Input,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SignRs256( 
            /* [in] */ ICertificate *PfxCertificate,
            /* [in] */ BSTR Playload,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SignRs256WithPrivateKey( 
            /* [in] */ BSTR RsaPrivateKey,
            /* [in] */ BSTR Playload,
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetCurrentIAT( 
            /* [retval][out] */ LONG *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct ISimpleJsonParserVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            ISimpleJsonParser * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            ISimpleJsonParser * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            ISimpleJsonParser * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            ISimpleJsonParser * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            ISimpleJsonParser * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            ISimpleJsonParser * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            ISimpleJsonParser * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetJsonValue )( 
            ISimpleJsonParser * This,
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Key,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ParseArray )( 
            ISimpleJsonParser * This,
            /* [in] */ BSTR Source,
            /* [retval][out] */ ISimpleJsonArray **pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Trim )( 
            ISimpleJsonParser * This,
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Trimer,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetStringValue )( 
            ISimpleJsonParser * This,
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Key,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetIntValue )( 
            ISimpleJsonParser * This,
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Key,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetBoolValue )( 
            ISimpleJsonParser * This,
            /* [in] */ BSTR Source,
            /* [in] */ BSTR Key,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *JwtBase64UrlEncode )( 
            ISimpleJsonParser * This,
            /* [in] */ BSTR Input,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *JwtBase64UrlDecode )( 
            ISimpleJsonParser * This,
            /* [in] */ BSTR Input,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SignRs256 )( 
            ISimpleJsonParser * This,
            /* [in] */ ICertificate *PfxCertificate,
            /* [in] */ BSTR Playload,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SignRs256WithPrivateKey )( 
            ISimpleJsonParser * This,
            /* [in] */ BSTR RsaPrivateKey,
            /* [in] */ BSTR Playload,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetCurrentIAT )( 
            ISimpleJsonParser * This,
            /* [retval][out] */ LONG *pVal);
        
        END_INTERFACE
    } ISimpleJsonParserVtbl;

    interface ISimpleJsonParser
    {
        CONST_VTBL struct ISimpleJsonParserVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISimpleJsonParser_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISimpleJsonParser_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISimpleJsonParser_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISimpleJsonParser_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define ISimpleJsonParser_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define ISimpleJsonParser_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define ISimpleJsonParser_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define ISimpleJsonParser_GetJsonValue(This,Source,Key,pVal)	\
    ( (This)->lpVtbl -> GetJsonValue(This,Source,Key,pVal) ) 

#define ISimpleJsonParser_ParseArray(This,Source,pVal)	\
    ( (This)->lpVtbl -> ParseArray(This,Source,pVal) ) 

#define ISimpleJsonParser_Trim(This,Source,Trimer,pVal)	\
    ( (This)->lpVtbl -> Trim(This,Source,Trimer,pVal) ) 

#define ISimpleJsonParser_GetStringValue(This,Source,Key,pVal)	\
    ( (This)->lpVtbl -> GetStringValue(This,Source,Key,pVal) ) 

#define ISimpleJsonParser_GetIntValue(This,Source,Key,pVal)	\
    ( (This)->lpVtbl -> GetIntValue(This,Source,Key,pVal) ) 

#define ISimpleJsonParser_GetBoolValue(This,Source,Key,pVal)	\
    ( (This)->lpVtbl -> GetBoolValue(This,Source,Key,pVal) ) 

#define ISimpleJsonParser_JwtBase64UrlEncode(This,Input,pVal)	\
    ( (This)->lpVtbl -> JwtBase64UrlEncode(This,Input,pVal) ) 

#define ISimpleJsonParser_JwtBase64UrlDecode(This,Input,pVal)	\
    ( (This)->lpVtbl -> JwtBase64UrlDecode(This,Input,pVal) ) 

#define ISimpleJsonParser_SignRs256(This,PfxCertificate,Playload,pVal)	\
    ( (This)->lpVtbl -> SignRs256(This,PfxCertificate,Playload,pVal) ) 

#define ISimpleJsonParser_SignRs256WithPrivateKey(This,RsaPrivateKey,Playload,pVal)	\
    ( (This)->lpVtbl -> SignRs256WithPrivateKey(This,RsaPrivateKey,Playload,pVal) ) 

#define ISimpleJsonParser_GetCurrentIAT(This,pVal)	\
    ( (This)->lpVtbl -> GetCurrentIAT(This,pVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISimpleJsonParser_INTERFACE_DEFINED__ */


#ifndef __IBrowserUi_INTERFACE_DEFINED__
#define __IBrowserUi_INTERFACE_DEFINED__

/* interface IBrowserUi */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IBrowserUi;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("217785e8-1188-4fdd-bbb8-1eb104d32151")
    IBrowserUi : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE OpenUrl( 
            /* [in] */ BSTR Url,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE OpenIEObject( 
            /* [in] */ BSTR Url,
            /* [in] */ LONG Left,
            /* [in] */ LONG Top,
            /* [in] */ LONG Height,
            /* [in] */ LONG Width,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetLastError( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE OpenIE( 
            /* [in] */ BSTR Url,
            /* [in] */ VARIANT_BOOL IsPrivate,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetIEVersion( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ClosePrivateIE( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE IsPrivateIEClosed( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IBrowserUiVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IBrowserUi * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IBrowserUi * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IBrowserUi * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IBrowserUi * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IBrowserUi * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IBrowserUi * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IBrowserUi * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *OpenUrl )( 
            IBrowserUi * This,
            /* [in] */ BSTR Url,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *OpenIEObject )( 
            IBrowserUi * This,
            /* [in] */ BSTR Url,
            /* [in] */ LONG Left,
            /* [in] */ LONG Top,
            /* [in] */ LONG Height,
            /* [in] */ LONG Width,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetLastError )( 
            IBrowserUi * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *OpenIE )( 
            IBrowserUi * This,
            /* [in] */ BSTR Url,
            /* [in] */ VARIANT_BOOL IsPrivate,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetIEVersion )( 
            IBrowserUi * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ClosePrivateIE )( 
            IBrowserUi * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *IsPrivateIEClosed )( 
            IBrowserUi * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        END_INTERFACE
    } IBrowserUiVtbl;

    interface IBrowserUi
    {
        CONST_VTBL struct IBrowserUiVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IBrowserUi_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IBrowserUi_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IBrowserUi_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IBrowserUi_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IBrowserUi_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IBrowserUi_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IBrowserUi_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IBrowserUi_OpenUrl(This,Url,pVal)	\
    ( (This)->lpVtbl -> OpenUrl(This,Url,pVal) ) 

#define IBrowserUi_OpenIEObject(This,Url,Left,Top,Height,Width,pVal)	\
    ( (This)->lpVtbl -> OpenIEObject(This,Url,Left,Top,Height,Width,pVal) ) 

#define IBrowserUi_GetLastError(This,pVal)	\
    ( (This)->lpVtbl -> GetLastError(This,pVal) ) 

#define IBrowserUi_OpenIE(This,Url,IsPrivate,pVal)	\
    ( (This)->lpVtbl -> OpenIE(This,Url,IsPrivate,pVal) ) 

#define IBrowserUi_GetIEVersion(This,pVal)	\
    ( (This)->lpVtbl -> GetIEVersion(This,pVal) ) 

#define IBrowserUi_ClosePrivateIE(This)	\
    ( (This)->lpVtbl -> ClosePrivateIE(This) ) 

#define IBrowserUi_IsPrivateIEClosed(This,pVal)	\
    ( (This)->lpVtbl -> IsPrivateIEClosed(This,pVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IBrowserUi_INTERFACE_DEFINED__ */


#ifndef __IHttpListener_INTERFACE_DEFINED__
#define __IHttpListener_INTERFACE_DEFINED__

/* interface IHttpListener */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IHttpListener;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("011e1175-8ac1-40d5-bc61-903999faac8d")
    IHttpListener : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Create( 
            /* [in] */ BSTR IPv4Address,
            /* [in] */ LONG Port,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetRequestUrl( 
            /* [in] */ LONG Timeout,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE SendResponse( 
            /* [in] */ BSTR ResponseCode,
            /* [in] */ BSTR ContentType,
            /* [in] */ BSTR Content,
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Close( void) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_RequestUrl( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_ListenPort( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE GetLastError( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE ClearUrl( void) = 0;
        
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE BeginGetRequestUrl( 
            /* [retval][out] */ VARIANT_BOOL *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IHttpListenerVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IHttpListener * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IHttpListener * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IHttpListener * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IHttpListener * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IHttpListener * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IHttpListener * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IHttpListener * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Create )( 
            IHttpListener * This,
            /* [in] */ BSTR IPv4Address,
            /* [in] */ LONG Port,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetRequestUrl )( 
            IHttpListener * This,
            /* [in] */ LONG Timeout,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *SendResponse )( 
            IHttpListener * This,
            /* [in] */ BSTR ResponseCode,
            /* [in] */ BSTR ContentType,
            /* [in] */ BSTR Content,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Close )( 
            IHttpListener * This);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_RequestUrl )( 
            IHttpListener * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_ListenPort )( 
            IHttpListener * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *GetLastError )( 
            IHttpListener * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *ClearUrl )( 
            IHttpListener * This);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *BeginGetRequestUrl )( 
            IHttpListener * This,
            /* [retval][out] */ VARIANT_BOOL *pVal);
        
        END_INTERFACE
    } IHttpListenerVtbl;

    interface IHttpListener
    {
        CONST_VTBL struct IHttpListenerVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IHttpListener_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IHttpListener_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IHttpListener_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IHttpListener_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IHttpListener_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IHttpListener_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IHttpListener_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IHttpListener_Create(This,IPv4Address,Port,pVal)	\
    ( (This)->lpVtbl -> Create(This,IPv4Address,Port,pVal) ) 

#define IHttpListener_GetRequestUrl(This,Timeout,pVal)	\
    ( (This)->lpVtbl -> GetRequestUrl(This,Timeout,pVal) ) 

#define IHttpListener_SendResponse(This,ResponseCode,ContentType,Content,pVal)	\
    ( (This)->lpVtbl -> SendResponse(This,ResponseCode,ContentType,Content,pVal) ) 

#define IHttpListener_Close(This)	\
    ( (This)->lpVtbl -> Close(This) ) 

#define IHttpListener_get_RequestUrl(This,pVal)	\
    ( (This)->lpVtbl -> get_RequestUrl(This,pVal) ) 

#define IHttpListener_get_ListenPort(This,pVal)	\
    ( (This)->lpVtbl -> get_ListenPort(This,pVal) ) 

#define IHttpListener_GetLastError(This,pVal)	\
    ( (This)->lpVtbl -> GetLastError(This,pVal) ) 

#define IHttpListener_ClearUrl(This)	\
    ( (This)->lpVtbl -> ClearUrl(This) ) 

#define IHttpListener_BeginGetRequestUrl(This,pVal)	\
    ( (This)->lpVtbl -> BeginGetRequestUrl(This,pVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IHttpListener_INTERFACE_DEFINED__ */


#ifndef ___IMailEvents_DISPINTERFACE_DEFINED__
#define ___IMailEvents_DISPINTERFACE_DEFINED__

/* dispinterface _IMailEvents */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__IMailEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("68CB8B02-D4AA-4A16-97A0-6B9488F98189")
    _IMailEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _IMailEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _IMailEvents * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _IMailEvents * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _IMailEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _IMailEvents * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _IMailEvents * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _IMailEvents * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _IMailEvents * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } _IMailEventsVtbl;

    interface _IMailEvents
    {
        CONST_VTBL struct _IMailEventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _IMailEvents_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _IMailEvents_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _IMailEvents_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _IMailEvents_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _IMailEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _IMailEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _IMailEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___IMailEvents_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_Mail;

#ifdef __cplusplus

class DECLSPEC_UUID("DF8A4FE2-221A-4504-987A-3FD4720DB929")
Mail;
#endif

#ifndef ___IFastSenderEvents_DISPINTERFACE_DEFINED__
#define ___IFastSenderEvents_DISPINTERFACE_DEFINED__

/* dispinterface _IFastSenderEvents */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__IFastSenderEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("A1B45F08-67E7-4276-A7CA-7664C08F9EF7")
    _IFastSenderEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _IFastSenderEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _IFastSenderEvents * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _IFastSenderEvents * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _IFastSenderEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _IFastSenderEvents * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _IFastSenderEvents * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _IFastSenderEvents * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _IFastSenderEvents * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } _IFastSenderEventsVtbl;

    interface _IFastSenderEvents
    {
        CONST_VTBL struct _IFastSenderEventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _IFastSenderEvents_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _IFastSenderEvents_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _IFastSenderEvents_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _IFastSenderEvents_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _IFastSenderEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _IFastSenderEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _IFastSenderEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___IFastSenderEvents_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_FastSender;

#ifdef __cplusplus

class DECLSPEC_UUID("FF80631D-E750-4C67-AFC3-5170AB72518B")
FastSender;
#endif

EXTERN_C const CLSID CLSID_Certificate;

#ifdef __cplusplus

class DECLSPEC_UUID("EAFC4EAA-9390-492A-8E53-E179527780F6")
Certificate;
#endif

EXTERN_C const CLSID CLSID_CertificateCollection;

#ifdef __cplusplus

class DECLSPEC_UUID("036C2F8C-8D3C-4F4B-9B36-3B6F1D29C0B4")
CertificateCollection;
#endif

EXTERN_C const CLSID CLSID_SimpleJsonParser;

#ifdef __cplusplus

class DECLSPEC_UUID("DD6B3C53-1871-4ADF-9C71-24B682012371")
SimpleJsonParser;
#endif

EXTERN_C const CLSID CLSID_SimpleJsonArray;

#ifdef __cplusplus

class DECLSPEC_UUID("6C589C71-6FDC-4859-A9CD-F3A7EA2206D0")
SimpleJsonArray;
#endif

EXTERN_C const CLSID CLSID_OAuthResponseParser;

#ifdef __cplusplus

class DECLSPEC_UUID("60534020-0887-486d-b6b7-0f177d43c00c")
OAuthResponseParser;
#endif

EXTERN_C const CLSID CLSID_BrowserUi;

#ifdef __cplusplus

class DECLSPEC_UUID("539c9dbc-57d4-4acb-8a91-329a2cc6270e")
BrowserUi;
#endif

#ifndef ___IHttpListenerEvents_DISPINTERFACE_DEFINED__
#define ___IHttpListenerEvents_DISPINTERFACE_DEFINED__

/* dispinterface _IHttpListenerEvents */
/* [helpstring][uuid] */ 


EXTERN_C const IID DIID__IHttpListenerEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)

    MIDL_INTERFACE("A386C7CB-8547-451E-A7DC-CD74BA675247")
    _IHttpListenerEvents : public IDispatch
    {
    };
    
#else 	/* C style interface */

    typedef struct _IHttpListenerEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            _IHttpListenerEvents * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            _IHttpListenerEvents * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            _IHttpListenerEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            _IHttpListenerEvents * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            _IHttpListenerEvents * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            _IHttpListenerEvents * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            _IHttpListenerEvents * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        END_INTERFACE
    } _IHttpListenerEventsVtbl;

    interface _IHttpListenerEvents
    {
        CONST_VTBL struct _IHttpListenerEventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define _IHttpListenerEvents_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define _IHttpListenerEvents_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define _IHttpListenerEvents_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define _IHttpListenerEvents_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define _IHttpListenerEvents_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define _IHttpListenerEvents_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define _IHttpListenerEvents_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */


#endif 	/* ___IHttpListenerEvents_DISPINTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_HttpListener;

#ifdef __cplusplus

class DECLSPEC_UUID("7cff2534-946a-495e-a356-7d01f71f3449")
HttpListener;
#endif

#ifndef __IOAuthResponseParser_INTERFACE_DEFINED__
#define __IOAuthResponseParser_INTERFACE_DEFINED__

/* interface IOAuthResponseParser */
/* [unique][helpstring][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IOAuthResponseParser;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("2b8c75e2-9631-4834-9d3b-32c51c7fb962")
    IOAuthResponseParser : public IDispatch
    {
    public:
        virtual /* [helpstring][id] */ HRESULT STDMETHODCALLTYPE Load( 
            /* [in] */ BSTR input) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_AccessToken( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_RefreshToken( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_IdToken( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_TokenExpiresInSeconds( 
            /* [retval][out] */ LONG *pVal) = 0;
        
        virtual /* [helpstring][id][propget] */ HRESULT STDMETHODCALLTYPE get_EmailInIdToken( 
            /* [retval][out] */ BSTR *pVal) = 0;
        
    };
    
#else 	/* C style interface */

    typedef struct IOAuthResponseParserVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IOAuthResponseParser * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            __RPC__deref_out  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IOAuthResponseParser * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IOAuthResponseParser * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IOAuthResponseParser * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IOAuthResponseParser * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IOAuthResponseParser * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IOAuthResponseParser * This,
            /* [in] */ DISPID dispIdMember,
            /* [in] */ REFIID riid,
            /* [in] */ LCID lcid,
            /* [in] */ WORD wFlags,
            /* [out][in] */ DISPPARAMS *pDispParams,
            /* [out] */ VARIANT *pVarResult,
            /* [out] */ EXCEPINFO *pExcepInfo,
            /* [out] */ UINT *puArgErr);
        
        /* [helpstring][id] */ HRESULT ( STDMETHODCALLTYPE *Load )( 
            IOAuthResponseParser * This,
            /* [in] */ BSTR input);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_AccessToken )( 
            IOAuthResponseParser * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_RefreshToken )( 
            IOAuthResponseParser * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_IdToken )( 
            IOAuthResponseParser * This,
            /* [retval][out] */ BSTR *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_TokenExpiresInSeconds )( 
            IOAuthResponseParser * This,
            /* [retval][out] */ LONG *pVal);
        
        /* [helpstring][id][propget] */ HRESULT ( STDMETHODCALLTYPE *get_EmailInIdToken )( 
            IOAuthResponseParser * This,
            /* [retval][out] */ BSTR *pVal);
        
        END_INTERFACE
    } IOAuthResponseParserVtbl;

    interface IOAuthResponseParser
    {
        CONST_VTBL struct IOAuthResponseParserVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IOAuthResponseParser_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IOAuthResponseParser_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IOAuthResponseParser_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IOAuthResponseParser_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IOAuthResponseParser_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IOAuthResponseParser_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IOAuthResponseParser_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IOAuthResponseParser_Load(This,input)	\
    ( (This)->lpVtbl -> Load(This,input) ) 

#define IOAuthResponseParser_get_AccessToken(This,pVal)	\
    ( (This)->lpVtbl -> get_AccessToken(This,pVal) ) 

#define IOAuthResponseParser_get_RefreshToken(This,pVal)	\
    ( (This)->lpVtbl -> get_RefreshToken(This,pVal) ) 

#define IOAuthResponseParser_get_IdToken(This,pVal)	\
    ( (This)->lpVtbl -> get_IdToken(This,pVal) ) 

#define IOAuthResponseParser_get_TokenExpiresInSeconds(This,pVal)	\
    ( (This)->lpVtbl -> get_TokenExpiresInSeconds(This,pVal) ) 

#define IOAuthResponseParser_get_EmailInIdToken(This,pVal)	\
    ( (This)->lpVtbl -> get_EmailInIdToken(This,pVal) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IOAuthResponseParser_INTERFACE_DEFINED__ */

#endif /* __EASendMailObjLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


