@ModelType WebProject1.DbMailTask
@Code
    ViewBag.Title = "Delete task"
End Code

<h2>Delete Email Task</h2>
<div style="margin: 20px auto 20px auto;">
    @Html.ActionLink("All Samples", "Index", "Home") &gt; @Html.ActionLink("Recipient List", "Index", "DbRecipients") &gt; Delete Email Task
</div>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>DbMailTask</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.TaskName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TaskName)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Status)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Status)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.TotalCount)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.TotalCount)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Succeeded)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Succeeded)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Failed)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Failed)
        </dd>
    </dl>

    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-danger" /> 
            @Html.ActionLink("Back", "Index", "DbRecipients", Nothing, New With {.class = "btn btn-default"})
        </div>
    End Using
</div>
