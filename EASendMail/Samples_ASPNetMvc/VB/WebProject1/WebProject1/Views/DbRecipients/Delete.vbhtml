@ModelType WebProject1.DbRecipient
@Code
    ViewBag.Title = "Delete - Send email from ASP.NET MVC with Database integration"
End Code

<h3>Are you sure you want to delete this?</h3>
<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Address)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Address)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-danger" /> 
             @Html.ActionLink("Back", "Index", Nothing, New With {.class = "btn btn-default"})
        </div>
    End Using
</div>
