document.onkeydown = function (event) {
    var e = event || window.event || arguments.callee.caller.arguments[0];
    if (e && e.keyCode == 13) {
        submit();
        if (e.preventDefault) e.preventDefault();//IE8不支持
        return false;
    }
};
function enableLogin() {
    $("#btnLogin").css("cursor", "pointer");
    $("#btnLogin").attr('href', 'javascript:submit()');
    $('#loginText').html("登录");
}
function disableLogin() {
    $("#btnLogin").css("cursor", "default");
    $("#btnLogin").attr('href', '#');
    $('#loginText').html("正在登录");
}
function submit() {
    var userName = $('#userName').val();
    if (!userName) {
        alert("请输入用户名!");
        return;
    }
    var password = $('#password').val();
    if (!password) {
        alert("请输入用户密码!");
        return;
    }
    var rememberMe = $('#rememberMe').val();
    var returnUrl = $('#returnUrl').val();
    var __RequestVerificationToken = $("input[name='__RequestVerificationToken']").val();
    disableLogin();

    $.ajax({
        //async: false,
        type: "post",
        url: "/Account/Login",
        data: { userName: userName, password: password, rememberMe: rememberMe, returnUrl: returnUrl, __RequestVerificationToken: __RequestVerificationToken },
        cache: false,
        success: function (result) {
            if (result.success) {
                $('#login-error').html('');
                location.href = result.redirectUrl;
            } else {
                enableLogin();
                $('#login-error').html(result.error);
            }
        },
        error: function () {
            enableLogin();
            alert('登录失败');
        }
    });
}
