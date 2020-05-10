
function serverAutoConfig() {

    var _autoChangeUser = true;
    var _autoChangeServer = true;
    var _autoOpenUserAuthentication = true;

    var _server = $('#Server');
    var _sender = $('#Sender');
    var _user = $('#User');
    var _password = $('#Password');

    var _useSsl = $('#IsSslConnection');
    var _useAuth = $('#IsAuthenticationRequired');

    var _ports = $('#Port');

    function changeServerFromSender(sender) {

        if (!_autoChangeServer) {
            return;
        }

        var pos = sender.lastIndexOf('@');
        if (pos == -1) {
            return;
        }

        var domain = sender.substr(pos + 1).toLowerCase();
        var guessServer = "";
        if (domain == 'hotmail.com') {
            guessServer = 'smtp.live.com';
        }
        else if (domain == 'gmail.com') {
            guessServer = 'smtp.gmail.com';
        }
        else if (domain == 'yahoo.com') {
            guessServer = 'smtp.mail.yahoo.com';
        }
        else if (domain == 'aol.com') {
            guessServer = 'smtp.aol.com';
        }
        else if (domain == 'qq.com') {
            guessServer = 'smtp.qq.com';
        }

        if (guessServer != '') {
            $(_server).val(guessServer);

            $(_useSsl).attr('checked', 'checked');

            $(_useAuth).attr('checked', 'checked');
            enableUserAuthentication(true);

            $(_ports).children('option').removeAttr('selected');
            $(_ports).children('option[value=\"587\"]').attr("selected", "selected");
        }
    }

    function enableUserAuthentication(enabled) {

        if (enabled) {
            $(_user).removeAttr('readonly', 'readonly');
            $(_password).removeAttr('readonly', 'readonly');
        }
        else {
            $(_user).attr('readonly', 'readonly');
            $(_password).attr('readonly', 'readonly');
        }
    }

    var init = function () {

        if ($(_server).length == 0) {
            alert('No server config tag found!');
            return;
        }

        $(_server).on('keyup', function (event) {
            _autoChangeServer = ($(_server).val() == '');
            if (!_autoChangeServer && _autoOpenUserAuthentication) {
                $(_useAuth).attr('checked', 'checked');
                enableUserAuthentication(true);
            }
        });

        $(_user).on('keyup', function (event) {
            _autoChangeUser = ($(_server).val() == '');
        });

        $(_useAuth).on('click', function (event) {
            _autoOpenUserAuthentication = false;
            enableUserAuthentication($(_useAuth).is(':checked'));
        });

        $(_sender).on('change', function (event) {
            if (_autoChangeUser) {
                $(_user).val($(_sender).val());
            }
            changeServerFromSender($(_sender).val());
        });

        enableUserAuthentication($(_useAuth).is(':checked'));
    }

    this.init = init;
    return this;
}

