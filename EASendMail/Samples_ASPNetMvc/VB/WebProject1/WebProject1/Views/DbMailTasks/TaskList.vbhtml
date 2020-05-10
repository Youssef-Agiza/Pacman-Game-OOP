@ModelType IEnumerable(Of WebProject1.DbMailTask)

<table class="table table-bordered" id="mailTaskList">
    <tr>
        <th>
            <span class="fa fa-tasks" style="color: #4F8A10"></span>
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.TaskName)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Failed)

        <th>
            @Html.DisplayNameFor(Function(model) model.Succeeded)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.TotalCount)
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
                <span class="fa fa-refresh fa-spin" style="color: #0099dd"></span>
            </td>
            <td>
                @Html.HiddenFor(Function(modelItem) item.TaskId)
                @Html.ActionLink(item.TaskName, "Index", "DbRecipientResults", New With {.id = item.TaskId}, Nothing)
            </td>

            <td>
                @Html.DisplayFor(Function(modelItem) item.Failed)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Succeeded)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.TotalCount)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Status)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.CreationTime)
            </td>
            <td>
                @If (item.Status = DbMailTaskStatus.Completed Or item.Status = DbMailTaskStatus.Terminated) Then
                    @Html.ActionLink("Delete", "Delete", New With {.id = item.TaskId})
                Else
                    @Html.ActionLink("Delete", "Delete", New With {.id = item.TaskId}, New With {.style = "display:none"})
                    @<span>&nbsp;</span>
                End If
            </td>
        </tr>
    Next
</table>
