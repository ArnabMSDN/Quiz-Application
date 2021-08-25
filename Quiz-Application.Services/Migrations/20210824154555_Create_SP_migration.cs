using Microsoft.EntityFrameworkCore.Migrations;

namespace Quiz_Application.Services.Migrations
{
    public partial class Create_SP_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE GetQuizList
			(
			 @ExamID int,
			 @CandidateID varchar(50),
			 @SessionID nvarchar(max)
			)
			AS
			BEGIN
				SET NOCOUNT ON;

				SELECT
				R.Sl_No,
				R.SessionID,
				R.CandidateID,
				C.Name AS CandidateName,
				R.ExamID,
				E.Name AS Exam,
				E.FullMarks,
				R.QuestionID,
				Q.DisplayText AS Question,
				R.AnswerID,
				A.DisplayText AS Answer,
				R.SelectedOptionID,
				A1.DisplayText AS SelectedOption,
				R.IsCorrent,
				R.CreatedOn
				INTO #TMP_Data
				FROM[Result] R
			    LEFT JOIN Candidate C ON R.CandidateID = Candidate_ID
				LEFT JOIN Exam E ON R.ExamID = E.ExamID
				LEFT JOIN Question Q ON R.QuestionID = Q.QuestionID
				LEFT JOIN Choice A ON R.AnswerID = A.ChoiceID
				LEFT JOIN Choice A1 ON R.SelectedOptionID = A1.ChoiceID
				WHERE R.CandidateID = @CandidateID AND R.ExamID = @ExamID

				SELECT
				D.SessionID,
				D.ExamID,
				D.Exam,	
				CONVERT(varchar, D.CreatedOn, 106) AS QuizDate,
  				(CAST(COUNT(D.Sl_No) as varchar(20)) + '/' + CAST(CAST(D.FullMarks AS INT) AS VARCHAR(20))) AS Score
				FROM #TMP_Data D WHERE D.IsCorrent=1
				GROUP BY D.SessionID,D.ExamID,D.Exam,D.FullMarks,D.CreatedOn				

				DROP TABLE #TMP_Data

				SET NOCOUNT OFF;
				END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP PROCEDURE GetQuizList");
		}
    }
}
