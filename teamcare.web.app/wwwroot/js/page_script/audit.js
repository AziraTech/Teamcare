$(document).ready(function () {
    $(".date-filter").daterangepicker({
        showDropdowns: true,
        minYear: 1981,
        maxYear: parseInt(moment().format("YYYY"), 10),
        //autoUpdateInput: false,
    }, function (start, end, label) {
    });

    doFilter();
  
});

function ResetFilter() {
    $("#sortByUser").val('');
    $("#txtdaterange").val('');
    doFilter();
}

function ClearLogdata() {
    $('#txtlogtext').val('');
    $('#hdnlogid').val('');
}


async function doFilter() {
    var optSortBy = $("#sortByUser").val();
    var daterange = $("#txtdaterange").val();
    await $.ajax({
        type: "POST",
        url: '/Audit/SortFilterOptionList',
        data: { filterByserviceuser: optSortBy, daterange: daterange },
        success: function (data) {
            if (data) {
                $('#partialViewDataContent').html('');
                $('#partialViewDataContent').html(data);
            } else { }
        }
    });
}
