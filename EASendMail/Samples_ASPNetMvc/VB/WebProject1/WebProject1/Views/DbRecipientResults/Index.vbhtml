@ModelType IEnumerable(Of WebProject1.DbRecipientResult)
@Code
    ViewBag.Title = "Db Recipients status in task"
End Code

<h2>Recipient status in current task</h2>

<div style="margin: 20px auto 20px auto;">
    @Html.ActionLink("Go Back", "Index", "DbRecipients")
</div>

<table class="table table-bordered">
    <tr>
        <th style="width: 50px;">
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ResultId)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.TaskId)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Recipient)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Status)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.CreationTime)
        </th>
        <th></th>
    </tr>

    @For Each item In Model
        @<tr>
            <td>
                @if item.IsSucceeded Then
                    @<span class="fa fa-check" style="color:#0099dd"></span>
                Else
                    @<span class="fa fa-close" style="color:#ff0000;"></span>
                End If
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.ResultId)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.TaskId)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Recipient)
            </td>
            <td>

                @Html.DisplayFor(Function(modelItem) item.Status)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.CreationTime)
            </td>
            <td></td>
        </tr>
    Next
</table>

<hr />

<div style="margin: 20px auto 20px auto;">
    @Html.ActionLink("Go Back", "Index", "DbRecipients")
</div>

