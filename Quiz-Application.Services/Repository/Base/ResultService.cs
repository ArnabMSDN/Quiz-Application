using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Quiz_Application.Services.Entities;
using Quiz_Application.Services.Repository.Interfaces;
using System.Text;

namespace Quiz_Application.Services.Repository.Base
{
    public class ResultService<TEntity> : IResult<TEntity> where TEntity : BaseEntity
    {
        private readonly QuizDBContext _dbContext;
        private DbSet<TEntity> _dbSet;
        public ResultService(QuizDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public async Task<int> AddResult(List<TEntity> entity)
        {
            int output = 0;
            _dbSet.AddRange(entity);
            output = await _dbContext.SaveChangesAsync();
            return output;
        }
        public async Task<IEnumerable<QuizAttempt>> GetAttemptHistory(string argCandidateID)
        {
            try
            {
                List<QuizAttempt> obj = await _dbContext.Set<QuizAttempt>().FromSqlRaw(@"SELECT
                CAST(ROW_NUMBER() OVER (ORDER BY R.CreatedOn DESC) AS int) Sl_No,
                R.SessionID,
                R.ExamID,
                E.Name AS Exam,
                CONVERT(varchar, R.CreatedOn, 106) AS Date,
                (CAST(COUNT(R.Sl_No) as varchar(20)) + '/' + CAST(CAST(E.FullMarks AS INT) AS VARCHAR(20))) AS Score,
				CASE 
					WHEN ((CAST(COUNT(R.Sl_No) AS decimal)/E.FullMarks *100) >50) THEN '1' 
					ELSE '0'
				END AS 'Status'
                FROM Result R
                LEFT JOIN Exam E ON R.ExamID = E.ExamID
                WHERE R.CandidateID ='" + argCandidateID + "' AND R.IsCorrent = 1"
                +"GROUP BY R.SessionID, R.ExamID, E.Name, E.FullMarks, R.CreatedOn", argCandidateID).ToListAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
            }
        }
        public async Task<IEnumerable<QuizReport>> ScoreReport(ReqReport argRpt)
        {
            try
            {
                List<QuizReport> obj = await _dbContext.Set<QuizReport>().FromSqlRaw(@"EXEC GetReport {0},{1},{2}", argRpt.ExamID, argRpt.CandidateID, argRpt.SessionID).ToListAsync();  
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
            }
        }
        public async Task<string> GetCertificateString(ReqCertificate argRpt)
        {
            Candidate _candidate =await _dbContext.Candidate.Where(e => e.Candidate_ID == argRpt.CandidateID.ToString()).FirstOrDefaultAsync();          

            try
            {
                string cert =null;
                cert= @"<html>
<head>
<style type='text/css'>
.outer-border {
    width: 800px;
    height: 650px;
    padding: 20px;
    text-align: center;
    border: 10px solid #673AB7;
    margin-left: 21%
}

.inner-dotted-border {
    width: 750px;
    height: 600px;
    padding: 20px;
    text-align: center;
    border: 5px solid #673AB7;
    border-style: dotted
}

.certification {
    font-size: 50px;
    font-weight: bold;
    color: #663ab7
}

.certify {
    font-size: 25px
}

.name {
    font-size: 30px;
    color: green
}

.fs-30 {
    font-size: 30px
}

.fs-20 {
    font-size: 20px
}
</style>
</head>
<body>
<div class='outer-border'>
    <div class='inner-dotted-border'><br> 
	<span><img src='https://www.citypng.com/public/uploads/preview/hd-gold-black-certificate-logo-transparent-png-31625761576hadwkhbj6t.png' alt='avatar' class='w3-left w3-circle w3-margin-right' style='width:100px'></span>
	<br><br><br>
	<span class='certification'>Certificate of Completion</span> 
	<br><br> 
	<span class='certify'><i>This is to certify that</i></span> 
	<br><br> <span class='name'><b>"+ _candidate.Name + @"</b></span><br />
	<br /> <span class='certify'><i>has successfully completed the certification</i></span>
	<br /><br /> <span class='fs-30'>"+argRpt.Exam+@"</span> <br /><br /> 
	<span class='fs-20'>with score of <b>"+argRpt.Score+@"</b></span>
	<br /><br /><br /> 
	<span class='certify'><i>dated</i></span><br> <span class='fs-30'>"+argRpt.Date+@"</span>
    </div>
</div>
</body>
</html>";
                return cert.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
            }
        }

    }   
}
