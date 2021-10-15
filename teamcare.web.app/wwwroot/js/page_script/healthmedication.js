let editorDiagnosis = null;
let editorPrescriptionHistory = null;
let editorSocialHistory = null;
let editorAllergyInformation = null;
let editorOtherSignificantInformation = null;


$(document).ready(function () {

    $('#tabhealthmedication').click(function () {
        BindMedication();
    });

});

function BindMedication() {
     
    $.ajax({
        type: "GET",
        url: '/HealthMedication/HealthMedicationTabBind',
        data: { id: $('#hdnserviceuserid').val() },
        success: function (data) {
            if (data != null) {
                $('#HealthMedicationTabContentData').html('');
                $('#HealthMedicationTabContentData').html(data);
                ckeditorInit();
            }
        }
    });
}

function ckeditorInit() {
    ClassicEditor.create(document.querySelector('#ckeDiagnosis')).then(newEditor => {
        editorDiagnosis = newEditor;
    }).catch(error => {
        console.error(error);
    });
    ClassicEditor.create(document.querySelector('#ckePrescriptionHistory')).then(newEditor => {
        editorPrescriptionHistory = newEditor;
    }).catch(error => {
        console.error(error);
    });
    ClassicEditor.create(document.querySelector('#ckeSocialHistory')).then(newEditor => {
        editorSocialHistory = newEditor;
    }).catch(error => {
        console.error(error);
    });
    ClassicEditor.create(document.querySelector('#ckeAllergyInformation')).then(newEditor => {
        editorAllergyInformation = newEditor;
    }).catch(error => {
        console.error(error);
    });
    ClassicEditor.create(document.querySelector('#ckeOtherSignificantInformation')).then(newEditor => {
        editorOtherSignificantInformation = newEditor;
    }).catch(error => {
        console.error(error);
    });
}

function saveHealths(ctrl) {

    var ckValDiagnosis = editorDiagnosis.getData();
    var ckValPrescriptionHistory = editorPrescriptionHistory.getData();
    var ckValSocialHistory = editorSocialHistory.getData();
    var ckValAllergyInformation = editorAllergyInformation.getData();
    var ckValOtherSignificantInformation = editorOtherSignificantInformation.getData();


    if (ckValDiagnosis.trim() != "" || ckValPrescriptionHistory.trim() != "" || ckValSocialHistory.trim() != "" || ckValAllergyInformation.trim() != "" || ckValOtherSignificantInformation.trim() != "") {

        $('#btnhealthsubmit').disabled = !0;
        $('#btnhealthsubmit').attr("data-kt-indicator", "on");
        setTimeout(function () {
            $('#btnhealthsubmit').removeAttr("data-kt-indicator");
            $('#btnhealthsubmit').disabled = !1;

            //ajax call for the submit;
            var HealthMedication = {
                Id: $('#hdnhealthid').val(),
                ServiceUserId: $('#hdnserviceuserid').val(),
                Diagnosis: ckValDiagnosis,
                PrescriptionHistory: ckValPrescriptionHistory,
                SocialHistory: ckValSocialHistory,
                AllergyInformation: ckValAllergyInformation,
                OtherSignificantInformation: ckValOtherSignificantInformation
            }
            var healthMedicationViewModel = {
                HealthMedication: HealthMedication
            }

            $.ajax({
                type: "POST",
                url: '/HealthMedication/Save',
                data: { healthMedicationViewModel: healthMedicationViewModel },
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
                            text: "Medication Profile Saved Successfully.",
                            icon: "success",
                            buttonsStyling: !1,
                            confirmButtonText: "Ok, got it!",
                            customClass: {
                                confirmButton: "btn btn-light"
                            }
                        });

                        BindMedication();
                    }
                }
            });
            //on success show message
        }, 2e3);
    } else {
        Swal.fire({
            text: "Please! fill up at least one field.",
            icon: "info",
            buttonsStyling: !1,
            confirmButtonText: "Ok, got it!",
            customClass: {
                confirmButton: "btn btn-light"
            }
        });
    }
}