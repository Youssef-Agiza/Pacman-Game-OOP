function initTopStatusBar() {
    _statusBar = $('#task_status');
    _statusIcon = null;
    _statusText = null;

    _this = new Object();

    var init = function () {
        if ($(_statusBar).length == 0) {
            window.alert('No status bar found, please add @Html.Partial("~/Views/Shared/TopStatusBar.cshtml") in your view!');
            return;
        }

        _statusIcon = $(_statusBar).find('span.fa');
        _statusText = $(_statusBar).find('span.current-alert-status');
    }

    var status = function (msg) {

        $(_statusText).text(msg);

        $(_statusIcon).removeClass('fa-check fa-close');
        $(_statusIcon).addClass('fa-refresh fa-spin')

        $(_statusBar).removeClass('alert-danger');
        $(_statusBar).addClass('alert-info');
        $(_statusBar).fadeIn();
    }

    var error = function (msg) {

        $(_statusText).text(msg);

        $(_statusIcon).removeClass('fa-refresh fa-spin fa-check');
        $(_statusIcon).addClass('fa-close')

        $(_statusBar).removeClass('alert-info');
        $(_statusBar).addClass('alert-danger');
        $(_statusBar).fadeIn();
    }

    var success = function (msg) {
        $(_statusText).text(msg);

        $(_statusIcon).removeClass('fa-refresh fa-spin fa-check');
        $(_statusIcon).addClass('fa-check')

        $(_statusBar).removeClass('alert-danger');
        $(_statusBar).addClass('alert-info');
        $(_statusBar).fadeIn();
    }

    var hide = function () {
        $(_statusBar).hide();
    }

    var fadeOut = function () {
        $(_statusBar).fadeOut();
    }

    _this.hide = hide;
    _this.fadeOut = fadeOut;
    _this.status = status;
    _this.error = error;
    _this.success = success;

    init();
    return _this;
}
