
var modallog = new bootstrap.Modal(document.querySelector('#kt_modal_update_log'));
var frm = document.querySelector("#kt_modal_updatelog");

const logfrm = FormValidation
    .formValidation(frm,
        {
            fields: {
                logtext: {
                    validators: {
                        notEmpty: {
                            message: "Log Text is required"
                        }
                    }
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap: new FormValidation.plugins.Bootstrap5({
                    rowSelector: ".fv-row",
                    eleInvalidClass: "",
                    eleValidClass: ""
                })
            }
        }
    );


$(document).ready(function () {
    $(".date-filter").daterangepicker({
        showDropdowns: true,
        minYear: 1981,
        maxYear: parseInt(moment().format("YYYY"), 10),
        //autoUpdateInput: false,
    }, function (start, end, label) {
    });

    //$('.date-filter').on('apply.daterangepicker', function (ev, picker) {
    //    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
    //});

    //$('.date-filter').on('cancel.daterangepicker', function (ev, picker) {
    //    $(this).val('');
    //});

    doFilter();

    $('#btn_log_send').click(function (e) {
        e.preventDefault();
        logfrm.validate().then(function (s) {
            if ("Valid" == s) {
                $('#btn_log_send').disabled = !0;
                $('#btn_log_send').attr("data-kt-indicator", "on");
                setTimeout(function () {
                    $('#btn_log_send').removeAttr("data-kt-indicator");
                    $('#btn_log_send').disabled = !1;

                    //ajax call for the submit;

                    $.ajax({
                        type: "POST",
                        url: '/ServiceUserLog/UpdateLog',
                        data: { id: $('#hdnlogid').val(), logtext: $('#txtlogtext').val() },
                        success: function (data) {
                            if (data == 1) {
                                Swal.fire({
                                    text: "Data has been successfully submitted!",
                                    icon: "success",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                }).then(function (q) {
                                    ClearLogdata();
                                    q.isConfirmed && modallog.hide();
                                    window.location.reload();
                                });
                            }
                            else {

                            }
                        }
                    });
                    //on success show message
                }, 2e3);

            }
            else {
                Swal.fire({
                    text: "Sorry, looks like there are some errors detected, please try again.",
                    icon: "error",
                    buttonsStyling: !1,
                    confirmButtonText: "Ok, got it!",
                    customClass: {
                        confirmButton: "btn btn-light"
                    }
                });
            }
        });
    });

});

function ResetFilter() {
    $("#chkIsArchive").prop('checked', false);
    $("#sortByUser").val('');
    $("#txtdaterange").val('');
    doFilter();
}

function ClearLogdata() {
    $('#txtlogtext').val('');
    $('#hdnlogid').val('');
}


function IsApproveLog(ctrl) {
    var id = $(ctrl).attr('id');
    var status = $(ctrl).attr('logstatus');
    Swal.fire({
        title: 'Are you sure?',
        text: "It will be change status!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, changed it!',
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve) {
                $.ajax({
                    url: '/ServiceUserLog/IsApprove',
                    type: 'POST',
                    data: { id: id, status: status },
                    dataType: 'json'
                }).done(function (response) {
                    //$(ctrl).closest('tr').find('.approvestatus').html("<span class='badge badge-light-success'>Approved</span>");
                    Swal.fire('Changed!', 'Your log has been changed.', 'success');
                    window.location.reload();

                }).fail(function () {
                    Swal.fire('Oops...', 'Something went wrong with ajax !', 'error')
                });
            });
        },
    });
}


function IsInVisibleLog(ctrl) {
    var id = $(ctrl).attr('id');
    var status = $(ctrl).attr('logstatus');

    Swal.fire({
        title: 'Are you sure?',
        text: "It will be change permanently!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, changed it!',
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve) {
                $.ajax({
                    url: '/ServiceUserLog/IsInVisible',
                    type: 'POST',
                    data: { id: id, status: status },
                    dataType: 'json'
                }).done(function (response) {
                    Swal.fire('Changed!', 'Your log has been changed.', 'success');
                    window.location.reload();

                }).fail(function () {
                    Swal.fire('Oops...', 'Something went wrong with ajax !', 'error')
                });
            });
        },
    });
}


function AdminUpdateLog(ctrl) {
    ClearLogdata();
    $('#kt_modal_update_log').modal('show');
    var id = $(ctrl).attr('id');
    $('#lblname').text($(ctrl).attr('name'));

    $.ajax({
        type: "POST",
        url: '/ServiceUsers/GetByLogId',
        data: { id: id },
        success: function (data) {
            if (data !=null) {
                if (data.logMessageUpdated != null) {
                    $('#txtlogtext').val(data.logMessageUpdated);

                } else{
                    $('#txtlogtext').val(data.logMessage);
                }

            } else { }
        }
    });

    $('#hdnlogid').val(id);
}

async function doFilter() {
    var optFilterByApprove = $("#chkIsArchive").prop('checked');
    var optSortBy = $("#sortByUser").val();
    var daterange = $("#txtdaterange").val();
    await $.ajax({
        type: "POST",
        url: '/ServiceUserLog/SortFilterOptionList',
        data: { sortBy: optSortBy, filterBy: optFilterByApprove, daterange: daterange },
        success: function (data) {
            if (data) {
                $('#partialViewDataContent').html('');
                $('#partialViewDataContent').html(data);
                $('.btntogal').trigger('click');
            } else { }
        }
    });  
}