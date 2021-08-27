using Microsoft.EntityFrameworkCore.Migrations;

namespace Quiz_Application.Services.Migrations
{
    public partial class AddedSPGetReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE GetReport
			(		
				@ExamID int,
				@CandidateID varchar(50),
				@SessionID nvarchar(max)
			)
			AS
			BEGIN
				SET NOCOUNT ON;		

				SELECT
				R.CandidateID,
				R.SessionID,
				R.ExamID,
				E.Name AS Exam,
				CONVERT(VARCHAR, R.CreatedOn, 106) AS Date,
				CASE 
					WHEN ((CAST(COUNT(R.Sl_No) AS decimal)/E.FullMarks *100) >50) THEN 'Excellent! You gave '+CAST(COUNT(R.Sl_No) as varchar)+' correct answers out of '+CAST(CAST(E.FullMarks AS int) AS varchar)+' questions. You are very close to become an expert. Keep learning!' 
					ELSE 'Oops! You gave '+CAST(COUNT(R.Sl_No) as varchar)+' correct answers out of '+CAST(CAST(E.FullMarks AS int) AS varchar)+' questions. You really need to work hard to become an expert.'
				END AS 'Message'	 
				FROM Result R
				LEFT JOIN Exam E ON R.ExamID=E.ExamID
				WHERE R.ExamID=@ExamID AND R.CandidateID = @CandidateID AND R.SessionID=@SessionID 	AND R.IsCorrent=1
				GROUP BY R.CandidateID,R.SessionID,R.ExamID,E.Name,E.FullMarks,R.CreatedOn				

				SET NOCOUNT OFF;
			END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC GetReport");
        }
    }
}
