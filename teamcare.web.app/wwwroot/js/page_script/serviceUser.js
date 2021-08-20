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
					Id: 0,
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
						if (data) {
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
						else { }
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
	var optArchive = 0;
	if ($('#chkIsArchive').prop('checked') === true) {
		optArchive = 1;
	} else {
		optArchive = 0;
	}

	await $.ajax({
		type: "POST",
		url: '/ServiceUsers/SortFilterOption',
		data: { sortBy: +optSortBy, filterBy: optFilterBy, archiveBy: optArchive },
		success: function (data) {
			if (data) {
				$('#partialViewDataContent').html('');
				$('#partialViewDataContent').html(data);			
				$('#TotalServiceUser').text($('#hdnTotalServiceUser').val());
			} else { }
		}
	});
}

$(document).ready(function() {
    doSortFilterBy();
});