// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(document).ready(function () {
    var ExmID = 0;   
    var Duration = 0;    

    $('#ddlExam').prop('disabled', false);
    $('#btnStart').prop('disabled', false);
    $.ajax({
        type: "GET",
        url: "/Exam/ExamList",
        data: "{}",
        success: function (data) {
            var string = '<option value="-1">--- Please Select ---</option>';
            for (var i = 0; i < data.length; i++) { string += '<option value="' + data[i].examID + '">' + data[i].name + '</option>'; }
            $("#ddlExam").html(string);
        }
    });

    //Start the Exam
    $('#btnStart').click(function () {
        if ($("#ddlExam").val() > 0) {
            $('#ddlExam').prop('disabled', true);
            $('#btnStart').prop('disabled', true);
            ExmID = $("#ddlExam").val();
            $.post('/Exam/ExamDetails', { ExamID: ExmID },
                function (data) {
                    Duration = data.duration;                                       
                    StartTimer(Duration);
                    PopulateQuestions(ExmID)
                });           
        }
        else
            alert('Please select your skill.');        
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

function StartTimer(Duration) {
   var deadline = new Date();
   deadline.setHours(deadline.getHours() + Duration);
   var x = setInterval(function () {
      var now = new Date().getTime();
      var t = deadline.getTime() - now;
      var hours = Math.floor((t % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      var minutes = Math.floor((t % (1000 * 60 * 60)) / (1000 * 60));
      var seconds = Math.floor((t % (1000 * 60)) / 1000);
      document.getElementById("timer").innerHTML = "Time : " + hours + ":" + minutes + ":" + seconds;
      if (t < 0) {
            clearInterval(x);
            document.getElementById("timer").innerHTML = "EXPIRED";
      }
   }, 1000);
}

function PopulateQuestions(ExmID) {
    $.post('/Exam/Questions', { ExamID: ExmID },
        function (data) {
            console.log(data);
        });
}