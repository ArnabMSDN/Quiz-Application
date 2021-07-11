// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(document).ready(function () {
  $.ajax({
     type: "GET",
     url: "/Exam/ExamList",
     data: "{}",
     success: function (data) {
       var string = '<option value="-1">Please select the exam</option>';
       for (var i = 0; i < data.length; i++) { string += '<option value="' + data[i].examID + '">' + data[i].name + '</option>'; }
       $("#ddlExam").html(string);
      }
  });
});

$('#chooseFile').change(function () {   
    var file = $('#chooseFile')[0].files[0].name;
    $('#noFile').text(file);
});

function SaveImage() {
   var formData = new FormData();    
   var CandidateID = $('#Candidate_ID').val();
   var file = document.getElementById("chooseFile").files[0];
   formData.append("Candidate-ID", CandidateID);
   formData.append("Candidate-Img", file);
   $.ajax({
      type: "POST",
      url: "/Home/SaveImage",
      data: formData,
      processData: false,
      contentType: false,
      success: function (response) {
          console.log(response);
         // window.location.href = '/Home/Index';
      }
   });
}

//Image Upload Preview  
function ShowImagePreview(input) {
  if (input.files && input.files[0]) {
     var reader = new FileReader();
     reader.onload = function (e) {
        $('#imgProfile').prop('src', e.target.result);
     };
     reader.readAsDataURL(input.files[0]);
  }
}