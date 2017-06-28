/// <reference path="jquery-1.10.2.min.js" />
function getAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isUserRegistred(accessToken);
            }
        }
    }
}

function isUserRegistred(accessToken) {
    $.ajax({
        url: '/api/Account/UserInfo',
        method: 'GET',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            if (response.HasRegistered) {
                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('userName', response.Email);
                window.location.href = "Data.html";
            }
            else {
                signupExternalUser(accessToken,response.LoginProvider);
            }
        },
        error: function (jQXH) {

        }
    });
}

function signupExternalUser(accessToken,provider) {
    $.ajax({
        url: '/api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function () {
            
            window.location.href = "/api/Account/ExternalLogin?provider="+provider+"&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A16691%2FLogin.html&state=JsmVudujP7MoARl00hJn1G3WGk5aidgCfUEkLPDREEc1";
        },
        error: function (jQXH) {

        }
    });
}