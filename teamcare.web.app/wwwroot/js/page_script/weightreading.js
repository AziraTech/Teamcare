var frmeditweight = null;

var editweightfrm = null;

$(document).ready(function () {
  
    GetweightData();
    fillWeightChart(null);
});

function ResetFilterWeight() {
    $("#txtdaterangeweight").val('');
    GetweightData();

}

function fillWeightChart(daterange) {
    $.ajax({
        type: "GET",
        url: '/Weightreading/GetWeightReadingChartData',
        data: { id: $('#hdnserviceuserid').val(), daterange: daterange },
        success: function (data) {
            if (data && data.statuscode === 1) {
                $('#kt_charts_weight_chart').html("");
                var weight = [];
                var category = [];
                maxValue = 25;

                data.data.forEach(function (element) {
                    if (element.weight > maxValue) {
                        maxValue = element.weight;
                    }

                    category.push(element.testDate);
                    weight.push(element.weight);
                });
                var options = {
                    series: [{
                        name: "Weight",
                        data: weight
                    }],
                    chart: {
                        height: 500,
                        type: 'line',
                        zoom: {
                            enabled: false
                        }
                    },
                    dataLabels: {
                        enabled: false
                    },
                    stroke: {
                        curve: 'straight'
                    },
                    title: {
                        text: 'Weight Analysis',
                        align: 'left'
                    },
                    grid: {
                        row: {
                            colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                            opacity: 0.5
                        },
                    },
                    xaxis: {
                        categories: category,
                        title: {
                            text: 'Dates'
                        }
                    },
                    yaxis: {
                        title: {
                            text: 'Weight'
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

                var chart = new ApexCharts(document.querySelector("#kt_charts_weight_chart"), options);
                chart.render();
            }
            else {
                $('#kt_charts_weight_chart').html("No data found");
            }
        }
    });
}

function GetweightData(filtered) {
    var daterange = $("#txtdaterangeweight").val();

    $.ajax({
        type: "GET",
        url: '/Weightreading/GetWeightReadingData',
        data: { id: $('#hdnserviceuserid').val(), daterange: daterange },
        success: function (data) {
            if (data != undefined) {
                $('#divWeightdataContent').html('');
                $('#divWeightdataContent').html(data);
                if (filtered) {
                    fillWeightChart(daterange);
				}
                $(".date-filter-weight").daterangepicker({
                    showDropdowns: true,
                    minYear: 1981,
                    maxYear: parseInt(moment().format("YYYY"), 10),
                    locale: {
                        format: 'DD/MM/yyyy'
                    }
                }, function (start, end, label) {
                });

                $('.weightdropdown').hover(
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
            }
        }
    });
}

function openAddEditWeightModal(id) {

    $.ajax({
        type: "POST",
        url: '/WeightReading/WeightModalBind',
        data: { id: id },
        success: function (data) {
            if (data != null) {
                $('#divAddEditWeightReading').html('');
                $('#divAddEditWeightReading').html(data);

                $("#txtweighttestdate").daterangepicker({
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


                frmeditweight = document.querySelector("#kt_modal_new_weight");

                editweightfrm = FormValidation
                    .formValidation(frmeditweight,
                        {
                            fields: {
                                testdate: {
                                    validators: {
                                        notEmpty: {
                                            message: "Testdate is required"
                                        }
                                    }
                                },
                                weighttestdate: {
                                    validators: {
                                        notEmpty: {
                                            message: "Weigt is required"
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

                if (id == "") {
                    $('#lblheaderweight').text('Create Weight Reading');
                } else {

                    $('#lblheaderweight').text('Edit Weight Reading');
                }

                $('#create_weightrecord').modal('show');

            }
            else {

            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function saveweightReadingDetails(sender) {

    editweightfrm.validate().then(function (s) {
        if ("Valid" == s) {

            $('#btnaddweightreding').disabled = !0;
            $('#btnaddweightreding').attr("data-kt-indicator", "on");
            setTimeout(function () {
                $('#btnaddweightreding').removeAttr("data-kt-indicator");
                $('#btnaddweightreding').disabled = !1;

                //ajax call for the submit;
                var weightReading = {
                    Id: $('#hdnweightid').val(),
                    ServiceUserId: $('#hdnserviceuserid').val(),
                    WeightTestdate: $('#txtweighttestdate').val(),
                    Weight: $('#txtweight').val(),

                }
                var weightReadingViewModel = {
                    weightReading: weightReading
                }
                $.ajax({
                    type: "POST",
                    url: '/Weightreading/Save',
                    data: { weightReadingViewModel: weightReadingViewModel },
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
                                text: "Weight Reading has been created successfully.",
                                icon: "success",
                                buttonsStyling: !1,
                                confirmButtonText: "Ok, got it!",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then(function (q) {
                                q.isConfirmed;
                                $('#create_weightrecord').modal('hide');
                                $('.modal-backdrop').remove();
                                //$("body").css("overflow", "");
                                //$("body").css("padding-right", "");

                                GetweightData();
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

function DeleteWeightReading(ctrl, Id) {

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
                    url: '/WeightReading/Delete',
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
                                GetweightData();
                            });

                        }
                    }
                });

            });
        },
    });
}