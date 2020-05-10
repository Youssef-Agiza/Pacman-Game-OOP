@ModelType WebProject1.DbMailTask
@Code
    ViewBag.Title = "Create a new email task - Send email from ASP.NET MVC with Database integration"
End Code

<h2>Create Email Task</h2>
<div style="margin: 20px auto 20px auto;">
    @Html.ActionLink("All Samples", "Index", "Home") &gt; @Html.ActionLink("Recipient List", "Index", "DbRecipients") &gt; Create Email Task
</div>

@Using (Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.name = "thisForm", .id = "thisForm"}))
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">
        <hr />
        @Html.HiddenFor(Function(model) model.AuthType)
        @Html.Hidden("RecipientCount", ViewBag.RecipientCount.ToString())
        @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

        <div class="form-group">
            @Html.LabelFor(Function(model) model.TaskName, New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.EditorFor(Function(model) model.TaskName,
                                                                  New With
                                                               {
                                                                   .htmlAttributes = New With
                                                                   {
                                                                       .class = "form-control",
                                                                       .style = "width:80%; max-width:80%;",
                                                                       .placeholder = "My Task (Optional)"
                                                                   }
                                                               })
                @Html.ValidationMessageFor(Function(model) model.TaskName, "", New With {.class = "text-danger"})
            </div>
        </div>

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
                                                                       .placeholder = "sender@example.com"
                                                                   }
                                                               })
                @Html.ValidationMessageFor(Function(model) model.Sender, "", New With {.class = "text-danger"})
            </div>
        </div>

        <div class="form-group">
            @Html.Label("To", New With {.class = "control-label col-md-2"})
            <div class="col-md-10">
                @Html.Editor("Current email will be sent to all recipients in Recipients List",
                                       New With
                                       {
                                           .htmlAttributes = New With
                                           {
                                               .class = "form-control",
                                               .style = "width:80%; max-width:80%;",
                                               .readonly = "readonly",
                                               .placeholder = "Current email will be sent to all recipients in Recipients List",
                                               .id = "RecipientTo"
                                           }
                                       })
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
                <input type="button" value="Create Task and Send Email" id="ButtonCreateTask" class="btn btn-primary" />
                @Html.ActionLink("Back", "Index", "DbRecipients", Nothing, New With {.class = "btn btn-default"})
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
            $('#RecipientTo').val(
                'Current email will be sent to all recipients in Recipients List. Total'
                + $('#RecipientCount').val()
                + ' recipient(s)');
            initCreateTask();
        });

        function initCreateTask() {

            serverAutoConfig().init();
            var statusModal = initModalBox();

            var _form = $('#thisForm');
            var _syncSendButton = $('#ButtonCreateTask');


            $(_syncSendButton).unbind();
            $(_syncSendButton).on('click', function (event) {
                event.preventDefault();
                if ($('#RecipientCount').val() * 1 == 0) {
                    statusModal.alert("Recipient list contains 0 address, Please add recipient in Recipient List.");
                    return;
                }

                $(_form).validate();
                if (!$(_form).valid()) {
                    scrollToError();
                    return;
                }

                $(_form).attr('action', '@Url.Action("Create")');
                $(_form).submit();
            })
        }
    </script>
End Section