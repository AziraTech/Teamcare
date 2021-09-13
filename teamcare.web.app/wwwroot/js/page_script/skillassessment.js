
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



var modallivingskill = new bootstrap.Modal(document.querySelector('#kt_modal_livingskill'));
var frmlivingskill = document.querySelector("#kt_modal_new_livingskill");

const livingskillfrm = FormValidation
    .formValidation(frmlivingskill,
        {
            fields: {
                livingname: {
                    validators: {
                        notEmpty: {
                            message: "Name is required"
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
                        Position: 0, //$('#txtposition').val()
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
        //start: function (e, ui) {
        //    // creates a temporary attribute on the element with the old index
        //    $(this).attr('data-previndex', ui.item.index());
        //},
        update: function (e, ui) {
            var itemOrder = $('#sortable').sortable("toArray");
            var SkillGroupS = new Array();
            for (var i = 0; i < itemOrder.length; i++) {
                var SkillGroup = {};
                SkillGroup.Id = itemOrder[i];
                SkillGroup.Position = i;
                SkillGroupS.push(SkillGroup);
            }

            var skillAssessmentCreateViewModel = {
                SkillGroups: SkillGroupS,
            }

            $.ajax({
                type: "POST",
                url: '/SkillsAssessment/MovePosition',
                data: { skillAssessmentCreateViewModel: skillAssessmentCreateViewModel },
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
                            text: "Position Updated successfully.",
                            icon: "success",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        }).then(function (q) {
                            q.isConfirmed
                        });
                    }
                }
            });

        }
    });
    $("#sortable").disableSelection();

    $('#new_livingskill_submit').click(function (e) {
        e.preventDefault();
        livingskillfrm.validate().then(function (s) {
            if ("Valid" == s) {
                $('#new_livingskill_submit').disabled = !0;
                $('#new_livingskill_submit').attr("data-kt-indicator", "on");
                setTimeout(function () {
                    $('#new_livingskill_submit').removeAttr("data-kt-indicator");
                    $('#new_livingskill_submit').disabled = !1;

                    //ajax call for the submit;
                    var LivingSkill = {
                        Id: 0,
                        GroupId: $('#hdngroupid').val(),
                        SKillName: $('#txtlivingname').val(),
                        Position: 0, //$('#txtposition').val()
                    }
                    var skillAssessmentCreateViewModel = {
                        LivingSkill: LivingSkill,
                    }
                    $.ajax({
                        type: "POST",
                        url: '/SkillsAssessment/SaveLivingSkill',
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
                                    q.isConfirmed && modallivingskill.hide();
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

});

function AddLivingskill(ctrl) {
    $('#hdngroupid').val('');
    $('#spngroupname').text('');
    $('#kt_modal_livingskill').modal('show');
    var id = $(ctrl).attr('id');
    var name = $(ctrl).attr('name');
    $('#spngroupname').html(name);
    $('#hdngroupid').val(id);
}

function ViewLivingskill(ctrl) {
    var id = $(ctrl).attr('id');
    var name = $(ctrl).attr('name');
    $('#spngroup').html(name);
    $.ajax({
        type: "POST",
        url: '/SkillsAssessment/LivingList',
        data: { Id: id },
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
                $('#partialViewDataContent').html('');
                $('#partialViewDataContent').html(data);


                $("#sortableliving").sortable({
                    /*stop: function(event, ui) {
                        alert("New position: " + ui.item.index());
                    }*/
                    //start: function (e, ui) {
                    //    // creates a temporary attribute on the element with the old index
                    //    $(this).attr('data-previndex', ui.item.index());
                    //},
                    update: function (e, ui) {
                        var itemOrder = $('#sortableliving').sortable("toArray");
                        var LivingSkillS = new Array();
                        for (var i = 0; i < itemOrder.length; i++) {
                            var LivingSkill = {};
                            LivingSkill.Id = itemOrder[i];
                            LivingSkill.Position = i;
                            LivingSkill.GroupId = id;
                            LivingSkillS.push(LivingSkill);
                        }

                        var skillAssessmentCreateViewModel = {
                            LivingSkills: LivingSkillS,
                        }

                        $.ajax({
                            type: "POST",
                            url: '/SkillsAssessment/LivingMovePosition',
                            data: { skillAssessmentCreateViewModel: skillAssessmentCreateViewModel },
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
                                        text: "Position Updated successfully.",
                                        icon: "success",
                                        buttonsStyling: !1,
                                        confirmButtonText: "Ok, got it!",
                                        customClass: {
                                            confirmButton: "btn btn-primary"
                                        }
                                    }).then(function (q) {
                                        q.isConfirmed
                                    });
                                }
                            }
                        });

                    }
                });
                $("#sortableliving").disableSelection();

            }
            $('#kt_modal_data_livingskill').modal('show');

        }
    });
}

function DeleteSkillGroup(ctrl) {
    var id = $(ctrl).attr('id');

    Swal.fire({
        title: 'Are you sure?',
        text: "you want to delete this group? \r\nAll skills within this group will also be deleted.",
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
                    url: '/SkillsAssessment/DeleteSkillGroup',
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
                                text: "Skill Group Removed Successfully!",
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

function DeleteLivingSkill(ctrl) {
    var id = $(ctrl).attr('id');

    Swal.fire({
        title: 'Are you sure?',
        text: "you want to delete this skill?",
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
                    url: '/SkillsAssessment/DeleteLivingSkill',
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
                                text: "Living Skill Removed Successfully!",
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