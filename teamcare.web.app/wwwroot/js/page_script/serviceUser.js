$(".date-dob").daterangepicker({
	singleDatePicker: true,
	showDropdowns: true,
	minYear: 1981,
	maxYear: parseInt(moment().format("YYYY"), 10)
}, function (start, end, label) {
	//var years = moment().diff(start, "years");
	//alert("You are " + years + " years old!");
});
$(".date-admission").daterangepicker({
	singleDatePicker: true,
	showDropdowns: true,
	minYear: 2001,
	maxYear: parseInt(moment().format("YYYY"), 10)
}, function (start, end, label) {
	//var years = moment().diff(start, "years");
	//alert("You are " + years + " years old!");
});

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
		tempFileId = response;
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


// Stepper lement
var serviceUserId = $('#selectedId').val();
var element = document.querySelector("#su-profile-stepper");
var r = document.querySelector("#su-profile-form");
var step1 = document.querySelector('#su-step-pi');
var step2 = document.querySelector('#su-step-ad');
var step3 = document.querySelector('#su-step-nok');
var submit = document.querySelector('[data-kt-stepper-action="submit"]');
var modal = new bootstrap.Modal(document.querySelector('#kt_modal_create_app'));
submit.addEventListener("click", function (e) {
	e.preventDefault();
	fv3.validate().then(function (s) {
		if ("Valid" == s) {

			submit.disabled = !0;
			submit.setAttribute("data-kt-indicator", "on");
			setTimeout(function () {
				submit.removeAttribute("data-kt-indicator");
				submit.disabled = !1;

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
					NextOfKin: $('#su-next_of_kin').val(),
					RelationshipToPerson: $('#su-related_person').val(),
					Address: $('#su-address').val(),
					ContactDetails: $('#su-contact_detail').val(),
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
						if (data.statuscode==1) {
							Swal.fire({
								text: "Form has been successfully submitted!",
								icon: "success",
								buttonsStyling: !1,
								confirmButtonText: "Ok, got it!",
								customClass: {
									confirmButton: "btn btn-primary"
								}
							}).then(function (q) {
								q.isConfirmed && modal.hide();
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
});

// Initialize Stepper
var stepper = new KTStepper(element);

var stepIndex = 0;
// Handle next step
stepper.on("kt.stepper.next", function (stepper)
{
	stepIndex = stepper.getCurrentStepIndex();
	switch (stepIndex) {
		case 1:
			fv1.validate().then(function (t) {
				"Valid" == t ? (stepper.goNext(), KTUtil.scrollTop()) : Swal.fire({
					text: "Sorry, looks like there are some errors detected, please try again.",
					icon: "error",
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
					icon: "error",
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
const fv2 = FormValidation
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
const fv3 = FormValidation
	.formValidation(
		step3,
		{
			fields: {
				next_of_kin: {
					validators: {
						notEmpty: {
							message: "Next of kin is required"
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

async function doSortFilterBy() {
	var optFilterBy = $("#filterBy").val();
	var optSortBy = $("#sortBy").val();
	await $.ajax({
		type: "POST",
		url: '/ServiceUsers/SortFilterOption',
		data: { sortBy: +optSortBy, filterBy: optFilterBy },
		success: function (data) {
			if (data) {
				$('#partialViewDataContent').html('');
				$('#partialViewDataContent').html(data);
				$('#TotalServiceUser').text($('#hdnTotalServiceUser').val());
			} else { }
		}
	});
}

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
			Swal.fire({
					text: showMessage,
					icon: icon,
					buttonsStyling: !1,
					confirmButtonText: "Ok",
					customClass: { confirmButton: "btn btn-light" }
			});
		}
	});
}


function RemoveProfile() {
	$('#OldProfile').remove();
}

$(document).ready(function() {
	doSortFilterBy();

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


async function sendServiceUserLog(serviceUserId, logMessageId, dbType, logId)
{
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
			if (data)
			{
				switch (dbType)
				{
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

async function fncEditOrDelete(opType, logId, msgAreaId)
{
	if (opType == 'D')
	{
		await Swal.fire({
			title: 'Are you sure?',
			text: "You want to delete selected service user log.",
			type: 'warning',
			showCancelButton: true,
			confirmButtonColor: '#3085d6',
			cancelButtonColor: '#d33',
			confirmButtonText: 'Yes, delete it!',
			showLoaderOnConfirm: true,
			preConfirm: async function () {	 await sendServiceUserLog(serviceUserId, 'sul-log_message', opType, logId); }
		});
	}
	else
	{
		$('#editOrDelete').val(opType);
		$('#editOrDeleteId').val(logId);

		 await $.ajax({
			type: "POST",
			url: '/ServiceUsers/GetByLogId',
			data: { id: logId},
			success: function (data) {
				if (data !=null) {
					$('#sul-log_message').val(data.logMessage);
					$('#' + msgAreaId).html = '';
					$('#' + msgAreaId).html = data.logMessage;
				} else { }
			}
		});

	}
}
