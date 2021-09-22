var userupdId = $('#hdnuserid').val();
var base64Image = null;
var fileName = null;
var fileType = null;
var tempFileId = null;
    
var userupdfrm = document.querySelector("#kt_account_profile_details_update");

const upduserfrm = FormValidation
    .formValidation(userupdfrm,
        {
            fields: {
                mytitle: {
                    validators: {
                        notEmpty: {
                            message: "Select user title is required"
                        }
                    }
                },
                myfirst_name: {
                    validators: {
                        notEmpty: {
                            message: "FirstName is required"
                        }
                    }
                },
                mylast_name: {
                    validators: {
                        notEmpty: {
                            message: "LastName is required"
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

    $("#user-profile-photo-update").change(function () {

        var formData = new FormData();     
        formData.append('image', $('#user-profile-photo-update')[0].files[0]); 

        $.ajax({
            type: "POST",
            url: '/DocumentUpload',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                tempFileId = response.id;
                fileName = response.fileName;
                fileType = response.contentType;
            }
        });
    });

    $("#btntab").click(function () {
        $(".nav a[href='#kt_profile_details_edit']").tab("show");
    });

    $("#btndiscard").click(function (e) {
        e.preventDefault();
        window.location.reload();
    });

    $('#account_profile_Update_submit').click(function (e) {
        e.preventDefault();
        upduserfrm.validate().then(function (s) {
            if ("Valid" == s) {

                //ajax call for the submit;
                var User = {
                    Id: userupdId,
                    Title: $('#ddlmytitle').val(),
                    FirstName: $('#myfirstname').val(),
                    LastName: $('#mylastname').val(),
                    Position: $('#myposition').val(),
                    Mobile_No: $('#mymobileno').val(),
                    Phone_No: $('#myphoneno').val(),
                    Address: $('#myaddress').val(),
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
                    url: '/User/MyProfileUpdate',
                    data: { userCreateViewModel: userCreateViewModel },
                    success: function (data) {
                        if (data.statuscode == 1) {
                            Swal.fire({
                                text: "Profile updated has been successfully.",
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