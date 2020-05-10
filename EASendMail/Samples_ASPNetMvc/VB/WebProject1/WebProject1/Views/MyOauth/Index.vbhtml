@ModelType WebProject1.MailTask
@Code
    ViewBag.Title = "Send email using Gmail/Microsoft XOAUTH2"
End Code

<h2>Send email using Gmail/Microsoft XOAUTH2</h2>
<div style="margin-top:20px;">
    @Html.ActionLink("All Samples", "Index", "Home") &gt; Send email using Gmail XOAUTH2
</div>

@Html.Partial("~/Views/Shared/TopStatusBar.vbhtml")


@Using (Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.name = "thisForm", .id = "thisForm"}))

    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <hr />
        @Html.HiddenFor(Function(model) model.TaskId)
        @Html.HiddenFor(Function(model) model.AuthType)
        @Html.HiddenFor(Function(model) model.IsAsyncTask)
        @Html.HiddenFor(Function(model) model.Sender)
        @Html.HiddenFor(Function(model) model.IsSslConnection)
        @Html.HiddenFor(Function(model) model.Protocol)

        @Html.Hidden("AutoAsyncSend", ViewBag.AutoAsyncSend.ToString())
        @Html.Hidden("TokenIsExisted", ViewBag.TokenIsExisted.ToString())

        @Html.Hidden("IsSyncSendSucceeded", ViewBag.IsSyncSendSucceeded.ToString())
        @Html.Hidden("SyncSendStatus", ViewBag.SyncSendStatus.ToString())

        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

        <div class="form-group">
            <label class="control-label col-md-2">SMTP Server</label>
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Server, New With {.htmlAttributes = New With {.class = "form-control", .readonly = "readonly"}})
                @Html.ValidationMessageFor(Function(model) model.Server, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Port</label>
            <div class="col-md-10">
                @Html.DropDownList("Port", Nothing, New With {.class = "form-control"})
            </div>
        </div>

         <div class="form-group">
             <label class="control-label col-md-2">Port</label>
             <div class="col-md-10">
                 @Html.DropDownList("OauthProvider", Nothing, New With {.class = "form-control"})
             </div>
         </div>
        <hr />

        <div class="form-group">
            <label class="control-label col-md-2">From:</label>
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.ReplyTo,
                                               New With
                     {
                                              .htmlAttributes = New With
                         {
                                                  .class = "form-control",
                                                  .style = "width:80%; max-width:80%;",
                                                  .placeHolder = "If you don't set sender address, authenticated user address will be used automatically (recommended)."
                                              }
                                          })
                @Html.ValidationMessageFor(Function(model) model.Sender, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2">To:</label>
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.RecipientTo,
                                          New With
                     {
                                         .htmlAttributes = New With
                         {
                                             .class = "form-control",
                                             .style = "width:80%; max-width:80%;",
                                             .placeHolder = "recipient@example.com"
                                         }
                                     })
                @Html.ValidationMessageFor(Function(model) model.RecipientTo, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Subject:</label>
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Subject, New With {.htmlAttributes = New With {.class = "form-control", .style = "width:80%; max-width:80%;"}})
                @Html.ValidationMessageFor(Function(model) model.Subject, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Body Text:</label>
            <div class="col-md-10">
                @Html.TextAreaFor(Function(model) model.TextBody, 10, 20, New With {.class = "form-control", .style = "height:100px; width:80%; max-width:80%;"})
                @Html.ValidationMessageFor(Function(model) model.TextBody, "", New With {.class = "text-danger"})
            </div>
        </div>


        <div class="form-group">
            <div class="col-md-2"></div>
            <div class="col-md-10">
                <input type="button" value="Send Email" id="ButtonSyncSend" class="btn btn-default" />
                <input type="button" value="Ajax Send Email" id="ButtonAsyncSend" class="btn btn-default" />

                <input type="button" value="Clear Token" id="ButtonClearToken" class="btn btn-default" disabled="disabled" />
                @Html.ActionLink("Back", "Index", "Home", Nothing, New With {.class = "btn btn-default"})
            </div>
        </div>

    </div>
End Using

@Html.Partial("~/Views/Shared/HtmlModalBox.vbhtml")

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/sample")

    <script>
        $(function () {
            initGmailSample();
        });

        function initGmailSample() {
            var statusBar = initTopStatusBar();
            var statusModal = initModalBox();

            var _form = $('#thisForm');
            var _syncSendButton = $('#ButtonSyncSend');
            var _asyncSendButton = $('#ButtonAsyncSend');
            var _clearTokenButton = $('#ButtonClearToken');
            var _oauthProvider = $("#OauthProvider");

            if ($(_oauthProvider).val() * 1 == 2) {
                $('#Port').attr('disabled', 'disabled');
            }

            $(_oauthProvider).unbind();
            $(_oauthProvider).on('change', function (event) {
                switch ($(_oauthProvider).val() * 1) {
                    case 0:
                        $('#Server').val('smtp.gmail.com');
                        $('#Port').removeAttr('disabled');
                        $('#Protocol').val('SMTP');
                        break;
                    case 1:
                        $('#Server').val('smtp.live.com');
                        $('#Port').removeAttr('disabled');
                        $('#Protocol').val('SMTP');
                        break;
                    case 2:
                        $('#Server').val('outlook.office365.com');
                        $('#Port').attr('disabled', 'disabled');
                        $('#Protocol').val('ExchangeEWS');
                        break;
                    case 3:
                        $('#Server').val('https://www.googleapis.com/upload/gmail/v1/users/me/messages/send?uploadType=media');
                        $('#Port').attr('disabled', 'disabled');
                        $('#Protocol').val('GmailApi');
                        break;
                }

                $('#TokenIsExisted').val('False');
            });

            showSyncSendResult();

            // oauth is completed, user clicked ajax send, so we sent it automatically now.
            if ($('#AutoAsyncSend').val() == 'True' &&
                $('#TokenIsExisted').val() == 'True') {
                statusBar.hide();
                asyncSend();
            }

            function showSyncSendResult() {
                if ($('#SyncSendStatus').val() == '') {
                    statusBar.hide();
                    return;
                }

                if ($('#IsSyncSendSucceeded').val() == 'True') {
                    statusBar.success($('#SyncSendStatus').val());
                }
                else {
                    statusBar.error($('#SyncSendStatus').val());
                }
            }

            $(_syncSendButton).unbind();
            $(_syncSendButton).on('click', function (event) {
                event.preventDefault();
                $(_form).validate();
                if (!$(_form).valid()) {
                    scrollToError();
                    return;
                }

                $('#Port').removeAttr('disabled');
                $('#IsAsyncTask').val('False');
                $(_form).attr('action', '@Url.Action("Index")');
                $(_form).submit();
            })

            $(_asyncSendButton).unbind();
            $(_asyncSendButton).on('click', function (event) {
                event.preventDefault();

                $(_form).validate();
                if (!$(_form).valid()) {
                    scrollToError();
                    return;
                }

                $('#Port').removeAttr('disabled');
                // because access token is existed, we don't need to redirect to google login page, send email directly.
                if ($('#TokenIsExisted').val() == 'True') {
                    return asyncSend();
                }

                // access token doesn't exist, submit form to redirect page to google login page.
                $('#IsAsyncTask').val('True');
                $(_form).attr('action', '@Url.Action("Index")');
                $(_form).submit();
            })

            if ($('#TokenIsExisted').val() == 'True') {
                $(_clearTokenButton).removeAttr('disabled');
            }
            else {
                $(_clearTokenButton).attr('disabled', 'disabled');
            }

            $(_clearTokenButton).unbind()
            $(_clearTokenButton).on('click', function (event) {

                $(_form).attr('action', '@Url.Action("ClearToken")');

                var req_data = $(_form).serialize();
                var req = $(_form).attr('action');

                statusModal.status('Please wait ...');

                $.post(req, req_data)
               .done(function (taskId) {
                   $('#TokenIsExisted').val('False');
                   statusModal.success('Token is cleared.');
                   $(_clearTokenButton).attr('disabled', 'disabled');
               })
               .fail(function (msg) {
                   statusModal.error(msg.status + ': ' + msg.statusText);
               });
            })

            function asyncSend() {
                $('#IsAsyncTask').val('True');
                $(_form).attr('action', '@Url.Action("AsyncSend")');

                var req_data = $(_form).serialize();
                var req = $(_form).attr('action');

                statusModal.status('Please wait ...');

                $.post(req, req_data)
                .done(function (taskId) {
                    queryStatus(taskId);
                })
                .fail(function (msg) {
                    statusModal.error(msg.status + ': ' + msg.statusText);
                });
            }

            function queryStatus(id) {
                var interval = 300;
                var req = '@Url.Action("QueryAsyncTask")' + '/' + id;

                var task = function () {
                    $.ajax({
                        type: "GET",
                        url: req,
                        cache: false,
                        dataType: "json"
                    }).done(function (msg) {

                        statusModal.status(msg.Status);

                        if (!msg.Completed) {
                            setTimeout(task, interval);
                            return;
                        }

                        if (msg.HasError) {
                            statusModal.error(msg.Status);
                            $('#TokenIsExisted').val('False');
                        }
                        else {
                            statusModal.success('Completed');
                        }

                    }).fail(function (msg) {
                        statusModal.error(msg.status + ': ' + msg.statusText);
                    });
                };

                setTimeout(task, interval);
            }

        }
    </script>

End Section

