
var base64Image = null;
var fileName = null;
var fileType = null;
var tempFileId = null;

var docfileName = null;
var docfileType = null;
var doctempFileId = null;

//ckeditors
let editorProfile = null;
let editorBehaviour = null;
let editorCognition = null;
let editorPsychologicalNeeds = null;
let editorCommunication = null;

let editorMobility = null;
let editorNutrition = null;
let editorContinence = null;
let editorSkin= null;
let editorBreathing= null;

let editorDrug = null;
let editorAltered = null;
let editorIndependent = null;
let editorSummary  = null;

// Stepper lement
var serviceUserId = $('#selectedId').val();
var element = null;
var r = null;

var fv2 = null;

Dropzone.autoDiscover = false;

async function setAsFavourite(vFavauriteUser, imageId) {
	await $.ajax({
		type: "POST",
		url: '/ServiceUsers/SetAsFavouriteUser',
		data: { FavauriteUser: vFavauriteUser },
		success: function (data) {
			var showMessage = ""; var icon = "";
			document.getElementById(imageId).src = "";
			if (data.statuscode == 1) {
				showMessage = "Add Favourite Successful.";
				icon = 'success';
				document.getElementById(imageId).src = "/media/svg/files/on_favourite_star.svg";
			} else if (data.statuscode == 2) {
				showMessage = "Remove Favourite Successful.";
				icon = 'success';
				document.getElementById(imageId).src = "/media/svg/files/off_favourite_star.svg";
			}
			else {
				showMessage = data.message;
				icon = 'error';
			}
			//Swal.fire({
			//    text: showMessage,
			//    icon: icon,
			//    buttonsStyling: !1,
			//    confirmButtonText: "Ok",
			//    customClass: { confirmButton: "btn btn-light" }
			//});
		}
	});
}


function RemoveProfile() {
	$('#OldProfile').remove();
}

function RemoveDocument() {
	$('#OldDocument').remove();
}

$(document).ready(function () {

	$('#tabassessments').click(function () {
		$.ajax({
			type: "GET",
			url: '/ServiceUsers/AssessmentTabBind',
			data: {},
			success: function (data) {
				if (data != null) {
					$('#AssessmentTabContentData').html('');
					$('#AssessmentTabContentData').html(data);

					setCurrentTabAssessment($('#hdnassettab').val());
				}
			}
		});
	});

	$('#tabdocumentsmanager').click(function () {
		var serviceUserId = $('#hdnserviceuserid').val();
		$.ajax({
			type: "GET",
			url: '/ServiceUsers/DocumentsManagerTabBind',
			data: { docId: "", serviceUserId: serviceUserId },
			success: function (data) {
				if (data != null) {
					$('#DocumentsManagerTabContentData').html('');
					$('#DocumentsManagerTabContentData').html(data);
				}
			}
		});
	});

	$('#archive_submit').click(function () {

		if ($('#ddlreason').val() != "") {

			$.ajax({
				type: "POST",
				url: '/ServiceUsers/ArchiveUserReason',
				data: { ReasonId: $('#ddlreason').val(), Userid: $('#hdnserviceuserid').val() },
				success: function (data) {
					if (data != null) {
						if (data.statuscode == 1) {
							Swal.fire({
								text: "Service user has been archived successfully.",
								icon: "success",
								buttonsStyling: !1,
								confirmButtonText: "Ok, got it!",
								customClass: {
									confirmButton: "btn btn-primary"
								}
							}).then(function (q) {
								q.isConfirmed
								$('#kt_modal_ArchiveReason').modal('hide');
								//window.location.href = "/ServiceUsers";
								window.location.reload();

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
				}
			});
		} else {
			Swal.fire({
				text: "Oops! It looks like a required field has not been set.",
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

var rServiceUserLogSubmit = document.querySelector("#su-service_user_log_form");
const fv4 = FormValidation
	.formValidation(
		rServiceUserLogSubmit,
		{
			fields:
			{
				'sul-log_message':
				{
					validators: {
						notEmpty: {
							message: "Please Insert Log Message"
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


async function sendServiceUserLog(serviceUserId, logMessageId, dbType, logId) {
	if (dbType == 'I' || dbType == 'U') {
		if ($('#' + logMessageId).val() == null || $('#' + logMessageId).val().trim() == "") {
			Swal.fire({
				text: "Please fill Log Message First.",
				icon: "info",
				buttonsStyling: !1,
				confirmButtonText: "Ok",
				customClass: { confirmButton: "btn btn-light" }
			});
			return;
		}
	}
	var opType = $('#editOrDelete').val();
	var DelLogId = $('#editOrDeleteId').val();
	dbType = opType != 'I' ? opType : dbType;
	logId = DelLogId != '' ? DelLogId : logId;
	var logMessage = '';
	if (dbType == 'I' || dbType == 'U') { logMessage = $('#' + logMessageId).val(); }
	await $.ajax({
		type: "POST",
		url: '/ServiceUsers/saveLog',
		data: { logId: logId, dbType: dbType, serviceUserId: serviceUserId, logMessage: logMessage },
		success: function (data) {
			var showMessage = ""; var icon = "";
			if (data) {
				switch (dbType) {
					case "I": showMessage = "Log Added Successful."; break;
					case "U": showMessage = "Log Updated Successful."; break;
					case "D": showMessage = "Log Removed Successful."; break;
				} icon = 'success';
				$('#editOrDelete').val('I');
				$('#editOrDeleteId').val('');
				$('#' + logMessageId).val('');

				$('#lstServiceUserLogList').html('');
				$('#lstServiceUserLogList').html(data);

			} else {
				icon = 'error';
				showMessage = 'Please Check, There may be some error to save log.';
			}
			Swal.fire({
				text: showMessage,
				icon: icon,
				buttonsStyling: !1,
				confirmButtonText: "Ok",
				customClass: { confirmButton: "btn btn-light" }
			}); //return;
		}
	});
	$('#editOrDelete').val('I');
	$('#editOrDeleteId').val('');
}

async function fncEditOrDelete(opType, logId, msgAreaId) {
	if (opType == 'D') {
		await Swal.fire({
			title: 'Are you sure?',
			text: "You want to delete selected service user log.",
			type: 'warning',
			showCancelButton: true,
			confirmButtonColor: '#3085d6',
			cancelButtonColor: '#d33',
			confirmButtonText: 'Yes, delete it!',
			showLoaderOnConfirm: true,
			preConfirm: async function () { await sendServiceUserLog(serviceUserId, 'sul-log_message', opType, logId); }
		});
	}
	else {
		$('#editOrDelete').val(opType);
		$('#editOrDeleteId').val(logId);

		await $.ajax({
			type: "POST",
			url: '/ServiceUsers/GetByLogId',
			data: { id: logId },
			success: function (data) {
				if (data != null) {
					$('#sul-log_message').val(data.logMessage);
					$('#' + msgAreaId).html = '';
					$('#' + msgAreaId).html = data.logMessage;
				} else { }
			}
		});

	}
}


async function setCurrentTabAssessment(id) {

	var serviceuserid = $('#hdnserviceuserid').val();
	$.ajax({
		type: "POST",
		url: '/ServiceUsers/AssessmentGroupSkill',
		data: { Id: id, ServiceUserId: serviceuserid },
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
				$('#AssessmentTabContent').html('');
				$('#AssessmentTabContent').html(data);

				var iaselement = document.querySelector("#kt_modal_create_ias_stepper");

				if (iaselement) {

					var rr = document.querySelector("#kt_modal_create_ias_form");

					var iasstep1 = document.querySelector('#ias-step-first');
					var iasstep2 = document.querySelector('#ias-step-second');
					var iasstep3 = document.querySelector('#ias-step-third');
					var iasstepper = new KTStepper(iaselement);
					iasstepper.on("kt.stepper.next", function (stepper) {
						stepIndex = iasstepper.getCurrentStepIndex();
						switch (stepIndex) {
							case 1:
								iasfv1.validate().then(function (t) {
									"Valid" == t ? (iasstepper.goNext(), KTUtil.scrollTop()) : Swal.fire({
										text: "Sorry, looks like there are some errors detected, please try again.",
										icon: "info",
										buttonsStyling: !1,
										confirmButtonText: "Ok, got it!",
										customClass: {
											confirmButton: "btn btn-light"
										}
									});
								});
								break;
							case 2:
								iasfv2.validate().then(function (t) {
									"Valid" == t ? (iasstepper.goNext(), KTUtil.scrollTop()) : Swal.fire({
										text: "Sorry, looks like there are some errors detected, please try again.",
										icon: "info",
										buttonsStyling: !1,
										confirmButtonText: "Ok, got it!",
										customClass: {
											confirmButton: "btn btn-light"
										}
									});
								});
								break;
							default:
								break;
						}

					});

					// Handle previous step
					iasstepper.on("kt.stepper.previous", function (iasstepper) {
						iasstepper.goPrevious(); // go previous step
					});
					const iasfv1 = FormValidation
						.formValidation(
							iasstep1,
							{
								fields: {
									ias_profile: {
										validators: {

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
					iasfv2 = FormValidation
						.formValidation(
							iasstep2,
							{
								fields: {
									marital_status: {
										validators: {
											notEmpty: {
												message: "Select marital stauts"
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
					iasckeditorInit();
				}
				else {
					assessmentDetails();
					$('#new_assessment_submit').click(function () {

						var group = $('#assessmentTbl tr');
						var skill = group.find('input[type="radio"]').get();

						var skilldata = [];
						var totalskill = $.map(skill, function (element) {
							var skillid = $(element).attr("skillid");
							skilldata.push({
								SkillId: skillid,
							});
						});

						var distinctskill = [...new Set(skilldata.map(x => x.SkillId))];

						if (distinctskill.length === group.find('input[type="radio"]:checked').length) {

							var selectedresult = group.find('input[type="radio"]:checked').get();

							var assessmsnedata = [];
							var columns = $.map(selectedresult, function (element) {
								var grpname = $(element).attr("grpname");
								var skillname = $(element).attr("skillname");
								var skillid = $(element).attr("skillid");
								var levelid = $(element).attr("levelid");
								var risklevelid = $(element).attr("risklevelid");
								//var id = $(element).attr("id");
								assessmsnedata.push({
									Id: 0,
									SkillGroup: grpname,
									SkillName: skillname,
									SkillId: skillid,
									SkillLevel: levelid,
									RiskLevel: risklevelid

								});
							});

							var Assessment = {
								ServiceUserId: $('#hdnserviceuserid').val(),
								AssessmentTypeId: $('#hdnassessmenttype').val()
							}

							var assessmentCreateViewModel = {
								Assessment: Assessment,
								AssessmentSkills: assessmsnedata
							}

							$.ajax({
								type: "POST",
								url: '/ServiceUsers/AssessmentSave',
								data: { assessmentCreateViewModel: assessmentCreateViewModel },
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
											text: "Form has been successfully submitted!",
											icon: "success",
											buttonsStyling: !1,
											confirmButtonText: "Ok, got it!",
											customClass: {
												confirmButton: "btn btn-primary"
											}
										}).then(function (q) {
											q.isConfirmed && $('#create_assessment').modal('hide');
											setCurrentTabAssessment(id);
										});
									}
								}

							});

						}
						else {
							Swal.fire({
								text: "Sorry, Please select every skill at least one level, please try again.",
								icon: "info",
								buttonsStyling: !1,
								confirmButtonText: "Ok, got it!",
								customClass: {
									confirmButton: "btn btn-light"
								}
							});

						}

					});
				}
			}
		}
	});
}

function saveInitialAssessment(sender) {
	var grpname = $('#ckeProfile').attr('grpname');
	var skillname = $('#ckeProfile').attr('skillname');
	var skillid = $('#ckeProfile').attr('skillid');
	var section = $('#ckeProfile').attr('section');
	var assessmsnedata = [];
	assessmsnedata.push({
		Id: 0,
		SkillGroup: grpname,
		SkillName: skillname,
		SkillId: skillid,
		InitialAssessmentSection: section,
		InitialAssessmentContent: editorProfile.getData()
	});
	if (editorBehaviour.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeBehaviour').attr('section'),
			InitialAssessmentContent: editorBehaviour.getData()
		});
	}
	if (editorCognition.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeCognition').attr('section'),
			InitialAssessmentContent: editorCognition.getData()
		});
	}
	if (editorPsychologicalNeeds.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckePsychologicalNeeds').attr('section'),
			InitialAssessmentContent: editorPsychologicalNeeds.getData()
		});
	}
	if (editorCommunication.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeCommunication').attr('section'),
			InitialAssessmentContent: editorCommunication.getData()
		});
	}
	if (editorMobility.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeMobility').attr('section'),
			InitialAssessmentContent: editorMobility.getData()
		});
	}

	if (editorNutrition.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeNutrition').attr('section'),
			InitialAssessmentContent: editorNutrition.getData()
		});
	}
	if (editorContinence.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeContinence').attr('section'),
			InitialAssessmentContent: editorContinence.getData()
		});
	}
	if (editorSkin.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeSkin').attr('section'),
			InitialAssessmentContent: editorSkin.getData()
		});
	}
	if (editorBreathing.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeBreathing').attr('section'),
			InitialAssessmentContent: editorBreathing.getData()
		});
	}
	if (editorDrug.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeDrugTherapies').attr('section'),
			InitialAssessmentContent: editorDrug.getData()
		});
	}
	if (editorAltered.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeAlteredState').attr('section'),
			InitialAssessmentContent: editorAltered.getData()
		});
	}
	if (editorIndependent.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeIndependentSkills').attr('section'),
			InitialAssessmentContent: editorIndependent.getData()
		});
	}
	if (editorSummary.getData()?.length > 0) {
		assessmsnedata.push({
			Id: 0,
			SkillGroup: grpname,
			SkillName: skillname,
			SkillId: skillid,
			InitialAssessmentSection: $('#ckeSummaryofNeeds').attr('section'),
			InitialAssessmentContent: editorSummary.getData()
		});
	}
	var Assessment = {
		ServiceUserId: $('#hdnserviceuserid').val(),
		AssessmentTypeId: $('#hdnassessmenttype').val()
	}
	var assessmentCreateViewModel = {
		Assessment: Assessment,
		AssessmentSkills: assessmsnedata
	}

	$.ajax({
		type: "POST",
		url: '/ServiceUsers/AssessmentSave',
		data: { assessmentCreateViewModel: assessmentCreateViewModel },
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
					text: "Form has been successfully submitted!",
					icon: "success",
					buttonsStyling: !1,
					confirmButtonText: "Ok, got it!",
					customClass: {
						confirmButton: "btn btn-primary"
					}
				}).then(function (q) {
					q.isConfirmed && $('#create_assessment').modal('hide');
					setCurrentTabAssessment(id);
				});
			}
		}

	});

}

function assessmentDetails(ctrl) {
	if (ctrl != undefined) {
		var id = $(ctrl).attr('id');
		var type = $(ctrl).attr('type');
		var srvuserid = $(ctrl).attr('srvuserid');
		$.ajax({
			type: "POST",
			url: '/ServiceUsers/AssessmentSkillDetails',
			data: { Id: id, Type: type, ServiceUserId: srvuserid },
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
					$('#assessment_details').modal('show');
					$('#AssessmentDetailsContent').html('');
					$('#AssessmentDetailsContent').html(data);

				}
			}
		});
	}
}

function iasckeditorInit() {
	ClassicEditor.create(document.querySelector('#ckeProfile')).then(newEditor => {
		editorProfile = newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeBehaviour')).then(newEditor => {
		editorBehaviour= newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeCognition')).then(newEditor => {
		editorCognition = newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckePsychologicalNeeds')).then(newEditor => {
		editorPsychologicalNeeds = newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeCommunication')).then(newEditor => {
		editorCommunication = newEditor;
	}).catch(error => {
		console.error(error);
	});

	ClassicEditor.create(document.querySelector('#ckeMobility')).then(newEditor => {
		editorMobility = newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeNutrition')).then(newEditor => {
		editorNutrition= newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeContinence')).then(newEditor => {
		editorContinence= newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeSkin')).then(newEditor => {
		editorSkin = newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeBreathing')).then(newEditor => {
		editorBreathing = newEditor;
	}).catch(error => {
		console.error(error);
	});

	ClassicEditor.create(document.querySelector('#ckeDrugTherapies')).then(newEditor => {
		editorDrug = newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeAlteredState')).then(newEditor => {
		editorAltered = newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeIndependentSkills')).then(newEditor => {
		editorIndependent = newEditor;
	}).catch(error => {
		console.error(error);
	});
	ClassicEditor.create(document.querySelector('#ckeSummaryofNeeds')).then(newEditor => {
		editorSummary = newEditor;
	}).catch(error => {
		console.error(error);
	});
}

function ArchiveUserModal(ctrl) {
	var id = $(ctrl).attr('id');
	if (id != undefined) {
		$('#kt_modal_ArchiveReason').modal('show');
	}
}

function UnArchiveUser(ctrl) {
	var id = $(ctrl).attr('id');
	if (id != undefined) {

		Swal.fire({
			title: 'Are you sure?',
			text: "The service user will be unarchived.",
			type: 'warning',
			showCancelButton: true,
			confirmButtonColor: '#3085d6',
			cancelButtonColor: '#d33',
			confirmButtonText: 'Yes, do it!',
			showLoaderOnConfirm: true,
			preConfirm: function () {
				return new Promise(function (resolve) {
					$.ajax({
						type: "POST",
						url: '/ServiceUsers/UnArchiveUser',
						data: { Userid: $('#hdnserviceuserid').val() },
						success: function (data) {
							if (data != null) {
								if (data.statuscode == 1) {
									Swal.fire({
										text: "Service user has been unarchived successfully.",
										icon: "success",
										buttonsStyling: !1,
										confirmButtonText: "Ok, got it!",
										customClass: {
											confirmButton: "btn btn-primary"
										}
									}).then(function (q) {
										q.isConfirmed
										//window.location.href = "/ServiceUsers";
										window.location.reload();
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
						}
					});

				});
			},
		});
	}
}


function openAddEditServiceUserModal(id) {

	$.ajax({
		type: "POST",
		url: '/ServiceUsers/ServiceUserModalBind',
		data: { id: id },
		success: function (data) {
			if (data != null) {
				$('#divAddEditServiceUser').html('');
				$('#divAddEditServiceUser').html(data);

				$('.ddlselect2').select2();

				$(".date-dob").daterangepicker({
					singleDatePicker: true,
					showDropdowns: true,
					minYear: 1981,
					maxYear: parseInt(moment().format("YYYY"), 10),
					locale: {
						format: 'DD/MM/yyyy'
					}
				}, function (start, end, label) {
				});
				$(".date-admission").daterangepicker({
					singleDatePicker: true,
					showDropdowns: true,
					minYear: 2001,
					maxYear: parseInt(moment().format("YYYY"), 10),
					locale: {
						format: 'DD/MM/yyyy'
					}
				}, function (start, end, label) {
				});

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
				});

				element = document.querySelector("#su-profile-stepper");
				r = document.querySelector("#su-profile-form");

				var step1 = document.querySelector('#su-step-pi');
				var step2 = document.querySelector('#su-step-ad');

				// Initialize Stepper
				var stepper = new KTStepper(element);

				var stepIndex = 0;
				// Handle next step
				stepper.on("kt.stepper.next", function (stepper) {
					stepIndex = stepper.getCurrentStepIndex();
					switch (stepIndex) {
						case 1:
							fv1.validate().then(function (t) {
								"Valid" == t ? (stepper.goNext(), KTUtil.scrollTop()) : Swal.fire({
									text: "Sorry, looks like there are some errors detected, please try again.",
									icon: "info",
									buttonsStyling: !1,
									confirmButtonText: "Ok, got it!",
									customClass: {
										confirmButton: "btn btn-light"
									}
								});
							});
							break;
						case 2:
							fv2.validate().then(function (t) {
								"Valid" == t ? (stepper.goNext(), KTUtil.scrollTop()) : Swal.fire({
									text: "Sorry, looks like there are some errors detected, please try again.",
									icon: "info",
									buttonsStyling: !1,
									confirmButtonText: "Ok, got it!",
									customClass: {
										confirmButton: "btn btn-light"
									}
								});
							});
							break;
						default:
							break;
					}

				});

				// Handle previous step
				stepper.on("kt.stepper.previous", function (stepper) {
					stepper.goPrevious(); // go previous step              
				});

				const fv1 = FormValidation
					.formValidation(
						step1,
						{
							fields: {
								first_name: {
									validators: {
										notEmpty: {
											message: "First name is required"
										}
									}
								},
								last_name: {
									validators: {
										notEmpty: {
											message: "Last name is required"
										}
									}
								},
								personal_tel_no: {
									validators: {
										notEmpty: {
											message: "Telephone number is required"
										}
									}
								},
								residence: {
									validators: {
										notEmpty: {
											message: "Select residence"
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
				fv2 = FormValidation
					.formValidation(
						step2,
						{
							fields: {
								marital_status: {
									validators: {
										notEmpty: {
											message: "Select marital stauts"
										}
									}
								},
								religion: {
									validators: {
										notEmpty: {
											message: "Select religion"
										}
									}
								},
								ethnicity: {
									validators: {
										notEmpty: {
											message: "Select ethnicity"
										}
									}
								},
								first_language: {
									validators: {
										notEmpty: {
											message: "Select prefered first language"
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
					$('#lblheader').text('Create Service Users Information');
				} else {

					$('#lblheader').text('Edit Service Users Information');
				}

				$('#modalAddEditServiceUser').modal('show');

			}
			else {

			}
		},
		error: function (xhr) {
			console.log(xhr);
		}
	});
}

function saveEditServiceUser(sender) {
	fv2.validate().then(function (s) {
		if ("Valid" == s) {

			$('#btnsaveserviceuser').disabled = !0;
			$('#btnsaveserviceuser').attr("data-kt-indicator", "on");
			setTimeout(function () {
				$('#btnsaveserviceuser').removeAttr("data-kt-indicator");
				$('#btnsaveserviceuser').disabled = !1;

				//ajax call for the submit;
				var ServiceUser = {
					Id: serviceUserId,
					Title: $('#su-title').val(),
					FirstName: $('#su-first_name').val(),
					LastName: $('#su-last_name').val(),
					KnownAs: $('#su-known_as').val(),
					DateOfBirth: $('#su-date_of_birth').val(),
					LegalStatus: $('#su-legal_status').val(),
					NHSIdNumber: $('#su-nhs_id').val(),
					NationalInsuranceNo: $('#su-national_insuarance').val(),
					DateOfAdmission: $('#su-date_of_admission').val(),
					ResidenceId: $('#su-residence').val(),
					PersonalTelNo: $('#su-personal_tel_no').val(),
					MaritalStatus: $('#su-marital_status').val(),
					Religion: $('#su-religion').val(),
					Ethnicity: $('#su-ethnicity').val(),
					PreferredFirstLanguage: $('#su-language').val(),
					CurrentPreviousOccupation: $('#su-occupation').val(),
					FileName: fileName,
					FileType: fileType,
					TempFileId: tempFileId
				}
				var serviceUserCreateViewModel = {
					ServiceUser: ServiceUser
				}


				$.ajax({
					type: "POST",
					url: '/ServiceUsers/Save',
					data: { serviceUserCreateViewModel: serviceUserCreateViewModel },
					success: function (data) {
						if (data.statuscode == 1) {
							Swal.fire({
								text: "Form has been successfully submitted!",
								icon: "success",
								buttonsStyling: !1,
								confirmButtonText: "Ok, got it!",
								customClass: { confirmButton: "btn btn-primary" }
							}).then(function (q) {
								q.isConfirmed;
								$('#modalAddEditServiceUser').hide();
								$('.modal-backdrop').remove();
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
}



/* Add New Service Users Document */
var myDropzonedoc = new Dropzone();
var myDropzonedoc1 = new Dropzone();

function addNewDocument(userId) {
	$('#kt_modal_Add_Document').modal('show');

	$(".date-receive").daterangepicker(
		{
			singleDatePicker: true,
			showDropdowns: true,
			minYear: 2001,
			maxYear: parseInt(moment().format("YYYY"), 10),
			locale: { format: 'DD/MM/yyyy' }
		}, function (start, end, label) { }
	);

	$('.ddlselect2').select2();


	myDropzonedoc = new Dropzone(".drzoncreate", {
		url: "/DocumentUpload", // Set the url for your upload script location
		paramName: "file", // The name that will be used to transfer the file
		maxFiles: 1,
		maxFilesize: 10, // MB
		addRemoveLinks: true,
		acceptedFiles: ".png,.jpg,.gif,.bmp,.jpeg,.pdf, .xls, .xlsx",
		success: function (file, response) {
			doctempFileId = response.id;
			docfileName = file.name;
			docfileType = file.type;
		}
	});

}

async function sendServiceUserDocument(dbType, docId) {
	if (dbType == 'I' || dbType == 'U') {
		//if ($('#' + logMessageId).val() == null || $('#' + logMessageId).val().trim() == "") {
		//    Swal.fire({
		//        text: "Please fill Log Message First.",
		//        icon: "info",
		//        buttonsStyling: !1,
		//        confirmButtonText: "Ok",
		//        customClass: { confirmButton: "btn btn-light" }
		//    });
		//    return;
		//}
	}

	var serviceUserId = $('#hdnserviceuserid').val();
	var opType = $('#editOrDelete').val();
	var DeldocId = $('#editOrDeleteId').val();
	dbType = opType != 'I' ? opType : dbType;
	docId = DeldocId != '' ? DeldocId : docId;

	var dateReceived = $('#sud-date_of_receive').val();
	var title = $('#sud-title').val();
	var description = $('#sud-description').val();
	var documentCategory = $('#sud-document_category').val();

	var postData = null;
	if (dbType == 'I' || dbType == 'U' || dbType == 'D') {
		postData =
		{
			docId: docId,
			dbType: dbType,
			serviceUserId: serviceUserId,
			TempFileId: doctempFileId,
			data:
			{
				ServiceUserDocument:
				{
					ServiceUserId: serviceUserId,
					DateReceived: dateReceived,
					Title: title,
					Description: description,
					DocumentCategory: documentCategory,
					FileName: docfileName,
					FileType: docfileType,
				}
			}
		};
	}


	await $.ajax({
		type: "POST",
		url: '/ServiceUsers/saveServiceUserDocument',
		data: postData,
		success: function (data) {
			var showMessage = ""; var icon = "";
			if (data) {
				switch (dbType) {
					case "I": showMessage = "Service User Document Added Successful."; break;
					case "U": showMessage = "Service User Document Updated Successful."; break;
					case "D": showMessage = "Service User Document Removed Successful."; break;
				}
				icon = 'success';
				var edirOrNot = $('#editOrDelete').val();
				$('#editOrDelete').val('I');
				$('#editOrDeleteId').val('');
				$('#ServiceUsersDocumentsTabContent').html('');
				$('#ServiceUsersDocumentsTabContent').html(data);

				$("body").css("overflow", "");
				$("body").css("padding-right", "");

				try { $('#kt_modal_Add_Document').modal('hide'); } catch (ex) { }
				try { $('#kt_modal_Update_Document').modal('hide'); } catch (xe) { }

			} else {
				icon = 'error';
				showMessage = 'Please Check, There may be some error to save Service User Document.';
			}

			$('#sud-title').val('');
			$('#sud-description').val('');
			$('#sud-document_category').val('');

			Swal.fire({
				text: showMessage,
				icon: icon,
				buttonsStyling: !1,
				confirmButtonText: "Ok",
				customClass: { confirmButton: "btn btn-light" }
			}); //return;
		}
	});
	$('#editOrDelete').val('I');
	$('#editOrDeleteId').val('');
}

async function UpdateServiceUserDocument(opType, docId) {
	if (opType == 'D') {
		await Swal.fire({
			title: 'Are you sure?',
			text: "You want to delete selected service user document.",
			type: 'warning',
			showCancelButton: true,
			confirmButtonColor: '#3085d6',
			cancelButtonColor: '#d33',
			confirmButtonText: 'Yes, delete it!',
			showLoaderOnConfirm: true,
			preConfirm: async function () {
				$('#editOrDelete').val('D');
				$('#editOrDeleteId').val('');
				//sendServiceUserDocument(opType, docId);

				await $.ajax({
					type: "POST",
					url: '/ServiceUsers/saveServiceUserDocumentDelete',
					data: { docId: docId },
					success: async function (data) {
						if (data.statuscode == 3) {

							Swal.fire({
								text: data.message,
								icon: 'error',
								buttonsStyling: !1,
								confirmButtonText: "Ok",
								customClass: { confirmButton: "btn btn-light" }
							});

						}
						else if (data.statuscode == 1) {

							await $.ajax({
								type: "GET",
								url: '/ServiceUsers/DocumentsManagerTabBind',
								data: { docId: "", serviceUserId: serviceUserId },
								success: function (data) {
									if (data != null) {
										Swal.fire({
											text: 'Service User Document Removed Successful.',
											icon: 'success',
											buttonsStyling: !1,
											confirmButtonText: "Ok",
											customClass: { confirmButton: "btn btn-success" }
										});
										$('#DocumentsManagerTabContentData').html('');
										$('#DocumentsManagerTabContentData').html(data);
									}
								}
							});
						}
					}
				});
			}
		});
	}
	else {
		$('#editOrDelete').val(opType);
		$('#editOrDeleteId').val(docId);

		await $.ajax({
			type: "GET",
			url: '/ServiceUsers/GetByServiceUserDocId',
			data: { docId: docId, serviceUserId: '' },
			success: function (data) {
				console.log('data', data);
				if (data) {
					$('#showServiceUserDocumentModel').html('');
					$('#showServiceUserDocumentModel').html(data);

					$('#kt_modal_Update_Document').modal('show');

					$(".date-receive-u").daterangepicker(
						{
							singleDatePicker: true,
							showDropdowns: true,
							minYear: 2001,
							maxYear: parseInt(moment().format("YYYY"), 10),
							locale: { format: 'DD/MM/yyyy' }
						}, function (start, end, label) { }
					);

					$('.ddlselect2').select2();


					myDropzonedoc1 = new Dropzone(".drzonupdate", {
						url: "/DocumentUpload", // Set the url for your upload script location
						paramName: "file", // The name that will be used to transfer the file
						maxFiles: 1,
						maxFilesize: 10, // MB
						addRemoveLinks: true,
						acceptedFiles: ".png,.jpg,.gif,.bmp,.jpeg,.pdf, .xls, .xlsx",
						success: function (file, response) {
							doctempFileId = response.id;
							docfileName = file.name;
							docfileType = file.type;
						}
					});


				}
			}
		});




	}
}



async function DownloadServiceUserDocument(docId, prePath, blobName) {
	if (blobName != '') {
		var host = location.host;
		var downloadLink = 'http://' + host + prePath + blobName;


	}

}