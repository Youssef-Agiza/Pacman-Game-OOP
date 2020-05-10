@ModelType WebProject1.MailTask
@Code
    ViewBag.Title = "Send a simple email from ASP.NET MVC"
End Code

<h2>Send a simple email from ASP.NET MVC</h2>
<div style="margin-top:20px;">
    @Html.ActionLink("All Samples", "Index", "Home") &gt; Send a simple email from ASP.NET MVC
</div>

@Html.Partial("~/Views/Shared/TopStatusBar.vbhtml")

@Using (Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.name = "thisForm", .id = "thisForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <hr />
        @Html.HiddenFor(Function(model) model.TaskId)
        @Html.HiddenFor(Function(model) model.AuthType)

        @Html.Hidden("IsSyncSendSucceeded", ViewBag.IsSyncSendSucceeded.ToString())
        @Html.Hidden("SyncSendStatus", ViewBag.SyncSendStatus.ToString())

        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
        <div class="form-group">
            @Html.LabelFor(Function(model) model.Sender, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Sender,
                                                            New With
                                                          {
                                                              .htmlAttributes = New With
                                                              {
                                                                  .class = "form-control",
                                                                  .style = "width:80%; max-width:80%;",
                                                                  .placeHolder = "sender@example.com"
                                                              }
                                                          })
                @Html.ValidationMessageFor(Function(model) model.Sender, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.RecipientTo, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.RecipientTo,
                             New With
                             {
                                 .htmlAttributes = New With
                                 {
                                     .class = "form-control",
                                     .style = "width:80%; max-width:80%;",
                                     .placeholder = "recipient@example.com"
                                 }
                             })
                @Html.ValidationMessageFor(Function(model) model.RecipientTo, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Subject, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Subject, New With {.htmlAttributes = New With {.class = "form-control", .style = "width:80%; max-width:80%;"}})
                @Html.ValidationMessageFor(Function(model) model.Subject, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.TextBody, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.TextAreaFor(Function(model) model.TextBody, 5, 20, New With {.class = "form-control", .style = "width:80%; max-width:80%;"})
                @Html.ValidationMessageFor(Function(model) model.TextBody, "", New With {.class = "text-danger"})
            </div>
        </div>

        <hr />

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Server, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.Server,
                        New With
                        {
                            .htmlAttributes = New With
                            {
                                .class = "form-control",
                                .placeholder = "Input your server address"
                            }
                        })
                @Html.ValidationMessageFor(Function(model) model.Server, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(Function(model) model.Port, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.DropDownList("Port", Nothing, New With {.class = "form-control"})
                @Html.ValidationMessageFor(Function(model) model.Port, "", New With {.class = "text-danger"})
            </div>
        </div>


        <div class="form-group">
            <div class="col-md-10 col-md-offset-2">
                <div class="checkbox">
                    <label>
                        @Html.CheckBoxFor(Function(model) model.IsSslConnection)
                        @Html.DisplayNameFor(Function(model) model.IsSslConnection)
                    </label>
                </div>
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2">Protocol</label>
            <div class="col-md-10">
                @Html.DropDownList("Protocol", Nothing, New With {.class = "form-control"})
            </div>
        </div>


        <div class="form-group">
            <div class="col-md-10 col-md-offset-2">
                <div class="checkbox">
                    <label>
                        @Html.CheckBoxFor(Function(model) model.IsAuthenticationRequired)
                        @Html.DisplayNameFor(Function(model) model.IsAuthenticationRequired)
                    </label>
                </div>
            </div>
        </div>


        <div id="user_password_panel">
            <div class="form-group">
                @Html.LabelFor(Function(model) model.User, New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.User, New With {.htmlAttributes = New With {.class = "form-control"}})
                    @Html.ValidationMessageFor(Function(model) model.User, "", New With {.class = "text-danger"})
                </div>
            </div>

            <div class="form-group">
                @Html.LabelFor(Function(model) model.Password, New With {.class = "control-label col-md-2"})
                <div class="col-md-10">
                    @Html.EditorFor(Function(model) model.Password, New With {.htmlAttributes = New With {.class = "form-control", .type = "Password"}})
                    @Html.ValidationMessageFor(Function(model) model.Password, "", New With {.class = "text-danger"})
                </div>
            </div>
        </div>

         <hr />

         <div class="form-group">
             <div class="col-md-offset-2 col-md-10">
                 <input type="button" value="Send Email" id="ButtonSyncSend" class="btn btn-default" />
                 <input type="button" value="Ajax Send Email" id="ButtonAsyncSend" class="btn btn-default" />
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
            initSimpleSample();
        });

        function initSimpleSample() {

            serverAutoConfig().init();
            var statusBar = initTopStatusBar();
            var statusModal = initModalBox();

            var _form = $('#thisForm');
            var _syncSendButton = $('#ButtonSyncSend');
            var _asyncSendButton = $('#ButtonAsyncSend');

            showSyncSendResult();

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

                $(_form).attr('action', '@Url.Action("AsyncSend")');

                var req_data = $(_form).serialize();
                var req = $(_form).attr('action');

                statusBar.hide();
                statusModal.status('Please wait ...');

                $.post(req, req_data)
                .done(function (taskId) {
                    queryStatus(taskId);
                })
                .fail(function (msg) {
                    statusModal.error(msg.status + ': ' + msg.statusText);
                });
            })

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
