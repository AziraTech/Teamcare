
var frmassettype = null;
var frmvalid = null;

$(document).ready(function () {

    BindType();
  
});


function BindType() {
        $.ajax({
            type: 'POST',
            url: '/AssessmentType/BindType',
            data: { },
            success: function (htmlresponse) {
                if (htmlresponse != null) {
                    $('#AssetTypeDataContent').html('');
                    $('#AssetTypeDataContent').html(htmlresponse);

                    $("#sortable").sortable({
                        /*stop: function(event, ui) {
                            alert("New position: " + ui.item.index());
                        }*/
                        //start: function (e, ui) {
                        //    // creates a temporary attribute on the element with the old index
                        //    $(this).attr('data-previndex', ui.item.index());
                        //},
                        update: function (e, ui) {
                            var itemOrder = $('#sortable').sortable("toArray");
                            var AssessmentTypeList = new Array();
                            for (var i = 0; i < itemOrder.length; i++) {
                                var AssessmentType = {};
                                AssessmentType.Id = itemOrder[i];
                                AssessmentType.Position = i;
                                AssessmentTypeList.push(AssessmentType);
                            }

                            var assessmentTypeCreateViewModel = {
                                AssessmentTypeList: AssessmentTypeList,
                            }

                            $.ajax({
                                type: "POST",
                                url: '/AssessmentType/MovePosition',
                                data: { assessmentTypeCreateViewModel: assessmentTypeCreateViewModel },
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

                                    }
                                }
                            });

                        }
                    });
                    $("#sortable").disableSelection();
                }
            }, error: function (e) {

            }
        });
    
}


function DeleteType(ctrl) {
    var id = $(ctrl).attr('id');

    Swal.fire({
        title: 'Are you sure?',
        text: "you want to delete this type? \r\n All group & skills within this type will also be deleted.",
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
                    url: '/AssessmentType/DeleteAssessmentType',
                    data: { id: id },
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
                                text: "Type Removed Successfully!",
                                icon: "success",
                                buttonsStyling: !1,
                                confirmButtonText: "Ok, got it!",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then(function (q) {
                                q.isConfirmed;
                                window.location.reload();
                            });
                        }
                    }
                });
            });
        },
    });
}

function openAddEditAssetTypeModal(id) {

    $.ajax({
        type: "POST",
        url: '/AssessmentType/AssessmentTypeBind',
        data: { id: id },
        success: function (data) {
            if (data != null) {
                $('#divAddEditType').html('');
                $('#divAddEditType').html(data);

                $('.ddlselect2').select2();

                frmassettype = document.querySelector("#kt_modal_assettype");

                 frmvalid = FormValidation
                    .formValidation(frmassettype,
                        {
                            fields: {
                                name: {
                                    validators: {
                                        notEmpty: {
                                            message: "Name is required"
                                        }
                                    }
                                },
                                optionsgroup: {
                                    validators: {
                                        notEmpty: {
                                            message: "Optionsgroup is required"
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


                if (id == null) {
                    $('#lblheader').text('Create Assessment Type');
                } else {

                    $('#lblheader').text('Edit Assessment Type');
                }

                $('#modalAddEditType').modal('show');

            }
            else {

            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function saveAssessmentType(sender) {

    frmvalid.validate().then(function (s) {
        if ("Valid" == s) {

            $('#typesubmit').disabled = !0;
            $('#typesubmit').attr("data-kt-indicator", "on");
                setTimeout(function () {
                    $('#typesubmit').removeAttr("data-kt-indicator");
                    $('#typesubmit').disabled = !1;

                    //ajax call for the submit;
                    var AssessmentType = {
                        Id: $('#hdnassettypeid').val(),
                        TypeName: $('#txttypename').val(),
                        OptionsGroup: $('#ddloptionsgroup').val()
                    }
                    var assessmentTypeCreateViewModel = {
                        AssessmentType: AssessmentType,
                    }
                    $.ajax({
                        type: "POST",
                        url: '/AssessmentType/Save',
                        data: { assessmentTypeCreateViewModel: assessmentTypeCreateViewModel },
                        success: function (data) {
                            if (data.statuscode == 1) {
                                $('#modalAddEditType').hide();
                                //window.location.reload();
                                BindType();
                                $('#txttypename').val('');
                                $('.modal-backdrop').remove();

                            } else if (data.statuscode == 2) {
                                Swal.fire({
                                    text: "Sorry, TYpe Name already exists, please try again different type name.",
                                    icon: "info",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-light"
                                    }
                                });
                            }
                            else {
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