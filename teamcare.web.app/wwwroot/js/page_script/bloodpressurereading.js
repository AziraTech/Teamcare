var frmeditblood = null;

var editbloodfrm = null;

$(document).ready(function () {
    GetBloodData();
    fillBloodPressureChart(null);
});

function ResetFilterBlood() {
    $("#txtdaterangeblood").val('');
    GetBloodData();

}
function fillBloodPressureChart(daterange) {
    $.ajax({
        type: "GET",
        url: '/BloodPressure/GetBloodReadingChartData',
        data: { id: $('#hdnserviceuserid').val(), daterange: daterange },
        success: function (data) {
            if (data && data.statuscode === 1) {
                $('#kt_charts_blood_pressure_chart').html("");
                var systolic = [], diastolic = [], pulse = [];
                var category = [];
                maxValue = 25;
                data.data.forEach(function (element) {
                    if (element.systolicReading > maxValue) {
                        maxValue = element.systolicReading;
                    }
                    if (element.diastolicReading > maxValue) {
                        maxValue = element.diastolicReading;
                    }
                    if (element.pulse > maxValue) {
                        maxValue = element.pulse;
                    }
                    category.push(element.testDate);
                    systolic.push(element.systolicReading);
                    diastolic.push(element.diastolicReading);
                    pulse.push(element.pulse);
                });
                var options = {
                    series: [
                        {
                            name: "SYSTOLIC READING",
                            data: systolic //[28, 29, 33, 36, 32, 32, 33]
                        },
                        {
                            name: "DIASTOLIC READING",
                            data: diastolic //[12, 11, 14, 18, 17, 13, 13]
                        },
                        {
                            name: "PULSE",
                            data: pulse //[56, 40, 75, 78, 79, 80, 90]
                        }
                    ],
                    chart: {
                        height: 500,
                        type: 'line',
                        dropShadow: {
                            enabled: true,
                            color: '#000',
                            top: 18,
                            left: 7,
                            blur: 10,
                            opacity: 0.2
                        },
                        toolbar: {
                            show: false
                        }
                    },
                    colors: ['#FF2E2E', '#2E2EFF', '#2EFF2E'],
                    dataLabels: {
                        enabled: true,
                    },
                    stroke: {
                        curve: 'smooth'
                    },
                    title: {
                        text: 'Blood Pressure Analysis',
                        align: 'left'
                    },
                    grid: {
                        borderColor: '#e7e7e7',
                        row: {
                            colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                            opacity: 0.5
                        },
                    },
                    markers: {
                        size: 1
                    },
                    xaxis: {
                        categories: category,//['16-Jan', '17-Jan', '18-Jan', '19-Jan', '20-Jan', '21-Jan', '22-Jan'],
                        title: {
                            text: 'Dates'
                        }
                    },
                    yaxis: {
                        title: {
                            text: 'Blood Pressure'
                        },
                        min: 0,
                        max: (maxValue + 10)
                    },
                    legend: {
                        position: 'top',
                        horizontalAlign: 'right',
                        floating: true,
                        offsetY: -25,
                        offsetX: -5
                    }
                };

                var chart = new ApexCharts(document.querySelector("#kt_charts_blood_pressure_chart"), options);
                chart.render();
            }
            else {
                $('#kt_charts_blood_pressure_chart').html("No data found");
            }
        }
    });
}
function GetBloodData(filtered) {
    var daterange = $("#txtdaterangeblood").val();

    $.ajax({
        type: "GET",
        url: '/BloodPressure/GetBloodReadingData',
        data: { id: $('#hdnserviceuserid').val(), daterange: daterange },
        success: function (data) {
            if (data != undefined) {
                $('#divblooddataContent').html('');
                $('#divblooddataContent').html(data);
                if (filtered) {
                    fillBloodPressureChart(daterange);
				}
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

