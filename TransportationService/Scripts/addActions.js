function validPartySize(partySize) {
    return partySize > 0 && partySize < 26;
}

function validTime(time) {
    return /1(0|1):\d{2} AM/.test(time) || /(12)|(0?[1-8]):\d{2} PM/.test(time) || /0?9:00 PM/.test(time);
}

function addNewReservation() {
    var customerName = $("#customerName").val();
    var partySize = parseInt($("#partySize").val());
    var phoneNumber = $("#phoneNumber").val();
    var date = $("#datepicker").val();
    var time = $("#time").val() + " " + $("#ampm").val();
    if (customerName == "" || !validPartySize(partySize) || phoneNumber.length != 10 || date == "" || !validTime(time)) {
        var messageBuilder = new MessageBuilder();
        if (customerName == "") {
            messageBuilder.addMessage("must include a name");
        }
        if (phoneNumber.length != 10) {
            messageBuilder.addMessage("must include a phone number (10 digits)");
        }
        if (date == "") {
            messageBuilder.addMessage("must include a date");
        }
        if (!validTime(time)) {
            messageBuilder.addMessage("must include a time between 10:00 AM and 9:00 PM");
        }
        if (!validPartySize(partySize)) {
            messageBuilder.addMessage("must include a party size between 1 and 25");
        }
        $("#reservationFailureMessage > .error-text").text(messageBuilder.getMessage("The reservation cannot be added because you"));
        rollDown($("#reservationFailureMessage"));
        setTimeout(function () {
            rollUp($("#reservationFailureMessage"));
        }, 10000);
        return false;
    }
    var request = {
        customerName: customerName,
        partySize: partySize,
        phoneNumber: phoneNumber,
        time: time,
        date: date
    }
    jQuery.ajaxSettings.traditional = true;
    $.post("/Admin/AddNewReservation", request, function (data) {
        if (data.success == "true") {
            $.notify.addMessage("The reservation was successfully added!", { type: "success", time: 6000 });
            $("#modal").modal('hide');
            viewReservations();
        } 
    });
}

function getInnerViewItem(text) {
    return '<div class="icon-remove delete-item"></div><div class="strip">' + text + '</div>';
}