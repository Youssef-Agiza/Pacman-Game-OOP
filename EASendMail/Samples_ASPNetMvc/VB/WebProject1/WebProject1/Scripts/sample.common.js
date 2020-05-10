function scrollToError() {
    if ($('.field-validation-error').length == 0) {
        return;
    }

    var errorDiv = $('.field-validation-error').first();
    var scrollPos = errorDiv.offset().top - 90;
    $(window).scrollTop(scrollPos);
}

function htmlEncode(input) {
    return $('<span>').text(input).html();
}

function defferedTimeout(dfd, func, interval, form, action) {
    var task = function () {
        func(form, action).done(function () {
            // console.log('Timeout executed!');
        }).always(function () {
            // console.log('Timeout done!');
            dfd.resolve();
        });
    };

    setTimeout(task, interval);
    return dfd.promise();
};

function ajaxUpdateForm(form, action) {
    showLoading(true);
    var dfd = $.Deferred();
    return defferedTimeout(dfd, delayPost, 500, form, action);
}

function delayPost(form, action) {
    var req_data = $(form).serialize();

    if (action) {
        $(form).attr('action', action);
        //console.log('customized action:' + action);
    }

    var req = $(form).attr('action');
    // console.log(req);
    // console.log(req_data);
    var dfd = $.post(req, req_data
    ).done(function (data) {

        showDlgStatus("Please wait ...");

        setTimeout(function () {
            hideDlgStatus();
        }, 500);

    }).fail(function (msg) {
        showDlgStatusError(msg.status + ': ' + msg.statusText);
    });

    return dfd;
}
