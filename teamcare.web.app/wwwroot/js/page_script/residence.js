
var base64Image = null;
var fileName = null;
var fileType = null;
var tempFileId = null;
var myDropzone = new Dropzone("#sv-profile-photo", {
    url: "/DocumentUpload", // Set the url for your upload script location
    paramName: "file", // The name that will be used to transfer the file
    maxFiles: 1,
    maxFilesize: 10, // MB
    addRemoveLinks: true,
    acceptedFiles: ".png,.jpg,.gif,.bmp,.jpeg",
    success: function (file, response) {
        tempFileId = response.id;
        fileName = file.name;
        fileType = file.type;
    }
    //accept: function (file, done) {
    //	base64Image = file;
    //	fileName = file.name;
    //	fileType = file.type;

    //	done();
    //}
});

var modal = new bootstrap.Modal(document.querySelector('#kt_modal_create_app'));
var r = document.querySelector("#kt_modal_new_residenceform");
//var upd = document.querySelector("#kt_modal_residenceform_Update");


const fv1 = FormValidation
    .formValidation(r,
        {
            fields: {
                res_name: {
                    validators: {
                        notEmpty: {
                            message: "Name is required"
                        }
                    }
                },
                res_address: {
                    validators: {
                        notEmpty: {
                            message: "Address is required"
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


//const resupd = FormValidation
//    .formValidation(upd,
//        {
//            fields: {
//                res_name: {
//                    validators: {
//                        notEmpty: {
//                            message: "Name is required"
//                        }
//                    }
//                },
//                res_address: {
//                    validators: {
//                        notEmpty: {
//                            message: "Address is required"
//                        }
//                    }
//                },

//            },
//            plugins: {
//                trigger: new FormValidation.plugins.Trigger(),
//                bootstrap: new FormValidation.plugins.Bootstrap5({
//                    rowSelector: ".fv-row",
//                    eleInvalidClass: "",
//                    eleValidClass: ""
//                })
//            }
//        }
//    );

async function setCurrentTab(tabName, id) {
    await $.ajax({
        type: "POST",
        url: '/Residence/SetCurrentTab',
        data: { tabName: tabName, id: id },
        success: function (data) {
            if (data) {

                switch (tabName) {
                    case 'Overview':
                        $('#tbLnkOverview').addClass('active');
                        $('#tbLnkServiceUser').removeClass('active');
                        break;
                    case 'Service_User':
                        $('#tbLnkOverview').removeClass('active');
                        $('#tbLnkServiceUser').addClass('active');
                        break;

                }

                $('#ShowCurrentTab').html('');
                $('#ShowCurrentTab').html(data);
            } else { }
        }
    });
}

function RemoveResidence(serviceUserCount, residenceId)
{
    if (+serviceUserCount > 0) {
        Swal.fire({
            text: 'There are service user available, please change their residence, then Remove.',
            icon: "error",
            buttonsStyling: !1,
            confirmButtonText: "Ok, got it!",
            customClass: { confirmButton: "btn btn-light" }
        });
    }
    else {
        Swal.fire({
            title: 'Are you sure?',
            text: "It will be deleted permanently!, \r\nAlso remove related data.",
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
                        url: '/Residence/Delete',
                        data: { id: residenceId },
                        success: function (data) {
                            if (data.success) {
                                Swal.fire({
                                    text: "Form has been successfully removed!",
                                    icon: "success",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: { confirmButton: "btn btn-primary" }
                                }).then(function (q) {
                                    
                                    var host = location.origin;
                                    //window.location.href = 'http://' + host + '/Residence'
                                    location.replace(host + '/Residence');
                                    //https://localhost/Residence
                                });
                            } else {
                                Swal.fire({
                                    text: data.message,
                                    icon: "error",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: { confirmButton: "btn btn-light" }
                                });
                            }
                        }
                    });
                });
            },
        });
    }

    
}

var newResidenceID = 0;
$(document).ready(function () {

    $('#new_card_residence_submit').click(function (e) {
        e.preventDefault();
        fv1.validate().then(function (s) {
            if ("Valid" == s) {

                $('#new_card_residence_submit').disabled = !0;
                $('#new_card_residence_submit').attr("data-kt-indicator", "on");
                setTimeout(function () {
                    $('#new_card_residence_submit').removeAttr("data-kt-indicator");
                    $('#new_card_residence_submit').disabled = !1;
                    if ($('#hdnresidenceid').val() != null) { newResidenceID = $('#hdnresidenceid').val(); }
                    //ajax call for the submit;
                    var Residence = {
                        Id: newResidenceID,
                        Name: $('#txtname').val(),
                        Address: $('#txtaddress').val(),
                        Capacity: $('#txtcapacity').val(),
                        Home_Tel_No: $('#txttelno').val(),
                        FileName: fileName,
                        FileType: fileType,
                        TempFileId: tempFileId

                    }
                    var residenceCreateViewModel = {
                        Residence: Residence,
                        TempFileId: tempFileId
                    }

                    $.ajax({
                        type: "POST",
                        url: '/Residence/Save',
                        data: { residenceCreateViewModel: residenceCreateViewModel },
                        success: function (data) {

                            if (data.statuscode == 3)
                            {
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
                            else
                            {
                                Swal.fire({
                                    text: "Form has been successfully submitted!",
                                    icon: "success",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                }).then(function (q) {
                                    Cleardata();
                                    q.isConfirmed && modal.hide();
                                });

                                $('#ShowCurrentTab').html('');
                                $('#ShowCurrentTab').html(data);
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

    function RemoveProfile() {
        $('#OldProfile').remove();
    }

    $('#editResidenceRecords').click(function (e) {
        e.preventDefault();
        $('#kt_modal_create_app').modal('show');
    });

    function Cleardata() {
        $('#txtname').val('');
        $('#txtaddress').val('');
        $('#txtcapacity').val('');
        $('#txttelno').val('');
    }
});

