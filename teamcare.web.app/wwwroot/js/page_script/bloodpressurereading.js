var frmeditblood = null;

var editbloodfrm = null;

$(document).ready(function () {
    GetBloodData();
});

function ResetFilterBlood() {
    $("#txtdaterangeblood").val('');
    GetBloodData();

}

function GetBloodData() {
    var daterange = $("#txtdaterangeblood").val();

    $.ajax({
        type: "GET",
        url: '/BloodPressure/GetBloodReadingData',
        data: { id: $('#hdnserviceuserid').val(), daterange: daterange },
        success: function (data) {
            if (data != undefined) {
                $('#divblooddataContent').html('');
                $('#divblooddataContent').html(data);

                $('.blooddropdown').hover(
                    function () {

                        $(this).find('ul').css({
                            "display": "block",
                            "margin-top": 0
                        });

                    },
                    function () {

                        $(this).find('ul').css({
                            "display": "none",
                            "margin-top": 0
                        });

                    }
                );


                $(".date-filter-blood").daterangepicker({
                    showDropdowns: true,
                    minYear: 1981,
                    maxYear: parseInt(moment().format("YYYY"), 10),
                    locale: {
                        format: 'DD/MM/yyyy'
                    }
                }, function (start, end, label) {
                });


            }
        }
    });
}

function openAddEditBloodModal(id) {

    $.ajax({
        type: "POST",
        url: '/BloodPressure/BloodModalBind',
        data: { id: id },
        success: function (data) {
            if (data != null) {
                $('#divAddEditBloodReading').html('');
                $('#divAddEditBloodReading').html(data);

                $("#txttestdate").daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    minYear: 1981,
                    maxYear: parseInt(moment().format("YYYY"), 10),
                    startDate: new Date(),
                    locale: {
                        format: 'DD/MM/yyyy'
                    },

                }, function (start, end, label) {
                });

              
                frmeditblood = document.querySelector("#kt_modal_new_blood");

                editbloodfrm = FormValidation
                    .formValidation(frmeditblood,
                        {
                            fields: {
                                testdate: {
                                    validators: {
                                        notEmpty: {
                                            message: "Testdate is required"
                                        }
                                    }
                                },
                                systolicreading: {
                                    validators: {
                                        notEmpty: {
                                            message: "Systolic Reading is required"
                                        }
                                    }
                                },
                                diastolicreading: {
                                    validators: {
                                        notEmpty: {
                                            message: "Diastolic Reading is required"
                                        }
                                    }
                                },
                                pulse: {
                                    validators: {
                                        notEmpty: {
                                            message: "Pulse is required"
                                        }
                                    }
                                },
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

                if (id == "") {
                    $('#lblheader').text('Create Blood Pressure Reading');
                } else {

                    $('#lblheader').text('Edit Blood Pressure Reading');
                }

                $('#create_bloodrecord').modal('show');

            }
            else {

            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function saveBloodReadingDetails(sender) {

    editbloodfrm.validate().then(function (s) {
        if ("Valid" == s) {

            $('#btnaddbloodreding').disabled = !0;
            $('#btnaddbloodreding').attr("data-kt-indicator", "on");
            setTimeout(function () {
                $('#btnaddbloodreding').removeAttr("data-kt-indicator");
                $('#btnaddbloodreding').disabled = !1;

                //ajax call for the submit;
                var BloodPressureReading = {
                    Id: $('#hdnbloodid').val(),
                    ServiceUserId: $('#hdnserviceuserid').val(),
                    BloodTestdate: $('#txttestdate').val(),
                    SystolicReading: $('#txtsystolicreading').val(),
                    DiastolicReading: $('#txtdiastolicreading').val(),
                    Pulse: $('#txtpulse').val(),


                }
                var bloodPressureReadingViewModel = {
                    BloodPressureReading: BloodPressureReading
                }
                $.ajax({
                    type: "POST",
                    url: '/BloodPressure/Save',
                    data: { bloodPressureReadingViewModel: bloodPressureReadingViewModel },
                    success: function (data) {
                        if (data.statuscode == 3) {
                            Swal.fire({
                                text: data.message,
                                icon: "error",
                                buttonsStyling: !1,
                                confirmButtonText: "Ok, got it!",
                                customClass: {
                                    confirmButton: "btn btn-light"
                                }
                            });
                        } else {
                            Swal.fire({
                                text: "Blood Pressure Reading has been created successfully.",
                                icon: "success",
                                buttonsStyling: !1,
                                confirmButtonText: "Ok, got it!",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then(function (q) {
                                q.isConfirmed;
                                $('#create_bloodrecord').modal('hide');
                                $('.modal-backdrop').remove();
                                //$("body").css("overflow", "");
                                //$("body").css("padding-right", "");

                                GetBloodData();
                            });
                        }
                    }
                });
                //on success show message
            }, 2e3);

        }
        else {
            Swal.fire({
                text: "Sorry, looks like there are some feilds is required, please try again.",
                icon: "error",
                buttonsStyling: !1,
                confirmButtonText: "Ok, got it!",
                customClass: {
                    confirmButton: "btn btn-light"
                }
            });
        }
    });
}

function DeleteBloodReading(ctrl, Id) {

    Swal.fire({
        title: 'Are you sure?',
        text: "It will be deleted permanently!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve) {
                $.ajax({
                    type: "POST",
                    url: '/BloodPressure/Delete',
                    data: { id: Id },
                    success: function (data) {
                        if (data.statuscode == 3) {
                            Swal.fire({
                                text: data.message,
                                icon: "error",
                                buttonsStyling: !1,
                                confirmButtonText: "Ok, got it!",
                                customClass: {
                                    confirmButton: "btn btn-light"
                                }
                            });

                        } else {
                            Swal.fire({
                                text: "Contact Removed Successfully!",
                                icon: "success",
                                buttonsStyling: !1,
                                confirmButtonText: "Ok, got it!",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then(function (q) {
                                q.isConfirmed;
                                $('.modal-backdrop').remove();
                                GetBloodData();
                            });

                        }
                    }
                });

            });
        },
    });
}