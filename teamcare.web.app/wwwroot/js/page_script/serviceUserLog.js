$(document).ready(function () {
    $(".date-dob").daterangepicker({
        showDropdowns: true,
        minYear: 1981,
        maxYear: parseInt(moment().format("YYYY"), 10)
    }, function (start, end, label) {
    });


});

function IsApproveLog(ctrl) {
    var id = $(ctrl).attr('id');
    Swal.fire({
        title: 'Are you sure?',
        text: "It will be approved permanently!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, approved it!',
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve) {             
                $.ajax({
                    url: '/ServiceUserLog/IsApprove',
                    type: 'POST',
                    data: { id: id },
                    dataType: 'json'
                }).done(function (response) {

                    $(ctrl).closest('tr').find('.approvestatus').html("<span class='badge badge-light-success'>Approved</span>");

                    Swal.fire('Approved!', 'Your log has been approved.', 'success')
                }).fail(function () {
                    Swal.fire('Oops...', 'Something went wrong with ajax !', 'error')
                });
            });
        },
    });
}


function IsInVisibleLog(ctrl) {
    var id = $(ctrl).attr('id');
    Swal.fire({
        title: 'Are you sure?',
        text: "It will be hide permanently!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, hide it!',
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve) {
                $.ajax({
                    url: '/ServiceUserLog/IsInVisible',
                    type: 'POST',
                    data: { id: id },
                    dataType: 'json'
                }).done(function (response) {
                    $(ctrl).closest('tr').hide();
                    Swal.fire('Approved!', 'Your log has been visible.', 'success')
                }).fail(function () {
                    Swal.fire('Oops...', 'Something went wrong with ajax !', 'error')
                });
            });
        },
    });
}