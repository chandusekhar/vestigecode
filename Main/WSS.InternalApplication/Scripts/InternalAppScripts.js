$(document).ready(function() {
});

function AddDeleteAccountFunctionality() {
    $('.btn-delete-account').btsConfirmButton({ msg: "&nbsp;&nbsp;Click again to confirm" }, function(e) {
        //alert(this.attr("data-formpath") + "--" + this.attr("data-accId") + "--" + this.attr("data-accNumber"));
        RemoveLinkedAccount(this.attr("data-accId"), this.attr("data-accNumber"), this.attr("data-formpath") + "");
    });
}

function RemoveLinkedAccount(acId, accNumber, url) {
    $.post(url, { accId: acId },
        function (result) {
            if (result.IsDeleted == true) {
                $('tr[data-customId="' + accNumber + '"]').remove();
                $('tr[data-target="' + accNumber + '"]').remove();
                $('#ddlLinkedAccount option[value="' + result.Id + '"]').remove();
                $("#ddlLinkedAccount").change();
                //$("#ddlLinkedAccount option").filter(function () {
                //    var $this = $(this);
                //    return $this == accNumber;
                //}).remove();
            }
        });
}