var userId = $('#selectedId').val();
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
var userfrm = document.querySelector("#kt_modal_new_userform");

const ufrm = FormValidation
    .formValidation(userfrm,
        {
            fields: {
                usertitle: {
                    validators: {
                        notEmpty: {
                            message: "Select user title is required"
                        }
                    }
                },
                first_name: {
                    validators: {
                        notEmpty: {
                            message: "FirstName is required"
                        }
                    }
                },
                last_name: {
                    validators: {
                        notEmpty: {
                            message: "LastName is required"
                        }
                    }
                },
                email: {
                    validators: {
                        notEmpty: {
                            message: "Email address is required"
                        },
                        emailAddress: {
                            message: "The value is not a valid email address"
                        }
                    }
                },
                userrole: {
                    validators: {
                        notEmpty: {
                            message: "UserRole is required"
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

    $('#new_card_user_submit').click(function (e) {
        e.preventDefault();
        ufrm.validate().then(function (s) {
            if ("Valid" == s) {
                $('#new_card_user_submit').disabled = !0;
                $('#new_card_user_submit').attr("data-kt-indicator", "on");
                setTimeout(function () {
                    $('#new_card_user_submit').removeAttr("data-kt-indicator");
                    $('#new_card_user_submit').disabled = !1;

                    //ajax call for the submit;
                    var User = {
                        Id: userId,
                        Title: $('#ddltitle').val(),
                        FirstName: $('#txtfirstname').val(),
                        LastName: $('#txtlastname').val(),
                        Email: $('#txtemail').val(),
                        UserRole: $('#txtrole').val(),
                        Position: $('#txtposition').val(),
                        Mobile_No: $('#txtmobileno').val(),
                        Phone_No: $('#txtphoneno').val(),
                        Address: $('#txtaddress').val(),
                        IsActive: true,
                        FileName: fileName,
                        FileType: fileType,
                        TempFileId: tempFileId

                    }
                    var userCreateViewModel = {
                        User: User,
                        TempFileId: tempFileId
                    }

                    $.ajax({
                        type: "POST",
                        url: '/User/Save',
                        data: { userCreateViewModel: userCreateViewModel },
                        success: function (data) {
                            if (data.statuscode == 1) {
                                Swal.fire({
                                    text: "User has been successfully submitted!",
                                    icon: "success",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                }).then(function (q) {
                                    Cleardata();
                                    q.isConfirmed && modal.hide();
                                    window.location.reload();
                                });
                            }
                            else if (data.statuscode == 2) {
                                Swal.fire({
                                    text: "Sorry, user email already exists, please try again different email.",
                                    icon: "info",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-light"
                                    }
                                });

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
                    //on success show message
                }, 2e3);
            }
            else {
                Swal.fire({
                    text: "Sorry, looks like there are some feilds is required, please try again.",
                    icon: "info",
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

function Cleardata() {
    $('#ddltitle').val('');
    $('#txtfirstname').val('');
    $('#txtlastname').val('');
    $('#txtemail').val('');
    $('#txtrole').val('');
    $('#txtposition').val('');
    $('#txtmobileno').val('');
    $('#txtphoneno').val('');
    $('#txtaddress').val('');
}

function RemoveProfile() {
    $('#OldProfile').remove();
}