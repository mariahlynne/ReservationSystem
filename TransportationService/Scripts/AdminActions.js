function addReservation() {
    $.post("/Admin/AddReservation", {}, function (data) {
        $("#modal").replaceWith(data);
        $("#modal").modal();
    });
}

$(document).ready(function () {
    $.post("/Admin/RefreshAdmin", {}, function (data) {
        if (data.error == undefined || data.error != true) {
            userManager.currentUser = data.user;
            $("#mainArea-header").html(data.headerText);
            $("#sign-out-text").removeClass('hidden');
        }
    });
    viewReservations();
});

function viewReservations() {
    $.post("/Admin/ViewReservations", {}, function (html) {
        $("#view-container").html(html);
        $("#view-item-table").tablesorter();
    });
}

function timeKeyPress(event) {
    return event.charCode === 0 || /\d|:/.test(String.fromCharCode(event.charCode));
}

function digitOnlyKeyPress(event) {
    return event.charCode === 0 || /\d/.test(String.fromCharCode(event.charCode));
}

function processFilter() {
    var filterByColumn = $("#filterType").val();
    var text = document.getElementById('filterUsage').value;
    var date = document.getElementById('filterDateUsage').value;
    $(".activity-row").each(function () {
        var isHidden = true;
        var isHiddenDate = true;
        //check first filter
        if (filterByColumn == -1) {
            $(this).children("td").each(function () {
                if (-1 !== this.innerHTML.toLowerCase().indexOf(text.toLowerCase())) {
                    isHidden = false;
                }
            });
        } else {
            var length = text.length;
            $(this).children("td").each(function (ndx) {
                if (ndx == filterByColumn) {
                    if (-1 !== this.innerHTML.toLowerCase().indexOf(text.toLowerCase())) {
                        isHidden = false;
                    }
                }
            });
        }
        //check date
        $(this).children("td").each(function (ndx) {
            if (ndx == 1) {
                if (-1 !== this.innerHTML.toLowerCase().indexOf(date.toLowerCase())) {
                    isHiddenDate = false;
                }
            }
        });
        if (isHidden || isHiddenDate) {
            $(this).addClass("hide");
        }
        else {
            $(this).removeClass("hide");
        }
    });
}

function deleteItemClick(id, event) {
    var callback = function (d) {
        if (d.isValid) {
            $.post("/Utility/GetConfirmationHTML", {}, function (html) {
                $("#modal").replaceWith(html);
                $("#modal").modal();
                $("#confirm-button").click(function (e) {
                    $.post("/Admin/CancelReservation", { id: id }, function (data) {
                        $.notify.addMessage("Successfully cancelled!", { type: "success", time: 6000 });
                        var sel = "removeItemById('" + id + "')";
                        if ($("button[onclick='" + sel + "']")) {
                            $(".view-container-right").html("");
                        }
                        var item = $("#view-tr-" + id);
                        rollUp(item, 300, function () { item.remove(); });
                        $("#modal").modal("hide");
                    });
                });
            });
        }
    }
    $.post("/Utility/IsValidCancelReservation", { id: id }, function (d) {
        callback(d);
    });
    if (event) {
        event.stopPropagation();
        event.preventDefault();
    }
    viewReservations();
    return false;
}