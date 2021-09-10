
var modalskillgroup = new bootstrap.Modal(document.querySelector('#kt_modal_create_app'));

var frmskillgroup = document.querySelector("#kt_modal_new_skillgroup");

const skillgroupfrm = FormValidation
    .formValidation(frmskillgroup,
        {
            fields: {
                groupname: {
                    validators: {
                        notEmpty: {
                            message: "GroupName is required"
                        }
                    }
                },
                skillposition: {
                    validators: {
                        notEmpty: {
                            message: "Position is required"
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

    $('#new_skillgroup_submit').click(function (e) {
        e.preventDefault();
        skillgroupfrm.validate().then(function (s) {
            if ("Valid" == s) {
                $('#new_skillgroup_submit').disabled = !0;
                $('#new_skillgroup_submit').attr("data-kt-indicator", "on");
                setTimeout(function () {
                    $('#new_skillgroup_submit').removeAttr("data-kt-indicator");
                    $('#new_skillgroup_submit').disabled = !1;

                    //ajax call for the submit;
                    var SkillGroup = {
                        Id: 0,
                        GroupName: $('#txtgroupname').val(),
                        Position: $('#txtposition').val()
                    }
                    var skillAssessmentCreateViewModel = {
                        SkillGroup: SkillGroup,
                    }
                    $.ajax({
                        type: "POST",
                        url: '/SkillsAssessment/Save',
                        data: { skillAssessmentCreateViewModel: skillAssessmentCreateViewModel },
                        success: function (data) {
                            if (data.statuscode == 1) {
                                Swal.fire({
                                    text: "Form has been successfully submitted!",
                                    icon: "success",
                                    buttonsStyling: !1,
                                    confirmButtonText: "Ok, got it!",
                                    customClass: {
                                        confirmButton: "btn btn-primary"
                                    }
                                }).then(function (q) {
                                    q.isConfirmed && modalskillgroup.hide();
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


    $("#sortable").sortable({
        /*stop: function(event, ui) {
            alert("New position: " + ui.item.index());
        }*/
        start: function (e, ui) {
            // creates a temporary attribute on the element with the old index
            $(this).attr('data-previndex', ui.item.index());
        },
        update: function (e, ui) {
            // gets the new and old index then removes the temporary attribute
            var newIndex = ui.item.index();
            var oldIndex = $(this).attr('data-previndex');
            var element_id = ui.item.attr('id');
            //alert('id of Item moved = ' + element_id + ' old position = ' + oldIndex + ' new position = ' + newIndex);
            $.ajax({
                type: "POST",
                url: '/SkillsAssessment/MovePosition',
                data: { Id: element_id, newposition: newIndex },
                success: function (data) {
                    if (data.statuscode == 1) {
                        Swal.fire({
                            text: "Position Updated successfully.",
                            icon: "success",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then(function (q) {
                            q.isConfirmed
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

            $(this).removeAttr('data-previndex');
        }
    });
    $("#sortable").disableSelection();

});

function AddLivingskill(ctrl) {
    $('#kt_modal_livingskill').modal('show');
    var id = $(ctrl).attr('id');
}
