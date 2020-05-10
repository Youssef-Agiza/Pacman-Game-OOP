@ModelType IEnumerable(Of WebProject1.DbRecipient)
@Code
    ViewBag.Title = "Recipient List - Send email from ASP.NET MVC with Database integration"
End Code

<h2>Send email from ASP.NET MVC with Database integration</h2>

<div style="margin: 20px auto 20px auto;">
    @Html.ActionLink("All Samples", "Index", "Home") &gt; Send email from ASP.NET MVC with Database integration
</div>

<div class="alert alert-info alert-dismissible current-status" style="margin-top: 10px;">
    <p>
        Please add recipient in Email Recipients at first, then click "Email Tasks" -&gt; "Create New Task" to create a new task for all recipients.
    </p>
</div>

<p>
    <h3>Email Recipients - <small>@Html.ActionLink("Add Recipient", "Create")</small></h3>
</p>

<table class="table table-bordered">
    <tr>
        <th>
            <span class="fa fa-address-book" style="color: #0099dd"></span>
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Address)
        </th>
        <th></th>
    </tr>

    @For Each item In Model
        @<tr>
            <td></td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Name)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Address)
            </td>
            <td>
                @Html.ActionLink("Edit", "Edit", New With {.id = item.RecipientId}) |
                @Html.ActionLink("Delete", "Delete", New With {.id = item.RecipientId})
            </td>
        </tr>
    Next
</table>

<hr />

<p>
    <h3>Email Tasks - <small>@Html.ActionLink("Create New Task", "Create", "DbMailTasks")</small></h3>
</p>

<div class="alert alert-info alert-dismissible current-status" style="margin-top: 10px;">
    <p>
        Task status are refreshed automatically per second.
    </p>
</div>

<div id="task_panel"></div>

<hr />

@Html.ActionLink("Back", "Index", "Home", Nothing, New With {.class = "btn btn-default"})

@Section Scripts

    <script>
        $(function () {
            $('#task_panel').load('@Url.Action("TaskList", "DbMailTasks")');

            queryStatus();
        });

        function queryStatus() {
            var interval = 1000;
            var req = '@Url.Action("QueryTasks", "DbMailTasks")';

            var task = function () {
                $.ajax({
                    type: "GET",
                    url: req,
                    cache: false,
                    dataType: "json"
                }).done(function (taskArray) {
                    updateTasks(taskArray);

                    setTimeout(task, interval);

                }).fail(function (msg) {
                    console.error(msg.status + ': ' + msg.statusText);
                });
            };

            setTimeout(task, interval);
        }

        function updateTasks(taskArray) {
            var taskTable = $('#mailTaskList');

            for (var i = 0; i < taskArray.length; i++) {
                //   console.log(taskArray[i]);
                updateSingleTask(taskTable, taskArray[i]);
            }
        }

        function updateSingleTask(taskTable, task) {

            var taskColumn = $(taskTable).find('input:hidden[value=\"' + task.TaskId + '\"]').parent();
            var icon = $(taskColumn).prev('td').find('span.fa');

            $(icon).removeClass('fa-refresh fa-spin fa-check fa-close');
            if (statusToString(task.Status) == 'Completed') {
                $(icon).addClass('fa-check');
            }
            else if (statusToString(task.Status) == 'Terminated') {
                $(icon).addClass('fa-close');
            }
            else {
                $(icon).addClass('fa-refresh fa-spin');
            }

            if (task.Failed > 0) {
                $(icon).css('color', '#ff0000');
            }
            else {
                $(icon).css('color', '#0099dd');
            }

            taskColumn = $(taskColumn).next('td');
            $(taskColumn).text(task.Failed);

            taskColumn = $(taskColumn).next('td');
            $(taskColumn).text(task.Succeeded);

            taskColumn = $(taskColumn).next('td');
            $(taskColumn).text(task.TotalCount);

            taskColumn = $(taskColumn).next('td');
            $(taskColumn).text(statusToString(task.Status));

            taskColumn = $(taskColumn).next('td');
            var d = new Date(parseInt(task.CreationTime.substr(6)));
            $(taskColumn).text(d.getDate() + '/'
                + (d.getMonth() + 1)
                + '/' + d.getFullYear()
                + ' ' + d.getHours()
                + ':' + d.getMinutes()
                + ":" + d.getSeconds()
                );

            taskColumn = $(taskColumn).next('td');

            if (statusToString(task.Status) == 'Completed' || statusToString(task.Status) == 'Terminated') {
                $(taskColumn).find('a').show();
            }
            else {
                $(taskColumn).find('a').hide();
            }
        }

        function statusToString(status)
        {
            switch (status)
            {
                case 0:
                    return 'Pending';
                case 1:
                    return 'Running';
                case 2:
                    return 'Completed';
                case 3:
                    return 'Terminated';
                default:
                    return 'Unknown';
            }
        }

    </script>
End Section