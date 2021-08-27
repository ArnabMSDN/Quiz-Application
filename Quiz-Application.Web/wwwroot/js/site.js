// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

$(document).ready(function () {
    
    var ExmID = 0;
    var Score = null;
    var Status = null;
    var QuestionID = 0;
    var AnswerID = 0;
    var Duration = 0;
    var index = 0;
    var qIndex = 0;
    var objData = [];
    var result = [];
    var checkTime = [];
    var objReport = null;

    $('#ddlExam').prop('disabled', false);
    $('#btnStart').prop('disabled', false);
    $('#btnSubmit').prop('disabled', false);
    $('#btnSave').prop('disabled', true);
    $('#eqMain button.w3-left').prop('disabled', true);
    $('#eqMain button.w3-right').prop('disabled', true);
    $("#eqReport").children().prop('disabled', true);
    $("#eqScore").children().prop('disabled', true);

    $.ajax({
        type: "GET",
        url: "/api/Exams",
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
            $('#btnSave').prop('disabled', false);
            ExmID = $("#ddlExam").val();
            $.get('/api/Exam/', { ExamID: ExmID },
               function (data) {
                   Duration = data.duration;
                   StartTimer(Duration, checkTime);                   
                   PopulateQuestions(ExmID);                   
               });
        }
        else           
            $.alert({
                icon: 'fa fa-warning',
                type: 'orange',
                title: 'Select Skill',
                content: 'Please select your skill !',
                boxWidth: '40%',
                useBootstrap: false,
                closeIcon: true,
                closeIconClass: 'fa fa-close'
            });
    });

    $('#btnPrev').click(function () {
        QuestionID = 0;
        AnswerID = 0;
        //console.log(index);
        index = (index - 1) % qIndex;
        var count = index + 1;
        //console.log(objData.questions[index]);
        if (index <= qIndex - 1) {
            $('div#eqMain p').empty();
            var Ostring = "<div style='padding: 5px;' id='eqOption'>";
            $('#eqCount').html("(" + count + " of " + qIndex + ")");
            $('div#eqMain h3').html(objData.exam + " Quiz");
            $('div#eqMain h4').html("Question " + count + " : " + objData.questions[index].questionText);
            QuestionID = objData.questions[index].questionID;
            AnswerID = objData.questions[index].answer.optionID;
            let obj = result.find(o => o.QuestionID === QuestionID);                         
            //console.log(obj.SelectedOption);
            for (var i in objData.questions[index].options) {
                if (!$.isEmptyObject(obj)) {
                    if (obj.SelectedOption == objData.questions[index].options[i].optionID) {
                        Ostring = Ostring + "<input class='w3-radio' type='radio' name='option' value='" + objData.questions[index].options[i].optionID + "' checked><label> " + objData.questions[index].options[i].option + "</label><br/>";
                    }
                    else {
                        Ostring = Ostring + "<input class='w3-radio' type='radio' name='option' value='" + objData.questions[index].options[i].optionID + "'><label> " + objData.questions[index].options[i].option + "</label><br/>";
                    }
                }
                else {
                    Ostring = Ostring + "<input class='w3-radio' type='radio' name='option' value='" + objData.questions[index].options[i].optionID + "'><label> " + objData.questions[index].options[i].option + "</label><br/>";
                }
            }
            Ostring = Ostring + "</div>";
            //console.log(Ostring);
            $('div#eqMain p').append(Ostring);
            $('#eqMain button.w3-right').prop('disabled', false);
            if (index == 0) {
                $('#eqMain button.w3-left').prop('disabled', true);
            }
        }
    });

    $('#btnNext').click(function () {
        QuestionID = 0;
        AnswerID = 0;
        //console.log(index);
        index = (index + 1) % qIndex;
        var count = index + 1;
        if (index <= qIndex - 1) {
            //console.log(objData.questions[index]);
            $('div#eqMain p').empty();
            var Ostring = "<div style='padding: 5px;' id='eqOption'>";
            $('#eqCount').html("(" + count + " of " + qIndex + ")");
            $('div#eqMain h3').html(objData.exam + " Quiz");
            $('div#eqMain h4').html("Question " + count + " : " + objData.questions[index].questionText);
            QuestionID = objData.questions[index].questionID;
            AnswerID = objData.questions[index].answer.optionID;
            let obj = result.find(o => o.QuestionID === QuestionID);
            //console.log(obj);
            for (var i in objData.questions[index].options) {
                //console.log(i, data.questions[0].options[i]);   
                if (!$.isEmptyObject(obj)) {
                    if (obj.SelectedOption == objData.questions[index].options[i].optionID) {
                        Ostring = Ostring + "<input class='w3-radio' type='radio' name='option' value='" + objData.questions[index].options[i].optionID + "' checked><label> " + objData.questions[index].options[i].option + "</label><br/>";
                    }
                    else {
                        Ostring = Ostring + "<input class='w3-radio' type='radio' name='option' value='" + objData.questions[index].options[i].optionID + "'><label> " + objData.questions[index].options[i].option + "</label><br/>";
                    }
                }
                else {
                    Ostring = Ostring + "<input class='w3-radio' type='radio' name='option' value='" + objData.questions[index].options[i].optionID + "'><label> " + objData.questions[index].options[i].option + "</label><br/>";
                }
            }
            Ostring = Ostring + "</div>";
            //console.log(Ostring);
            $('div#eqMain p').append(Ostring);
            $('#eqMain button.w3-left').prop('disabled', false);
            if (index == qIndex - 1) {
                $('#eqMain button.w3-right').prop('disabled', true);
            }
        }
    });

    $('#btnSave').click(function () {       
        var ans = {
            CandidateID: $('#eqCandidateID').text(),
            ExamID: ExmID,
            QuestionID: QuestionID,
            AnswerID: AnswerID,
            SelectedOption: $('input[name="option"]:checked').val()           
        };
        if (result.some(item => item.QuestionID === QuestionID)) {
            //console.log('EXIST');
            UpdateItem(QuestionID);
        }
        else {
            result.push(ans);
        }       
        //console.log(result);       
        ans = [];
    });

    $('#btnSubmit').click(function () {              
        $.confirm({
            icon: 'fa fa-warning',
            title: 'Submit Quiz',
            content: 'Are you sure you want to submit the quiz ?',
            type: 'orange',
            closeIcon: true,
            closeIconClass: 'fa fa-close',
            boxWidth: '40%',
            useBootstrap: false,
            buttons: {
                Submit: {
                    text: 'Submit',
                    btnClass: 'btn-red',
                    action: function () {
                        $.post('/api/Score/', { objRequest: result },
                         function (data) {
                             if (data > 0) {
                                 stop(checkTime);
                                 $('#btnSubmit').prop('disabled', true);
                                 $("#eqReport").children().prop('disabled', false);
                                 $.alert({
                                     type: 'green',
                                     title: 'Success !',
                                     content: 'Please check the score.',
                                     boxWidth: '40%',
                                     useBootstrap: false,
                                     closeIcon: true,
                                     closeIconClass: 'fa fa-close'
                                 });
                             }
                             else {
                                 $('#btnSubmit').prop('disabled', false);
                                 $("#eqReport").children().prop('disabled', true);
                                 $.alert({
                                     type: 'red',
                                     title: 'Error !',
                                     content: 'Please try again.',
                                     boxWidth: '40%',
                                     useBootstrap: false,
                                     closeIcon: true,
                                     closeIconClass: 'fa fa-close'
                                 });
                             }
                         });
                    }
                },
                Cancel: function () {
                    $(this).remove();
                }
            }
        });
    });

    $('.btnScore').click(function () {
        var request = {
            ExamID: $(this).closest("tr").find('td:eq(2)').text(),
            CandidateID: $('#hdnCandidateID').val(),            
            SessionID: $(this).closest("tr").find('td:eq(1)').text()            
        };
        Score = $(this).closest("tr").find('td:eq(4)').text();
        Status = $(this).closest("tr").find('td:eq(6)').text();
        $.post('/api/Report/', { argRpt: request },
            function (data) {
                objReport = data;
                $('div#eqScore h3').html(data[0].exam + ' Test');
                $('div#eqScore .w3-container p:eq(0)').html('<strong>Candidate ID:</strong> ' + data[0].candidateID);
                $('div#eqScore .w3-container h5').html(data[0].message);
                $('div#eqScore .w3-container span').html('<strong>Date:</strong> ' + data[0].date);
                if (Status == "1") {
                    $("#eqScore").children().prop('disabled', false);
                }
                else { $("#eqScore").children().prop('disabled', true);
                }
            });
    });

    $('#btnReport').click(function () {
        console.log(objReport);
        var scoreFormat = {
            ExamID: objReport[0].examID,
            CandidateID: $('#hdnCandidateID').val(),
            SessionID: objReport[0].sessionID,
            Exam: objReport[0].exam,
            Date: objReport[0].date,
            Score: Score
        };
        console.log(scoreFormat);
        $.post('/api/CreatePDF/', { argPDFRpt:scoreFormat},
            function (data) {
                console.log(data);
                window.open(data.path, '_blank');
            });       
        objReport = [];
        Score = null;
    });

    function UpdateItem(QuestionID) {
        for (var i in result) {
            if (result[i].QuestionID == QuestionID) {               
                result[i].CandidateID= $('#eqCandidateID').text();
                result[i].ExamID= ExmID;
                result[i].QuestionID= QuestionID;
                result[i].AnswerID= AnswerID;
                result[i].SelectedOption= $('input[name="option"]:checked').val();                
                break;
            }
        }
    }

    function PopulateQuestions(ExmID) {
        $.get('/api/Questions', { ExamID: ExmID },
            function (data) {
                QuestionID = 0;
                AnswerID = 0;               
                objData = data;
                //console.log(objData);
                var Ostring = "<div style='padding: 5px;' id='eqOption'>";
                qIndex = data.questions.length;
                $('#eqCount').html("(1" + " of " + qIndex + ")");
                $('div#eqMain h3').html(data.exam + " Quiz");
                $('div#eqMain h4').html("Question 1 : " + data.questions[0].questionText);
                QuestionID = data.questions[0].questionID;
                AnswerID = data.questions[0].answer.optionID;
                for (var i in data.questions[0].options) {
                    //console.log(i, data.questions[0].options[i]);
                    Ostring = Ostring + "<input class='w3-radio' type='radio' name='option' value='" + data.questions[0].options[i].optionID + "'><label> " + data.questions[0].options[i].option + "</label><br/>";
                }
                Ostring = Ostring + "</div>";
                //console.log(Ostring);
                $('div#eqMain p').append(Ostring);
                $('#eqMain button.w3-right').prop('disabled', false);
            });
    }

    function StartTimer(Duration, checkTime) {
        var deadline = new Date();
        deadline.setHours(deadline.getHours() + Duration);
        if (checkTime.length== 0) {
            var x = setInterval(function () {
                var now = new Date().getTime();
                var t = deadline.getTime() - now;
                var hours = Math.floor((t % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                var minutes = Math.floor((t % (1000 * 60 * 60)) / (1000 * 60));
                var seconds = Math.floor((t % (1000 * 60)) / 1000);
                document.getElementById("timer").innerHTML = "Time : " + hours + ":" + minutes + ":" + seconds;
                if (t < 0) {
                    clearInterval(x);
                    document.getElementById("timer").innerHTML = "Time : 00:00:00";
                }
            }, 1000);
            checkTime.push(x);            
        }
    }

    function stop(checkTime) {
        clearInterval(checkTime[0]);
        checkTime = [];       
    }

});

//$('#chooseFile').change(function () {
//    var file = $('#chooseFile')[0].files[0].name;
//    $('#noFile').text(file);
//});

//function SaveImage() {
//    var formData = new FormData();
//    var CandidateID = $('#Candidate_ID').val();
//    var file = document.getElementById("chooseFile").files[0];
//    formData.append("Candidate-ID", CandidateID);
//    formData.append("Candidate-Img", file);
//    $.ajax({
//        type: "POST",
//        url: "/Home/SaveImage",
//        data: formData,
//        processData: false,
//        contentType: false,
//        success: function (response) {
//            //console.log(response);
//        }
//    });
//}

////Image Upload Preview  
//function ShowImagePreview(input) {
//    if (input.files && input.files[0]) {
//        var reader = new FileReader();
//        reader.onload = function (e) {
//            $('#imgProfile').prop('src', e.target.result);
//        };
//        reader.readAsDataURL(input.files[0]);
//    }
//}



