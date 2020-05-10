function initModalBox() {

    var _confirm = $('#dialogbox_confirm');
    var _alert = $('#dialogbox_alert');
    var _status = $('#dialogbox_status');

    var _confirmText = null;
    var _alertText = null;
    var _statusText = null;
    var _bodyStatusText = null;
    var _statusIcon = null;

    var _statusIsOn = false;

    var _this = new Object();

    var init = function () {

        if ($(_confirm).length == 0 ||
           $(_alert).length == 0 ||
            $(_status).length == 0) {
            window.alert('Failed to initialize modal dialogbox, please add @Html.Partial("~/Views/Shared/HtmlModalBox.cshtml") in your view!');
            return;
        }

        _confirmText = $(_confirm).find('div.modal-body p');
        _alertText = $(_alert).find('div.modal-body p');
        _statusText = $(_status).find('span.current-modal-status');
        _statusIcon = $(_status).find('span.fa');
        _bodyStatusText = $(_status).find('div.current-body-status');
        _statusIsOn = false;
    }

    var alert = function (msg, callback) {
        var dfd = $.Deferred();

        $(_alertText).text(msg);
        $(_alert).unbind('hidden.bs.modal');
        $(_alert).on('hidden.bs.modal', function (event) {
            if (typeof callback === 'function') {
                callback();
            }
            dfd.resolve();
        });

        $(_alert).modal();
        return dfd.promise();
    }

    var confirm = function (msg, callback) {
        var dfd = $.Deferred();

        $(_confirmText).text(msg);
        var primaryButton = $(_confirm).find('button.btn-primary');
        $(primaryButton).unbind();
        $(primaryButton).click(function (e) {
            e.preventDefault();
            $(_confirm).modal('hide');
            if (typeof callback === 'function') {
                callback();
            }
            dfd.resolve();
        });

        $(_confirm).modal();
        return dfd.promise();
    }

    var bodyStatus = function (msg)
    {
        $(_bodyStatusText).html(msg);
        if (_statusIsOn) {
            return;
        }

        if ($(_status).css('display') == 'none') {
            $(_status).modal({ backdrop: 'static', keyboard: false });
        }

        $(_status).data('bs.modal').options.backdrop = 'static';
        $(_status).data('bs.modal').options.keyboard = false;

        $(_statusIcon).removeClass('fa-check fa-close')
        $(_statusIcon).addClass('fa-spin fa-spinner');
        $(_statusIcon).css('color', '#0099dd');
        _statusIsOn = true;
    }

    var status = function (msg) {

        $(_statusText).text(msg);
        if (_statusIsOn) {
            return;
        }

        if ($(_status).css('display') == 'none') {
            $(_status).modal({ backdrop: 'static', keyboard: false });
        }

        $(_status).data('bs.modal').options.backdrop = 'static';
        $(_status).data('bs.modal').options.keyboard = false;

        $(_statusIcon).removeClass('fa-check fa-close')
        $(_statusIcon).addClass('fa-spin fa-spinner');
        $(_statusIcon).css('color', '#0099dd');
        _statusIsOn = true;
    }

    var error = function (msg) {

        $(_statusText).text(msg);

        if ($(_status).css('display') == 'none') {
            $(_status).modal({ backdrop: true, keyboard: true });
        }

        $(_status).data('bs.modal').options.backdrop = true;
        $(_status).data('bs.modal').options.keyboard = true;

        $(_statusIcon).removeClass('fa-spinner fa-spin fa-check')
        $(_statusIcon).addClass('fa-close');
        $(_statusIcon).css('color', '#ff0000');
        _statusIsOn = false;
    }

    var success = function (msg) {

        $(_statusText).text(msg);

        if ($(_status).css('display') == 'none') {
            $(_status).modal({ backdrop: true, keyboard: true });
        }

        $(_status).data('bs.modal').options.backdrop = true;
        $(_status).data('bs.modal').options.keyboard = true;

        $(_statusIcon).removeClass('fa-spinner fa-spin fa-close')
        $(_statusIcon).addClass('fa-check');
        $(_statusIcon).css('color', '#4F8A10');
        _statusIsOn = false;
    }

    var hide = function (msg) {
        $(_status).modal('hide');
    }

    _this.alert = alert;
    _this.confirm = confirm;
    _this.status = status;
    _this.error = error;
    _this.success = success;
    _this.hide = hide;
    _this.bodyStatus = bodyStatus;

    init();
    return _this;
}

