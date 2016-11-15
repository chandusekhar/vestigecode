$(document).ready(function () {

    $('form').dirtyForms();
    $('[data-toggle="popover"]').popover();

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




function setupChangeSecuritySettingsButtonFunctionality(url) {

    $(".btn-change-security-settings")
        .btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" },
            function(e) {

                if ($("#SecurityQuestionAnswer").val() == "") {
                    $("#lblSecuritySettingsErrorMsg").text(ResourceKeys.EnterAnswerForSecurityQuestion);
                } else if (!isSecurityAnswer($("#SecurityQuestionAnswer").val())) {
                    $("#lblSecuritySettingsErrorMsg").text(ResourceKeys.AnswerNotWalid);
                } else if ($("#Questions").val() == "") {
                    $("#lblSecuritySettingsErrorMsg").text(ResourceKeys.SelectAQuestion);
                } else {
                    $("#lblSecuritySettingsErrorMsg").text("");
                }

                var customFunction = 'sendResetSecurityQuestion( \"' + url + '\");';
                if (CheckDirty("changeSecuritySettingsForm", customFunction, "leavePage")) {
                    sendResetSecurityQuestion(url);
                    return false;
                }
            });
}



function sendResetSecurityQuestion(url) {
    toastr.remove();
    if ($("#lblSecuritySettingsErrorMsg").text() == "") {
        $.ajax({
            type: "POST",
            url: url,
            data: $("#changeSecuritySettingsForm").serialize(),
            success: function (data) {
                if (data.message == undefined || data.message == null || data.message == "") {
                    $('#changeSecuritySettingsPanelBody').removeClass("in").addClass("collapse");
                    showNotification(ResourceKeys.QuestionChangedSuccessfully,
                        ResourceKeys.AnswerChangeSuccessMessage,
                        ResourceKeys.AnswerChangeSuccess);
                    ClearAllDataFromForms();
                } else { $("#lblSecuritySettingsErrorMsg").text(data.message); }
            },
            error: function (data) {
                showNotification(data.message, "", ResourceKeys.Error);
            }
        });
    }
    return false;
}



function setupChangeEmailButtonFunctionality(url) {

    $(".btn-change-email").btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function (e) {

        if ($("#changeEmail1").val().trim() == "") {
            $("#lblEmailErrorMsg").text(ResourceKeys.PleaseEnterEmailAddress).removeClass("hidden");
            return false;
        } else if ($("#changeEmail2").val().trim() == "") {
            $("#lblEmailErrorMsg").text(ResourceKeys.PleaseConfirmYourEmailAddress).removeClass("hidden");
            return false;
        } else if ($("#changeEmail1").val().trim() != $("#changeEmail2").val().trim()) {
            $("#lblEmailErrorMsg").text(ResourceKeys.EmailAddressAndConfirmEmailAddressDoNotMatch).removeClass("hidden");
            return false;
        } else if (!isEmail($("#changeEmail1").val().trim())) {
            $("#lblEmailErrorMsg").text(ResourceKeys.PleaseEnterValidEmailAddress).removeClass("hidden");
            return false;
        } else {
            $("#lblEmailErrorMsg").text("").addClass("hidden");
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
                if (data.Message == undefined || data.Message == null || data.Message == "") {
                    window.location= data.ReturnUrl;
                } else
                {
                    $("#lblEmailErrorMsg").text(data.Message).removeClass("hidden");
                }
            },
            error: function (data) {
                showNotification(data.responseJSON.Message, "", ResourceKeys.Error);
            }
        });
    }
}



function setupChangePasswordButtonFunctionality(url) {
    $(".btn-change-password").btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function (e) {

        if ($("#currentPassword").val() == "") {
            $("#lblPasswordResetMsg").text(ResourceKeys.PleaseEnterCurrentPassword);
            return false;
        } else if ($("#newPassword1").val() == "") {
            $("#lblPasswordResetMsg").text(ResourceKeys.PleaseEnterNewPassword);
            return false;
        } else if ($("#newPassword2").val() == "") {
            $("#lblPasswordResetMsg").text(ResourceKeys.PleaseConfirmYourNewPassword);
            return false;
        } else if ($("#newPassword1").val() != $("#newPassword2").val()) {
            $("#lblPasswordResetMsg").text(ResourceKeys.NewPasswordAndConfirmNewPasswordDoNotMatch);
            return false;
        } else if (!isPassword($("#newPassword1").val())) {
            $("#lblPasswordResetMsg").text(ResourceKeys.NewPasswordDoesNotFulfillTheSpecifications);
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

    if ($("#lblPasswordResetMsg").text() == "") {
        $.ajax({
            type: "POST",
            url: myurl,
            data: $("#resetPasswordForm").serialize(),
            success: function (data) {
                if (data.Message == undefined || data.Message == null || data.Message == "") {
                    $('#changePasswordPanelBody').removeClass("in").addClass("collapse");
                    showNotification(data.Message,
                        ResourceKeys.YourPasswordWasChangedSuccessfully,
                        ResourceKeys.Success);
                    ClearAllDataFromForms();
                } else {
                    showNotification(data.Message, ResourceKeys.ErrorMessage, ResourceKeys.Error);
                }
            },
            error: function (data) {
                showNotification(data.responseJSON.Message, "", ResourceKeys.Error);
            }
        });
    }
}


function ClearAllDataFromForms() {
    $("input[type='radio'][name='UnsubscribeReasonSelected']").each(function (e) {
        $(this).prop("checked", false);
    });
    $("input[type='radio'][name='UnsubscribeReasonSelected']:first").prop("checked", true);
    $("#profilePanelGroup").find("input[type=text]").val("");
    $("#profilePanelGroup").find("input[type=password]").val("");
    $("#profilePanelGroup").find("textarea").val("");
    $("#additionalEmail").val("");
   
    $(".field-validation-error").html("");
    $("#lblPasswordResetMsg").text("");
    $("#lbladditionalEmailErrorMsg").text("");
    $("#lblEmailErrorMsg").text("").addClass("hidden");
    
    $('form:dirty').dirtyForms('setClean');
}



function setupUnsubscribeButtonFunctionality(url, redirectUrl) {
    $(".btn-unsubscribe").btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function (e) {

        var customFunction = 'sendUnsubscribe( \"' + url + '\", \"' + redirectUrl + '\");';
        if (CheckDirty("unsubscribeForm", customFunction, "leavePage")) {
            sendUnsubscribe(url, redirectUrl);
            return false;
        }
    });
}



function sendUnsubscribe(myurl, myRedirectUrl) {
    toastr.remove();

    var reason = $("input[type='radio'][name='UnsubscribeReasonSelected']:checked").val();
    if (reason + "" == "undefined") {
        showNotification(ResourceKeys.PleaseSelectaReasonToUnsubscribe, ResourceKeys.ErrorMessage, ResourceKeys.Error);
        return;
    }

    var othersText = $("#UnsubscribeReasonOtherText").val().trim();
    var unsubscribeComments = $("#UnsubscribeComments").val();
    if (reason == "Other" && othersText == "") {
        showNotification(ResourceKeys.PleaseSpecifyWhatisTheReasonToUnsubscribe, ResourceKeys.ErrorMessage, ResourceKeys.Error);
        return;
    }

    var wssAccountId = $("#WssAccountId").val();
    $.ajax({
        type: "POST",
        url: myurl,
        data: {
            wssAccountId: wssAccountId,
            reason: reason,
            othersText: othersText, unsubscribeComments: unsubscribeComments
        },
        success: function (data) {
            if (data.isDeleted == true) {
                $('#unsubscribePanelBody').removeClass("in").addClass("collapse");
                ClearAllDataFromForms();
                window.location = myRedirectUrl;
            } else {
                showNotification(data.errorMessage, ResourceKeys.ErrorMessage, ResourceKeys.Error);
            }
        },
        error: function (data) {
            showNotification(data.responseJSON.Message, "", ResourceKeys.Error);
        }
    });
}




function setupRemoveAdditionalEmailFunctionality(myurl) {
    $(".btn-delete-additional-email").btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function (e) {

        toastr.remove();

        $.ajax({
            type: "POST",
            url: myurl,
            data: {
                additionalEmailAddressId: this.attr("data-EmailId"),
                wssAccountId: this.attr("data-WssAccountId")
            },
            success: function (data) {
                if (data.message == undefined || data.message == null || data.message == "") {

                    $("#divAdditionalEmaillAddresses").html(data);
                    showNotification(ResourceKeys.AdditionalEmailAddressWasDeletedSuccessfully, ResourceKeys.SuccessMessage, ResourceKeys.Success);
                    $('#additionalEmailPanelBody').attr('class', 'panel-body panel-collapse in');
                    ClearAllDataFromForms();
                } else if (data.message != "") {
                    showNotification(data.Message, ResourceKeys.ErrorMessage, ResourceKeys.Error);
                }
            },
            error: function (data) {
                showNotification(data.message, "", ResourceKeys.Error);
            }
        });
    });
}



function setupAdditionalEmailFunctionality(url) {
    $(".btn-Additional-Email").btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function (e) {

        if ($("#additionalEmail").val().trim() === "") {
            $("#lbladditionalEmailErrorMsg").text(ResourceKeys.PleaseEnterEmailAddress);
            return false;
        } else if (!isEmail($("#additionalEmail").val())) {
            $("#lbladditionalEmailErrorMsg").text(ResourceKeys.PleaseEnterValidEmailAddress);
            return false;
        } else {
            $("#lbladditionalEmailErrorMsg").text("");
        }

        $(".email-address").each(function () {
            if ($(this).html() == $("#additionalEmail").val().trim()) {
                $("#lbladditionalEmailErrorMsg").text(ResourceKeys.EmailAlreadyExists);
            }
        });

        if ($("#lbladditionalEmailErrorMsg").text() == "") {
            var customFunction = 'sendAdditionalEmail( \"' + url + '\");';
            if (CheckDirty("additionalEmailForm", customFunction, "leavePage")) {
                sendAdditionalEmail(url);
                return false;
            }
        }
    });
}


function sendAdditionalEmail(myurl) {
    toastr.remove();
    var additionalEmailAddress = $("#additionalEmail").val().trim();
    var wssAccountId = $("#WssAccountId").val();
    $.ajax({
        type: "POST",
        url: myurl,
        data: {
            additionalemailAddress: additionalEmailAddress,
            wssAccountId: wssAccountId
        },
        success: function (data) {
            if (data.message == undefined || data.message == null || data.message == "") {
                $("#divAdditionalEmaillAddresses").html(data);
                showNotification(ResourceKeys.AdditionalEmailAddressWasAddedSuccessfully,
                    ResourceKeys.SuccessMessage,
                    ResourceKeys.Success);
                $('#additionalEmailPanelBody').attr('class', 'panel-body panel-collapse in');
                ClearAllDataFromForms();
            } else {
                $("#lbladditionalEmailErrorMsg").text(data.message);
                //showNotification(data.message, ResourceKeys.ErrorMessage, ResourceKeys.Error);
            }
        },
        error: function (data) {
            showNotification(data.message, "", ResourceKeys.Error);
        }
    });
}




function CheckDirty(skipFormId, customFunction, messageType) {
    var continueChecking = true; // to prevent looping if more than one form on the page
    var leavePage = true;
    $('form').each(function () {
        $(this).attr("id");
        if ($(this).attr("id") != skipFormId && $(this).hasClass("dirty") && continueChecking) {
            leavePage = false;
            continuChecking = false;

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
        $('#additionalEmailPanelBody').removeClass('in').addClass("collapse");
        eval(customFunction);
        return false;
    }
}

function DirtyFormToasterStyleWarning(customFunction, messageType) {
    toastr.remove();
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
        warningMessageText += ResourceKeys.ThereIsSomeDataFilledInAndNotSubmitted;
        warningMessageText += ResourceKeys.AreYouSureYouWantToLeaveThePage;
        warningMessageText += "<br/><br/><button type='button' class='btn btn-default' onclick='" + customFunction + "'>Leave the Page</button>";
        warningMessageText += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        warningMessageText += "<button type='button' class='btn btn-default' onclick='toastr.remove();'>Stay Here</button>";
    }

    if (messageType == 'panelChange') {
        var warningMessageText = '';
        warningMessageText += ResourceKeys.ThereIsSomeDataFilledInAndNotSubmitted;
        warningMessageText += ResourceKeys.AreYouSureYouWantToContinue;
        warningMessageText += '<br/><br/><button type="button" class="btn btn-default" onclick="ClearAllDataFromForms();$(\'#additionalEmailPanelBody\').removeClass(\'in\').addClass(\'collapse\');toastr.remove();' + customFunction + '">Proceed</button>';
        warningMessageText += '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
        warningMessageText += '<button type="button" class="btn btn-default" onclick="toastr.remove();">Stop</button>';
    }
    toastr.warning(warningMessageText, "Warning");
}

function IsActivateAccountFormValid() {
    var error_message = "";

    if (!$("#AgreeToTermsAndConditions").is(":checked")) {
        error_message += ResourceKeys.YouHaveToAcceptTheTermsAndConditions;
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
    ClearAllDataFromForms();
    window.location = myurl;
    return;
}

function AddDeleteAccountFunctionality() {
    $('.btn-remove-linked-account').btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function (e) {
        //alert(this.attr("data-formpath") + "--" + this.attr("data-utilityAccId"));
        RemoveLinkedAccount(this.attr("data-utilityAccId"), this.attr("data-formpath") + "");
    });
}

function RemoveLinkedAccount(acId, url) {

    $.post(url, { utilityAccId: acId },
        function (result) {
            if (result.IsDeleted != undefined && result.IsDeleted === false) {
                toastr.error(ResourceKeys.SorryAnErrorOccured + result.message, ResourceKeys.Error);
            } else {
                $('#divManageRecords').html(result);
                toastr.success(ResourceKeys.DeletedTheAccount, ResourceKeys.Success);

                if (result.IsDeleted == true) {
                    $('tr[data-customId="' + accNumber + '"]').remove();
                    $('tr[data-target="' + accNumber + '"]').remove();
                    $('#ddlBillsUtiliyuData option[value="' + result.Id + '"]').remove();
                    $("#ddlBillsUtiliyuData").change();
                }


                var billUrl = url.replace("DeleteLinkedUtilityAccount", "_bills");
                // var utilityAccountToShowBillsFor = $("[id^='EditDefault-']").prop("id").replace("EditDefault-", "");

                var utilityAccountToShowBillsFor = $("#defaultAccountCheckmark").attr("data-utilityAccId");
                showBillRecords(billUrl + "/" + utilityAccountToShowBillsFor);
            }
        });
}



function UpdateLinkedUtilityAccount(utility_acc_number, url) {

    var nickname = $('#EditNickname-' + utility_acc_number).val();
    var isDefaults = $('#EditDefault-' + utility_acc_number).prop('checked');

    $.post(url, {
        accId: utility_acc_number, nickName: nickname, isDefault: isDefaults
    },
           function (result) {
               if (result.isUpdated != undefined && result.isUpdated === false) {
                   toastr.error(ResourceKeys.SorryAnErrorOccured, ResourceKeys.Error);
               } else {
                   $('#divManageRecords').html(result);
                   toastr.success(ResourceKeys.UpdatedTheAccount, ResourceKeys.Success);
               }
           });
}


function LoadLinkedAccounts(url) {
    $.post(url, function (result) {
        $('#divManageRecords').html(result);
    });
}


function showBillRecords(billUrl) {
    $.ajax({
        type: "GET",
        url: billUrl,
        success: function (data) {
            $("#bills_records").html(data);
        }
    });


}

function SortBillRecords(ctrl, billUrl) {
    var sortDir = ctrl.dataset.sortdir;
    var sortOrder = ctrl.dataset.sortorder;
    var utilityAccountId = $("#hdnBillsUtilityAccountId").val();
    $.get(billUrl,
        { id: utilityAccountId, sortOrder: sortOrder, sortDir: sortDir },
        function (result) {
            if (result != undefined) {
                $("#bills_records").html(result);
            }
        });
}

