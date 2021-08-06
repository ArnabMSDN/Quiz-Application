// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(document).ready(function () {
    var ExmID = 0;   
    var Duration = 0;
    var qIndex = 0;        

    $('#ddlExam').prop('disabled', false);
    $('#btnStart').prop('disabled', false);   
    $('#eqMain button.w3-left').prop('disabled', true);
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
                    PopulateQuestions(ExmID, qIndex);
                });           
        }
        else
            alert('Please select your skill.');        
    });

    $('#eqPrev').click(function () {
        
    });

    $('#eqNext').click(function () {

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
            //console.log(response);
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

function PopulateQuestions(ExmID, qIndex) {
   $.post('/Exam/Questions', { ExamID: ExmID },
     function (data) {
         console.log(data);
         var Ostring = "<div style='padding: 5px;' id='eqOption'>";
         qIndex = data.questions.length;
         $('#eqCount').html("(1" + " of " + qIndex+")");
         $('div#eqMain h3').html(data.exam + " Quiz");
         $('div#eqMain h4').html("Q. " + data.questions[0].questionText);
         for (var i in data.questions[0].options) {
             console.log(i, data.questions[0].options[i]);
             Ostring = Ostring + "<input class='w3-radio' type='radio' name='option' value='" + data.questions[0].options[i].optionID+"'><label> " + data.questions[0].options[i].option+"</label><br/>";
         }
         Ostring = Ostring + "</div>";
         console.log(Ostring);
         $('div #eqMain #btnPrev').prop('disabled', false);
         $('div#eqMain p').append(Ostring);
     });
}