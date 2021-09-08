
var cfileName = null;
var cfileType = null;
var ctempFileId = null;
var myDropzoneContact = new Dropzone("#sv-contact-profile-photo", {
    url: "/DocumentUpload", // Set the url for your upload script location
    paramName: "file", // The name that will be used to transfer the file
    maxFiles: 1,
    maxFilesize: 10, // MB
    addRemoveLinks: true,
    acceptedFiles: ".png,.jpg,.gif,.bmp,.jpeg",
    success: function (file, response) {
        ctempFileId = response;
        cfileName = file.name;
        cfileType = file.type;
    }
});


var modalcontact = new bootstrap.Modal(document.querySelector('#kt_modal_create_contact'));
var frmcontact = document.querySelector("#kt_modal_new_contactform");

const contactfrm = FormValidation
    .formValidation(frmcontact,
        {
            fields: {
                first_name: {
                    validators: {
                        notEmpty: {
                            message: "FirstName is required"
                        }
                    }
                },
                middle_name: {
                    validators: {
                        notEmpty: {
                            message: "MiddleName is required"
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
                relationship: {
                    validators: {
                        notEmpty: {
                            message: "Relationship is required"
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

///////////////////////// Edit Contact

var ufileName = null;
var ufileType = null;
var utempFileId = null;
var myDropzoneEditContact = new Dropzone("#sv-contact-profile-photo_edit", {
    url: "/DocumentUpload", // Set the url for your upload script location
    paramName: "file", // The name that will be used to transfer the file
    maxFiles: 1,
    maxFilesize: 10, // MB
    addRemoveLinks: true,
    acceptedFiles: ".png,.jpg,.gif,.bmp,.jpeg",
    success: function (file, response) {
        utempFileId = response;
        ufileName = file.name;
        ufileType = file.type;
    }
});

var frmeditcontact = document.querySelector("#kt_modal_edit_contactform");

const editcontactfrm = FormValidation
    .formValidation(frmeditcontact,
        {
            fields: {
                ufirst_name: {
                    validators: {
                        notEmpty: {
                            message: "FirstName is required"
                        }
                    }
                },
                umiddle_name: {
                    validators: {
                        notEmpty: {
                            message: "MiddleName is required"
                        }
                    }
                },
                ulast_name: {
                    validators: {
                        notEmpty: {
                            message: "LastName is required"
                        }
                    }
                },
                uemail: {
                    validators: {
                        notEmpty: {
                            message: "Email address is required"
                        },
                        emailAddress: {
                            message: "The value is not a valid email address"
                        }
                    }
                },
                urelationship: {
                    validators: {
                        notEmpty: {
                            message: "Relationship is required"
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

$(document).ready(function () {

    $('#new_card_contact_submit').click(function (e) {
        e.preventDefault();
        contactfrm.validate().then(function (s) {
            if ("Valid" == s) {
                $('#new_card_contact_submit').disabled = !0;
                $('#new_card_contact_submit').attr("data-kt-indicator", "on");
                setTimeout(function () {
                    $('#new_card_contact_submit').removeAttr("data-kt-indicator");
                    $('#new_card_contact_submit').disabled = !1;

                    //ajax call for the submit;
                    var Contact = {
                        Id: 0,
                        ServiceUserId: $('#hdnserviceuserid').val(),
                        Title: $('#ddltitle').val(),
                        FirstName: $('#txtfirstname').val(),
                        MiddleName: $('#txtmiddlename').val(),
                        LastName: $('#txtlastname').val(),
                        Address: $('#txtaddress').val(),
                        Email: $('#txtemail').val(),
                        Mobile: $('#txtmobileno').val(),
                        Telephone: $('#txttelephoneno').val(),
                        Relationship: $('#ddlrelationship').val(),
                        IsNextOfKin: $('#chkIsNxtKin').prop('checked'),
                        IsEmergencyContact: $('#chkIsEmergContact').prop('checked'),
                        FileName: cfileName,
                        FileType: cfileType,
                        TempFileId: ctempFileId

                    }
                    var contactCreateViewModel = {
                        Contact: Contact,
                        TempFileId: ctempFileId
                    }
                    $.ajax({
                        type: "POST",
                        url: '/Contact/Save',
                        data: { contactCreateViewModel: contactCreateViewModel },
                        success: function (data)
                        {                            
                            if (data == 2)
                            {
                                Swal.fire({
                                    text: "Sorry, user email already exists, please try again different email.",
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
                                    q.isConfirmed && modalcontact.hide();
                                });
                                $('#kt_modal_create_contact').modal('hide');
                                $('#contactIndexPage').html('');
                                $('#contactIndexPage').html(data);
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

    $('#edit_card_contact_submit').click(function (e) {
        e.preventDefault();
        editcontactfrm.validate().then(function (s) {
            if ("Valid" == s) {
                $('#edit_card_contact_submit').disabled = !0;
                $('#edit_card_contact_submit').attr("data-kt-indicator", "on");
                setTimeout(function () {
                    $('#edit_card_contact_submit').removeAttr("data-kt-indicator");
                    $('#edit_card_contact_submit').disabled = !1;

                    //ajax call for the submit;
                    var Contact = {
                        Id: $('#hdncontactid').val(),
                        ServiceUserId: $('#hdnsrvuserid').val(),
                        Title: $('#ddlutitle').val(),
                        FirstName: $('#txtufirstname').val(),
                        MiddleName: $('#txtumiddlename').val(),
                        LastName: $('#txtulastname').val(),
                        Address: $('#txtuaddress').val(),
                        Email: $('#txtuemail').val(),
                        Mobile: $('#txtumobileno').val(),
                        Telephone: $('#txtutelephoneno').val(),
                        Relationship: $('#ddlurelationship').val(),
                        IsNextOfKin: $('#chkuIsNxtKin').prop('checked'),
                        IsEmergencyContact: $('#chkuIsEmergContact').prop('checked'),
                        FileName: ufileName,
                        FileType: ufileType,
                        TempFileId: utempFileId

                    }
                    var contactCreateViewModel = {
                        Contact: Contact,
                        TempFileId: utempFileId
                    }
                    $.ajax({
                        type: "POST",
                        url: '/Contact/Save',
                        data: { contactCreateViewModel: contactCreateViewModel },
                        success: function (data)
                        {
                            if (data == 2) {
                                Swal.fire({
                                    text: "Sorry, user email already exists, please try again different email.",
                                    icon: "error",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-light"
                                    }
                                });

                            } else {
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
                                    q.isConfirmed && modalcontact.hide();
                                });
                                $('#kt_modal_edit_contact').modal('hide');
                                $('#contactIndexPage').html('');
                                $('#contactIndexPage').html(data);
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

function Cleardata() {
    $('#ddltitle').val('');
    $('#txtfirstname').val('');
    $('#txtmiddlename').val('');
    $('#txtlastname').val('');
    $('#txtemail').val('');
    $('#txtmobileno').val('');
    $('#txttelephoneno').val('');
    $('#txtaddress').val('');
    $('#ddlrelationship').val('');
    $('#chkIsNxtKin').prop('checked', false);
    $('#chkIsEmergContact').prop('checked', false);

    $('#ddlutitle').val('');
    $('#txtufirstname').val('');
    $('#txtumiddlename').val('');
    $('#txtulastname').val('');
    $('#txtuemail').val('');
    $('#txtumobileno').val('');
    $('#txtutelephoneno').val('');
    $('#txtuaddress').val('');
    $('#ddlurelationship').val('');
    $('#chkuIsNxtKin').prop('checked', false);
    $('#chkuIsEmergContact').prop('checked', false);
}

function ContactDetilas(ctrl) {
    $('#kt_modal_contact_details').modal('show');

    var id = $(ctrl).attr('data-contact-id');

    $.ajax({
        type: "POST",
        url: '/Contact/Detail',
        data: { id: id },
        success: function (data) {
            if (data != null) {
                $('#spnfullname').html(data.firstName + " " + data.middleName + "" + data.lastName);
                $('#spnemail').html(data.email);
                $('#spnaddress').html(data.address);
                $('#spnmobile').html(data.mobile);
                $('#spntelephone').html(data.telephone);
                $('#spnralationship').html(data.relationship == 1 ? "Sibling" : data.relationship == 2 ? "Spouse" : data.relationship == 3 ? "Parent" : data.relationship == 4 ? "Child" : data.relationship == 5 ? "Grand Parent" : "Others");
                $('#spniskin').html(data.isNextOfKin == true ? "Yes" : "No");
                $('#spnemergency').html(data.isEmergencyContact == true ? "Yes" : "No");
                $('#hdncontactid').val(id);
            }
            else {

            }
        }
    });
}

function EditContactModal(ctrl) {
    var id = $('#hdncontactid').val();
    if (id != null && id != undefined) {
        $('#kt_modal_contact_details').modal('hide');
        Cleardata();
        $.ajax({
            type: "POST",
            url: '/Contact/Detail',
            data: { id: id },
            success: function (data) {
                if (data != null) {
                    $('#ddlutitle').val(data.title);
                    $('#ddlutitle').select2().trigger('change');
                    $('#txtufirstname').val(data.firstName);
                    $('#txtulastname').val(data.lastName);
                    $('#txtumiddlename').val(data.middleName);
                    $('#txtuemail').val(data.email);
                    $('#txtuaddress').val(data.address);
                    $('#txtumobileno').val(data.mobile);
                    $('#txtutelephoneno').val(data.telephone);
                    $('#ddlurelationship').val(data.relationship);
                    $('#ddlurelationship').select2().trigger('change');

                    if (data.isNextOfKin == true) {
                        $('#chkuIsNxtKin').prop('checked', true);
                    }
                    if (data.isEmergencyContact == true) {
                        $('#chkuIsEmergContact').prop('checked', true);
                    }
                    if (data.profilePhoto != null) {
                        $('#imgphoto').attr("src", data.prePath + "/" + data.profilePhoto.blobName + "?width=200");
                        $('#photodrop').hide();
                        $('#OldProfile').show();
                    } else {
                        $('#photodrop').show();
                        $('#OldProfile').hide();
                    }
                    $('#kt_modal_edit_contact').modal('show');
                }
                else {

                }
            }
        });
    }
}

function DeleteContact(ctrl, serviceUserId) {
    var id = $('#hdncontactid').val();

    Swal.fire({
        title: 'Are you sure?',
        text: "It will be deleted permanently!, \r\nAlso remove related data of service users.",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve)
            {
                $.ajax({
                    type: "POST",
                    url: '/Contact/Delete',
                    data: { id: id, serviceUserId: serviceUserId },
                    success: function (data) {
                        if (data == 1) {
                            Swal.fire({
                                text: "Sorry, There may be somekind of error, please try again.",
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
                                Cleardata();
                                q.isConfirmed && modalcontact.hide();
                            });
                            $('#contactIndexPage').html('');
                            $('#contactIndexPage').html(data);                            
                            $('#kt_modal_contact_details').modal('hide');
                        }
                    }
                });


                //$.ajax({
                //    url: '/Contact/Delete',
                //    type: 'POST',
                //    data: { id: id, serviceUserId: serviceUserId },
                //    dataType: 'json'
                //}).done(function (response) 
                //{
                    
                //}).fail(function () {
                //    Swal.fire('Oops...', 'Something went wrong with ajax !', 'error')
                //});
            });
        },
    });
}