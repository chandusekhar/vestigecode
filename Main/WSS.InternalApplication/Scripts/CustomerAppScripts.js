$(document).ready(function () {

    $('form').dirtyForms();


});


function isEmail(email) {
    var regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

    if (email.length <= 254)
    { return regex.test(email); }
    else
    { return false; }
}


function isPassword(password) {
    var regex = /^(?=.*\d)(?=.*[a-zA-Z]).{7,15}$/;

    if (password.length > 5 && password.length < 15)
    { return regex.test(password); }
    else
    { return false; }
}


function isSecurityAnswer(securityanswer) {
    var regex = /^[\w*\s*'*@*-*,*\.*]{1,250}$/;

    if (securityanswer.length <= 254)
    { return regex.test(securityanswer); }
    else
    { return false; }
}


function setupChangeEmailButtonFunctionality(url) {

    $(".btn-change-email").btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function (e) {

        if ($("#changeEmail1").val() == "") {
            $("#lblEmailErrorMsg").text("Please enter email address");
            return false;
        } else if ($("#changeEmail2").val() == "") {
            $("#lblEmailErrorMsg").text("Please confirm your email address");
            return false;
        } else if ($("#changeEmail1").val() != $("#changeEmail2").val()) {
            $("#lblEmailErrorMsg").text(" Email address and Confirm Email address do not match");
            return false;
        } else if (!isEmail($("#changeEmail1").val())) {
            $("#lblEmailErrorMsg").text("Please enter valid email address");
            return false;
        } else {
            $("#lblEmailErrorMsg").text("");
        }

        var customFunction = 'sendResetEmail( \"' + url + '\");';
        if (CheckDirty("resetEmailForm", customFunction, "leavePage")) {
            sendResetEmail(url);
            return false;
        }
    });
}



function sendResetEmail(url) {
    toastr.remove();
    if ($("#lblEmailErrorMsg").text() == "") {

        $.ajax({
            type: "POST",
            url: url,
            data: $("#resetEmailForm").serialize(),
            success: function (data) {
                if (data.Message == 'Email Already Exists') {
                    $("#lblEmailErrorMsg").text("Email already exists.");
                } else if (data.Message == "") {
                    showNotification("Email address was changed successfully", "Success!", "Success");
                    $('p#primaryEmailAddress').html('<strong>Profile:</strong> ' + $('#changeEmail1').val());
                }
            },
            error: function (data) {
                debugger;
            }
        });
    }
}

//function setupAdditionalEmailFunctionality(url) {
//    $('.btn-Additional-Email').btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function (e) {

//        var customFunction = 'sendAdditionalEmail( \"' + url + '\");';
//        if (CheckDirty("changeSecuritySettingsForm", customFunction, "leavePage")) {
//            window.sendAdditionalEmail(url);
//            return false;
//        }
//    });
//}


function setupChangePasswordButtonFunctionality(url) {
    $(".btn-change-password").btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function (e) {


        if ($("#currentPassword").val() == "") {
            $("#lblPasswordResetMsg").text("Please enter current password");
            return false;
        } else if ($("#newPassword1").val() == "") {
            $("#lblPasswordResetMsg").text("Please enter new password");
            return false;
        } else if ($("#newPassword2").val() == "") {
            $("#lblPasswordResetMsg").text("Please confirm your new password");
            return false;
        } else if ($("#newPassword1").val() != $("#newPassword2").val()) {
            $("#lblPasswordResetMsg").text(" New password and Confirm New password do not match");
            return false;
        } else if (!isPassword($("#newPassword1").val())) {
            $("#lblPasswordResetMsg").text("New Password does not fulfill the specifications");
            return false;
        } else {
            $("#lblPasswordResetMsg").text("");
        }

        var customFunction = 'sendResetPassword( \"' + url + '\");';
        if (CheckDirty("resetPasswordForm", customFunction, "leavePage")) {
            sendResetPassword(url);
            return false;
        }
    });
}



function sendResetPassword(myurl) {
    toastr.remove();
    //alert("sending change password ajax . ");
    //alert("form data:\n"  + $("#resetPasswordForm").serialize());
    //alert("url = " + myurl);

    if ($("#lblPasswordResetMsg").text() == "") {
        $.ajax({
            type: "POST",
            url: myurl,
            data: $("#resetPasswordForm").serialize(),
            success: function (data) {
                showNotification(data.Message, "You password was changed successfully ", "Success");
            },
            error: function (data) {
                debugger;
            }
        });
    }
}



function CheckDirty(skipFormId, customFunction, messageType) {

    var continueChecking = true; // to prevent looping if more than one form on the page
    var leavePage = true;
    $('form').each(function () {
        $(this).attr("id");
        if ($(this).attr("id") != skipFormId && $(this).hasClass("dirty") && continueChecking) {
            leavePage = false;
            continueChecking = false;

            DirtyFormToasterStyleWarning(customFunction, messageType);

        }
    });
    return leavePage;
}




function RedefineLinks(FormId, skipLinkId) {
    $('a')
        .click(function (event) {
            var targetUrl = $(this).attr("href");
            var id = $(this).attr("id");

            if ($("#" + FormId).hasClass("dirty") && id != skipLinkId) {

                event.preventDefault();
                DirtyFormToasterStyleWarning("toastr.remove(); ClearFieldsAndGo(\"" + targetUrl + "\");", "leavePage");
            }

        });
}



function AccordionHeaderClick(skipFormId, customFunction) {

    if (CheckDirty(skipFormId, customFunction, "panelChange")) {
        eval(customFunction);
        return false;
    }
}





function DirtyFormToasterStyleWarning(customFunction, messageType) {

    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toast-top-full-width",
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "0",
        "extendedTimeOut": "0",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        "tapToDismiss": false
    }

    if (messageType == "leavePage") {
        var warningMessageText = "";
        warningMessageText += "There is some data filled in and not submitted.";
        warningMessageText += "<br/>Are you sure you want to leave the page?";
        warningMessageText += "<br/><br/><button type='button' class='btn btn-default' onclick='" + customFunction + "'>Leave the Page</button>";
        warningMessageText += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        warningMessageText += "<button type='button' class='btn btn-default' onclick='toastr.remove();'>Stay Here</button>";
    }

    if (messageType == 'panelChange') {
        var warningMessageText = '';
        warningMessageText += 'There is some data filled in and not submitted.';
        warningMessageText += '<br/>Are you sure you want to continue?';
        warningMessageText += '<br/><br/><button type="button" class="btn btn-default" onclick="$(\'form:dirty\').dirtyForms(\'setClean\');toastr.remove();' +customFunction  + '">Proceed</button>';
        warningMessageText += '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
        warningMessageText += '<button type="button" class="btn btn-default" onclick="toastr.remove();">Stop</button>';
        }

    toastr.warning(warningMessageText, "Warning");

}




function IsActivateAccountFormValid() {
    var error_message = "";

    //if (!$("#NoPaperBills").is(":checked")) {
    //    error_message += "You have to confirm you will no longer receive paper bills<br>";
    //}

    if (!$("#AgreeToTermsAndConditions").is(":checked")) {
        error_message += "You have to accept the terms and conditions<br>";
    }

    $("#error-message").html(error_message);

    if (error_message == "") {
        return true;
    } else {
        return false;
    }
}

function ClearFieldsAndGo(myurl) {
    $('form:dirty').dirtyForms('setClean');
    window.location = myurl;
    return;
}
