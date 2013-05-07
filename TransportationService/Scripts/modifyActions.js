function updateReservation(reservationId) {
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
        reservationId: reservationId,
        customerName: customerName,
        partySize: partySize,
        phoneNumber: phoneNumber,
        time: time,
        date: date
    }
    jQuery.ajaxSettings.traditional = true;
    $.post("/Admin/UpdateReservation", request, function (data) {
        if (data.success == "true") {
            $.notify.addMessage("The reservation was successfully updated!", { type: "success", time: 6000 });
            $("#modal").modal('hide');
            $("#view-tr-" + data.id + " > td:nth-child(1)").text(request.customerName);
            $("#view-tr-" + data.id + " > td:nth-child(2)").text(data.dateTime);
            $("#view-tr-" + data.id + " > td:nth-child(3)").text(request.partySize);
            $("#view-tr-" + data.id + " > td:nth-child(4)").text(request.phoneNumber);
            $("#view-item-table").trigger("update");
        }
    });
}

function modifyReservation(event, rId) {
    $.post("/Admin/ModifyReservation", { reservationId: rId }, function (data) {
        $("#modal").replaceWith(data);
        $("#modal").modal();
    });
    event.stopPropagation();
    event.preventDefault();
    return false;
}