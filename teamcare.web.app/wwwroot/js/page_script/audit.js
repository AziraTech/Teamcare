$(document).ready(function () {
    $(".date-filter").daterangepicker({
        showDropdowns: true,
        minYear: 1981,
        maxYear: parseInt(moment().format("YYYY"), 10),
        locale: {
            format: 'DD/MM/yyyy'
        }
    }, function (start, end, label) {
    });

    doFilter();
  
});

function ResetFilter() {
    $("#sortByUser").val('');
    $("#sortByUser").select2().trigger('change');
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
            if (data !=null) {
                $('#partialViewDataContent').html('');
                $('#partialViewDataContent').html(data);
            } else {
                Swal.fire({
                    text: data.message,
                    icon: "error",
                    buttonsStyling: !1,
                    confirmButtonText: "Ok, got it!",
                    customClass: {
                        confirmButton: "btn btn-light"
                    }
                });
            }
        }
    });
}
