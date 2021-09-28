
var cfileName = null;
var cfileType = null;
var ctempFileId = null;

var modalcontactdetail = new bootstrap.Modal(document.querySelector('#kt_modal_contact_details'));


var frmeditcontact= null;

var editcontactfrm = null;


function ContactDetilas(ctrl) {
    $('#kt_modal_contact_details').modal('show');

    var id = $(ctrl).attr('data-contact-id');

    $.ajax({
        type: "GET",
        url: '/Contact/Detail',
        data: { id: id },
        success: function (data) {
            if (data != null) {
                $('#spnfullname').html(data.firstName + " " + data.middleName + " " + data.lastName);
                $('#spnemail').html(data.email);
                $('#spnaddress').html(data.address);
                $('#spnmobile').html(data.mobile);
                $('#spntelephone').html(data.telephone);
                $('#spnralationship').html(data.relationship == 1 ? "Sibling" : data.relationship == 2 ? "Spouse" : data.relationship == 3 ? "Parent" : data.relationship == 4 ? "Child" : data.relationship == 5 ? "Grand Parent" : "Others");
                $('#spniskin').html(data.isNextOfKin == true ? "Yes" : "No");
                $('#spnemergency').html(data.isEmergencyContact == true ? "Yes" : "No");
                $('#hdncontactid').val(id);

                $('#btneditcontact').attr('onclick', 'openAddEditContactModal("' + data.id + '")');
            }
            else {

            }
        }, error: function (xhr) {
            console.log(xhr);
        }

    });
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
            return new Promise(function (resolve) {
                $.ajax({
                    type: "POST",
                    url: '/Contact/Delete',
                    data: { id: id, serviceUserId: serviceUserId },
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
                                q.isConfirmed && modalcontactdetail.hide();
                                $('.modal-backdrop').remove();
                                $("body").css("overflow", "");
                                $("body").css("padding-right", "");

                            });
                            $('#contactIndexPage').html('');
                            $('#contactIndexPage').html(data);
                        }
                    }
                });

            });
        },
    });
}

function openAddEditContactModal(id) {

    $.ajax({
        type: "POST",
        url: '/Contact/ContactDetailBind',
        data: { id: id },
        success: function (data) {
            if (data != null) {
                $('#divAddEditContact').html('');
                $('#divAddEditContact').html(data);

                var myDropzoneContact = new Dropzone("#sv-contact-profile-photo-edit", {
                    url: "/DocumentUpload", // Set the url for your upload script location
                    paramName: "file", // The name that will be used to transfer the file
                    maxFiles: 1,
                    maxFilesize: 10, // MB
                    addRemoveLinks: true,
                    acceptedFiles: ".png,.jpg,.gif,.bmp,.jpeg",
                    success: function (file, response) {
                        ctempFileId = response.id;
                        cfileName = file.name;
                        cfileType = file.type;
                    }
                });

                frmeditcontact = document.querySelector("#kt_modal_edit_contactform");

                editcontactfrm =FormValidation
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
                                ulast_name: {
                                    validators: {
                                        notEmpty: {
                                            message: "LastName is required"
                                        }
                                    }
                                },
                                uemail: {
                                    validators: {
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

                if (id == null) {
                    $('#lblheader').text('Create Contact Information');
                } else {

                    $('#lblheader').text('Edit Contact Information');
                    $('#kt_modal_contact_details').modal('hide');
                }

                $('#modalAddEditContact').modal('show');

            }
            else {

            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
}

function saveContactDetails(sender) {

    editcontactfrm.validate().then(function (s) {
        if ("Valid" == s) {

            if ($('#txtuemail').val().trim() != "" || $('#txtuaddress').val().trim() != "" || $('#txtumobileno').val().trim() != "" || $('#txtutelephoneno').val().trim() != "") {
         
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

                            if (data.statuscode == 1) {


                                $.ajax({
                                    type: "POST",
                                    url: '/Contact/GetContactData',
                                    data: { id: data.id },
                                    success: function (data1) {
                                        if (data1 != undefined) {
                                            Swal.fire({
                                                text: "Contact has been submitted successfully.",
                                                icon: "success",
                                                buttonsStyling: !1,
                                                confirmButtonText: "Ok, got it!",
                                                customClass: {
                                                    confirmButton: "btn btn-primary"
                                                }
                                            }).then(function (q) {
                                                q.isConfirmed;
                                                $('#modalAddEditContact').modal('hide');
                                                $('.modal-backdrop').remove();
                                                $("body").css("overflow", "");
                                                $("body").css("padding-right", "");

                                            });
                                            $('#contactIndexPage').html('');
                                            $('#contactIndexPage').html(data1);
                                        }
                                    }
                                });
                            }
                        }
                    }
                });
                //on success show message
            }, 2e3);
            } else {
                Swal.fire({
                    text: "Please! fill up at least one field from Email, Address, Mobile No and Telephone No.",
                    icon: "info",
                    buttonsStyling: !1,
                    confirmButtonText: "Ok, got it!",
                    customClass: {
                        confirmButton: "btn btn-light"
                    }
                });
            }
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